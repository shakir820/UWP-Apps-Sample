﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Graphics.DirectX;
using Windows.UI;
using Windows.UI.Composition;

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Composition;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Shadow_Background.ImageLoader
{
    public class DeviceReplacedEventArgs : EventArgs
    {
        internal DeviceReplacedEventArgs(CompositionGraphicsDevice device, Object drawingLock)
        {
            GraphicsDevce = device;
            DrawingLock = drawingLock;
        }

        public CompositionGraphicsDevice GraphicsDevce { get; set; }
        public Object DrawingLock { get; set; }
    }

    public class ImageLoader
    {
        private static bool _intialized;
        private static ImageLoader _imageLoader;

        private DeviceLostHelper _deviceLostHelper;
        private Compositor _compositor;
        private CanvasDevice _canvasDevice;
        private CompositionGraphicsDevice _graphicsDevice;
        private Object _drawingLock;
        private event EventHandler<Object> _deviceReplacedEvent;

        public ImageLoader(Compositor compositor)
        {
            Debug.Assert(compositor != null && _compositor == null);

            _compositor = compositor;
            _drawingLock = new object();
            _deviceLostHelper = new DeviceLostHelper();

            _canvasDevice = new CanvasDevice();
            _canvasDevice.DeviceLost += DeviceLost;

            _deviceLostHelper.WatchDevice(_canvasDevice);
            _deviceLostHelper.DeviceLost += DeviceRemoved;

            _graphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(_compositor, _canvasDevice);
            _graphicsDevice.RenderingDeviceReplaced += RenderingDeviceReplaced;
        }

        public void Dispose()
        {
            lock (_drawingLock)
            {
                _compositor = null;

                if (_canvasDevice != null)
                {
                    _canvasDevice.DeviceLost -= DeviceLost;
                    _canvasDevice.Dispose();
                    _canvasDevice = null;
                }

                if (_graphicsDevice != null)
                {
                    _graphicsDevice.RenderingDeviceReplaced -= RenderingDeviceReplaced;
                    _graphicsDevice.Dispose();
                    _graphicsDevice = null;
                }
            }
        }

        static public void Initialize(Compositor compositor)
        {
            Debug.Assert(!_intialized);

            if (!_intialized)
            {
                _imageLoader = new ImageLoader(compositor);
                _intialized = true;
            }
        }

        static public ImageLoader Instance
        {
            get
            {
                Debug.Assert(_intialized);
                return _imageLoader;
            }
        }

        public void RegisterSurface(ManagedSurface surface)
        {
            _deviceReplacedEvent += surface.OnDeviceReplaced;
        }

        public void UnregisterSurface(ManagedSurface surface)
        {
            _deviceReplacedEvent -= surface.OnDeviceReplaced;
        }


        private void RaiseDeviceReplacedEvent()
        {
            _deviceReplacedEvent?.Invoke(this, new DeviceReplacedEventArgs(_graphicsDevice, _drawingLock));
        }

        public ManagedSurface LoadFromUri(Uri uri)
        {
            return LoadFromUri(uri, Size.Empty);
        }

        public ManagedSurface LoadFromUri(Uri uri, Size size)
        {
            return LoadFromUri(uri, Size.Empty, null);
        }

        public ManagedSurface LoadFromUri(Uri uri, Size size, LoadTimeEffectHandler handler)
        {
            ManagedSurface surface = new ManagedSurface(CreateSurface(size));
            var ignored = surface.Draw(_graphicsDevice, _drawingLock, new BitmapDrawer(uri, handler));

            return surface;
        }

        public ManagedSurface LoadFromFile(StorageFile file, Size size, LoadTimeEffectHandler handler)
        {
            ManagedSurface surface = new ManagedSurface(CreateSurface(size));

            var ignored = surface.Draw(_graphicsDevice, _drawingLock, new BitmapDrawer(file, handler));

            return surface;
        }

        public IAsyncOperation<ManagedSurface> LoadFromUriAsync(Uri uri)
        {
            return LoadFromUriAsyncWorker(uri, Size.Empty, null).AsAsyncOperation<ManagedSurface>();
        }

        public IAsyncOperation<ManagedSurface> LoadFromUriAsync(Uri uri, Size size)
        {
            return LoadFromUriAsyncWorker(uri, size, null).AsAsyncOperation<ManagedSurface>();
        }

        public IAsyncOperation<ManagedSurface> LoadFromUriAsync(Uri uri, Size size, LoadTimeEffectHandler handler)
        {
            return LoadFromUriAsyncWorker(uri, size, handler).AsAsyncOperation<ManagedSurface>();
        }

        public ManagedSurface LoadCircle(float radius, Color color)
        {
            ManagedSurface surface = new ManagedSurface(CreateSurface(new Size(radius * 2, radius * 2)));
            var ignored = surface.Draw(_graphicsDevice, _drawingLock, new CircleDrawer(radius, color));

            return surface;
        }

        public ManagedSurface LoadText(string text, Size size, CanvasTextFormat textFormat, Color textColor, Color bgColor)
        {
            ManagedSurface surface = new ManagedSurface(CreateSurface(size));
            var ignored = surface.Draw(_graphicsDevice, _drawingLock, new TextDrawer(text, textFormat, textColor, bgColor));

            return surface;
        }

        private async Task<ManagedSurface> LoadFromUriAsyncWorker(Uri uri, Size size, LoadTimeEffectHandler handler)
        {
            ManagedSurface surface = new ManagedSurface(CreateSurface(size));
            await surface.Draw(_graphicsDevice, _drawingLock, new BitmapDrawer(uri, handler));

            return surface;
        }

        private CompositionDrawingSurface CreateSurface(Size size)
        {
            Size surfaceSize = size;
            if (surfaceSize.IsEmpty)
            {
                //
                // We start out with a size of 0,0 for the surface, because we don't know
                // the size of the image at this time. We resize the surface later.
                //
                surfaceSize = default(Size);
            }

            var surface = _graphicsDevice.CreateDrawingSurface(surfaceSize, DirectXPixelFormat.B8G8R8A8UIntNormalized, DirectXAlphaMode.Premultiplied);

            return surface;
        }

        private void DeviceRemoved(DeviceLostHelper sender, object args)
        {
            _canvasDevice.RaiseDeviceLost();
        }

        private void DeviceLost(CanvasDevice sender, object args)
        {
            sender.DeviceLost -= DeviceLost;

            _canvasDevice = new CanvasDevice();
            _canvasDevice.DeviceLost += DeviceLost;
            _deviceLostHelper.WatchDevice(_canvasDevice);

            CanvasComposition.SetCanvasDevice(_graphicsDevice, _canvasDevice);
        }

        private void RenderingDeviceReplaced(CompositionGraphicsDevice sender, RenderingDeviceReplacedEventArgs args)
        {
            Task.Run(() =>
            {
                if (_deviceReplacedEvent != null)
                {
                    RaiseDeviceReplacedEvent();
                }
            });
        }
    }

}

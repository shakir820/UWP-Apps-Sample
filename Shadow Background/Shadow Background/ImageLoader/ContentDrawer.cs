using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Composition;
using Microsoft.Graphics.Canvas;

namespace Shadow_Background.ImageLoader
{
    public interface IContentDrawer
    {
        Task Draw(CompositionGraphicsDevice device, Object drawingLock, CompositionDrawingSurface surface, Size size);
    }

    public delegate void LoadTimeEffectHandler(CompositionDrawingSurface surface, CanvasBitmap bitmap, CompositionGraphicsDevice device);
}

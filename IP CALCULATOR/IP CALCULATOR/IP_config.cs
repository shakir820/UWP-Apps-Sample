using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace IP_CALCULATOR
{
    public class core_Func
    {
        public static int getIncrement(int num)
        {
            return IncBy2(num) * 2;
        }
        public static int IncBy2(int num)
        {
            int temp = num;
            if (num > 1)
                num--;
            if (num == 1)
                return 1;
            num = IncBy2(num) * 2;
            return num;
        }
    }

    class IP_config
    {
        public string get_class(IP_Address IP)
        {
            if(IP.first_Octet <= 126 && IP.first_Octet > 1)
            {
                return "Class A";
            }
            if (IP.first_Octet <= 191 && IP.first_Octet > 128)
            {
                return "Class B";
            }
            if (IP.first_Octet <= 223 && IP.first_Octet > 192)
            {
                return "Class C";
            }
            if (IP.first_Octet == 127)
            {
                return "LoopBack";
            }
            return "INVALID IP ADDRESS";
        }

        public static IP_Address get_SubnetNetwork(IP_Address ip = null, int cidr = 0)
        {
            IP_Address temp_mask;
            byte[] bit_Decimal = new byte[] { 0, 128, 192, 224, 240, 248, 252, 254, 255};

            byte firstOctet = ip.first_Octet;
            byte secondOctet = ip.second_Octet;
            byte thirdOctet = ip.third_Octet;
            byte fourthOctet = ip.fourth_Octet;
            
            if(cidr <= 8){
                int pwr = cidr;
                byte first = bit_Decimal[pwr];
                temp_mask = new IP_Address(first, 0, 0, 0);
            }else if(cidr <= 16 && cidr > 8){
                int pwr = cidr - 8;
                byte second = bit_Decimal[pwr];
                temp_mask = new IP_Address(255, second, 0, 0);
            }else if (cidr <= 24 && cidr > 16){
                int pwr = cidr - 16;
                byte third = bit_Decimal[pwr];
                temp_mask = new IP_Address(255, 255, third, 0);
            }else if(cidr <= 32 && cidr > 24){
                int pwr = cidr - 24;
                byte fourth = bit_Decimal[pwr];
                temp_mask = new IP_Address(255, 255, 255, fourth);
            }
            else
            {
                temp_mask = new IP_Address(0, 0, 0, 0);
            }

            IP_Address subnet = new IP_Address((byte)(temp_mask.first_Octet & ip.first_Octet), 
                (byte)(temp_mask.second_Octet & ip.second_Octet), 
                (byte)(temp_mask.third_Octet & ip.third_Octet), 
                (byte)(temp_mask.fourth_Octet & ip.fourth_Octet));

            return subnet;
        }
    }

    class IP_Address
    {
        public IP_Address(byte a, byte b, byte c, byte d)
        {
            first_Octet = a;
            second_Octet = b;
            third_Octet = c;
            fourth_Octet = d;
        }

        public byte first_Octet;
        public byte second_Octet;
        public byte third_Octet;
        public byte fourth_Octet;


    }

    class IP_class
    {
        string class_A = "Class A";
        string class_B = "Class B";
        string class_C = "Class C";
        string class_D = "Class D";
        string class_E = "Class E";
    }
}

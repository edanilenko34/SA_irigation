using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Helpers
{
    public class HexStrToBytes
    {
        public static byte[] ToBytes(string hexStr)
        {
            return hexStr.Split(' ').Select(s => Byte.Parse(s, System.Globalization.NumberStyles.HexNumber)).ToArray();

        }

        public static string ToHexString(string bytestr)
        {
            var bytes = Encoding.ASCII.GetBytes(bytestr);
            var res = new StringBuilder();
            foreach (var b in bytes)
            {
                res.Append(b.ToString("X2")+" ");
            }
            return res.ToString().TrimEnd();
        }
    }
}

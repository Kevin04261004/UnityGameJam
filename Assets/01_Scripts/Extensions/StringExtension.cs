using System.Text;

namespace DYLib
{
    public static class StringExtension
    {
        public static byte[] SerializeToBytes(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
    }
}
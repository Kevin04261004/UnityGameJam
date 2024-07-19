using System.Text;

namespace DYLib
{
    public static class ByteArrayExtension
    {
        public static string DeserializeToString(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
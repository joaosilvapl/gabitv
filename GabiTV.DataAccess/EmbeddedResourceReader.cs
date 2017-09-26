using System.IO;
using System.Reflection;

namespace GabiTV.DataAccess
{
    public class EmbeddedResourceReader
    {
        public static byte[] Read(string relativePath)
        {
            using (Stream stream = typeof(EmbeddedResourceReader).GetTypeInfo().Assembly.GetManifestResourceStream("GabiTV.DataAccess." + relativePath))
            {
                MemoryStream buffer = new MemoryStream();
                stream.CopyTo(buffer);

                return buffer.ToArray();
            }
        }
    }
}
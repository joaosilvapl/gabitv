using System.Net.Http;
using System.Threading.Tasks;
using GabiTV.Core;

namespace GabiTV.DataAccess
{
    public class HttpDataProvider : IDataProvider
    {
        public int GetTotalPhotoCount()
        {
            //TODO: implement
            return 3;
        }

        public PhotoData GetPhotoData(int index)
        {
            throw new System.NotImplementedException();
        }

        public async Task<byte[]> GetPhotoBytesAsync(int index)
        {
            var photoUri = $"{Settings.HttpDataProviderBaseUri}{index}.{Settings.HttpDataProviderFileExtension}";

            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetByteArrayAsync(photoUri);
            }
        }
    }
}
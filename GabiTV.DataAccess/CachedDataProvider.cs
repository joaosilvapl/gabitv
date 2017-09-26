using System.Threading.Tasks;
using GabiTV.Core;

namespace GabiTV.DataAccess
{
    /// <summary>
    /// A composite data provider, which adds caching on top of an inner provider
    /// </summary>
    public class CachedDataProvider : IDataProvider
    {
        private readonly IDataProvider _innerDataProvider;

        //TODO: inject
        private readonly IBlobCache _blobCache = new LocalStorageBlobCache();

        public CachedDataProvider(IDataProvider innerDataProvider)
        {
            this._innerDataProvider = innerDataProvider;
        }

        public int GetTotalPhotoCount()
        {
            return this._innerDataProvider.GetTotalPhotoCount();
        }

        public PhotoData GetPhotoData(int index)
        {
            return this._innerDataProvider.GetPhotoData(index);
        }

        public async Task<byte[]> GetPhotoBytesAsync(int index)
        {
            string photoBlobId = "Photo_" + index;
            return await this._blobCache.GetAsync(photoBlobId, this._innerDataProvider.GetPhotoBytesAsync(index));
        }
    }
}
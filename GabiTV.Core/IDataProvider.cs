using System.Threading.Tasks;

namespace GabiTV.Core
{
    public interface IDataProvider
    {
        int GetTotalPhotoCount();
        PhotoData GetPhotoData(int index);
        Task<byte[]> GetPhotoBytesAsync(int index);
    }
}
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace GabiTV.DataAccess
{
    public class LocalStorageBlobCache : IBlobCache
    {
        public async Task<byte[]> GetAsync(string id, Task<byte[]> fileProvider)
        {
            //TODO: exception handling
            //TODO: add thread management (getting from provider, writing to disk)
            //TODO: should we care about storage size management? What happens if we run out of space?

            StorageFolder localCacheFolder = ApplicationData.Current.LocalCacheFolder;

            var item = await localCacheFolder.TryGetItemAsync(id);
            
            byte[] fileBytes;

            if (item != null)
            {
                var file = await localCacheFolder.GetFileAsync(id);

                using (var stream = await file.OpenReadAsync())
                {
                    fileBytes = new byte[stream.Size];
                    using (DataReader reader = new DataReader(stream))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        reader.ReadBytes(fileBytes);
                    }
                }
            }
            else
            {
                fileBytes = await fileProvider;

                var file = await localCacheFolder.CreateFileAsync(id, CreationCollisionOption.ReplaceExisting);

                await FileIO.WriteBytesAsync(file, fileBytes);
            }

            return fileBytes;
        }
    }
}
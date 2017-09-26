using System;
using System.Threading.Tasks;
using GabiTV.Core;

namespace GabiTV.DataAccess
{
    public class EmbeddedDataProvider : IDataProvider
    {
        public int GetTotalPhotoCount()
        {
            return 3;
        }

        public int GetUnreadPhotoCount()
        {
            return 2;
        }

        public PhotoData GetPhotoData(int index)
        {
            switch (index)
            {
                case 0:
                    return new PhotoData
                    {
                        Title = "Curious cat",
                        Description = "Lorem 123"
                    };
                case 1:
                    return new PhotoData
                    {
                        Title = "Multiple cats",
                        Description = "Ipsum"
                    };
                case 2:
                    return new PhotoData
                    {
                        Title = "Funny cat",
                        Description = "Bla bla"
                    };
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        public async Task<byte[]> GetPhotoBytesAsync(int index)
        {
            return await Task.Factory.StartNew<byte[]>(() => EmbeddedResourceReader.Read($"CatPhotos.{index}.jpeg"));
        }
    }
}

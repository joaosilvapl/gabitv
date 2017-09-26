using System;
using System.Threading.Tasks;

namespace GabiTV.DataAccess
{
    public interface IBlobCache
    {
        Task<byte[]> GetAsync(string id, Task<byte[]> fileProvider);
    }
}
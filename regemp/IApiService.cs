using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace regemp
{
    interface IApiService<T>
    {
        Task<List<T>> RefreshDataAsync();
        Task SaveDataAsync(T item, bool isNewItem);
        Task DeleteDataAsync(string id);
    }
}

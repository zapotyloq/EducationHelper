using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mobile.Models;

namespace Mobile.Services
{
    public class NewsDataStore : IDataStore<New>
    {
        readonly List<New> items;

        public NewsDataStore()
        {
            items = new List<New>()
            {
                new New { Title = " sxs",Description="This is an item description." }
            };
        }

        public async Task<bool> AddAsync(New item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(New item)
        {
            var oldItem = items.Where((New arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where((New arg) => arg.Id.ToString() == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<New> GetAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id.ToString() == id));
        }

        public async Task<IEnumerable<New>> GetAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
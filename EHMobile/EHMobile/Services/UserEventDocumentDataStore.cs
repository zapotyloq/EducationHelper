using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHMobile.Models;

namespace EHMobile.Services
{
    public class UserEventDocumentDataStore : IDataStore<Event>
    {
        readonly List<Event> items;

        public UserEventDocumentDataStore()
        {
            items = new List<Event>()
            {
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  }
            };
        }

        public async Task<bool> AddItemAsync(Event item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            var oldItem = items.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Event arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class VoteDetailViewModel : BaseViewModel
    {
        public IDataStore<Vote> VDS => new VoteDataStore();
        public IDataStore<UserVote> UVDS => new UserVoteDataStore();
        public Vote Item { get; set; }
        public UserVote UserVote {get; set; }
        public List<VoteOption> VoteOptions { get; set; }
        public Command LoadItemsCommand { get; set; }
        public VoteDetailViewModel(Vote item, UserVote userVote, List<VoteOption> vos)
        {
            Title = item?.Name;
            Item = item;
            UserVote = userVote;
            VoteOptions = vos;
            //UserEventDocuments = new UserEventDocumentDataStore().GetUEDAsync(UserEvent.Id).Result;
            //UserEventDocuments = userEventDocuments != null ? userEventDocuments : new List<UserEventDocument>();
            //UserEvent = ((UserEventDataStore)DataStore).GetItemAsync(Item.Id, Auth.User.Id).Result;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                VoteOptions = await new VoteOptionDataStore().GetItemsByVoteIdAsync(Item.Id);
                UserVote = await new UserVoteDataStore().GetItemByVoteIdAsync(Item.Id);
                //UserEvent = await ((UserEventDataStore)DataStore).GetItemAsync(Item.Id,.Id);
                //UserEventDocuments = await new UserEventDocumentDataStore().GetUEDAsync(UserEvent.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

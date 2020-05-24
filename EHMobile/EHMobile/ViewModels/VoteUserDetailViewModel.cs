using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class VoteUserDetailViewModel : BaseViewModel
    {
        public IDataStore<UserVote> UV_DataStore => new UserVoteDataStore();
        public IDataStore<Vote> DataStore => new VoteDataStore();
        public Vote Vote { get; set; }
        public User User { get; set; }
        public UserVote UserVote { get; set; }
        public VoteOption VoteOption { get; set; }
        public Command LoadItemsCommand { get; set; }
        public VoteUserDetailViewModel(Vote vote, User _user)
        {
            User = _user;
            Vote = vote;

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                UserVote = await ((UserVoteDataStore)UV_DataStore).GetItemAsync(Vote.Id, User.Id);
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

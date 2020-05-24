using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Common.Models;
using EHMobile.Services;
using Xamarin.Forms;

namespace EHMobile.ViewModels
{
    public class VoteOptionDetailViewModel : BaseViewModel
    {
        public IDataStore<VoteOption> vo_DataStore => new VoteOptionDataStore();
        public IDataStore<Vote> DataStore => new VoteDataStore();
        public Vote Vote { get; set; }
        public VoteOption VoteOption { get; set; }
        public Command LoadItemsCommand { get; set; }
        public VoteOptionDetailViewModel(Vote vote, VoteOption voteOption)
        {
            VoteOption = voteOption;
            Vote = vote;

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                //UserVote = await ((UserVoteDataStore)V_DataStore).GetItemAsync(Vote.Id, User.Id);
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

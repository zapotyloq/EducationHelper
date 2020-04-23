using System;
using System.Collections.Generic;
using EHMobile.Models;

namespace EHMobile.ViewModels
{
    public class EventDetailViewModel : BaseViewModel
    {
        public Event Item { get; set; }
        public UserEvent UserEvent {get; set; }
        public List<UserEventDocument> UserEventDocuments { get; set; }
        public EventDetailViewModel(Event item = null, UserEvent userEvent = null, List<UserEventDocument> userEventDocuments = null)
        {
            Title = item?.Name;
            Item = item;
            UserEvent = userEvent;
            UserEventDocuments = userEventDocuments != null ? userEventDocuments : new List<UserEventDocument>();
        }
    }
}

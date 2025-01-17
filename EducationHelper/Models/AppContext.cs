﻿using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationHelper.Models
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<UserClass> UserClasses { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<UserVote> UserVotes { get; set; }
        public DbSet<VoteOption> VoteOptions { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserNew> UserNews { get; set; }
        public DbSet<UserEventDocument> UserEventDocuments { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
    }
}

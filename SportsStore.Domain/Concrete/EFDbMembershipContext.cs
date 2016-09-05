using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EFDbMembershipContext : IMembershipRepository
    {
        private Membership context = new Membership();

        public IQueryable<User> Users
        {
            get { return context.Users; }
        }

        public IQueryable<Role> Roles
        {
            get { return context.Roles; }
        }

        public IQueryable<UserInRole> AllUserInRole
        {
            get { return context.AllUserInRole; }
        }
    }
}

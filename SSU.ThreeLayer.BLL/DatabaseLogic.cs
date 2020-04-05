using SSU.ThreeLayer.DAL;
using SSU.ThreeLayer.Entities;
using System.Collections;

namespace SSU.ThreeLayer.BLL
{
    public class DatabaseLogic : IDatabaseLogic
    {
        private IBaseDatabase baseDatabase;

        public DatabaseLogic(IBaseDatabase baseDatabase)
        {
            this.baseDatabase = baseDatabase;
        }

        public void AddUser(User user)
        {
            baseDatabase.AddUser(user);
        }

        public void DeleteUser(string name)
        {
            baseDatabase.DeleteUser(name);
        }

        public void DeleteUser(uint index)
        {
            baseDatabase.DeleteUser(index);
        }

        public void AddAward(Award award)
        {
            baseDatabase.AddAward(award);
        }

        public void DeleteAward(string title)
        {
            baseDatabase.DeleteAward(title);
        }

        public void DeleteAward(uint index)
        {
            baseDatabase.DeleteAward(index);
        }

        public void AddLinker(User user, Award award)
        {
            baseDatabase.AddLinker(user, award);
        }

        public void AddLinker(uint userId, uint awardId)
        {
            baseDatabase.AddLinker(userId, awardId);
        }

        public void DeleteLinker(User user, Award award)
        {
            baseDatabase.DeleteLinker(user, award);
        }

        public void DeleteLinker(uint userId, uint awardId)
        {
            baseDatabase.DeleteLinker(userId, awardId);
        }

        public IEnumerable GetAllUsers()
        {
            return baseDatabase.GetAllUsers();
        }

        public IEnumerable GetAllAwards()
        {
            return baseDatabase.GetAllAwards();
        }

        public IEnumerable GetAllLinkers()
        {
            return baseDatabase.GetAllLinkers();
        }

        public IEnumerable GetUsersByAward(Award award)
        {
            return baseDatabase.GetUsersByAward(award);
        }

        public IEnumerable GetUsersByAward(string title)
        {
            return baseDatabase.GetUsersByAward(title);
        }

        public IEnumerable GetAwardsByUser(User user)
        {
            return baseDatabase.GetAwardsByUser(user);
        }

        public IEnumerable GetAwardsByUser(string name)
        {
            return baseDatabase.GetAwardsByUser(name);
        }

        public User GetUser(uint index)
        {
            return baseDatabase.GetUser(index);
        }

        public Award GetAward(uint index)
        {
            return baseDatabase.GetAward(index);
        }
    }
}

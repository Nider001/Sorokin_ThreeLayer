using System.Collections;
using SSU.ThreeLayer.Entities;

namespace SSU.ThreeLayer.DAL
{
    public interface IBaseDatabase
    {
        void AddUser(User user);
        void DeleteUser(string name);
        void DeleteUser(uint index);

        void AddAward(Award award);
        void DeleteAward(string title);
        void DeleteAward(uint index);

        void AddLinker(User user, Award award);
        void AddLinker(uint userId, uint awardId);

        void DeleteLinker(User user, Award award);
        void DeleteLinker(uint userId, uint awardId);

        IEnumerable GetAllUsers();
        IEnumerable GetAllAwards();
        IEnumerable GetAllLinkers();

        IEnumerable GetUsersByAward(Award award);
        IEnumerable GetUsersByAward(string title);
        IEnumerable GetAwardsByUser(User user);
        IEnumerable GetAwardsByUser(string name);

        User GetUser(uint index);
        Award GetAward(uint index);
    }
}

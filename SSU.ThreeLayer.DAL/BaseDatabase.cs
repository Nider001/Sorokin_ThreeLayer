using SSU.ThreeLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SSU.ThreeLayer.DAL
{
    public class BaseDatabase : IBaseDatabase
    {
        uint index_u; //номер, генерируется автоматически
        uint index_a; //номер, генерируется автоматически
        Hashtable users; //список юзеров
        Hashtable awards; //список наград
        List<Pair<User, Award>> linkers; //список пар "юзер-награда"

        public BaseDatabase() //конструктор класса
        {
            index_u = 0;
            index_a = 0;
            users = new Hashtable();
            awards = new Hashtable();
            linkers = new List<Pair<User, Award>>();
        }

        public void AddUser(User user)
        {
            if (user.Id == 0)
            {
                index_u++;
                user.Id = index_u;
                users.Add(index_u, user);
            }
            else if (!users.ContainsKey(user.Id))
            {
                users.Add(user.Id, user);
                index_u = Math.Max(index_u, user.Id);
            }
            else
            {
                throw new ArgumentException("User entry with ID " + user.Id + " already exists.");
            }
        }

        public void DeleteUser(string name)
        {
            linkers.RemoveAll(x => x.Key.Name == name);

            ICollection key = users.Keys;
            foreach (uint index in key)
            {
                User item = (User)users[index];
                if (string.Compare(name, item.Name) == 0)
                {
                    DeleteUser(index);
                    break;
                }
            }
        }

        public void DeleteUser(uint index)
        {
            User temp = (User)users[index];
            linkers.RemoveAll(x => x.Key.Id == temp.Id);

            users.Remove(index);
        }

        public void AddAward(Award award)
        {
            if (award.Id == 0)
            {
                index_a++;
                award.Id = index_a;
                awards.Add(index_a, award);
            }
            else if (!awards.ContainsKey(award.Id))
            {
                awards.Add(award.Id, award);
                index_a = Math.Max(index_a, award.Id);
            }
            else
            {
                throw new ArgumentException("Award entry with ID " + award.Id + " already exists.");
            }
        }

        public void DeleteAward(string title)
        {
            linkers.RemoveAll(x => x.Value.Title == title);

            ICollection key = awards.Keys;
            foreach (uint index in key)
            {
                Award item = (Award)awards[index];
                if (string.Compare(title, item.Title) == 0)
                {
                    DeleteAward(index);
                    break;
                }
            }
        }

        public void DeleteAward(uint index)
        {
            Award temp = (Award)awards[index];
            linkers.RemoveAll(x => x.Value.Id == temp.Id);

            awards.Remove(index);
        }

        public void AddLinker(User user, Award award)
        {
            Pair<User, Award> temp = new Pair<User, Award>(user, award);

            if (!linkers.Contains(temp))
            {
                linkers.Add(temp);
            }
            else
            {
                throw new ArgumentException("Linker from user " + temp.Key.Id + " to award " + temp.Value.Id + " already exists.");
            }
        }

        public void AddLinker(uint userId, uint awardId)
        {
            Pair<User, Award> temp = new Pair<User, Award>((User)users[userId], (Award)awards[awardId]);

            if (!linkers.Contains(temp))
            {
                linkers.Add(temp);
            }
            else
            {
                throw new ArgumentException("Linker from user " + temp.Key.Id + " to award " + temp.Value.Id + " already exists.");
            }
        }

        public void DeleteLinker(User user, Award award)
        {
            linkers.RemoveAll(x => x.Key == user && x.Value == award);
        }

        public void DeleteLinker(uint userId, uint awardId)
        {
            linkers.RemoveAll(x => x.Key.Id == userId && x.Value.Id == awardId);
        }

        public IEnumerable GetAllUsers()
        {
            return users.Values;
        }

        public IEnumerable GetAllAwards()
        {
            return awards.Values;
        }

        public IEnumerable GetAllLinkers()
        {
            return new List<Pair<User, Award>>(linkers);
        }

        public IEnumerable GetUsersByAward(Award award)
        {
            List<User> temp = new List<User>();
            foreach (var item in linkers)
            {
                if (item.Value.Id == award.Id)
                {
                    temp.Add(item.Key);
                }
            }

            return temp;
        }

        public IEnumerable GetUsersByAward(string title)
        {
            List<User> temp = new List<User>();
            foreach (var item in linkers)
            {
                if (item.Value.Title == title)
                {
                    temp.Add(item.Key);
                }
            }

            return temp;
        }

        public IEnumerable GetAwardsByUser(User user)
        {
            List<Award> temp = new List<Award>();
            foreach (var item in linkers)
            {
                if (item.Key.Id == user.Id)
                {
                    temp.Add(item.Value);
                }
            }

            return temp;
        }

        public IEnumerable GetAwardsByUser(string name)
        {
            List<Award> temp = new List<Award>();
            foreach (var item in linkers)
            {
                if (item.Key.Name == name)
                {
                    temp.Add(item.Value);
                }
            }

            return temp;
        }

        public User GetUser(uint index)
        {
            return (User)users[index];
        }

        public Award GetAward(uint index)
        {
            return (Award)awards[index];
        }
    }
}

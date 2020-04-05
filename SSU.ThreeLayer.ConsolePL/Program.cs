using SSU.ThreeLayer.BLL;
using SSU.ThreeLayer.Common;
using SSU.ThreeLayer.Entities;
using System;
using System.IO;

namespace SSU.ThreeLayer.ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabaseLogic data = DependencyResolver.DatabaseLogic;

            int action = -1;

            while (action != 0)
            {
                try
                {
                    Console.Write("Choose action:" + Environment.NewLine);
                    Console.Write("1. Show all users" + Environment.NewLine);
                    Console.Write("2. Show all awards" + Environment.NewLine);
                    Console.Write("3. Add user" + Environment.NewLine);
                    Console.Write("4. Add award" + Environment.NewLine);
                    Console.Write("5. Assign award" + Environment.NewLine);
                    Console.Write("6. Remove user" + Environment.NewLine);
                    Console.Write("7. Remove award" + Environment.NewLine);
                    Console.Write("8. Unassign award" + Environment.NewLine);
                    Console.Write("9. Add data from file" + Environment.NewLine);
                    Console.Write("10. Save database to file" + Environment.NewLine);
                    Console.Write("0. Exit" + Environment.NewLine);
                    action = int.Parse(Console.ReadLine());

                    Console.Clear();

                    switch (action)
                    {
                        case 1:
                            ShowUsers(data);
                            Console.ReadKey();
                            break;

                        case 2:
                            ShowAwards(data);
                            Console.ReadKey();
                            break;

                        case 3:
                            Console.Write("Enter user name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter date of birth: ");
                            DateTime dateOfBirth = Convert.ToDateTime(Console.ReadLine());

                            data.AddUser(new User(name, dateOfBirth));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 4:
                            Console.Write("Enter award title: ");
                            string title = Console.ReadLine();

                            data.AddAward(new Award(title));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 5:
                            Console.Write("Enter user №: ");
                            uint userId = uint.Parse(Console.ReadLine());
                            Console.Write("Enter award №: ");
                            uint awardId = uint.Parse(Console.ReadLine());

                            data.AddLinker(userId, awardId);
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 6:
                            Console.Write("Enter user №: ");

                            data.DeleteUser(uint.Parse(Console.ReadLine()));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 7:
                            Console.Write("Enter award №: ");

                            data.DeleteAward(uint.Parse(Console.ReadLine()));
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 8:
                            Console.Write("Enter user №: ");
                            uint userId_r = uint.Parse(Console.ReadLine());
                            Console.Write("Enter award №: ");
                            uint awardId_r = uint.Parse(Console.ReadLine());

                            data.DeleteLinker(userId_r, awardId_r);
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 9:
                            Console.Write("Enter file Name: ");

                            ReadFile(data, Console.ReadLine());
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;

                        case 10:
                            Console.Write("Enter file Name: ");

                            WriteFile(data, Console.ReadLine());
                            Console.WriteLine("Success");
                            Console.ReadKey();
                            break;
                    }
                    Console.Clear();
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                catch (ArgumentException e)
                {
                    Console.Clear();
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
            }

        }

        static void ReadFile(IDatabaseLogic databaseLogic, string filePath)
        {
            try
            {
                using (StreamReader inputfile = new StreamReader(filePath))
                {
                    if (inputfile.EndOfStream)
                    {
                        return;
                    }

                    inputfile.ReadLine();

                    while (!inputfile.EndOfStream)
                    {
                        while (inputfile.Peek() != '[')
                        {
                            uint id = uint.Parse(inputfile.ReadLine());
                            string name = inputfile.ReadLine();
                            DateTime dateOfBirth = Convert.ToDateTime(inputfile.ReadLine());

                            databaseLogic.AddUser(new User(id, name, dateOfBirth));

                            inputfile.ReadLine();
                        }

                        inputfile.ReadLine();

                        while (inputfile.Peek() != '[')
                        {
                            uint id = uint.Parse(inputfile.ReadLine());
                            string title = inputfile.ReadLine();

                            databaseLogic.AddAward(new Award(id, title));
                        }

                        inputfile.ReadLine();

                        while (!inputfile.EndOfStream)
                        {
                            uint id1 = uint.Parse(inputfile.ReadLine());
                            uint id2 = uint.Parse(inputfile.ReadLine());

                            databaseLogic.AddLinker(id1, id2);

                            inputfile.ReadLine();
                        }
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static void WriteFile(IDatabaseLogic databaseLogic, string filePath)
        {
            try
            {
                using (StreamWriter outputfile = new StreamWriter(filePath))
                {
                    outputfile.WriteLine("[Users]");

                    foreach (User user in databaseLogic.GetAllUsers())
                    {
                        outputfile.WriteLine(user.Id);
                        outputfile.WriteLine(user.Name);
                        outputfile.WriteLine(user.DateOfBirth.Day + "-" + user.DateOfBirth.Month + "-" + user.DateOfBirth.Year + Environment.NewLine);
                    }

                    outputfile.WriteLine("[Awards]");

                    foreach (Award award in databaseLogic.GetAllAwards())
                    {
                        outputfile.WriteLine(award.Id);
                        outputfile.WriteLine(award.Title);
                    }

                    outputfile.Write("[Linkers]");

                    foreach (Pair<User, Award> linker in databaseLogic.GetAllLinkers())
                    {
                        outputfile.WriteLine(Environment.NewLine + linker.Key.Id);
                        outputfile.WriteLine(linker.Value.Id);
                    }
                }
            }
            catch (IOException e)
            {
                throw new IOException(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static void ShowUsers(IDatabaseLogic databaseLogic)
        {
            foreach (User user in databaseLogic.GetAllUsers())
            {
                var temp = databaseLogic.GetAwardsByUser(user);
                Console.WriteLine(user.GetStringToShow()); //выводим информацию на экран

                foreach (Award award in temp)
                {
                    Console.WriteLine("   " + award.Title + " (№" + award.Id + ")");
                }

                Console.WriteLine();
            }
        }

        static void ShowAwards(IDatabaseLogic databaseLogic)
        {
            foreach (Award award in databaseLogic.GetAllAwards())
            {
                var temp = databaseLogic.GetUsersByAward(award);
                Console.WriteLine(award.GetStringToShow()); //выводим информацию на экран

                foreach (User user in temp)
                {
                    Console.WriteLine("   " + user.Name + " (№" + user.Id + ")");
                }

                Console.WriteLine();
            }
        }
    }
}

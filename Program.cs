using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] p = new Person[1000];
            bool shouldContinue = true;

            for (int x = 0; x < p.Length; x++)
            {
                p[x] = new Person();
            }

            while (shouldContinue)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Borats Online Bank Application");
                Console.WriteLine("Press 1 For Log-in");
                Console.WriteLine("Press 2 For Register");
                Console.WriteLine("Press 3 For Exit");
                Console.WriteLine("Enter Choice:");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login(p);
                        break;

                    case "2":
                        Console.Clear();
                        Register(p);
                        break;

                    case "3":
                        Console.WriteLine("Thank you for Using Borats Online Bank Application.");
                        Console.ReadKey();
                        shouldContinue = false;
                        break;

                    default:
                        Console.WriteLine("The choice you want to enter is incorrect, Please press any key.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static int index = -1;
        public static Person Register(Person[] p)
        {
            index++;
            Random random = new Random();
            int accountId = 0;

            //Unique AccountId Validation
            for (int x = 0; x < p.Length; x++)
            {
                accountId = random.Next(10000); //35

                if (accountId != p[x].AccountId) // 35 != 35
                {
                    p[index].AccountId = accountId;
                    break;
                }
            }          

            Console.WriteLine($"Register for account number : {accountId}");
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            p[index].Username = username;
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            p[index].Password = password;
            Console.WriteLine("Account Successfully Registered. Back to the Main Menu");
            Console.ReadKey();
            return p[index];
        }

        public static void Login(Person[] p)
        {
            
            Console.WriteLine("Log in using your Borat Account");
            Console.WriteLine("Enter username:");
            string usernameLogin = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string passwordLogin = Console.ReadLine();
            bool notFound = false;
            
            for(int x = 0; x <= index; x++)
            {                       
                if (p[x].Username == usernameLogin && p[x].Password == passwordLogin)
                {                       
                    bool shouldContinue = true;                    
                    while (shouldContinue)
                    {
                        Console.Clear();
                        Console.WriteLine($"Account Id : {p[x].AccountId}");
                        Console.WriteLine($"Account Name : {p[x].Username}");                                                   
                        Console.WriteLine("Account Successfully Logged In");                           
                        Console.WriteLine("Enter Login Option:");
                        Console.WriteLine("1.] Deposit");
                        Console.WriteLine("2.] Withdraw");
                        Console.WriteLine("3.] Check Balance");
                        Console.WriteLine("4.] Send Money");
                        Console.WriteLine("5.] Log out");                
                        Console.WriteLine("Enter Option:");
                        string loginOption = Console.ReadLine();
                        switch (loginOption)
                        {
                            case "1":
                                Console.WriteLine($"Account Id : {p[x].AccountId}");                           
                                Console.WriteLine($"Enter the amount you want to Deposit:");
                                bool depositNumberChecker = float.TryParse(Console.ReadLine(), out float number);
                                if (depositNumberChecker)
                                {     
                                    Console.WriteLine($"Number Deposited is $ {p[x].Deposit(Math.Truncate((number * 100)) / 100)}" );
                                    Console.WriteLine($"Your Balance is $ {p[x].Balance}");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Please Input numbers");
                                    Console.WriteLine("Press Any Key to Continue");
                                    Console.ReadKey();
                                    continue;
                                }
                                break;

                            case "2":
                                Console.WriteLine($"Account Id : {p[x].AccountId}");                                
                                Console.WriteLine($"Enter Withdrawal amount:");
                                bool withdrawalNumberChecker = float.TryParse(Console.ReadLine(), out float number2);
                                if (withdrawalNumberChecker)
                                {
                                    if(number2 > p[x].Balance)
                                    {
                                        Console.WriteLine("You cannot withdraw less the amount of your balance");
                                        Console.ReadKey(); 
                                    }
                                    else
                                    {    
                                        p[x].Withdrawal(Math.Truncate(number2 * 100)/ 100);
                                        Console.WriteLine($"You Withrawed $ {number2}");
                                        Console.WriteLine($"Your Balance is $ {p[x].Balance}");
                                        Console.ReadKey();
                                    }                                                                           
                                }
                                else
                                {
                                    Console.WriteLine("Please Input numbers");
                                    Console.WriteLine("Press Any Key to Continue");
                                    Console.ReadKey();
                                    continue;
                                }
                                break;
                                
                            case "3":
                                Console.WriteLine($"Account Id : {p[x].AccountId}");                                 
                                Console.WriteLine("Your Account Balance is $ {0:F2}", p[x].Balance);                                                       
                                Console.WriteLine("Press Any Key to Continue");
                                Console.ReadKey();
                                break;

                            case "4":
                                bool inputDigitsOnly = true;
                                Console.WriteLine("Please Input the 5-Digit Account Id of the reciever");                             
                                bool accountIdNumberChecker = int.TryParse(Console.ReadLine(), out int transferAccountId);
                                for(int y = 0; y <= index; y++)
                                {         
                                    if (accountIdNumberChecker)
                                    {                                
                                        if(p[y].AccountId == transferAccountId) // reciever
                                        {
                                            float number4;
                                            Console.WriteLine("Enter Amount of Money You want to Send:");  
                                            bool sendMoneyChecker = float.TryParse(Console.ReadLine(), out number4);
                                            if(sendMoneyChecker)
                                            {
                                                if(p[x].Balance < number4 )//balance of sender
                                                {
                                                    Console.WriteLine("Insufficient Funds");
                                                    Console.ReadKey();
                                                    break;                                            
                                                }else
                                                {
                                                    p[y].Balance += number4; //reciver
                                                    p[x].Balance -= number4; //sender
                                                    Console.WriteLine("Money Successfully sent");
                                                    Console.WriteLine("Going back to the Log In Menu");
                                                    Console.ReadKey();
                                                    break;        
                                                }                                              
                                            }
                                            else
                                            {
                                                inputDigitsOnly = false;
                                                break;       
                                            }                          
                                        }else
                                        {
                                            inputDigitsOnly = false;
                                        }                     
                                    }
                                    else
                                    {
                                        inputDigitsOnly = false;
                                    } 
                                }                                                                                                                              

                                if(!inputDigitsOnly)
                                {
                                    Console.WriteLine("Please input digits only or Invalid Account Id.");
                                    Console.WriteLine("Press Any Key to Continue");
                                    Console.ReadKey();
                                } 
                                break;

                            case "5":
                                Console.WriteLine("Logging Out");
                                Console.WriteLine("Press Any Key to Continue");
                                Console.ReadKey();
                                return;                               

                            default:
                                Console.WriteLine("Invalid Keyword. Please try again.");
                                Console.ReadKey();
                                break;
                        }
                    }
                }
                else
                {
                    notFound = true;
                }
            }

            if(notFound || index == -1)
            {
                Console.WriteLine("Invalid username or password. please try again");
                Console.ReadKey();
            }                   
        }
    }
}

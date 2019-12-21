using System;

namespace Task1
{
    class Program
    {
        static Bank bank = new Bank("Borats Bank");
        static void Main(string[] args)
        {
            bool shouldContinue = true;
            string username = string.Empty;
            string password = string.Empty;

            while (shouldContinue)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Borats Online Bank Application");
                Console.WriteLine("Press 1 For Log-in");
                Console.WriteLine("Press 2 For Register");
                Console.WriteLine("Press 3 For Exit");
                Console.WriteLine("Enter Choice:");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Log in using your Borat Account");
                        Console.WriteLine("Enter username:");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        password = "";              
                        var passwordMasked = bank.PasswordMasking();
                        var account = bank.Login(username, passwordMasked);
                        UserLoginMainMenu(account);
                        break;

                    case "2":
                        while(true)
                        {
                            Console.Clear();
                            Console.WriteLine("Registration of Account");
                            Console.WriteLine("Enter username:");
                            username = Console.ReadLine();
                            Console.WriteLine("Enter password:");
                            password = bank.PasswordMasking();
                            if(!bank.RegisterSuccess(username))
                            {
                                var id = bank.Register(username, password);
                                Console.WriteLine("\nCongratulations you have successfully registered.");
                                Console.WriteLine($"Here is your Account Id. {id}");
                                Console.WriteLine("Press Any Key to Continue.");
                                Console.ReadKey();
                                break;
                            }else
                            {
                                Console.WriteLine("\nUsername taken, Please try again.");
                                Console.WriteLine("Press Any Key to Continue.");
                                Console.ReadKey();
                            }               
                        }        
                        break;

                    case "3":
                        Console.WriteLine("Thank you for Using Borats Online Bank Application.");
                        Console.WriteLine("Press Any Key to Continue.");
                        Console.Beep();
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

        public static void UserLoginMainMenu(Account account)
        {
            float number;
            bool isNumber;
            bool shouldContinue = true;

            while (shouldContinue)
            {
                if (account != null)
                {
                    Console.Clear();
                    Console.WriteLine("Account Successfully Logged In");
                    Console.WriteLine($"Welcome back, {account.Username}");
                    Console.WriteLine($"Account Id: {account.AccountId}");
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
                            Console.WriteLine($"Account Id : {account.AccountId}");
                            Console.WriteLine($"Enter the amount you want to Deposit:");
                            isNumber = float.TryParse(Console.ReadLine(), out number);
                            if (isNumber)
                            {
                                Console.WriteLine($"Number Deposited is $ {bank.Deposit(number, account.Username)}");
                                Console.WriteLine($"Your Balance is $ {bank.ShowBalance(account.Username)}");
                                Console.ReadKey();
                            }
                            else
                            {
                                ShowErrorMessage();
                                continue;
                            }
                            break;

                        case "2":
                            Console.WriteLine($"Account Id : {account.AccountId}");
                            Console.WriteLine($"Enter Withdrawal amount:");
                            isNumber = float.TryParse(Console.ReadLine(), out number);
                            if (isNumber)
                            {
                                if (number > bank.ShowBalance(account.Username))
                                {
                                    Console.WriteLine("You cannot withdraw less the amount of your balance");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    bank.Withdrawal(bank.NumberTrancate(number), account.Username);
                                    Console.WriteLine($"You Withrawed $ {number}");
                                    Console.WriteLine($"Your Balance now is $ {bank.ShowBalance(account.Username)}");
                                    Console.WriteLine("Press Any Key to Continue");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                ShowErrorMessage();
                            }
                            break;

                        case "3":
                            Console.WriteLine($"Your Balance is $ {bank.ShowBalance(account.Username)}");
                            Console.WriteLine("Press Any Key to Continue");
                            Console.ReadKey();
                            break;

                        case "4":
                            Console.WriteLine("Please Input the Id of the reciever");
                            Guid reciever = Guid.Parse(Console.ReadLine());
                            var recieverFound = bank.SearchReciever(reciever);
                            if (recieverFound == null)
                            {
                                Console.WriteLine("Account not Found");
                                Console.WriteLine("Press Any Key to Continue");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Enter Amount of Money You want to Send:");
                                isNumber = float.TryParse(Console.ReadLine(), out number);
                                if (isNumber)
                                {
                                    if (account.Balance < number)
                                    {
                                        Console.WriteLine("Insufficient Funds");
                                        Console.WriteLine("Press Any Key to Continue");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        var moneyBalanceAfterTransfered = bank.Deposit(bank.NumberTrancate(number), reciever);
                                        var moneyBalanceAfterTransfered2 = bank.Withdrawal(bank.NumberTrancate(number), account.Username);
                                        Console.WriteLine("Money Successfully Sent");
                                        Console.WriteLine($"Your Balance now is $ {bank.ShowBalance(account.Username)}");
                                        Console.WriteLine("Press Any Key to Continue");
                                        Console.ReadKey();
                                    }
                                }
                                else
                                {
                                    ShowErrorMessage();
                                }
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
                else
                {
                    Console.WriteLine("\nInvalid Username or Password, press any key to continue.");
                    Console.ReadKey();
                    break;
                }
            }
        }

        public static void ShowErrorMessage()
        {
            Console.WriteLine("Please Input numbers");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();
        }
    }
}

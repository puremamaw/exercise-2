using System;

class Bank
{
    public string Name { get; set; }

    private Account[] Accounts { get; set; }

    private int TotalAccounts { get; set; } = 0;

    public Bank(string name)
    {
        Name = name;
        Accounts = new Account[100];
        for (var x = 0; x < Accounts.Length; x++) Accounts[x] = new Account();
    }


    public string GetWelcomeMessage()
    {
        return $"Welcome to {Name}";
    }


    public Guid Register(string username, string password)
    {

        Accounts[TotalAccounts].Username = username;
        Accounts[TotalAccounts].Password = password;

        return Accounts[TotalAccounts++].AccountId;
    }

    public Account Login(string username, string password)
    {
        foreach (Account a in Accounts)
        {
            if (a.Username == username && a.Password == password)
            {
                return a;
            }
        }
        return null;
    }

    

    public float Deposit(float money, string username)
    {
        foreach (Account a in Accounts)
        {
            if (a.Username == username)
            {
                return a.Balance += money;
            }
        }
        return 0;
    }

    //overload version used when sending money
    public float Deposit(float money, Guid username)
    {
        foreach (Account a in Accounts)
        {
            if (a.AccountId == username)
            {
                return a.Balance += money;
            }
        }
        return 0;
    }

    public float ShowBalance(string username)
    {
        foreach (Account a in Accounts)
        {
            if (a.Username == username)
            {
                return (float)Math.Truncate((a.Balance * 100)) / 100;
            }
        }
        return -1; //banks do not have negative balance
    }

    public float Withdrawal(float money, string username)
    {
        foreach (Account a in Accounts)
        {
            if (a.Username == username)
            {
                return a.Balance -= money;
            }
        }
        return 0;
    }

    public Account SearchReciever(Guid username)
    {
        foreach (Account a in Accounts)
        {
            if (a.AccountId == username)
            {
                return a;
            }
        }
        return null;
    }

    public Guid GetAccountId(string username)
    {
        foreach (Account a in Accounts)
        {
            if (a.Username == username)
            {
                return a.AccountId;
            }
        }
        return Guid.Empty;
    }

    public string PasswordMasking()
    {
        string password = string.Empty;
        do
        {       
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else
            {
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
        } while (true);

        return password;
    }

    public float NumberTrancate(float number)
    {
        return (float)Math.Truncate((number * 100)) / 100;
    }

    public bool RegisterSuccess(string username)
    {
        foreach (Account a in Accounts)
        {
            if (a.Username == username)
            {
                return true;
            }
        }
        return false;
    }
}
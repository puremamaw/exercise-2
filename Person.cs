class Person
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int AccountId { get; set; } 
    public double Balance { get; set; }

    public double Deposit(double number)
    {
        return Balance += number;
    }
    public double Withdrawal(double number)
    {
        return Balance -= number;
    }
}
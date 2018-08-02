using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace AltSourceBankingLedger
{
    class Program
    {
        private static List<string> userNameList = new List<string>();
        private static List<string> passwordList = new List<string>();

        private static List<string> namesList=new List<string>();
        private static List<string> emailList=new List<string>();
        private static List<Account> accounts= new List<Account>();

        private static Dictionary<string, string> loggedIn = new Dictionary<string, string>();

        private static Dictionary<string, Account> userAccountDetails = new Dictionary<string, Account>();

        private static List<double> balanceList=new List<double>();

        private static bool login=false;

        private static string userPath="<Enter own path>";

        static Account ac=new Account();
        static Checking ch=new Checking(0.0);


        public static void Menu()
        {
            
            Console.WriteLine("Select the menu option below");
            Console.WriteLine("\n1. Create a new account");
            Console.WriteLine("2. Login to your account");
            Console.WriteLine("Q Exit");
        
            string choice= Console.ReadLine();
            bool test=false;
         do{
            switch (choice)
            {
                case "1":
                //Create checking account
                    CreateAccount();  
                    break;

                case "2":
                //User login
                    Login();
                    break;

                case "7":
                //Logout
                  
                    break;
             
                case "Q":
                //Quit the application
                    Environment.Exit(0);

                    break;

                  
                default:
                    Console.WriteLine("Invalid selection\n");
                    Console.WriteLine("Please start again.");
                    Menu();
                    break;
                }
            }while(!test);
        }

        public static void MenuUser(string username)
        {
            bool test=false; 
           do{ 
     
        
            Console.WriteLine("3A. Deposit money into checking account");
            //Console.WriteLine("3B. Deposit money into savings account");
            Console.WriteLine("4A. Withdraw money from checking account");
            //Console.WriteLine("4B. Withdraw money from savings account");
            Console.WriteLine("5A. View checking account balance");
            //Console.WriteLine("5A. View savings account balance");
            Console.WriteLine("6A. View Transaction History for Checking account");
            //Console.WriteLine("6A. View Transaction History for Savings account");
            Console.WriteLine("7. Logout");
            Console.WriteLine("Q Exit");

            string choice= Console.ReadLine();
                switch (choice)
                {
                
                    case "3A":
                    //Deposit money into checking account
                        DepositChecking(username);
                        break;
                    
                    case "3B":
                    //Deposit money into savings account
                        break;

                    case "4A":
                    //Withdraw money from checking account
                        WithdrawChecking(username);
                        break;

                    /*case "4B":
                    //Withdraw money from savings account

                        break;*/

                    case "5A":
                    //View checking account balance
                        ViewCheckingBalance(username);
                        break;

                  /*  case "5B":
                    //View savings account balance

                        break;*/

                    case "6A":
                    //Check trasaction history for checking account
                        TransactionHistory(username);
                        break;

                    /*case "6B":
                    //Check transaction history for savings account
                    break;
                    */

                    case "7":
                    //Logout
                        Logout(username);
                        break;

                    default:
                        Console.WriteLine("Invalid option selected. Please select the correct option.");
                        test=false;
                        break;
                }
           } while(!test);

        }
        

        //Function to create account
        public static void CreateAccount()
        {
          try{
     
            Address ad=new Address();

            double accn=0.0;

            string usern="";

            Console.WriteLine("Welcome to Imaginary US Bank. Begin creating your account");
            Console.WriteLine("Enter your first name");
            try{
            ac.FirstName=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ac.FirstName = Console.ReadLine();
            }

            try{
            Console.WriteLine("Enter your last name");
            ac.LastName=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ac.LastName= Console.ReadLine();
            }

            Console.WriteLine("Enter Address");

            Console.WriteLine("Street");
            ad.Street= Console.ReadLine();

            try{
            Console.WriteLine("City");
            ad.City=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ac.FirstName = Console.ReadLine();
            }

            try{
            Console.WriteLine("State");
            ad.State=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ad.State=Console.ReadLine();
            }
            
            Console.WriteLine("Zip Code");
            try{
            ad.ZipCode=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ad.ZipCode=Console.ReadLine();  
            }

            Console.WriteLine("The address you entered is:"+ad.FullAddress);

            //---------------Check if the username and password you have entered already exist in the registered accounts.-----------//
            if(File.Exists(userPath+"RegisteredAccounts/RegisteredUsers.txt"))
            {
                string[] regLines = File.ReadAllLines(@userPath+"RegisteredAccounts/RegisteredUsers.txt");
                string[] userpass=new string[5];
            

                foreach(string line in regLines)
                {
                
                    userpass=line.Split(",");
                    userNameList.Add(userpass[0]);
                    passwordList.Add(userpass[1]);
                    namesList.Add(userpass[2]);
                    emailList.Add(userpass[4]);
                }


            }
            else
            {
                    using (StreamWriter file2 =
                    new StreamWriter(@userPath + "RegisteredAccounts/RegisteredUsers.txt", true))
                    {
                        
                    }
            }
            

            Console.WriteLine("Set a username");
            try{
            usern=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                usern=Console.ReadLine();
            
            }
            if(userNameList.Exists(x=>x==usern))
            {
                Console.WriteLine("Username already exists. Please set another username");
                usern=Console.ReadLine();
                ac.UserName=usern;
            }
            else{
                //usern=Console.ReadLine();
                ac.UserName=usern;
                userNameList.Add(ac.UserName);
            }
            

            Console.WriteLine("Set a password");

           try{
            ac.PassWord=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ac.PassWord=Console.ReadLine();
            
            }
            if(passwordList.Exists(x=>x==ac.PassWord))
            {
                Console.WriteLine("Password already exists. Please set another password");
                ac.PassWord=Console.ReadLine();
            }
            else{
                passwordList.Add(ac.PassWord);
            }
          

            Console.WriteLine("Enter your contact number");
            try{
            ac.ContactNumber=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ac.ContactNumber=Console.ReadLine();
            }


            Console.WriteLine("Enter your email address");
            try{
            ac.EmailId=Console.ReadLine();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                ac.ContactNumber=Console.ReadLine();
            }

            //---------------Check if the details already exist and the user is already registered, and redirect them to Login-----------//
            if(emailList.Exists(z=>z==ac.EmailId))
            {
                Console.WriteLine("This account already exists. Please login");
                Login();
            }


            Console.WriteLine("Enter the Account type you would like to open (Checking or Savings)");
            try{
            ac.AccountType=Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ac.AccountType=Console.ReadLine();
            }

            Console.WriteLine("The assigned account number is as given below ");
            accn=ac.AccountNumb();
            Console.WriteLine(accn);

            accounts.Add(ac);

            //Add the created account to the dictionary
            userAccountDetails.Add(ac.UserName, ac);
        

            string extension=".txt";
            
            String text = "Account has been created for the user "+ac.FirstName + " "+ac.LastName;

            //The user can replace the path to store the document
            File.WriteAllText(@userPath+"CreatedAccounts/Accounts"+ac.FirstName+ac.LastName+extension, text);
            using (StreamWriter file = 
            new StreamWriter(@userPath+"CreatedAccounts/Accounts"+ac.FirstName+ac.LastName+extension, true))
        {
            file.WriteLine("Account details are as follows:");
            file.WriteLine("First Name:"+ac.FirstName);
            file.WriteLine("Last Name:"+ac.LastName);
            file.WriteLine("Account type:"+ac.AccountType);
            file.WriteLine("Contact number:"+ac.ContactNumber);
            file.WriteLine("Email address:"+ac.EmailId);
            file.WriteLine("Full Address:"+ad.FullAddress);
            file.WriteLine("Account number:"+accn);
            file.WriteLine("View the transactions:"+userPath+"AccountTransactions/Transactions"+ac.FirstName+ac.LastName+extension);

        }

            string fullname=ac.FirstName+" "+ac.LastName;

            
         
            using (StreamWriter file2 = 
            new StreamWriter(@userPath+"RegisteredAccounts/RegisteredUsers.txt",true))
            {
                file2.WriteLine(ac.UserName+","+ac.PassWord+"," + fullname + ","+accn+","+ac.EmailId );
            }

            Console.WriteLine("Account has been created. Select from the options below");
            Menu();
          }
          catch(ValidationException)
          {
              Console.WriteLine("There was some error while entering the data. Please create your account again.");
              CreateAccount();
          }
        }
        //Function to create account ends


        //Function for user login
        public static void Login()
        {
            Console.WriteLine("Enter username");
            string username=Console.ReadLine();

            Console.WriteLine("Enter password");
            string password=Console.ReadLine();

            //The file RegisteredUsers.txt already exists
            string[] lines = File.ReadAllLines(@userPath+"RegisteredAccounts/RegisteredUsers.txt");
            string[] userpass=new string[4];
            foreach(string line in lines)
            {
                
                userpass=line.Split(",");
                userNameList.Add(userpass[0]);
                passwordList.Add(userpass[1]);
  
            }

           
                if(userNameList.Exists(x=>x==username) && passwordList.Exists(y=>y==password))
                {
      
                
                    login = true;

                }
                else if(!userNameList.Exists(x=>x==username) || !passwordList.Exists(y=>y==password))
                {
                    Console.WriteLine("You entered the incorrect username or password. Please insert the username and password again");
                    
                    Console.WriteLine("Enter username");
                    username=Console.ReadLine();

                    Console.WriteLine("Enter password");
                    password=Console.ReadLine();

                    if(userNameList.Exists(x=>x==username) && passwordList.Exists(y=>y==password))
                        login=true;
                    else{
                        login=false;
                        Console.WriteLine("Please check your details and login again.");
                        Menu();
                    }

                }
                else
                {
                    Console.WriteLine("Account does not exist. Please create an account");
                    CreateAccount();
                    login=false;
                }
            
            if(login==true)
            {
                
                Console.WriteLine("Account exists");

                //add the logged in user to the dictionary
                loggedIn.Add(userpass[0], userpass[1]);

                //Add the logged in user to the LoggedInUsers.txt file
                using (StreamWriter file3 = 
                new StreamWriter(@userPath+"LoggedIn/LoggedInUsers.txt",true))
                {
                    file3.WriteLine(username+","+password);
                }


        
                Console.WriteLine("Select from the options below");
                MenuUser(username);
                
            }
            else
            {
                Console.WriteLine("Back to main menu");
                Menu();
            }
        }

        //Function for user login ends


        //Function to deposit money into checking account
        public static void DepositChecking(string username)
        {
            try
            {

                string fullname = "";

                string[] lines = File.ReadAllLines(@userPath + "RegisteredAccounts/RegisteredUsers.txt");
                Dictionary<string, string> userNameMap = new Dictionary<string, string>();
                string[] userpass = new string[5];
                foreach (string line in lines)
                {
                    userpass = line.Split(",");
                    userNameList.Add(userpass[0]);
                    passwordList.Add(userpass[1]);
                    fullname = userpass[2];
                    userNameMap.Add(userpass[0], fullname);
                }
                

                if (userNameList.Exists(x => x == username))
                {

                    bool validBalance = false;
                    string fullName = userNameMap[username];
                    string[] name = fullName.Split(" ");
                
                    Console.WriteLine("Enter the amount you want to deposit");
                    ch.Deposit = Double.Parse(Console.ReadLine());
                   

                    if (ch.Deposit>=ch.MinBalance && ch.Deposit <= ch.MaxBalance)
                    {
                        validBalance = true;
                    
                    }
                    else if (ch.Deposit < ch.MinBalance)
                    {
                        Console.WriteLine("The deposit is lesser then the minimum deposit. Please enter the amount again. The minimum deposit is $500. ");
                        ch.Deposit = Double.Parse(Console.ReadLine());
                        if (ch.Deposit >= ch.MinBalance)
                            validBalance = true;
                        else
                        {
                            Console.WriteLine("The balance you entered was incorrect. Please select the menu options again");
                            MenuUser(username);
                        }

                    }
                    else if (ch.Deposit > ch.MaxBalance)
                    {
                        Console.WriteLine("The deposit is greater than the maximum allowed deposit. Please enter the amount again. The maximum deposit is " + ch.MaxBalance);
                        ch.Deposit = Double.Parse(Console.ReadLine());
                        if (ch.Deposit > ch.MinBalance)
                            validBalance = true;
                        else
                        {
                            Console.WriteLine("The balance you entered was incorrect. Please select the menu options again");
                            MenuUser(username);
                        }

                    }
                    string extension = ".txt";
                    String text = "Account type:Checking";
                    using (StreamWriter file3 =
                               new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                        {

                            file3.WriteLine(text);//Account type
                            file3.WriteLine("Account number:"+userpass[3]);//Account number
                  
                        }
                  
                    if (validBalance == true)
                    {
                        
                        Console.WriteLine("You deposited:" + ch.Deposit);     

                        using (StreamWriter file3 =
                               new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                        {
                        
                            file3.WriteLine("Deposit:" + ch.Deposit);
                            file3.WriteLine("Time(D):" + DateTime.Now);
                        }

                            using (StreamWriter file4 =
                                new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + extension, true))
                            {
                            
                            }
                            string balanceFilePath=@userPath + "AccountBalance/Balance" + name[0] + name[1] + extension;
                            string[] previousBalance=File.ReadAllLines(balanceFilePath);
                            string transactionsFilepath=@userPath + "AccountBalance/Balance" + name[0] + name[1] + extension;
                            string[] previousTransactions = File.ReadAllLines(transactionsFilepath);
                            
                            //-----------------Check if the user has had any previour transactions-------------------//
                            //If there have been no previous transactions
                            //if(previousBalance.Length==0)
                            if(previousTransactions.Length==0)
                            {
                                using (StreamWriter file3 =
                                new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + ".txt", true))
                                {
                                
                                    file3.WriteLine("Balance at:" + DateTime.Now);
                                    file3.WriteLine("Balance:" + ch.GetBalance());
                                                            
                                }
                                Console.WriteLine("Your account balance is:"+ch.GetBalance());

                                balanceList.Add(ch.GetBalance());
                                using (StreamWriter file4 =
                                new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + ".txt", true))
                                {
                                
                                    file4.WriteLine("------------------------------");
                                    file4.WriteLine("Time:"+DateTime.Now);
                                    file4.WriteLine("Balance:"+ch.GetBalance());

                                } 
                            }
                            else{
                                //If there have been previous transactions.
                               
                                string lastLine = File.ReadLines(balanceFilePath).Last();
                                string[] balanceString=lastLine.Split(":");
                                double getBalance=Double.Parse(balanceString[1]);
                                double newBalance = getBalance + ch.Deposit;
                                double getb = ch.GetBalance();
                                using (StreamWriter file3 =
                                new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + ".txt", true))
                                {
                                
                                    file3.WriteLine("Balance at:" + DateTime.Now);
                                    file3.WriteLine("Balance:" + newBalance);
                                                            
                                }
                         
                                Console.WriteLine("Your account balance is:"+newBalance);

                                balanceList.Add(newBalance);
                                using (StreamWriter file4 =
                                new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + ".txt", true))
                                {
                                
                                    file4.WriteLine("------------------------------");
                                    file4.WriteLine("Time:"+DateTime.Now);
                                    file4.WriteLine("Balance:"+newBalance);

                                } 

                                if(newBalance>ch.MaxBalance)
                                {
                                    Console.WriteLine("Your account has exceeded the maximum allowed balance.");
                                }

                            }

                        Console.WriteLine("Choose your next action from the options below");
                        MenuUser(username);

                    }
                         
                else
                {
                    Console.WriteLine("User does not exist. Get back to main menu to create an account.");
                    Menu();
                }
            }
            }
    
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Redirecting to the menu");
                MenuUser(username);
            }

        }
        //Function to deposit money ends

        
        //Function to withdraw money from Checking account
        public static void WithdrawChecking(string username)
        {
            try
            {
                string fullname = "";
                // Checking ch=new Checking(0.0);

                string[] lines = File.ReadAllLines(@userPath + "RegisteredAccounts/RegisteredUsers.txt");
                Dictionary<string, string> userNameMap = new Dictionary<string, string>();
                string[] userpass = new string[5];
                //Get the fields to verify account validity and create the file.
                foreach (string line in lines)
                {
                    userpass = line.Split(",");
                    userNameList.Add(userpass[0]);
                    passwordList.Add(userpass[1]);
                    fullname = userpass[2];
                    userNameMap.Add(userpass[0], fullname);
                }

                if (userNameList.Exists(x => x == username))
                {
                    Console.WriteLine("Enter the amount you want to withdraw");
                    ch.Withdrawal = Double.Parse(Console.ReadLine());

                    if (ch.Withdrawal > ch.MaxBalance)
                    {
                        Console.WriteLine("The amount you entered is more than the maximum balance on your account. Please enter an amount less than " + ch.MaxBalance);
                        ch.Withdrawal = Double.Parse(Console.ReadLine());


                    }
                    else
                    {

                        string extension = ".txt";
                        //String text = "Account type:Checking";

                        string fullName = userNameMap[username];
                        string[] name = fullName.Split(" ");

                        using (StreamWriter file4 =
                            new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + extension, true))
                        {
                            
                        }

                        using (StreamWriter file3 =
                            new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                        {
                            

                        }

                            //----------------------------Check if there have been previous transactions--------------------------------.
                            string balanceFilePath = @userPath + "AccountBalance/Balance" + name[0] + name[1] + extension;
                            string[] transactions = File.ReadAllLines(balanceFilePath);
                          
                            if(transactions.Length==0)
                            {
                                using (StreamWriter file3 =
                                    new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                                {
                                    file3.WriteLine("Withdrawal:" + ch.Withdrawal);
                                    file3.WriteLine("Time(W):" + DateTime.Now);
                                }
                                using (StreamWriter file3 =
                                new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                                {

                                    file3.WriteLine("Balance at:" + DateTime.Now);
                                    file3.WriteLine("Balance:" + ch.GetBalance());

                                }
                                if(ch.GetBalance()<ch.MinBalance)
                                {
                                    Console.WriteLine("Your account balance is below the required minimum account balance. Please deposit some money into your account");
                                    DepositChecking(username);
                                }
                                Console.WriteLine("Your account balance is:" + ch.GetBalance());
                            }
                            else{

                            //If there have been previous transactions.

                                string lastLine = File.ReadLines(balanceFilePath).Last();
                                string[] balanceString = lastLine.Split(":");
                                double getBalance = Double.Parse(balanceString[1]);
                                double newBalance = getBalance - ch.Withdrawal;
                                double getb = ch.GetBalance();
                                 using (StreamWriter file3 =
                                    new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                                {
                                    file3.WriteLine("Withdrawal:" + ch.Withdrawal);
                                    file3.WriteLine("Time(W):" + DateTime.Now);
                                }

                                using (StreamWriter file3 =
                                new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                                {
                          
                                   
                                    file3.WriteLine("Balance at:" + DateTime.Now);
                                    file3.WriteLine("Balance:"+newBalance);
     
                                }
                                Console.WriteLine("Your account balance is:" + newBalance);

                                 using (StreamWriter file4 =
                                new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + ".txt", true))
                                {
                                
                                    file4.WriteLine("------------------------------");
                                    file4.WriteLine("Time:"+DateTime.Now);
                                    file4.WriteLine("Balance:"+newBalance);
                                   

                                } 

                                if(newBalance<ch.MinBalance)
                                {
                                    Console.WriteLine("Your account balance is below the required minimum account balance. Please deposit some money into your account");
                                    DepositChecking(username);
                                }

                            }
                            
                           
                        }
           
                        Console.WriteLine("Choose you next activity from the menu below:");
                        MenuUser(username);
                    }

                else
                {
                    Console.WriteLine("User does not exist. Get back to main menu to create an account.");
                    Menu();
                }
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                MenuUser(username);
            }

        }
        //Function to withdraw money ends.

        //Function to view checking account balance.
        public static string ViewCheckingBalance(string username)
        {
             string fullname="";
            // Checking ch=new Checking(0.0);

            string[] lines = File.ReadAllLines(@userPath+"RegisteredAccounts/RegisteredUsers.txt");
            Dictionary<string, string> userNameMap=new Dictionary<string,string>();
            string[] userpass=new string[4];
            //Get the fields to verify account validity and create the file.
            foreach(string line in lines)
            {
                userpass=line.Split(",");
                userNameList.Add(userpass[0]);
                passwordList.Add(userpass[1]);
                fullname=userpass[2];
                userNameMap.Add(userpass[0], fullname);
            }

            string lastLine="";

            if(userNameList.Exists(x=>x==username))
            {
                string fullName = userNameMap[username];
                string[] name = fullName.Split(" ");
                string extension = ".txt";

                string transactionBalanceFilePath = @userPath + "AccountBalance/Balance" + name[0] + name[1] + extension;

                if (!File.Exists(transactionBalanceFilePath))
                {
                    using (StreamWriter file4 =
                           new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + ".txt", true))
                    {


                    }
                    Console.WriteLine("Your account does not have any balance so far. Please deposit money into account");
                    DepositChecking(username);
                }
                else
                {
                    lastLine = File.ReadLines(transactionBalanceFilePath).Last();
                    Console.WriteLine("Account: " + lastLine);
                }

         
            }
            else{
                Console.WriteLine("User does not exist. Please create an account.");
                Menu();
            }

            return lastLine;
        }

        //Function to view checking account balance ends.


        //Function to check transaction history
        public static void TransactionHistory(string username)
        {
            try
            {
                string[] lines = File.ReadAllLines(@userPath + "RegisteredAccounts/RegisteredUsers.txt");

                String fullname = "";
                string accountNumber = "";
                Dictionary<string, string> userNameMap = new Dictionary<string, string>();
                string[] userpass = new string[4];

                foreach (string line in lines)
                {
                    userpass = line.Split(",");
                    userNameList.Add(userpass[0]);
                    passwordList.Add(userpass[1]);
                    fullname = userpass[2];
                    userNameMap.Add(userpass[0], fullname);
                    accountNumber = userpass[3];
                }

                if (userNameList.Exists(x => x == username))
                {
                    string fullName = userNameMap[username];
                    string[] name = fullName.Split(" ");
                    string extension = ".txt";

                    string transactionFilePath = @userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension;
                    if(File.Exists(transactionFilePath)) 
                    {
                        ShowTransaction(username, name);
                    }
                    else {
                        
                        using (StreamWriter file3 =
                               new StreamWriter(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + extension, true))
                        {

                            file3.WriteLine("--------------Transactions----------");
                        }

                        using (StreamWriter file4 =
                               new StreamWriter(@userPath + "AccountBalance/Balance" + name[0] + name[1] + extension, true))
                        {

                           
                        }


                        Console.WriteLine("Currently, you have no transactions. Go back to the menu to select your options.");
                        MenuUser(username);
                    }


                    //ShowTransaction(username, name);
                }

                else
                {
                    Console.WriteLine("User does not exist. Get back to main menu to create an account.");
                    Menu();
                }
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }  
        //Function to check transaction history ends

        public static void ShowTransaction(string username, string[] name)
        {
            string[] transactions = File.ReadAllLines(@userPath + "AccountTransactions/Transactions" + name[0] + name[1] + ".txt");
                    if (transactions.Length == 0)
                    {
                        Console.WriteLine("Currently, you have no transactions. Go back to the menu to select your options.");
                        MenuUser(username);
                    }
                    else{
                        
                    foreach (string transac in transactions)
                    {
                        Console.WriteLine(transac);
                        //ac.TransactionHistory.Add(trans);

                    }
                    Console.WriteLine("Choose your next action from the options below");
                    //MenuUser(username);

                }
                   
        }

        public static  void Logout(string username)
        {

            Console.WriteLine("Are you sure you want to log out? If yes, type 1. To cancel, type 2");

            string option=Console.ReadLine();


            switch(option)
            {
                case "1":
                    Exit(username);
                    break;
                
                case "2":
                    MenuUser(username);
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    //If invalid option is entered, the user will be logged out and transferred to menu.
                    Exit(username);
                    Menu();
                    break;
            }

            //Remove the user details from the list  
        }

        public static void Exit(string username)
        {
            string path=userPath+"LoggedIn/LoggedInUsers.txt";
            string item = username.Trim();
            //Console.WriteLine(item);
            var lines=File.ReadAllLines(path);
          
            for (int i = 0; i < lines.Count() ; i++)
            {
                if (lines[i].Contains(item) )
                { 
                    lines[i]="";
                }
            }

            File.WriteAllLines(path, lines);   
            loggedIn.Remove(username);
            Environment.Exit(0);
            
        }


        public static void Main(string[] args)
        {
           /* ____Assumptions about the data____
            There can be multiple customers with the same name. 
            A customer can have multiple checking/ savings accounts.
            No checking or savings account can have the same number.*/
            string userPath = "/Users/yoshita.as.sharma/Documents/AltSourceProject/";

            if (!Directory.Exists(@userPath))
            {
                Directory.CreateDirectory(@userPath);
            }

            if (!Directory.Exists(@userPath + "AccountTransactions"))
            {
                Directory.CreateDirectory(@userPath + "AccountTransactions");
            }
           

            if (Directory.Exists(@userPath + "CreatedAccounts"))
            {
                Directory.CreateDirectory(@userPath + "CreatedAccounts");
            }
          

           /* if (!Directory.Exists(@userPath + "CheckingAccountSummary"))
            {
                Directory.CreateDirectory(@userPath + "CheckingAccountSummary");
            }*/
          

            if (!Directory.Exists(@userPath + "LoggedIn"))
            {
                Directory.CreateDirectory(@userPath + "LoggedIn");
            }
          
            if (!Directory.Exists(@userPath + "RegisteredAccounts"))
            {
                
                Directory.CreateDirectory(@userPath + "RegisteredAccounts");
            }


            if (!Directory.Exists(@userPath + "AccountBalance"))
            {
                Directory.CreateDirectory(@userPath + "AccountBalance");
            }

            Menu();

        }

    }
}



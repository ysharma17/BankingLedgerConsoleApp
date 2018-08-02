using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Resources;


namespace AltSourceBankingLedger
{
class Account
    {
        //fields
        
        private string firstName;

        private string lastName;

        private Address address;

        private string username;

        private string password;

        private double accountNumber;

        public enum AccountEnum {
        Checking=1,
        Savings=2
    }

        protected string accountType;

        protected string contactnumber;

        protected string emailid;
        
        protected double balance;
        
        protected double deposit;
        
        protected double withdrawal;

        private List<String> transcations;

        //properties
        [Display(Name = "Name", Description = "First Name + Last Name.")]
        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage =
        "Numbers and special characters are not allowed in the name. Please enter again")]
        [MaxLength(15,ErrorMessage="First Name should not more than 15 characters. Please enter again.")]  
        [MinLength(3,ErrorMessage="First Name should be more than 3 characters.Please enter again.")]  
        public string FirstName
        {
            get{return this.firstName;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "FirstName" });
                this.firstName=value;
            }
        }

        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage =
        "Numbers and special characters are not allowed in the name. Please enter it again")]
        [MaxLength(15,ErrorMessage="Last Name should not more than 15 characters. Please enter in again.")]  
        [MinLength(3,ErrorMessage="Last Name should be more than 3 characters. Please enter it again.")]  
        public string LastName
        {
            get{return this.lastName;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "LastName" });
                this.lastName=value;
            }
        }

        [Required(ErrorMessage = "Username is required.")]     
        [MaxLength(15,ErrorMessage="Username should not more than 15 characters. Please enter it again.")]  
        [MinLength(3,ErrorMessage="Username should be more than 6 characters. Please enter it again.")]    
        [RegularExpression(@"^[a-z0-9'\s]*$", ErrorMessage="Username should consist of only lowercase letters and numbers. Please enter it again.")]
        public string UserName
        {
            get{return this.username;}
            set{
                this.username=value;
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "UserName" });
                this.username=value;
            }
        }

        [Required(ErrorMessage = "Password is required.")]     
        //[MaxLength(15,ErrorMessage="Password should not more than 15 characters. Please enter it again.")]  
        //[MinLength(3,ErrorMessage="Password should be more than 6 characters. Please enter it again.")]   
        [DataType(DataType.Password, ErrorMessage="The password has not been entered correctly. Please enter it again.")] 
        public string PassWord
        {
            get{return this.password;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "PassWord" });
                this.password=value;
            }
        }
        
        [Required(ErrorMessage = "Contact number is required.")]   
        [DataType(DataType.PhoneNumber, ErrorMessage="The contact number has not been entered correctly. Please enter it again.")]
        [MaxLength(10, ErrorMessage="Phone number should be of maximum 10 digits")]
        [PhoneAttribute]
        
        public string ContactNumber
        {
            get{return this.contactnumber;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "ContactNumber" });
                this.contactnumber=value;
            }
        }

        [Required(ErrorMessage = "Email id is required.")]   
        [DataType(DataType.EmailAddress, ErrorMessage="The email address has not been entered correctly. Please enter it again.")]
        [EmailAddress]
        public string EmailId
        {
            get{return this.emailid;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "EmailId" });
                this.emailid=value;
            }
        }
        
        [Required(ErrorMessage = "Account type is required.")]   
        [EnumDataType(typeof(AccountEnum), ErrorMessage="The account type entered is invalid. Please enter again.")]        
        public string AccountType
        { 
            get { return this.accountType; }  
            set {
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "AccountType" });
                this.accountType=value;  
            }
        }

        public Address FullAddress
        {get { return this.address; } }


        [RegularExpression(@"^[0-9][.]\s]*$", ErrorMessage="The amount should only consist of numbers or decimal point. Please enter it again.")]
        public double Withdrawal
        {
            get { return this.withdrawal; }
            set { this.withdrawal = value; }
        }
        
        [RegularExpression(@"^[0-9][.]\s]*$", ErrorMessage="The amount should only consist of numbers or decimal point. Please enter it again.")]
        public double Deposit
        {
            get { return this.deposit; }
            set { this.deposit = value; }
        }
        public double AcctNumber
        { get { return this.accountNumber; } }

        public double Balance
        { get { return this.balance; }
          set { this.balance=value; }
        }

        public List<String> TransactionHistory
         { 
            get {return this.transcations; }
            set {this.transcations=value;}
         }


        //creates random Account Number
        public double AccountNumb()
        {
            Random rand = new Random();
            this.accountNumber = rand.Next(100000000, 1000000000);
            return accountNumber;
        }

        //Computes General Balance(resets values)
        public double GetBalance()
        {
            balance = balance + deposit - withdrawal;
            deposit = 0;
            withdrawal = 0;
            return balance;
        }
        //Computers Balance when withdrawal equals zero
        public double DepositBalance(double input)
        {
            deposit = input;
            withdrawal = 0;
            balance = balance + deposit - withdrawal;
            return balance;
        }

        //Computers balance when deposit equals zero
        public double WithdrawBalance(double input)
        {
            withdrawal = input;
            deposit = 0;
            balance = balance + deposit - withdrawal;
            return balance;
        }

        //client info
        public string ClientInfo()
        {
            string clientinfo = ("Account Holder: " + FirstName + " " + LastName + " "+ FullAddress);
            return clientinfo;
        }
    }
}
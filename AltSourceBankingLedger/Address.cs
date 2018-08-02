using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AltSourceBankingLedger
{
    class Address
    {
        private string street;

        private string city;

        private string state;

        private string zipcode;


        public string Street
        {
            get{return this.street;}
            set{this.street=value;}
        }

        [RegularExpression(@"^[a-zA-Z''-'\s]*$", ErrorMessage =
        "Numbers and special characters are not allowed in the city name. Please enter again")]
        [MaxLength(40,ErrorMessage="State should not more than 40 characters. Please enter it again.")]        
        public string City
        {
            get{return this.city;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "City" });
                this.city=value;
            }
        }

        [Required(ErrorMessage = "State is required.")]     
        [MaxLength(2,ErrorMessage="State should not more than 2 characters. Please enter it again.")]
        [RegularExpression(@"^[A-Z''-'\s]*$", ErrorMessage =
        "Numbers and special characters are not allowed in the state name. Please enter again")]
        public string State
        {
            get{return this.state;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "State" });
                this.state=value;
            }
        }

        [Required(ErrorMessage = " is required.")]     
        [MinLength(5,ErrorMessage="Zip code should not have less than 5 numbers. Please enter it again.")]        
        [MaxLength(10,ErrorMessage="State should not more than 10 numbers. Please enter it again.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Zip code must be numeric")]
        public string ZipCode
        {
            get{return this.zipcode;}
            set{
                Validator.ValidateProperty(value,
                new ValidationContext(this, null, null) { MemberName = "ZipCode" });
                this.zipcode=value;                
            }
        }

       public string FullAddress
        {
            get{return this.Street+" " +this.City+" "+this.State+" "+this.ZipCode;}
        }


    }
}
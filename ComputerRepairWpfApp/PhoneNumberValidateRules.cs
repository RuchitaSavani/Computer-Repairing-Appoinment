using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalWpfApp
{
    /// <summary>
    /// for validate phone number
    /// </summary>
    class PhoneNumberValidateRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double pNumber;


            if (!double.TryParse(value.ToString(), out pNumber))
            {
                return new ValidationResult(false, "Only Digits Allowed!");
            }

            if ((value.ToString().Trim().Length) != 10)
            {
                return new ValidationResult(false, "Enter 10 digits Mobile number");
            }

            if ((value.ToString() == ""))
            {
                return new ValidationResult(false, "Mobile Number is Required!");
            }


            return ValidationResult.ValidResult;
        }
    }
}

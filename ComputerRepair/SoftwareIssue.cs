using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// it is sub class or derived class of laptop which hold the information about customers software issue
    /// </summary>
    public class SoftwareIssue : Laptop
    {
        
        public SoftwareIssue()
        {

        }

        public SoftwareIssue(string fullName, string modelNumber, string itemDescription, string phoneNumber, string issue) : base(fullName, modelNumber, itemDescription, phoneNumber, issue)
        {
        }

        public override string GetItemDescription()
        {
            throw new NotImplementedException();
        }

        public override string GetModelNumber()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalProject
{
    /// <summary>
    /// store the data for customer laptop 
    /// </summary>
    [XmlInclude(typeof(SoftwareIssue))]
    [XmlInclude(typeof(HardwareIssue))]
    [XmlInclude(typeof(ScreenIssue))]
    [XmlInclude(typeof(OtherIssue))]
    [Serializable]
    public abstract class Laptop : ILaptop
    {
        private string fullName;
        private string modelNumber;
        private string itemDescription;
        private string phoneNumber;
        private string issueWithDevice;

        public Laptop()
        {

        }
        protected Laptop(string fullName, string modelNumber, string itemDescription , string phoneNumber , string issue)
        {
            this.fullName = fullName;
            this.modelNumber = modelNumber;
            this.itemDescription = itemDescription;
            this.PhoneNumber = phoneNumber;
            this.issueWithDevice = issue;
        }

        public string FullName { get => fullName; set => fullName = value; }
        public string ModelNumber { get => modelNumber; set => modelNumber = value; }
        public string ItemDescription { get => itemDescription; set => itemDescription = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string IssueWithDevice { get => issueWithDevice; set => issueWithDevice = value; }

        public abstract string GetItemDescription();
        public abstract string GetModelNumber();

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

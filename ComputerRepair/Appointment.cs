using System;
using System.Xml;
using System.Xml.Serialization;

namespace FinalProject
{  
    /// <summary>
    /// keep data store of booked appoinments
    /// </summary>
        public class Appointment :  IDisposable

        {
            private string time;
            private Laptop laptop;
            private string deviceIssue;
            private string warrenty;

            [XmlElement]
            public string Time { get => this.time; set => this.time = value; }
            [XmlElement]
            public Laptop Laptop { get => this.laptop; set => this.laptop = value; }
            [XmlElement]
            public string DeviceIssue { get => this.deviceIssue; set => this.deviceIssue = value; }
        public string Warrenty { get => warrenty; set => warrenty = value; }

        public Appointment()
            {

            }
            public Appointment(Laptop laptop)
            {
                this.laptop = laptop;
            }

            public Appointment(Laptop laptop, string time)
            {
                this.laptop = laptop;
                this.time = time;
            }

           
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
         
        }
     
}
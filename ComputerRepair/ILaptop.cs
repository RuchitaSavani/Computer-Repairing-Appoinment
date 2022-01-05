using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    /// <summary>
    /// interface for laptop class
    /// </summary>
    public interface ILaptop
    {
        
        string ModelNumber { get; set; }
        string ItemDescription { get; set; }
        string FullName { get; set; }
        string PhoneNumber { get; set; }

        string GetModelNumber();

        string GetItemDescription();
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechBank.DomainModel.Models.Admin
{
    public class CustomerCreateVM
    {
        [BindProperty]
        public InputModel Input { get; set; }
    }

    public class CustomerEditVM
    {
        public AppCustomer Customer { get; set; }

        public CustomerEditVM(AppCustomer customer)
        {
            Customer = customer;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; }
        public virtual Company MotherCompany { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        
        public string Email { get; set; }
       
        public string ContactPerson { get; set; }
       
        public string ContactPersonEmail { get; set; }
        
        public string ContactPersonPhone { get; set; }
        public string FiscalYearStart { get; set; }
        public string CompanyLogo { get; set; }
    }
}

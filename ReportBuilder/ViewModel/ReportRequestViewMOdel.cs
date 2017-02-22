using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportBuilder.ViewModel
{
    public class ReportRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
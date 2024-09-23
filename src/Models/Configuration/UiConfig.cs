using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace epicmorg.jira.issue.web.reporter.Models.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class UiConfig
    {
        [Url]
        public string LogoUrl { get; set; }

        [Required]
        public string HeaderText { get; set; }

        [Required]
        public string DescriptionText { get; set; }
          
        [Required]
        public string LicensedTo { get; set; }


        [Required]
        public string Theme { get; set; }

        public string GetAssemblyVersion() { return GetType().Assembly.GetName().Version.ToString();  }

        public string GetAssemblyProduct {
            get {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product.ToString();
            }
        }

    }
}



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CustomReg.Domain;


namespace CustomReg.Models
{
    public class RegistrationViewModel
    {
        public IEnumerable<FieldConfig> Config;

        /// <summary>
        /// default controller needed for model binding
        /// </summary>
        public RegistrationViewModel()
        {
        }
        /// <summary>
        /// this constructor will require a config which is used to determine visibility of fields.
        /// </summary>
        /// <param name="config"></param>
        public RegistrationViewModel(IEnumerable<FieldConfig> config)
        {
            Config = config;
        }

        public bool IsFieldVisible(string fieldName)
        {
            var field = Config.FirstOrDefault(x => x.Name.Equals(fieldName));
            return (field != null && field.IsVisible);
        }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "LoginId is required")]
        public string LoginId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
    }
    
}
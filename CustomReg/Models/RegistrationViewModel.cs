

using System;
using System.Collections.Generic;
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string LoginId { get; set; }

        public string Password { get; set; }
        
    }
    
}
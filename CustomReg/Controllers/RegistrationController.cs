using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using CustomReg.Domain;
using CustomReg.Models;
using CustomReg.Utils.InputOutput;

namespace CustomReg.Controllers
{
    /// <summary>
    /// This is the controller that handles registration stuff
    /// </summary>
    public class RegistrationController : Controller
    {
        private readonly IFileManager _fileManager;
        private string TemplateFilePath => Server.MapPath(ConfigurationManager.AppSettings["FieldTemplateFilePath"]);
        
        public RegistrationController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        // this is the main entry point for the application
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This action loads the registration form configuration.
        /// Fields are visible based on the configuration specified on the config page
        /// </summary>
        /// <returns></returns>
        public ActionResult FillForm()
        {
            //read config from json file
            var config = _fileManager.LoadContentAs<IEnumerable<FieldConfig>>(TemplateFilePath);

            var model = new RegistrationViewModel(config);

            return View(model);
        }

        /// <summary>
        /// This action handles the form submissions.
        /// It starts by loding the config from json file and then loops through all fields that are visible
        /// then retrieves their values from the form model and creates and dictionary of field names and values
        /// then send the resulting collection to the view to display values entered.
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        /// ///TODO: we can also overwrite the Required attribute with our config by removing errors for fields whose Required is set to false in config
        [HttpPost]
        public ActionResult SubmitRegistration(RegistrationViewModel customerModel)
        {
            var config = _fileManager.LoadContentAs<IEnumerable<FieldConfig>>(TemplateFilePath);
            var resultingFields = new Dictionary<string, string>();

            foreach (var field in config)
            {
                //remove errors for any field that is not visible
                if (!field.IsVisible && ModelState[field.Name] != null && ModelState[field.Name].Errors.Any())
                {
                    ModelState[field.Name].Errors.Clear();
                    continue;
                }
                
               
                var property = customerModel.GetType().GetProperty(field.Name);
                var value = string.Empty;

                if (field.IsVisible && property != null)
                {

                if (property?.PropertyType.Name == "String")
                {
                    value = (string)(property?.GetValue(customerModel, null));
                }
                else if (property?.PropertyType.Name == "DateTime")
                {
                    value = ((DateTime)(property?.GetValue(customerModel, null))).ToString("d");
                }
                    resultingFields.Add(field.Name, value);
                }
            }

            //if model is still not valid, we need to not continue
            if (!ModelState.IsValid)
            {
                customerModel.Config = config;
                return View("FillForm", customerModel);
            }   
            
          
            //display result
            return View("ThankYou", resultingFields);
        }

       
    }
}
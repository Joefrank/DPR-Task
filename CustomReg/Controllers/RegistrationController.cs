using System;
using System.Collections.Generic;
using System.Configuration;
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
        [HttpPost]
        public ActionResult SubmitRegistration(RegistrationViewModel customerModel)
        {
            ModelState.AddModelError("FirstName", "You need to provide first name");
            var config = _fileManager.LoadContentAs<IEnumerable<FieldConfig>>(TemplateFilePath);
            customerModel.Config = config;
            return View("FillForm", customerModel);

            //validate input based on config
           
            var resultingFields = new Dictionary<string, string>();

            //register customer
            foreach (var field in config)
            {
                var property = customerModel.GetType().GetProperty(field.Name);
                var value = string.Empty;

                if (property?.PropertyType.Name == "String")
                {
                    value = (string)(property?.GetValue(customerModel, null));
                }
                else if (property?.PropertyType.Name == "DateTime")
                {
                    value = ((DateTime)(property?.GetValue(customerModel, null))).ToString("d");
                }
                
                if (field.IsVisible && property != null)
                {
                    resultingFields.Add(field.Name,value);
                }
                
            }
            //display result

            return View("ThankYou", resultingFields);
        }

       
    }
}
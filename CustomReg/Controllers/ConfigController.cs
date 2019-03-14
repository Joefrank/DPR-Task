using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using CustomReg.Domain;
using CustomReg.Utils.InputOutput;

namespace CustomReg.Controllers
{
    /// <summary>
    /// This controller is used to handle the configuration of fields that are on the registration form
    /// Any field set to visible will be displayed on the form and vice versa
    /// </summary>
    public class ConfigController : Controller
    {
        private readonly IFileManager _fileManager;
        private string TemplateFilePath => Server.MapPath(ConfigurationManager.AppSettings["FieldTemplateFilePath"]);

        public ConfigController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        /// <summary>
        ///   This is the action for the landing page of the configuration
        /// It simply loads the configuration from a json file and displays option on the screen
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var templates = _fileManager.LoadContentAs<IEnumerable<FieldConfig>>(TemplateFilePath);

            return View(templates);
        }

        /// <summary>
        /// This action persis the config that comes from client side into a json file
        /// When successful, it takes user to confirmation page if not it keeps user on current page.
        /// </summary>
        /// <param name="templateModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveConfig(IEnumerable<FieldConfig> templateModel)
        {
            var result = _fileManager.PersistContent(templateModel, TemplateFilePath);

            if (!result)
            {
                return RedirectToAction("Index");
            }

            return View("ConfigSaved");
        }

    }
}
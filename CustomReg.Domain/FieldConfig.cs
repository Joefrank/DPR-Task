
namespace CustomReg.Domain
{
    /// <summary>
    /// class is used to encapsulate configuration of form fields
    /// </summary>
    public class FieldConfig
    {
        public string Name { get; set; }

        public bool IsVisible { get; set; }

        public bool Required { get; set; }
    }
}

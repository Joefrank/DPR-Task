
using Autofac;
using CustomReg.Utils.InputOutput;


namespace CustomReg
{
    /// <summary>
    /// IN this file we are register all our dependencies
    /// </summary>
    public class DataModule : Module
    {  
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonFileManager>().As<IFileManager>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
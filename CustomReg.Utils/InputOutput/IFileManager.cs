
namespace CustomReg.Utils.InputOutput
{
    /// <summary>
    /// Interface used to define File input output contract
    /// </summary>
    public interface IFileManager
    {
        T LoadContentAs<T>(string filePath);

        bool PersistContent(object objSerial, string filePath);
    }
}

namespace EntityFrameworkSP_Demo.Helpers.FileHelpers
{
    public interface IFileHelper
    {
        string Upload(IFormFile file, string root);
        void Delete(string filePath);
        string Update(IFormFile file, string root, string filePath);
    }
}

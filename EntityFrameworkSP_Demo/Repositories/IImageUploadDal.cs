using EntityFrameworkSP_Demo.Entities;
using System.Linq.Expressions;

namespace EntityFrameworkSP_Demo.Repositories
{
    public interface IImageUploadDal
    {
        void Add(ImageUpload imageUpload);
        void Delete(int id);
        void Update(ImageUpload imageUpload);
        List<ImageUpload> GetAll(Expression<Func<ImageUpload, bool>> filter = null);
        ImageUpload Get(Expression<Func<ImageUpload, bool>> filter = null);
    }
}

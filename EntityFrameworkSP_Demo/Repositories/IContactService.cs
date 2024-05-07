using EntityFrameworkSP_Demo.Entities;

namespace EntityFrameworkSP_Demo.Repositories
{
    public interface IContactService
    {
        public Task<List<Contact>> GetContactListAsync();
        public Task<IEnumerable<Contact>> GetContactByIdAsync(Guid Id);
        public Task<int> AddContactAsync(Contact contact);  
        public Task<int> UpdateContactAsync(Guid Id, Contact contact);
        public Task<int> DeleteContactAsync(Guid Id);
    }
}

using EntityFrameworkSP_Demo.Data;
using EntityFrameworkSP_Demo.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkSP_Demo.Repositories
{
    public class ContactService : IContactService
    {
        private readonly DbContextClass _dbContext;

        public ContactService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Contact>> GetContactListAsync()
        {
            return await _dbContext.Contact
                .FromSqlRaw<Contact>("GetContactList")
                .ToListAsync();

        }

        public async Task<IEnumerable<Contact>> GetContactByIdAsync(Guid Id)
        {
            var param = new SqlParameter("@Id", Id);

            var contactDetails = await Task.Run(() => _dbContext.Contact
                            .FromSqlRaw(@"exec GetContactByID @Id", param).ToListAsync());

            return  contactDetails;
        }

        public async Task<int> AddContactAsync(Contact contact)
        {
            // Generate a unique identifier (GUID) for the contact
            contact.Id = Guid.NewGuid();

            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Id", contact.Id));
            parameter.Add(new SqlParameter("@Name", contact.Name));
            parameter.Add(new SqlParameter("@Email", contact.Email));
            parameter.Add(new SqlParameter("@Phone", contact.Phone));
            parameter.Add(new SqlParameter("@Address", contact.Address));
            parameter.Add(new SqlParameter("@Subject", contact.Subject));
            parameter.Add(new SqlParameter("@RecordCreatedOn", contact.RecordCreatedOn));
            parameter.Add(new SqlParameter("@LastUpdatedDate ", contact.LastUpdatedDate));

            var result = await Task.Run(() => _dbContext.Database.ExecuteSqlRawAsync
            (@"exec AddNewContact @Id, @Name, @Email, @Phone, @Address, @Subject, @RecordCreatedOn, @LastUpdatedDate", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateContactAsync(Guid Id, Contact contact)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Id", Id));
            parameter.Add(new SqlParameter("@Name", contact.Name));
            parameter.Add(new SqlParameter("@Email", contact.Email));
            parameter.Add(new SqlParameter("@Phone", contact.Phone));
            parameter.Add(new SqlParameter("@Address", contact.Address));
            parameter.Add(new SqlParameter("@Subject", contact.Subject));
            parameter.Add(new SqlParameter("@RecordCreatedOn", contact.RecordCreatedOn));
            parameter.Add(new SqlParameter("@LastUpdatedDate ", contact.LastUpdatedDate));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync
           (@"exec UpdateContact @Id,  @Name, @Email, @Phone, @Address, @Subject, @RecordCreatedOn, @LastUpdatedDate ", parameter.ToArray()));

            return result;
        }


        public async Task<int> DeleteContactAsync(Guid Id)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteContactByID {Id}"));
        }
    }
}

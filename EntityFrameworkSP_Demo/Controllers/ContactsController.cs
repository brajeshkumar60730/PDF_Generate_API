using EntityFrameworkSP_Demo.Entities;
using EntityFrameworkSP_Demo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkSP_Demo.Controllers
{
    [Authorize] // Secures all endpoints in this controller
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService contactService;
        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }
        [HttpGet("getcontactlist")]
        public async Task<List<Contact>> GetContactListAsync()
        {
            try
            {
                 return await contactService.GetContactListAsync();
            }
            catch 
            {
                throw;
            }
        }
        [HttpGet("getcontactbyid")]
        public async Task<IEnumerable<Contact>> GetContactByIdAsync(Guid Id)
        {
            try
            {
                var response = await contactService.GetContactByIdAsync(Id);
                if (response == null)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
        [HttpPost("addcontact")]
        public async Task<IActionResult> AddContactAsync(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await contactService.AddContactAsync(contact);
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }
        [HttpPut("updatecontact")]
        public async Task<IActionResult> UpdateContactAsync( Guid Id, Contact contact)
        {
            
            
            try
            {
                var result = await contactService.UpdateContactAsync(Id,contact);
                return Ok(result);
                
            }
            catch
            {
                throw;
            }
        }
        [HttpDelete("deletecontact")]
        public async Task<int> DeleteContactAsync(Guid Id)
        {
            try
            {
                var response = await contactService.DeleteContactAsync(Id);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}

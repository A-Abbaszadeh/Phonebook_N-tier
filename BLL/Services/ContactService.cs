using BLL.Dto;
using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ContactService
    {
        DatabaseContext context = new DatabaseContext();

        /// <summary>
        /// دریافت لیست مخاطبین
        /// </summary>
        /// <returns></returns>
        public List<ContactListDto> GetContactLists()
        {
            var contacts = context.Contacts.Select(c => new ContactListDto
            {
                Id = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                PhoneNumber = c.PhoneNumber
            }).ToList();
            return contacts;
        }

        /// <summary>
        /// جستجو در لیست مخاطبین
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns></returns>
        public List<ContactListDto> SearchContact(string SearchKey)
        {
            var contactQuery = context.Contacts.AsQueryable();
            if (!string.IsNullOrEmpty(SearchKey))
            {
                contactQuery = contactQuery.Where(
                    c =>
                    c.FirstName.Contains(SearchKey)
                    ||
                    c.LastName.Contains(SearchKey)
                    ||
                    c.PhoneNumber.Contains(SearchKey)
                    ||
                    c.Company.Contains(SearchKey)
                    );
            }

            var searchResult = contactQuery.Select(c => new ContactListDto
            {
                Id = c.Id,
                FullName = $"{c.FirstName} {c.LastName}",
                PhoneNumber = c.PhoneNumber
            }).ToList();

            return searchResult;
        }
    }
}

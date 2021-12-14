using BLL.Dto;
using DAL.Contexts;
using DAL.Entities;
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

        /// <summary>
        /// حذف مخاطب
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultDto DeleteContact(int Id)
        {
            var contact = context.Contacts.Find(Id);
            if (contact != null)
            {
                context.Remove(contact);
                context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = $"{contact.FirstName} {contact.LastName} با موفقیت از لیست مخاطبین حذف گردید."
                };
            }

            return new ResultDto
            {
                IsSuccess = false,
                Message = "مخاطب مورد نظر یافت نشد"
            };
        }

        /// <summary>
        /// افزودن مخاطب جدید
        /// </summary>
        /// <param name="newContact"></param>
        /// <returns></returns>
        public ResultDto AddContact(AddContactDto newContact)
        {
            if (string.IsNullOrEmpty(newContact.PhoneNumber))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "شماره تلفن را وارد نمائید"
                };
            }
            Contact contact = new Contact()
            {
                FirstName = newContact.FirstName,
                LastName = newContact.LastName,
                PhoneNumber = newContact.PhoneNumber,
                Company = newContact.Company,
                Description = newContact.Description,
            };

            context.Contacts.Add(contact);
            context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = $"{contact.FirstName} {contact.LastName} با موفقیت ثبت گردید"
            };
        }

        /// <summary>
        /// نمایش کامل اطلاعات مخاطب
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ResultDto<GetContactDetailDto> GetContactDetail(int Id)
        {
            var contact = context.Contacts.Find(Id);

            if (contact == null)
            {
                return new ResultDto<GetContactDetailDto>()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "مخاطب مورد نظر یافت نشد"
                };
            }

            var data = new GetContactDetailDto()
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Company = contact.Company,
                PhoneNumber = contact.PhoneNumber,
                Description = contact.Description,
                CreatedAt = contact.CreatedAt,
            };

            return new ResultDto<GetContactDetailDto>()
            {
                Data = data,
                IsSuccess = true
            };
        }

        /// <summary>
        /// ویرایش مخاطب
        /// </summary>
        /// <param name="editContact"></param>
        /// <returns></returns>
        public ResultDto EditContact(EditContactDto editContact)
        {
            var contact = context.Contacts.Find(editContact.Id);

            if (context == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مخاطب مورد نظر یافت نشد"
                };
            }

            contact.FirstName = editContact.FirstName;
            contact.LastName = editContact.LastName;
            contact.Company = editContact.Company;
            contact.PhoneNumber = editContact.PhoneNumber;
            contact.Description = editContact.Description;
            context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"اطلاعات {contact.FirstName} {contact.LastName} با موفقیت به ویرایش شد."
            };
        }
    }
}

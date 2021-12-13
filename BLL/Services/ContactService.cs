﻿using BLL.Dto;
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
    }
}

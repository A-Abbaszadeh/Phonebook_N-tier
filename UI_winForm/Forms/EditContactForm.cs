using BLL.Dto;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_winForm.Forms
{
    public partial class EditContactForm : Form
    {
        private readonly ContactService _contactService;
        private readonly int ContactId;
        public EditContactForm(int contactId)
        {
            InitializeComponent();
            _contactService = new ContactService();
            ContactId = contactId;
        }

        private void EditContactForm_Load(object sender, EventArgs e)
        {
            var contact = _contactService.GetContactDetail(ContactId);
            if (contact.IsSuccess)
            {
                firstNameTextBox.Text = contact.Data.FirstName;
                lastNameTextBox.Text = contact.Data.LastName;
                companyTextBox.Text = contact.Data.Company;
                phoneNumberTextBox.Text = contact.Data.PhoneNumber;
                descriptionTextBox.Text = contact.Data.Description;
            }
            else
            {
                MessageBox.Show(contact.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void editContactButton_Click(object sender, EventArgs e)
        {
            var editContactResult = _contactService.EditContact(new EditContactDto
            {
                Id = ContactId,
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Company = companyTextBox.Text,
                PhoneNumber = phoneNumberTextBox.Text,
                Description = descriptionTextBox.Text
            });
            if (editContactResult.IsSuccess)
            {
                MessageBox.Show(editContactResult.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(editContactResult.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

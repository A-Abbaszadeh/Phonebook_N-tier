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
    public partial class AddContactForm : Form
    {
        private readonly ContactService _contactService;
        public AddContactForm()
        {
            InitializeComponent();
            _contactService = new ContactService();
        }

        private void addContactButton_Click(object sender, EventArgs e)
        {
            var addContactResult = _contactService.AddContact(new AddContactDto
            {
                FirstName = firstNameTextBox.Text,
                LastName = lastNameTextBox.Text,
                Company = companyTextBox.Text,
                PhoneNumber = phoneNumberTextBox.Text,
                Description = descriptionTextBox.Text,
            });
            if (addContactResult.IsSuccess)
            {
                MessageBox.Show(addContactResult.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(addContactResult.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

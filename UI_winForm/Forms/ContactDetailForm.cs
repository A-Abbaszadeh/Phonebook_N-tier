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
    public partial class ContactDetailForm : Form
    {
        private readonly ContactService _contactService;
        private readonly int contactId;
        public ContactDetailForm(int contactId)
        {
            InitializeComponent();
            _contactService = new ContactService();
            this.contactId = contactId;
        }

        private void ContactDetailForm_Load(object sender, EventArgs e)
        {
            var contact = _contactService.GetContactDetail(contactId);
            if (contact.IsSuccess)
            {
                idLabel.Text = contact.Data.Id.ToString();
                firstNameLabel.Text = contact.Data.FirstName;
                lastNameLabel.Text = contact.Data.LastName;
                companyLabel.Text = contact.Data.Company;
                phoneNumberLabel.Text = contact.Data.PhoneNumber;
                createdAtLabel.Text = contact.Data.CreatedAt.ToString();
                descriptionTextBox.Text = contact.Data.Description;
            }
            else
            {
                MessageBox.Show(contact.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

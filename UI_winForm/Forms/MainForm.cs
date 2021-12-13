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
    public partial class MainForm : Form
    {
        private readonly ContactService _contactService;
        public MainForm()
        {
            InitializeComponent();
            _contactService = new ContactService();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            var contactList = _contactService.GetContactLists();
            contactsGridView.DataSource = contactList;

            contactsGridView.Columns[0].HeaderText = "شناسه";
            contactsGridView.Columns[1].HeaderText = "نام و نام خانوادگی";
            contactsGridView.Columns[2].HeaderText = "شماره تماس";

            contactsGridView.Columns[0].Width = 80;
            contactsGridView.Columns[1].Width = 200;
            contactsGridView.Columns[2].Width = 150;

            this.Cursor = Cursors.Default;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            var contactList = _contactService.SearchContact(searchTextBox.Text);
            contactsGridView.DataSource = contactList;

            this.Cursor = Cursors.Default;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int Id = int.Parse(contactsGridView.CurrentRow.Cells[0].Value.ToString());
            var deleteResult = _contactService.DeleteContact(Id);
            if (deleteResult.IsSuccess)
            {
                MessageBox.Show(deleteResult.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MainForm_Load(null, null);
            }
            else
            {
                MessageBox.Show(deleteResult.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void showAddContactFormButton_Click(object sender, EventArgs e)
        {
            AddContactForm addContactForm = new AddContactForm();
            addContactForm.ShowDialog();
            MainForm_Load(null, null);
        }

        private void showContactDetailFormButton_Click(object sender, EventArgs e)
        {
            ShowContactDetail();
        }


        private void contactsGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowContactDetail();
        }

        private void ShowContactDetail()
        {
            int Id = int.Parse(contactsGridView.CurrentRow.Cells[0].Value.ToString());

            ContactDetailForm contactDetailForm = new ContactDetailForm(Id);
            contactDetailForm.ShowDialog();
        }
    }
}

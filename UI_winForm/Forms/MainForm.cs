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
    }
}

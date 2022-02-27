using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
//using Contact.conClass;

namespace Contact
{
    public partial class Contact : Form
    {
        public Contact()
        {
            InitializeComponent();
        }

        conClass c = new conClass();

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if(success == true)
            {
                MessageBox.Show("New Contact Successfully Inserted");
                clear();
            }
            else
            {
                MessageBox.Show("Failed New Add Contact. Try Again !!");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactId = int.Parse(txtboxContactId.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNo.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact has been Successfully Updated");
                clear();
            }
            else
            {
                MessageBox.Show("Contact has not Updated !!!");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void dgvContactList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNo.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactId.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtboxContactId.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactId = int.Parse(txtboxContactId.Text);

            bool success = c.Delete(c);
            if (success == true)
            {
                MessageBox.Show("Contact Successfully Deleted");
                clear();
            }
            else
            {
                MessageBox.Show("Contact Not Deleted !!!");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxSearch.Text;
            string sqlconn = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
            SqlConnection conn = new SqlConnection(sqlconn);

            string sql = "select * from tbl_contact where ContactId like '%" + keyword + "%' or FirstName like '%" + keyword + "%' or LastName like '%" + keyword + "%' or ContactNo like '%" + keyword + "%' or Addres like '%" + keyword + "%' or Gender like '%" + keyword + "%' ";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}

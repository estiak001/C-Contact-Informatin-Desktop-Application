using Econtact.econtactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Econtact_TextChanged(object sender, EventArgs e)
        {

        }
        contactClass c = new contactClass();
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if(success == true)
            {
                MessageBox.Show("New contact successfully Inserted");
                clear();
            }
            else
            {
                MessageBox.Show("Faild to add new contact");
            }
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void clear()
        {
            textBoxContactId.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxContactNo.Text = "";
            textBoxAddress.Text = "";
            cmbGender.Text = "";
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            c.ContactId = int.Parse(textBoxContactId.Text);
            c.FirstName = textBoxFirstName.Text;
            c.LastName = textBoxLastName.Text;
            c.ContactNo = textBoxContactNo.Text;
            c.Address = textBoxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Update(c);

            if (success == true)
            {
                MessageBox.Show("Contact has been successfully Updated !");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                clear();
            }
            else
            {
                MessageBox.Show("Faild to Update. Tray again!");
            }
        }

        private void DgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            textBoxContactId.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxContactNo.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            c.ContactId = Convert.ToInt32(textBoxContactId.Text);
            bool success = c.Delet(c);
            if (success == true)
            {
                MessageBox.Show("Contact delet successfully !");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                clear();
            }
            else
            {
                MessageBox.Show("Faild to delet!");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblContact WHERE FirstName LIKE  '%"+keyword+"%' OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword+"%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Lab1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.Clients' table. You can move, or remove it, as needed.
            this.clientsTableAdapter.Fill(this.dataSet1.Clients);
            // TODO: This line of code loads data into the 'dataSet1.Item' table. You can move, or remove it, as needed.
            this.itemTableAdapter.Fill(this.dataSet1.Item);
            // TODO: This line of code loads data into the 'dataSet1.Pawnshop' table. You can move, or remove it, as needed.
            this.pawnshopTableAdapter.Fill(this.dataSet1.Pawnshop);

        }

        private void PawnshopBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.pawnshopBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pawnshopBindingSource.AddNew();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            pawnshopBindingSource.EndEdit();
            pawnshopTableAdapter.Update(dataSet1);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            itemBindingSource.AddNew();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            itemBindingSource.EndEdit();
            itemTableAdapter.Update(dataSet1);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            clientsBindingSource.AddNew();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            clientsBindingSource.EndEdit();
            clientsTableAdapter.Update(dataSet1);
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT IdClients, SUM(Price) FROM PawnShop GROUP BY IdClients", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dt.Columns["Column1"].ColumnName = "Price Spent";
            dt.Columns["IdClients"].ColumnName = "Clients Id";

            string header = (sender as Button).Text;

            ExportToPDF(dataGridView1, header);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Item.Title, PawnShop.Description, PawnShop.Price FROM Item INNER JOIN PawnShop ON Item.Id = PawnShop.IdItem", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

            string header = (sender as Button).Text;

            ExportToPDF(dataGridView2, header);
        }
        private void ExportToPDF(DataGridView dataGridView, string header)
        {
            if (dataGridView.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = "Result.pdf";

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {

                    if (File.Exists(save.FileName))
                    {
                        try
                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)
                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)
                    {
                        try
                        {

                            PdfPTable pTable = new PdfPTable(dataGridView.Columns.Count);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn col in dataGridView.Columns)
                            {

                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));

                                pTable.AddCell(pCell);

                            }

                            foreach (DataGridViewRow viewRow in dataGridView.Rows)
                            {

                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {

                                    pTable.AddCell(dcell.Value.ToString());

                                }

                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {

                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);

                                PdfWriter.GetInstance(document, fileStream);
                                Paragraph paragraph = new Paragraph(header +  ":");
                                Paragraph paragraph1 = new Paragraph(" ");
                                document.Open();
                                document.Add(paragraph);
                                document.Add(paragraph1);
                                document.Add(pTable);

                                document.Close();

                                fileStream.Close();

                            }

                            MessageBox.Show("Data Export Successfully", "info");

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Error while exporting Data" + ex.Message);

                        }

                    }

                }

            }
            else
            {

                MessageBox.Show("No Record Found", "Info");

            }

        }
        private void Button9_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(Id) FROM Clients", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            dt.Columns["Column1"].ColumnName = "Clients";
            string header = (sender as Button).Text;

            ExportToPDF(dataGridView3, header);
        }
        private void BindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
            this.Validate();
            this.itemBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);

        }

        private void BindingNavigatorCountItem2_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.clientsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet1);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT MIN(Price) FROM PawnShop", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Columns["Column1"].ColumnName = "Minimal Price";
            dataGridView4.DataSource = dt;

            string header = (sender as Button).Text;

            ExportToPDF(dataGridView4, header);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from PawnShop where Id=@Id", con);
                cmd.Parameters.AddWithValue("Id", int.Parse(textBox1.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");                
            }                      
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {

        }

        private void Button18_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Clients where SurName=@SurName", con);
                cmd.Parameters.AddWithValue("SurName", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Item where Title=@Title", con);
                cmd.Parameters.AddWithValue("Title", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Item where Note=@Note", con);
                cmd.Parameters.AddWithValue("Note", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button12_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from PawnShop where Price>@Price", con);
                cmd.Parameters.AddWithValue("Price", int.Parse(textBox1.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from PawnShop where Due=@Due", con);
                cmd.Parameters.AddWithValue("Due", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Clients where MiddleName=@MiddleName", con);
                cmd.Parameters.AddWithValue("MiddleName", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from PawnShop where Price<@Price", con);
                cmd.Parameters.AddWithValue("Price", int.Parse(textBox1.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from PawnShop where Date=@Date", con);
                cmd.Parameters.AddWithValue("Date", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=DPZKF\SQLEXPRESS;Initial Catalog=Lab1; Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * from Clients where Name=@Name", con);
                cmd.Parameters.AddWithValue("Name", textBox1.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Succsefully");
                    string header = (sender as Button).Text + " " + textBox1.Text;

                    ExportToPDF(dataGridView5, header);
                }
                else
                    MessageBox.Show("Not found");
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex);
            }
        }

    }
}

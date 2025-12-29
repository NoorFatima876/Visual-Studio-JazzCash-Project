using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace JazzCashDemo
{
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
        }

        // GAS BILL
        private void btnGas_Click(object sender, EventArgs e)
        {
            PayBill("Gas Bill");
        }

        // WATER BILL
        private void btnWater_Click(object sender, EventArgs e)
        {
            PayBill("Water Bill");
        }

        // ELECTRICITY BILL
        private void btnElectricity_Click(object sender, EventArgs e)
        {
            PayBill("Electricity Bill");
        }

        // COMMON BILL PAYMENT METHOD
        private void PayBill(string billType)
        {
            if (txtConsumerNo.Text == "" || txtAmount.Text == "")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            decimal amount = Convert.ToDecimal(txtAmount.Text);

            MySqlConnection con = DB.GetConnection();
            con.Open();

            // 1️⃣ Deduct Balance
            string deduct = "UPDATE users SET balance = balance - @a WHERE phone = @p";
            MySqlCommand cmd1 = new MySqlCommand(deduct, con);
            cmd1.Parameters.AddWithValue("@a", amount);
            cmd1.Parameters.AddWithValue("@p", LoginForm.LoggedUserPhone);
            cmd1.ExecuteNonQuery();

            // 2️⃣ Save Transaction
            string insert = "INSERT INTO transactions(user_phone, type, details, amount) " +
                            "VALUES(@p, @t, @d, @a)";
            MySqlCommand cmd2 = new MySqlCommand(insert, con);
            cmd2.Parameters.AddWithValue("@p", LoginForm.LoggedUserPhone);
            cmd2.Parameters.AddWithValue("@t", billType);
            cmd2.Parameters.AddWithValue("@d", txtConsumerNo.Text);
            cmd2.Parameters.AddWithValue("@a", amount);
            cmd2.ExecuteNonQuery();

            con.Close();

            MessageBox.Show(billType + " Paid Successfully!", 
                            "JazzCash", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtConsumerNo.Clear();
            txtAmount.Clear();
        }
    }
}

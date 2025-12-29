private void btnLoad_Click(object sender, EventArgs e)
{
    decimal amount = Convert.ToDecimal(txtAmount.Text);
    var con = DB.GetConnection();
    con.Open();

    string deduct = "UPDATE users SET balance = balance - @a WHERE phone=@p";
    MySqlCommand cmd1 = new MySqlCommand(deduct, con);
    cmd1.Parameters.AddWithValue("@a", amount);
    cmd1.Parameters.AddWithValue("@p", LoginForm.LoggedUserPhone);
    cmd1.ExecuteNonQuery();

    string insert = "INSERT INTO transactions(user_phone,type,details,amount) " +
                    "VALUES(@p,'Mobile Load',@d,@a)";
    MySqlCommand cmd2 = new MySqlCommand(insert, con);
    cmd2.Parameters.AddWithValue("@p", LoginForm.LoggedUserPhone);
    cmd2.Parameters.AddWithValue("@d", txtNumber.Text);
    cmd2.Parameters.AddWithValue("@a", amount);
    cmd2.ExecuteNonQuery();

    con.Close();
    MessageBox.Show("Mobile Load Successful!");
}

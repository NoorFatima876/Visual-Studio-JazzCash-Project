public static string LoggedUserPhone;

private void btnLogin_Click(object sender, EventArgs e)
{
    var con = DB.GetConnection();
    con.Open();

    string query = "SELECT * FROM users WHERE phone=@p AND password=@pw";
    MySqlCommand cmd = new MySqlCommand(query, con);

    cmd.Parameters.AddWithValue("@p", txtPhone.Text);
    cmd.Parameters.AddWithValue("@pw", txtPassword.Text);

    MySqlDataReader dr = cmd.ExecuteReader();

    if (dr.Read())
    {
        LoggedUserPhone = txtPhone.Text;
        new DashboardForm().Show();
        this.Hide();
    }
    else
    {
        MessageBox.Show("Invalid Login");
    }
    con.Close();
}

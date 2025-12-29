private void btnRegister_Click(object sender, EventArgs e)
{
    var con = DB.GetConnection();
    con.Open();

    string query = "INSERT INTO users(name, phone, password) VALUES(@n,@p,@pw)";
    MySqlCommand cmd = new MySqlCommand(query, con);

    cmd.Parameters.AddWithValue("@n", txtName.Text);
    cmd.Parameters.AddWithValue("@p", txtPhone.Text);
    cmd.Parameters.AddWithValue("@pw", txtPassword.Text);

    cmd.ExecuteNonQuery();
    con.Close();

    MessageBox.Show("Registered Successfully!");
}

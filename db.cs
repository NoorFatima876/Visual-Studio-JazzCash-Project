using MySql.Data.MySqlClient;

namespace JazzCashDemo
{
    class DB
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(
                "server=localhost;database=jazzcash_db;user=root;password=;"
            );
        }
    }
}

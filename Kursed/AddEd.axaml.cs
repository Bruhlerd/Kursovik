using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace Bulkaa;

public partial class AddEd : Window
{
    private List<Orders> Orders;
    private Orders CurrenOrder;
    public AddEd(Orders currentOrder, List<Orders> orders)
    {
        InitializeComponent();
        CurrenOrder = currentOrder;
        this.DataContext = currentOrder;
        Orders = orders;
    }
    
    private MySqlConnection conn;
    string connStr = "server=localhost;database=schema_name;port=3306;User Id=root;password=Pass&_word1";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Orders.FirstOrDefault(x => x.ID == CurrenOrder.ID);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO orders VALUES (" + Convert.ToInt32(id.Text) + ", '" + cl_id.Text + "', '" + av_id.Text + "',  '" + ad_t.Text + "', '" + date.Text + "', '" + price.Text + "');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE orders SET client_id = '" + cl_id.Text + "', avto_id = '" +  av_id.Text + "',  adres_to = '" + ad_t.Text +"', date = '" + date.Text + "', price = " + price.Text + " WHERE ID = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Table back = new Table();
        this.Close();
        back.Show(); 
    }
}
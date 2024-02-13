using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;

namespace Bulkaa;

public partial class Table : Window
{
    public Table()
    {
        InitializeComponent();
    ShowTable(fullTable);
        FillStatus();
    }
    string fullTable = "SELECT orders.ID, orders.client_id, orders.avto_id,  orders.adres_to, orders.date, orders.price FROM orders JOIN client on orders.client_id = client.Id ";

    private List<Orders> orders;
    private List<Avto> stats;
    private List<Client> clin;
    string connStr = "server=localhost;database=schema_name;port=3306;User Id=root;password=Pass&_word1";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        orders = new List<Orders>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var ord = new Orders()
            {
                ID = reader.GetInt32("ID"),
                client_id = reader.GetInt32("client_id"),
                avto_id = reader.GetInt32("avto_id"),
               
                adres_to = reader.GetString("adres_to"),
                date = reader.GetString("date"),
                price = reader.GetInt32("price"),
            };
            orders.Add(ord);
        }
        conn.Close();
        DataGrid.ItemsSource = orders;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = orders;
        gds = gds.Where(x => x.date.Contains(Search_Goods.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }
    
    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            Orders usr = DataGrid.SelectedItem as Orders;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM orders WHERE ID = " + usr.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            orders.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        Orders ord = new Orders();
        AddEd add = new AddEd(ord, orders);
        add.Show();
        this.Close();
    }
    
    private void Edit(object? sender, RoutedEventArgs e)
    {
        Orders currenOrder = DataGrid.SelectedItem as Orders;
        if (currenOrder == null)
            return;
        AddEd edit = new AddEd(currenOrder, orders);
        edit.Show();
        this.Close();
    }
    
    private void CmbStatus(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as Client;
        var filteredUsers = orders
            .Where(x => x.client_id == currentGender.Id)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }
    
    public void FillStatus()
    {
        clin = new List<Client>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from client", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new Client()
            {
                Id = reader.GetInt32("Id"),
                FIO = reader.GetString("FIO"),
                telephone = reader.GetInt32("telephone")

            };
            clin.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = clin;
    }
}
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Bulkaa;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    string connectionString = "server=localhost; database=schema_name; port=3306; User Id=root;password=Pass&_word1";

    public void Vxod(object? sender, RoutedEventArgs e)
    {
        if (Login.Text == "user" && Password.Text == "pass")
        {
            Table tbl = new Table();
            this.Hide();
            tbl.Show();
        }
    }
    
    
    public void Exit_PR(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}
namespace Bulkaa;

public class Avto   
{
    public int ID { get; set; }
    public string mark { get; set; }
    public int tonnage { get; set; }
    
}

public class Client 
{
    public int Id { get; set; }
    public string FIO { get; set; }
    public int telephone { get; set; }
}

public class Orders
{
    public int ID { get; set;}
    public int client_id { get; set; }
    public int avto_id { get; set; }
    public string adres_to { get; set; }
    public string date { get; set; }
    public int price { get; set; }
    
}

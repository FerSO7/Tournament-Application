public class Tournament 
{   
    public string Id { get; set; }
    public string Date { get; set; }

    public Tournament(string id, string date)
    {
        Id = id;
        Date = date;
    }
}

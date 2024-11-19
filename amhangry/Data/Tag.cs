public class Tag
{
    public int Id { get; set; }
    public List<Recipe> Recipe {get;}=new()!;
    public string Name { get; set; }

    public Tag(string name)
    {
        Name = name;
    }

    private Tag(){
        Name = "";
    }
}
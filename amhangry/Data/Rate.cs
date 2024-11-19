public class Rate
{
    public int RateId { get; set; }
    public int RecipeId {get;set;}
    public Recipe Recipe {get;set;}=null!;


    public double Value { get; set; }
    public string RatedBy { get; set; }

    public Rate(string ratedBy,double value)
    {
        RatedBy = ratedBy;
        Value = value;
    }

    private Rate(){
        RatedBy = "";
    }
}

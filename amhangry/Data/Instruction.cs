public class Instruction
{
    public string Step { get; set; }
    public int InstructionId { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;

    public Instruction(string step)
    {
        Step = step;
    }

    private Instruction() { 
        Step = "";
    }
}


public enum Unit
{
    Gram = 0,
    Kilogram = 1,
    Milliliter = 2,
    Liter = 3
}

public class UnitEntity
{
    public int UnitEntityId { get; set; }

    public Unit Unit { get; set; }

    public UnitEntity() { }

    public UnitEntity(Unit unit)
    {
        Unit = unit;
    }

    public override string ToString()
    {
        return Unit.ToString();
    }
}
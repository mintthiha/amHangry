public interface IDataLoader<T>
{
    List<T> LoadFromDatabase();

    void SaveToDatabase();
}
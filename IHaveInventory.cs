namespace Iteration
{
    public interface IHaveInventory
    {
        GameObject Locate(string id);
        void Put(Item item);
        GameObject Take(string id);
        string Name { get; }
    }
}
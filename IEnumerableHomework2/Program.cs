public class Entity
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
}

class Program
{
    static Dictionary<int, List<Entity>> MakeDictionary(List<Entity> list)
    {
        return list.Aggregate(new Dictionary<int, List<Entity>>(),
            (dictionary, entity) =>
            {
                if (!dictionary.ContainsKey(entity.ParentId))
                    dictionary[entity.ParentId] = new List<Entity>();
                dictionary[entity.ParentId].Add(entity);
                return dictionary;
            });
    }

    static void Main()
    {
        var dictionary = MakeDictionary(new List<Entity> {
            new Entity { Id = 1, ParentId = 0, Name = "Root entity" },
            new Entity { Id = 2, ParentId = 1, Name = "Child of 1 entity" },
            new Entity { Id = 3, ParentId = 1, Name = "Child of 1 entity" },
            new Entity { Id = 4, ParentId = 2, Name = "Child of 2 entity" },
            new Entity { Id = 5, ParentId = 4, Name = "Child of 4 entity" }
        });

        foreach (var list in dictionary)
        {
            Console.Write($"Key = {list.Key}, Value = List {{");
            foreach (var entity in list.Value)
                Console.Write($" Entity {{ Id = {entity.Id} }},");
            Console.WriteLine("\b }");
        }
    }
}
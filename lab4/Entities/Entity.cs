namespace lab4.Entities
{
    public abstract class EntityJson
    {
    }

    public interface IEntity<TJson>
    {
        public TJson toJSON();
    }
}

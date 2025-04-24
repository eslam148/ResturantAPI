namespace ResturantAPI.Domain
{
    public class Entity<T>
    {
        public T Id { get; protected set; }
        public Entity(T id)
        {
            Id = id;
        }
        public Entity()
        {
        }
        public override bool Equals(object obj)
        {
            if (obj is Entity<T> entity)
            {
                return Id.Equals(entity.Id);
            }

            return false;
        }

        public DateTime? CreatedAt { get; set; }
    }

    public class BaseEntity:Entity<int>
    {
        
    }
}

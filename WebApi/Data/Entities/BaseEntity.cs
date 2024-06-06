namespace WebApi.Data.Entities
{
    public class BaseEntity<TBase>
    {
        public TBase Id { get; set; }
    }
}

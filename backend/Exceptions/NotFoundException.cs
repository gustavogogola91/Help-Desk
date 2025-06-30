namespace backend.Exceptions
{
    public class NotFoundException : Exception
    {
        public long? Id { get; set; }
        
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, long id) : base(message)
        {
            Id = id;
        }
    }
}
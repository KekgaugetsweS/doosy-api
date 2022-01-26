namespace Doosy.Framework.Domain
{
    public class ObjectResponse<T> : ResponseBase where T : class
    {
        public T Data { get; set; }
    }
}

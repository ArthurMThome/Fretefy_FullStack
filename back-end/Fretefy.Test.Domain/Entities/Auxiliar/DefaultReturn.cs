using System.Net;

namespace Fretefy.Test.Domain.Entities.Auxiliar
{
    public class DefaultReturn<T>
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T Obj { get; set; }
    }
}

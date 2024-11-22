using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Response_Model
{
    public class GenericResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

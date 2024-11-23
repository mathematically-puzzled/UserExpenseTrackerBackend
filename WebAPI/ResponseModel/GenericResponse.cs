namespace WebAPI.ResponseModel
{
    public class GenericResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

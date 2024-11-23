namespace WebAPI.ResponseModel.Model
{
    public class GenericResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

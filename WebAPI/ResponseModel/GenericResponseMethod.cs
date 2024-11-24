using WebAPI.ResponseModel.Model;

namespace WebAPI.ResponseModel
{
    public class GenericResponseMethod
    {
        public GenericResponseModel GenerateResponseMethod(int StatusCode, string Message, object Data)
        {
            GenericResponseModel response = new();
            response.StatusCode = StatusCode;
            response.Message = Message;
            response.Data = Data;
            return response;
        }
    }
}

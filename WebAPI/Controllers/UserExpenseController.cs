using Application.Features.ExpenseFeatures.Commands;
using Application.Features.ExpenseFeatures.Queries;
using Application.Models.Expense;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ResponseModel;
using WebAPI.ResponseModel.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "UserExpenseController")]
    public class UserExpenseController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        GenericResponseMethod responseGenerator = new();

        public UserExpenseController(GenericResponseMethod responseGenerator, ISender mediatrSender)
        {
            this.responseGenerator = responseGenerator;
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> AddNewExpense(NewExpense UserExpense)
        {
            try
            {
                await _mediatrSender.Send(new AddNewExpenseRequest(UserExpense));
                return responseGenerator.GenerateResponseMethod(200, "Expense added", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        [HttpDelete("delete")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> DeleteExpense(Guid ExpenseId)
        {
            try
            {
                await _mediatrSender.Send(new DeleteExpenseRequest(ExpenseId));
                return responseGenerator.GenerateResponseMethod(200, "Record Successfully Deleted", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        [HttpPut("update")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> UpdateExpense(UpdateExpense UpdateExpense)
        {
            try
            {
                await _mediatrSender.Send(new UpdateExpenseRequest(UpdateExpense));
                return responseGenerator.GenerateResponseMethod(200, "Expense updated.", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        [HttpPost("fetch")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> FetchAllExpense(ExpenseRequestModel RequestModel)
        {
            try
            {
                List<Expenses> ExpenseInDb = await _mediatrSender.Send(new FetchExpenseRequest(RequestModel));
                return responseGenerator.GenerateResponseMethod(200, "Data fetched successfully", ExpenseInDb);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }
    }
}

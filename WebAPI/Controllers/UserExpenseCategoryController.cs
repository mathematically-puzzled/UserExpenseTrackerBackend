using Application.Features.ExpenseCategoryFeatures.Queries;
using Application.Features.ExpenseCategoryFeatures.Commands;
using Application.Models.ExpenseCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ResponseModel;
using WebAPI.ResponseModel.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "UserExpenseCategoryController")]
    public class UserExpenseCategoryController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        GenericResponseMethod responseGenerator = new();

        public UserExpenseCategoryController(GenericResponseMethod responseGenerator, ISender mediatrSender)
        {
            this.responseGenerator = responseGenerator;
            _mediatrSender = mediatrSender;
        }

        /// <summary>
        /// Fetch User Expense Categories.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns>
        /// User expense categories along with the ones from Admin.
        /// </returns>
        [HttpPost("fetch")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> GetUserExpenseCategory(Guid UserId)
        {
            try
            {
                object ExpenseCtgList = await _mediatrSender.Send(new FetchUserExpenseCategoriesRequest(UserId));
                return responseGenerator.GenerateResponseMethod(200, "Properties Fetched", ExpenseCtgList);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        /// <summary>
        /// Method to add a new expense category.
        /// </summary>
        /// <param name="ExpCtg"></param>
        [HttpPost("add")]
        [AllowAnonymous]
        public async Task<GenericResponseModel> AddUserExpenseCategory(AddExpenseCategory ExpCtg)
        {
            try
            {
                bool isSuccesful = await _mediatrSender.Send(new AddUserExpenseCategoryRequest(ExpCtg));
                return responseGenerator.GenerateResponseMethod(200, "Expense Type created.", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }

        /// <summary>
        /// Method to delete an expense category
        /// </summary>
        /// <param name="Id"></param>
        [HttpDelete]
        [AllowAnonymous]
        public async Task<GenericResponseModel> DeleteUserExpenseCategory(Guid Id)
        {
            try
            {
                bool isSuccesful = await _mediatrSender.Send(new DeleteUserExpenseCategoryRequest(Id));
                return responseGenerator.GenerateResponseMethod(200, "Expense Type deleted.", null);
            }
            catch (Exception ex)
            {
                return responseGenerator.GenerateResponseMethod(500, ex.Message, null);
            }
        }
    }
}

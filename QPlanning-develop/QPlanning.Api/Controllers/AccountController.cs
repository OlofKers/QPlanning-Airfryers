using System.Linq;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using QPlanning.Common.Auth;
using QPlanning.Api.Controllers.Base;
using MediatR;
using QPlanning.Api.Helpers.Constants;
using QPlanning.Business.Dto.Commands;
using QPlanning.Business.UseCases.Authentication.Account.Update.Dto.Command;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Create.Dto.Command;
using QPlanning.Business.UseCases.Authorization.Claims.Roles.Delete.Dto.Command;

namespace QPlanning.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseControllerWithMediatR
    {
        public AccountController(IMediator mediator) : base(mediator) { }

        // POST api/account/add
        [HttpPost]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("add")]
        public async Task<ActionResult> Add([FromBody] CreateUserCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.UserName))
                return BadRequest("Username is required.");

            if (!IsValidEmail(command.Email))
                return BadRequest("Email is invalid.");

            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // PUT api/account/update
        [HttpPut]
        [Authorize(Policy = Policies.AdminOnly)]
        [Authorize(Policy = "ElevatedRights")]
        [Route("update")]
        public async Task<ActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST api/account/delete
        [HttpPost]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("delete")]
        public async Task<ActionResult> Delete([FromBody] DeleteUserCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST api/account/resetPassword
        [HttpPost]
        [Authorize(Policy = Policies.AtLeastMedewerker)]
        [Route("resetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET api/account/getAllRoles
        [HttpGet]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("getAllRoles")]
        public ActionResult GetAllRoles()
        {
            return Ok(UserRole.AllRoles);
        }

        // GET api/account/getAllUsers
        [HttpGet]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("getAllUsers")]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await Mediator.Send(new GetAllUserCommand());
            return result.Users.Any() ? Ok(result) : BadRequest(result);
        }

        // POST api/account/addClaimRoleToUser
        [HttpPost]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("addClaimRoleToUser")]
        public async Task<ActionResult> AddClaimRoleToUser([FromBody] CreateRoleCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Role))
                return BadRequest("Role cannot be empty.");

            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST api/account/deleteClaimRoleFromUser
        [HttpPost]
        [Authorize(Policy = Policies.AdminOnly)]
        [Route("deleteClaimRoleFromUser")]
        public async Task<ActionResult> RemoveClaimRoleToUser([FromBody] DeleteRoleCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Role))
                return BadRequest("Role cannot be empty.");

            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // Helper method
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

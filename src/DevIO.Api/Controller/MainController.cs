using DevIO.Bussiness.Interfaces;
using DevIO.Bussiness.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevIO.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificator _notificator;
        public readonly IUser _appUser;

        public Guid UserId { get; set; }
        public bool UserIsAuthenticated { get; set; }

        public MainController(
            INotificator notificator,
            IUser appUser)
        {
            _notificator = notificator;
            _appUser = appUser;

            if (_appUser.IsAuthenticated())
            {
                UserId = _appUser.GetUserId();
                UserIsAuthenticated = true;
            }
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificator.GetNotifications().Select(s => s.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyErrorInvalidModelState(modelState);

            return CustomResponse();
        }

        protected bool OperationValid()
        {
            return !_notificator.HasNotification();
        }

        protected void NotifyErrorInvalidModelState(ModelStateDictionary modelState)
        {
            IEnumerable<ModelError> errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (ModelError error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string errorMsg)
        {
            _notificator.Handle(new Notification(errorMsg));
        }
    }
}

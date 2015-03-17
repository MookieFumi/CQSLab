using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace CQSLab.UI.Infrastructure.Attributes
{
    public class AccessControlFilter : ActionFilterAttribute
    {
        public string ControllerName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var accessControl = GetAccessControl(filterContext);

            if (accessControl.NoAccess)
            {
                throw new UnauthorizedAccessException();
            }

            filterContext.Controller.ViewBag.ReadOnly = accessControl.ReadOnly;
        }

        private AccessControl GetAccessControl(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLowerInvariant();

            var dictionary = new Dictionary<string, AccessControl>
                             {
                                 {Strings.CUSTOMERS_CONTROLLER_NAME, new AccessControlBuilder().WithNoAccess().Build()},
                                 {Strings.PRODUCTS_CONTROLLER_NAME, new AccessControlBuilder().IsReadOnly().Build()}
                             };

            if (dictionary.ContainsKey(controllerName))
            {
                return dictionary[controllerName];
            }

            return new AccessControlBuilder().Build();
        }

        class AccessControl
        {
            public bool ReadOnly { get; set; }
            public bool NoAccess { get; set; }
        }

        class AccessControlBuilder
        {
            private readonly AccessControl _accessControl;

            public AccessControlBuilder()
            {
                _accessControl = new AccessControl();
            }

            public AccessControlBuilder WithNoAccess()
            {
                _accessControl.NoAccess = true;
                return this;
            }

            public AccessControlBuilder IsReadOnly()
            {
                _accessControl.ReadOnly = true;
                return this;
            }

            public AccessControl Build()
            {
                return _accessControl;
            }
        }
    }
}

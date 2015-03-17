using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using CQSLab.CrossCutting;

namespace CQSLab.UI.Infrastructure.Attributes
{
    public class AccessControlFilter : ActionFilterAttribute
    {
        public string ControllerName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Retry.Do(() =>
            {
                var accessControl = GetAccessControl(filterContext);

                if (accessControl.IsNoAccess)
                {
                    throw new UnauthorizedAccessException();
                }

                filterContext.Controller.ViewBag.ReadOnly = accessControl.IsReadOnly;    
            }, TimeSpan.FromSeconds(3));
        }

        private AccessControl GetAccessControl(ActionExecutingContext filterContext)
        {
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLowerInvariant();

            var dictionary = new Dictionary<string, AccessControl>
                             {
                                 {Strings.CUSTOMERS_CONTROLLER_NAME, new AccessControlBuilder().IsNoAccess().Build()},
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
            public bool IsReadOnly { get; set; }
            public bool IsNoAccess { get; set; }
        }

        class AccessControlBuilder
        {
            private readonly AccessControl _accessControl;

            public AccessControlBuilder()
            {
                _accessControl = new AccessControl();
            }

            public AccessControlBuilder IsNoAccess()
            {
                _accessControl.IsNoAccess = true;
                return this;
            }

            public AccessControlBuilder IsReadOnly()
            {
                _accessControl.IsReadOnly = true;
                return this;
            }

            public AccessControl Build()
            {
                return _accessControl;
            }
        }
    }
}

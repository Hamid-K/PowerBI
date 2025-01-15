using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ODataRoutingAttribute : Attribute, IControllerConfiguration
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002A84 File Offset: 0x00000C84
		public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
		{
			if (controllerSettings == null)
			{
				throw Error.ArgumentNull("controllerSettings");
			}
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			ServicesContainer services = controllerSettings.Services;
			IHttpActionSelector httpActionSelector = new ODataActionSelector(ServicesExtensions.GetActionSelector(services));
			services.Replace(typeof(IHttpActionSelector), httpActionSelector);
			services.Insert(typeof(ValueProviderFactory), 0, new ODataValueProviderFactory());
		}
	}
}

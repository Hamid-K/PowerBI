using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Services;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class ODataFormattingAttribute : Attribute, IControllerConfiguration
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003008 File Offset: 0x00001208
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
			MediaTypeFormatterCollection formatters = controllerSettings.Formatters;
			if (!formatters.Where((MediaTypeFormatter f) => f != null && Decorator.GetInner<MediaTypeFormatter>(f) is ODataMediaTypeFormatter).Any<MediaTypeFormatter>())
			{
				ODataFormattingAttribute.RemoveFormatters(formatters, formatters.Where((MediaTypeFormatter f) => f is XmlMediaTypeFormatter || f is JsonMediaTypeFormatter));
				formatters.InsertRange(0, this.CreateODataFormatters());
			}
			ServicesContainer services = controllerSettings.Services;
			IActionValueBinder actionValueBinder = new PerRequestActionValueBinder(ServicesExtensions.GetActionValueBinder(services));
			controllerSettings.Services.Replace(typeof(IActionValueBinder), actionValueBinder);
			IContentNegotiator contentNegotiator = new PerRequestContentNegotiator(ServicesExtensions.GetContentNegotiator(services));
			controllerSettings.Services.Replace(typeof(IContentNegotiator), contentNegotiator);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000030E5 File Offset: 0x000012E5
		public virtual IList<ODataMediaTypeFormatter> CreateODataFormatters()
		{
			return ODataMediaTypeFormatters.Create();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030EC File Offset: 0x000012EC
		private static void RemoveFormatters(MediaTypeFormatterCollection formatterCollection, IEnumerable<MediaTypeFormatter> formattersToRemove)
		{
			foreach (MediaTypeFormatter mediaTypeFormatter in formattersToRemove.ToArray<MediaTypeFormatter>())
			{
				formatterCollection.Remove(mediaTypeFormatter);
			}
		}
	}
}

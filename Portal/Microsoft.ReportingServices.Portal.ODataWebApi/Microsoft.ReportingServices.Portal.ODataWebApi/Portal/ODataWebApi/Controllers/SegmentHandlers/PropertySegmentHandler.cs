using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Microsoft.OData.UriParser;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.Controllers.SegmentHandlers
{
	// Token: 0x02000040 RID: 64
	internal class PropertySegmentHandler : ISegmentHandler
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0000C828 File Offset: 0x0000AA28
		public object Handle(object source, ODataPathSegment segment)
		{
			string propertyName = null;
			NavigationPropertySegment navigationPropertySegment = segment as NavigationPropertySegment;
			if (navigationPropertySegment != null)
			{
				propertyName = navigationPropertySegment.NavigationProperty.Name;
			}
			PropertySegment propertySegment = segment as PropertySegment;
			if (propertySegment != null)
			{
				propertyName = propertySegment.Property.Name;
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new InvalidOperationException("PropertyName cannot be null or empty");
			}
			PropertyInfo propertyInfo = source.GetType().GetProperties().FirstOrDefault((PropertyInfo p) => string.Compare(p.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0);
			if (propertyInfo == null)
			{
				return null;
			}
			object value;
			try
			{
				value = propertyInfo.GetValue(source);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					HttpResponseException ex2 = ex.InnerException as HttpResponseException;
					if (ex2 != null)
					{
						throw new HttpResponseException(ex2.Response.StatusCode);
					}
				}
				throw ex;
			}
			return value;
		}
	}
}

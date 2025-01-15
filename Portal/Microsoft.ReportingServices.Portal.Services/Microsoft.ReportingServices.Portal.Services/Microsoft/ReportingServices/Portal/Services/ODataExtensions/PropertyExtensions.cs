using System;
using Microsoft.ReportingServices.Library.Soap;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200004F RID: 79
	internal static class PropertyExtensions
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x00011F4B File Offset: 0x0001014B
		public static global::Model.Property ToWebAPI(this Microsoft.ReportingServices.Library.Soap.Property soapProperty)
		{
			if (soapProperty == null)
			{
				throw new ArgumentNullException("soapProperty");
			}
			return new global::Model.Property
			{
				Name = soapProperty.Name,
				Value = soapProperty.Value
			};
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011F78 File Offset: 0x00010178
		public static Microsoft.ReportingServices.Library.Soap.Property ToSoapAPI(this global::Model.Property webApiProperty)
		{
			if (webApiProperty == null)
			{
				throw new ArgumentNullException("webApiProperty");
			}
			return new Microsoft.ReportingServices.Library.Soap.Property
			{
				Name = webApiProperty.Name,
				Value = webApiProperty.Value
			};
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00011FA5 File Offset: 0x000101A5
		public static Microsoft.SqlServer.ReportingServices2010.Property ToProxyAPI(this global::Model.Property webApiProperty)
		{
			if (webApiProperty == null)
			{
				throw new ArgumentNullException("webApiProperty");
			}
			return new Microsoft.SqlServer.ReportingServices2010.Property
			{
				Name = webApiProperty.Name,
				Value = webApiProperty.Value
			};
		}
	}
}

using System;
using Microsoft.SqlServer.ReportingServices2010;
using Model;

namespace Microsoft.ReportingServices.Portal.Services.ODataExtensions
{
	// Token: 0x0200004C RID: 76
	internal static class ValidValueExtensions
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		public static global::Model.ValidValue ToWebApiValidValue(this Microsoft.SqlServer.ReportingServices2010.ValidValue validValue, ReportParameterType reportParameterType)
		{
			if (validValue == null)
			{
				throw new ArgumentNullException("validValue");
			}
			return new global::Model.ValidValue
			{
				Value = ParameterValueExtensions.ToWebApiValue(validValue.Value, reportParameterType, null),
				Label = validValue.Label
			};
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		public static global::Model.ValidValue ToWebAPI(this Microsoft.SqlServer.ReportingServices2010.ValidValue validValue)
		{
			if (validValue == null)
			{
				throw new ArgumentNullException("validValue");
			}
			return new global::Model.ValidValue
			{
				Label = validValue.Label,
				Value = validValue.Value
			};
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00011C15 File Offset: 0x0000FE15
		public static Microsoft.SqlServer.ReportingServices2010.ValidValue ToSoapAPI(this global::Model.ValidValue validValue)
		{
			if (validValue == null)
			{
				throw new ArgumentNullException("validValue");
			}
			return new Microsoft.SqlServer.ReportingServices2010.ValidValue
			{
				Label = validValue.Label,
				Value = validValue.Value
			};
		}
	}
}

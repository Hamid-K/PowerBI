using System;
using Microsoft.OData.Client;
using Microsoft.ReportingServices.Portal.ODataClient.V1.Model;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.PowerBIIntegration
{
	// Token: 0x020000A7 RID: 167
	public static class ExtensionMethods
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0000E6D0 File Offset: 0x0000C8D0
		[OriginalName("IsEnabled")]
		public static DataServiceQuerySingle<bool> IsEnabled(this DataServiceQuerySingle<PowerBIUserInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return source.CreateFunctionQuerySingle<bool>("PowerBIIntegration.IsEnabled", false, Array.Empty<UriOperationParameter>());
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000E6F6 File Offset: 0x0000C8F6
		[OriginalName("Logout")]
		public static DataServiceActionQuery Logout(this DataServiceQuerySingle<PowerBIUserInfo> source)
		{
			if (!source.IsComposable)
			{
				throw new NotSupportedException("The previous function is not composable.");
			}
			return new DataServiceActionQuery(source.Context, source.AppendRequestUri("PowerBIIntegration.Logout"), Array.Empty<BodyOperationParameter>());
		}
	}
}

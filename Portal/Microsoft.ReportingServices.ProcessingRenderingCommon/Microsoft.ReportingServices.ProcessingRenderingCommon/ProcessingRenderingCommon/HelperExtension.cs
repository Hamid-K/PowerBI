using System;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon
{
	// Token: 0x020000CE RID: 206
	internal static class HelperExtension
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00013359 File Offset: 0x00011559
		private static bool IsValueNull(this IDataMultiValueParameter mvParameter)
		{
			return mvParameter.Value == null || mvParameter.Value is DBNull;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00013373 File Offset: 0x00011573
		private static bool IsValuesNullOrEmpty(this IDataMultiValueParameter mvParameter)
		{
			return mvParameter.Values == null || mvParameter.Values.Length == 0;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00013389 File Offset: 0x00011589
		private static bool IsExpandableOracleType(this IDataMultiValueParameter mvParameter, ServerType serverType)
		{
			return serverType == ServerType.Oracle && (mvParameter.Value is DateTime || mvParameter.Value is bool);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000133AE File Offset: 0x000115AE
		internal static bool ShouldMakeExpandable(this IDataMultiValueParameter mvParameter, ServerType serverType)
		{
			return (serverType == ServerType.Teradata || mvParameter.IsExpandableOracleType(serverType)) && !mvParameter.IsValueNull() && mvParameter.IsValuesNullOrEmpty();
		}
	}
}

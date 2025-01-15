using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000069 RID: 105
	internal static class SkuResources
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0000DC06 File Offset: 0x0000BE06
		internal static void ThrowOperationNotSupportedException(RestrictedFeatures feature)
		{
			throw SkuResources.GetOperationNotSupportedException(feature);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000DC0E File Offset: 0x0000BE0E
		internal static OperationNotSupportedException GetOperationNotSupportedException(RestrictedFeatures feature)
		{
			return new OperationNotSupportedException(SkuResources.RestrictedFeatureName(feature));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000DC1B File Offset: 0x0000BE1B
		internal static string GetOperationNotSupportedMessage(RestrictedFeatures feature)
		{
			return ErrorStringsWrapper.rsOperationNotSupported(SkuResources.RestrictedFeatureName(feature));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000DC28 File Offset: 0x0000BE28
		private static string RestrictedFeatureName(RestrictedFeatures feature)
		{
			switch (feature)
			{
			case RestrictedFeatures.NonSqlDataSources:
				return ErrorStrings.SkuNonSqlDataSources;
			case RestrictedFeatures.OtherSkuDatasources:
				return ErrorStrings.SkuOtherSkuDatasources;
			case RestrictedFeatures.RemoteDataSources:
				return ErrorStrings.SkuRemoteDataSources;
			case RestrictedFeatures.Caching:
				return ErrorStrings.SkuCaching;
			case RestrictedFeatures.ExecutionSnapshots:
				return ErrorStrings.SkuExecutionSnapshots;
			case RestrictedFeatures.History:
				return ErrorStrings.SkuHistory;
			case RestrictedFeatures.Delivery:
				return ErrorStrings.SkuDelivery;
			case RestrictedFeatures.Scheduling:
				return ErrorStrings.SkuScheduling;
			case RestrictedFeatures.Extensibility:
				return ErrorStrings.SkuExtensibility;
			case RestrictedFeatures.CustomAuth:
				return ErrorStrings.SkuCustomAuth;
			case RestrictedFeatures.Sharepoint:
				return ErrorStrings.SkuSharepoint;
			case RestrictedFeatures.ScaleOut:
				return ErrorStrings.SkuScaleOut;
			case RestrictedFeatures.Subscriptions:
				return ErrorStrings.SkuSubscriptions;
			case RestrictedFeatures.DataDrivenSubscriptions:
				return ErrorStrings.SkuDataDrivenSubscriptions;
			case RestrictedFeatures.CustomRolesSecurity:
				return ErrorStrings.SkuCustomRolesSecurity;
			case RestrictedFeatures.ReportBuilder:
				return ErrorStrings.SkuReportBuilder;
			case RestrictedFeatures.ModelItemSecurity:
				return ErrorStrings.SkuModelItemSecurity;
			case RestrictedFeatures.DynamicDrillthrough:
				return ErrorStrings.SkuDynamicDrillthrough;
			case RestrictedFeatures.NoCpuThrottling:
				return ErrorStrings.SkuNoCpuThrottling;
			case RestrictedFeatures.NoMemoryThrottling:
				return ErrorStrings.SkuNoMemoryThrottling;
			case RestrictedFeatures.EventGeneration:
				return ErrorStrings.SkuEventGeneration;
			case RestrictedFeatures.ComponentLibrary:
				return ErrorStrings.SkuComponentLibrary;
			case RestrictedFeatures.SharedDataset:
				return ErrorStrings.SkuSharedDataset;
			case RestrictedFeatures.DataAlerting:
				return ErrorStrings.SkuDataAlerting;
			case RestrictedFeatures.Crescent:
				return ErrorStrings.SkuCrescent;
			case RestrictedFeatures.KpiItems:
				return ErrorStrings.SkuKpiItems;
			case RestrictedFeatures.CommentAlerting:
				return ErrorStrings.SkuCommentAlerting;
			}
			throw new InternalCatalogException("Unexpected feature" + feature.ToString());
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Editions;
using Model;

namespace Microsoft.ReportingServices.Portal
{
	// Token: 0x02000012 RID: 18
	internal static class FeaturesUtil
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002AA4 File Offset: 0x00000CA4
		internal static IEnumerable<CatalogItemType> GetRestrictedItems(SkuType sku)
		{
			List<CatalogItemType> list = new List<CatalogItemType>();
			try
			{
				if (!SkuUtil.IsFeatureEnabled(sku, RestrictedFeatures.KpiItems))
				{
					list.Add(CatalogItemType.Kpi);
				}
			}
			catch (EvaluationCopyExpiredException)
			{
				list.Add(CatalogItemType.Kpi);
				list.Add(CatalogItemType.MobileReport);
			}
			return list;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AEC File Offset: 0x00000CEC
		internal static IEnumerable<RestrictedFeatures> GetRestrictedFeatures(SkuType sku)
		{
			List<RestrictedFeatures> list = new List<RestrictedFeatures>();
			IEnumerable<RestrictedFeatures> enumerable = Enum.GetValues(typeof(RestrictedFeatures)).Cast<RestrictedFeatures>();
			try
			{
				foreach (RestrictedFeatures restrictedFeatures in enumerable)
				{
					if (!SkuUtil.IsFeatureEnabled(sku, restrictedFeatures))
					{
						list.Add(restrictedFeatures);
					}
				}
			}
			catch (EvaluationCopyExpiredException)
			{
				return enumerable;
			}
			return list;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ReportingServices.Editions
{
	// Token: 0x02000015 RID: 21
	public static class SkuUtils
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000030C0 File Offset: 0x000012C0
		public static bool SkuCanUpgradeTo(SkuType from, SkuType to)
		{
			return from.GetAttribute<SkuCanUpgradeTo>().SupportsUpgradeTo.Contains(to);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000030D8 File Offset: 0x000012D8
		public static bool SkuSupportsDatabasesCreatedBy(SkuType from, SkuType to)
		{
			return from.GetAttribute<SkuSupportsDatabasesCreatedBy>().SupportsDatabasesCreatedBy.Contains(to);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000030F0 File Offset: 0x000012F0
		public static SkuType SkuWithGuid(string guid)
		{
			IEnumerable<SkuType> enumerable = ((SkuType[])Enum.GetValues(typeof(SkuType))).Where((SkuType s) => s.GetAttribute<SkuDetails>().Guid == guid);
			if (enumerable.Count<SkuType>() == 0)
			{
				throw new ArgumentException("No matching SKU");
			}
			return enumerable.First<SkuType>();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003148 File Offset: 0x00001348
		public static SkuType ParseSkuFromCommandLineName(ProductType productType, string skuName)
		{
			IEnumerable<SkuType> enumerable = ((SkuType[])Enum.GetValues(typeof(SkuType))).Where((SkuType s) => s.GetAttribute<SkuDetails>().Product == productType && s.GetAttribute<SkuStrings>().CommandLineName.Equals(skuName, StringComparison.InvariantCultureIgnoreCase));
			if (enumerable.Count<SkuType>() == 0)
			{
				throw new ArgumentException("No matching SKU");
			}
			return enumerable.First<SkuType>();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031A8 File Offset: 0x000013A8
		public static ProductType ParseProduct(string productName)
		{
			IEnumerable<ProductType> enumerable = ((ProductType[])Enum.GetValues(typeof(ProductType))).Where((ProductType p) => p.GetAttribute<SkuStrings>().ShortName == productName);
			if (enumerable.Count<ProductType>() == 0)
			{
				throw new ArgumentException("No matching product");
			}
			return enumerable.First<ProductType>();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003200 File Offset: 0x00001400
		public static IEnumerable<SkuType> GetSkusForProduct(ProductType productType)
		{
			return ((SkuType[])Enum.GetValues(typeof(SkuType))).Where((SkuType s) => s.GetAttribute<SkuDetails>().Product == productType);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003240 File Offset: 0x00001440
		internal static SkuType ParseSqlKeySkuName(ProductType productType, string skuName)
		{
			IEnumerable<SkuType> enumerable = ((SkuType[])Enum.GetValues(typeof(SkuType))).Where((SkuType s) => s.GetAttribute<SkuDetails>().Product == productType && s.GetAttribute<SkuStrings>().PkConfigName.Equals(skuName, StringComparison.InvariantCultureIgnoreCase));
			if (enumerable.Count<SkuType>() == 0)
			{
				throw new ArgumentException("No matching SKU");
			}
			return enumerable.First<SkuType>();
		}
	}
}

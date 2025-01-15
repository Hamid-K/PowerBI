using System;

namespace Microsoft.InfoNav
{
	// Token: 0x0200004A RID: 74
	internal static class PropertyDataCategoryExtensions
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00002CA0 File Offset: 0x00000EA0
		internal static bool IsMonth(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Month);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00002CA9 File Offset: 0x00000EA9
		internal static bool IsYear(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Year);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00002CB2 File Offset: 0x00000EB2
		internal static bool IsDecade(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Decade);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00002CBB File Offset: 0x00000EBB
		internal static bool IsGeography(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Geography);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00002CC4 File Offset: 0x00000EC4
		internal static bool IsAddress(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Address);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00002CCE File Offset: 0x00000ECE
		internal static bool IsCity(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.City);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00002CD8 File Offset: 0x00000ED8
		internal static bool IsContinent(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Continent);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00002CE2 File Offset: 0x00000EE2
		internal static bool IsCountry(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Country);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00002CEF File Offset: 0x00000EEF
		internal static bool IsCounty(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.County);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00002CFC File Offset: 0x00000EFC
		internal static bool IsRegion(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Region);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00002D09 File Offset: 0x00000F09
		internal static bool IsPostalCode(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.PostalCode);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00002D16 File Offset: 0x00000F16
		internal static bool IsStateOrProvince(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.StateOrProvince);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00002D23 File Offset: 0x00000F23
		internal static bool IsPlace(this PropertyDataCategory category)
		{
			return category.HasDataCategory(PropertyDataCategory.Place);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00002D30 File Offset: 0x00000F30
		internal static bool IsPartialMatch(this PropertyDataCategory partialMatch)
		{
			return partialMatch.HasDataCategory(PropertyDataCategory.PartialMatch);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00002D3D File Offset: 0x00000F3D
		public static bool HasDataCategory(this PropertyDataCategory type, PropertyDataCategory flag)
		{
			return (type & flag) == flag;
		}
	}
}

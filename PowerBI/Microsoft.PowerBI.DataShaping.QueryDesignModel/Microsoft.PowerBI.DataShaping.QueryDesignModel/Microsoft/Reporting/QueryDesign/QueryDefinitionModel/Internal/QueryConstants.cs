using System;
using System.Globalization;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x0200010B RID: 267
	public static class QueryConstants
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0002AE16 File Offset: 0x00029016
		public static DateTime EarliestSupportedDateTimeForAS2012
		{
			get
			{
				return new DateTime(1900, 3, 1, new GregorianCalendar());
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0002AE2C File Offset: 0x0002902C
		public static DateTime EarliestSupportedDateTimeUtcForAS2012
		{
			get
			{
				return new DateTime(1900, 3, 1, 0, 0, 0, 0, new GregorianCalendar(), DateTimeKind.Utc);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002AE4F File Offset: 0x0002904F
		public static DateTime EarliestSupportedDateTime
		{
			get
			{
				return new DateTime(1601, 1, 1, new GregorianCalendar());
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002AE64 File Offset: 0x00029064
		public static DateTime EarliestSupportedDateTimeUtc
		{
			get
			{
				return new DateTime(1601, 1, 1, 0, 0, 0, 0, new GregorianCalendar(), DateTimeKind.Utc);
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002AE87 File Offset: 0x00029087
		internal static DateTime GetEarliestSupportedDateTime(ModelCapabilities modelCapabilities)
		{
			if (!modelCapabilities.IsASServerNewerThan2012())
			{
				return QueryConstants.EarliestSupportedDateTimeForAS2012;
			}
			return QueryConstants.EarliestSupportedDateTime;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002AE9C File Offset: 0x0002909C
		internal static DateTime GetEarliestSupportedDateTime(DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			if (!daxCapabilitiesAnnotation.IsASServerNewerThan2012())
			{
				return QueryConstants.EarliestSupportedDateTimeForAS2012;
			}
			return QueryConstants.EarliestSupportedDateTime;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002AEB1 File Offset: 0x000290B1
		internal static DateTime GetEarliestSupportedDateTimeUtc(ModelCapabilities modelCapabilities)
		{
			if (!modelCapabilities.IsASServerNewerThan2012())
			{
				return QueryConstants.EarliestSupportedDateTimeUtcForAS2012;
			}
			return QueryConstants.EarliestSupportedDateTimeUtc;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002AEC6 File Offset: 0x000290C6
		private static bool IsASServerNewerThan2012(this ModelCapabilities capabilities)
		{
			return capabilities.DaxFunctions.BinaryMinMax == BinaryMinMaxType.DefaultSupport;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0002AED6 File Offset: 0x000290D6
		private static bool IsASServerNewerThan2012(this DaxCapabilitiesAnnotation daxCapabilitiesAnnotation)
		{
			return daxCapabilitiesAnnotation.DaxFunctions.SupportsBinaryMinMax;
		}
	}
}

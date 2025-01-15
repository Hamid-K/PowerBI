using System;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting
{
	// Token: 0x020000CA RID: 202
	internal static class DateTimeUtil
	{
		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002186D File Offset: 0x0001FA6D
		public static DateTime RemoveSubsecondComponent(this DateTime value)
		{
			long ticks = value.Ticks;
			return new DateTime(ticks - ticks % 10000000L);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00021884 File Offset: 0x0001FA84
		public static double GetSubsecondComponent(this TimeSpan value)
		{
			ArgumentValidation.CheckCondition(value >= TimeSpan.Zero, "value");
			return (double)(value.Ticks % 10000000L) / 10000000.0;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000218B4 File Offset: 0x0001FAB4
		internal static bool IsDateTimeSupportedInDAX(DateTime dateTime, ModelCapabilities capabilities)
		{
			return dateTime >= QueryConstants.GetEarliestSupportedDateTime(capabilities);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000218C2 File Offset: 0x0001FAC2
		internal static bool IsDateTimeSupportedInDAX(DateTime dateTime, DaxCapabilitiesAnnotation capabilities)
		{
			return dateTime >= QueryConstants.GetEarliestSupportedDateTime(capabilities);
		}
	}
}

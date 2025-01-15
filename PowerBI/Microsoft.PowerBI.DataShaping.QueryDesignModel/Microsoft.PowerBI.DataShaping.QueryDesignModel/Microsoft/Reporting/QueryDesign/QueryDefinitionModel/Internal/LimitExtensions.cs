using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000EE RID: 238
	internal static class LimitExtensions
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x00023BA0 File Offset: 0x00021DA0
		public static Limit GetLimitForGroup(this IEnumerable<Limit> limits, Group group)
		{
			return limits.FirstOrDefault((Limit l) => l.RefersTo(group));
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00023BCC File Offset: 0x00021DCC
		public static bool HasLimitForGroup(this IEnumerable<Limit> limits, Group group)
		{
			return limits.GetLimitForGroup(group) != null;
		}
	}
}

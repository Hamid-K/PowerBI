using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Library;

namespace Microsoft.ReportingServices.Portal.Services.SystemResources
{
	// Token: 0x02000037 RID: 55
	internal sealed class SystemResourceTypeNameComparer : IEqualityComparer<SystemResource>
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000FA2B File Offset: 0x0000DC2B
		public bool Equals(SystemResource x, SystemResource y)
		{
			return string.Equals(x.TypeName, y.TypeName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000FA3F File Offset: 0x0000DC3F
		public int GetHashCode(SystemResource obj)
		{
			return obj.TypeName.ToLowerInvariant().GetHashCode();
		}
	}
}

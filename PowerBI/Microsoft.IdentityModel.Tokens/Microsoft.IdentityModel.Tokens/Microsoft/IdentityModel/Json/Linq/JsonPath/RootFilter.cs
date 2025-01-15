using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Linq.JsonPath
{
	// Token: 0x020000DD RID: 221
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x00030FCB File Offset: 0x0002F1CB
		private RootFilter()
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00030FD3 File Offset: 0x0002F1D3
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JToken[] { root };
		}

		// Token: 0x040003E9 RID: 1001
		public static readonly RootFilter Instance = new RootFilter();
	}
}

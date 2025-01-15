using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000DC RID: 220
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000C1B RID: 3099 RVA: 0x0003084B File Offset: 0x0002EA4B
		private RootFilter()
		{
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00030853 File Offset: 0x0002EA53
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JToken[] { root };
		}

		// Token: 0x040003CD RID: 973
		public static readonly RootFilter Instance = new RootFilter();
	}
}

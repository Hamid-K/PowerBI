using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Json.Linq
{
	// Token: 0x020000C7 RID: 199
	internal class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x0002D1AE File Offset: 0x0002B3AE
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002D1B7 File Offset: 0x0002B3B7
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}

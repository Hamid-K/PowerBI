using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C8 RID: 200
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0002D9FE File Offset: 0x0002BBFE
		[NullableContext(2)]
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002DA07 File Offset: 0x0002BC07
		[NullableContext(1)]
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

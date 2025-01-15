using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Linq
{
	// Token: 0x020000C8 RID: 200
	internal class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0002D8D6 File Offset: 0x0002BAD6
		[NullableContext(2)]
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002D8DF File Offset: 0x0002BADF
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

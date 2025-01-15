using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000055 RID: 85
	public abstract class RequestFailedDetailsParser
	{
		// Token: 0x06000299 RID: 665
		[NullableContext(1)]
		public abstract bool TryParse(Response response, [Nullable(2)] out ResponseError error, [Nullable(new byte[] { 2, 1, 1 })] out IDictionary<string, string> data);
	}
}

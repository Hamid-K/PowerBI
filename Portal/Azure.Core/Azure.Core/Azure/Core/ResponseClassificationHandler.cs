using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200005E RID: 94
	public abstract class ResponseClassificationHandler
	{
		// Token: 0x06000343 RID: 835
		[NullableContext(1)]
		public abstract bool TryClassify(HttpMessage message, out bool isError);
	}
}

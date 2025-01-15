using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000023 RID: 35
	internal abstract class JsonNameTable
	{
		// Token: 0x060000A0 RID: 160
		[return: Nullable(2)]
		public abstract string Get(char[] key, int start, int length);
	}
}

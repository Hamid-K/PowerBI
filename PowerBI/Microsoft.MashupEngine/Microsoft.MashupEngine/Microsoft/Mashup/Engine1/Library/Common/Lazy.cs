using System;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010D9 RID: 4313
	internal static class Lazy
	{
		// Token: 0x060070E9 RID: 28905 RVA: 0x00183942 File Offset: 0x00181B42
		public static Lazy<T> New<T>(Func<T> getValue)
		{
			return new Lazy<T>(getValue);
		}
	}
}

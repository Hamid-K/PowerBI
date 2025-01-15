using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200048D RID: 1165
	public static class FuncEqualityComparer
	{
		// Token: 0x06001A41 RID: 6721 RVA: 0x0004F457 File Offset: 0x0004D657
		public static FuncEqualityComparer<T> Create<T>(Func<T, T, bool> comparer)
		{
			return new FuncEqualityComparer<T>(comparer);
		}
	}
}

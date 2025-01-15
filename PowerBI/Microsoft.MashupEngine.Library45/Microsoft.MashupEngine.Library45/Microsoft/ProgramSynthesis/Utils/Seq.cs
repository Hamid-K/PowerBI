using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200040B RID: 1035
	public static class Seq
	{
		// Token: 0x0600177E RID: 6014 RVA: 0x00004FAE File Offset: 0x000031AE
		public static IEnumerable<T> Of<T>(params T[] args)
		{
			return args;
		}
	}
}

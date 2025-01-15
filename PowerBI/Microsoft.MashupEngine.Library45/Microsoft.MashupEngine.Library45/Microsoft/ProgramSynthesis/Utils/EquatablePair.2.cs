using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000488 RID: 1160
	public static class EquatablePair
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x0004F285 File Offset: 0x0004D485
		public static EquatablePair<T1, T2> Create<T1, T2>(T1 item1, T2 item2) where T1 : IEquatable<T1> where T2 : IEquatable<T2>
		{
			return new EquatablePair<T1, T2>(item1, item2);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0004F28E File Offset: 0x0004D48E
		public static IEnumerable<T> AsEnumerable<T>(this EquatablePair<T, T> pair) where T : IEquatable<T>
		{
			yield return pair.Item1;
			yield return pair.Item2;
			yield break;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000498 RID: 1176
	public class IdentityEquality : IEqualityComparer<object>
	{
		// Token: 0x06001A6F RID: 6767 RVA: 0x00002130 File Offset: 0x00000330
		private IdentityEquality()
		{
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0004FC1A File Offset: 0x0004DE1A
		public static IdentityEquality Comparer { get; } = new IdentityEquality();

		// Token: 0x06001A71 RID: 6769 RVA: 0x0004FC21 File Offset: 0x0004DE21
		public int GetHashCode(object value)
		{
			return RuntimeHelpers.GetHashCode(value);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0002D924 File Offset: 0x0002BB24
		public bool Equals(object left, object right)
		{
			return left == right;
		}
	}
}

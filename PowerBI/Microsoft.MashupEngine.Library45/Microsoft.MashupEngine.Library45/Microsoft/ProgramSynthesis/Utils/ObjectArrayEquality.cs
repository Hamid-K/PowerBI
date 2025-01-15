using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004CB RID: 1227
	public class ObjectArrayEquality : IEqualityComparer<object[]>
	{
		// Token: 0x06001B5A RID: 7002 RVA: 0x00002130 File Offset: 0x00000330
		private ObjectArrayEquality()
		{
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0005249B File Offset: 0x0005069B
		public static ObjectArrayEquality Comparer
		{
			get
			{
				return ObjectArrayEquality.Lazy.Value;
			}
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000524A7 File Offset: 0x000506A7
		public bool Equals(object[] x, object[] y)
		{
			return ValueEquality.Comparer.Equals(x, y);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000524B5 File Offset: 0x000506B5
		public int GetHashCode(object[] obj)
		{
			return ValueEquality.Comparer.GetHashCode(obj);
		}

		// Token: 0x04000D72 RID: 3442
		private static readonly Lazy<ObjectArrayEquality> Lazy = new Lazy<ObjectArrayEquality>(() => new ObjectArrayEquality());
	}
}

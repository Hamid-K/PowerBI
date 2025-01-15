using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	internal class OneWayDuplicateKeyComparer<[Nullable(0)] TKey> : IComparer<TKey> where TKey : IComparable
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002454 File Offset: 0x00000654
		private OneWayDuplicateKeyComparer()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000245C File Offset: 0x0000065C
		internal static OneWayDuplicateKeyComparer<TKey> Instance
		{
			get
			{
				return OneWayDuplicateKeyComparer<TKey>.m_instance;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002464 File Offset: 0x00000664
		public int Compare(TKey x, TKey y)
		{
			int num = x.CompareTo(y);
			if (num == 0)
			{
				return 1;
			}
			return num;
		}

		// Token: 0x04000018 RID: 24
		private static readonly OneWayDuplicateKeyComparer<TKey> m_instance = new OneWayDuplicateKeyComparer<TKey>();
	}
}

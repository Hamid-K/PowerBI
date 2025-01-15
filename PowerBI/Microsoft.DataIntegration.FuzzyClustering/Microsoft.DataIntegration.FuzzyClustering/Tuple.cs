using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000002 RID: 2
	internal class Tuple<T1, T2>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		internal T1 Item1 { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		internal T2 Item2 { get; private set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		internal Tuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002088 File Offset: 0x00000288
		public override int GetHashCode()
		{
			int num = 0;
			if (this.Item1 != null)
			{
				int num2 = num;
				T1 item = this.Item1;
				num = num2 ^ item.GetHashCode();
			}
			if (this.Item2 != null)
			{
				int num3 = num;
				T2 item2 = this.Item2;
				num = num3 ^ item2.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is Tuple<T1, T2>))
			{
				return false;
			}
			Tuple<T1, T2> tuple = (Tuple<T1, T2>)obj;
			return EqualityComparer<T1>.Default.Equals(this.Item1, tuple.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, tuple.Item2);
		}
	}
}

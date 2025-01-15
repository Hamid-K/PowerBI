using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002C8 RID: 712
	internal sealed class QueryPercentile : IEquatable<QueryPercentile>
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0002A5AF File Offset: 0x000287AF
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0002A5B7 File Offset: 0x000287B7
		public bool Exclusive { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0002A5C0 File Offset: 0x000287C0
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x0002A5C8 File Offset: 0x000287C8
		public double K { get; set; }

		// Token: 0x060017BA RID: 6074 RVA: 0x0002A5D4 File Offset: 0x000287D4
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.K.GetHashCode(), this.Exclusive.GetHashCode());
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0002A604 File Offset: 0x00028804
		public bool Equals(QueryPercentile other)
		{
			bool? flag = Util.AreEqual<QueryPercentile>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return other.Exclusive == this.Exclusive && other.K == this.K;
		}
	}
}

using System;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CF8 RID: 3320
	internal sealed class CubeLevelRange : IEquatable<CubeLevelRange>
	{
		// Token: 0x06005A08 RID: 23048 RVA: 0x0013AD78 File Offset: 0x00138F78
		public CubeLevelRange(ICubeLevel coarse, ICubeLevel fine)
		{
			this.coarse = coarse;
			this.fine = fine;
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x06005A09 RID: 23049 RVA: 0x0013AD8E File Offset: 0x00138F8E
		public ICubeLevel Coarse
		{
			get
			{
				return this.coarse;
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x06005A0A RID: 23050 RVA: 0x0013AD96 File Offset: 0x00138F96
		public ICubeLevel Fine
		{
			get
			{
				return this.fine;
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x0013AD9E File Offset: 0x00138F9E
		public ICubeHierarchy Hierarchy
		{
			get
			{
				return this.coarse.Hierarchy;
			}
		}

		// Token: 0x06005A0C RID: 23052 RVA: 0x0013ADAC File Offset: 0x00138FAC
		public CubeLevelRange Union(CubeLevelRange other)
		{
			return new CubeLevelRange((other.coarse.Number < this.coarse.Number) ? other.coarse : this.coarse, (other.fine.Number > this.fine.Number) ? other.fine : this.fine);
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x0013AE0A File Offset: 0x0013900A
		public bool Equals(CubeLevelRange other)
		{
			return this.coarse.Equals(other.coarse) && this.fine.Equals(other.fine);
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x0013AE32 File Offset: 0x00139032
		public override bool Equals(object other)
		{
			return this.Equals(other as CubeLevelRange);
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x0013AE40 File Offset: 0x00139040
		public override int GetHashCode()
		{
			return this.coarse.GetHashCode() + 37 * this.fine.GetHashCode();
		}

		// Token: 0x04003245 RID: 12869
		private readonly ICubeLevel coarse;

		// Token: 0x04003246 RID: 12870
		private readonly ICubeLevel fine;
	}
}

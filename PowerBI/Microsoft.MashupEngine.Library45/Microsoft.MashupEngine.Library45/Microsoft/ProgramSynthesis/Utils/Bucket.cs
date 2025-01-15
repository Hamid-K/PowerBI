using System;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000401 RID: 1025
	internal class Bucket : IEquatable<Bucket>
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00046F62 File Offset: 0x00045162
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00046F6A File Offset: 0x0004516A
		public long Id { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00046F73 File Offset: 0x00045173
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00046F7B File Offset: 0x0004517B
		public int HashCode { get; private set; }

		// Token: 0x0600173F RID: 5951 RVA: 0x00046F84 File Offset: 0x00045184
		public Bucket(long id, int hashCode)
		{
			this.Id = id;
			this.HashCode = hashCode;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00046F9A File Offset: 0x0004519A
		public bool Equals(Bucket other)
		{
			return other != null && (other == this || other.Id == this.Id);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00046FB5 File Offset: 0x000451B5
		public override bool Equals(object other)
		{
			return other != null && (other == this || this.Equals(other as Bucket));
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00046FCE File Offset: 0x000451CE
		public void Merge(Bucket other)
		{
			this.Id = other.Id;
			this.HashCode = other.HashCode;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00046FE8 File Offset: 0x000451E8
		public override int GetHashCode()
		{
			return (this.Id.GetHashCode() * 10939) ^ this.HashCode;
		}
	}
}

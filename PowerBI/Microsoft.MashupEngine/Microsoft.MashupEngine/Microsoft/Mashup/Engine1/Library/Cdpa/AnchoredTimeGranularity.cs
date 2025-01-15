using System;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DAF RID: 3503
	internal class AnchoredTimeGranularity : ITimeGranularity, IEquatable<ITimeGranularity>
	{
		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x00148309 File Offset: 0x00146509
		// (set) Token: 0x06005F31 RID: 24369 RVA: 0x00148311 File Offset: 0x00146511
		public DateTime Anchor { get; set; }

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x0014831A File Offset: 0x0014651A
		// (set) Token: 0x06005F33 RID: 24371 RVA: 0x00148322 File Offset: 0x00146522
		public ITimeGranularity Granularity { get; set; }

		// Token: 0x06005F34 RID: 24372 RVA: 0x0014832C File Offset: 0x0014652C
		public override int GetHashCode()
		{
			return 37 * this.Anchor.GetHashCode() + this.Granularity.NullableGetHashCode<ITimeGranularity>();
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x00148356 File Offset: 0x00146556
		public override bool Equals(object other)
		{
			return this.Equals(other as AnchoredTimeGranularity);
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x00148356 File Offset: 0x00146556
		public bool Equals(ITimeGranularity other)
		{
			return this.Equals(other as AnchoredTimeGranularity);
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x00148364 File Offset: 0x00146564
		public bool Equals(AnchoredTimeGranularity other)
		{
			return other != null && this.Anchor.Equals(other.Anchor) && this.Granularity.NullableEquals(other.Granularity);
		}
	}
}

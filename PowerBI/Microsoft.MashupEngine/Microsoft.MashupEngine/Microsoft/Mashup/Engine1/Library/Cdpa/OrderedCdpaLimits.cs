using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DFB RID: 3579
	[DataContract]
	internal class OrderedCdpaLimits : CdpaLimits
	{
		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x0600606C RID: 24684 RVA: 0x001499B1 File Offset: 0x00147BB1
		// (set) Token: 0x0600606D RID: 24685 RVA: 0x001499B9 File Offset: 0x00147BB9
		[DataMember(Name = "order", IsRequired = true)]
		public string Order { get; set; }

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x0600606E RID: 24686 RVA: 0x001499C2 File Offset: 0x00147BC2
		// (set) Token: 0x0600606F RID: 24687 RVA: 0x001499CA File Offset: 0x00147BCA
		[DataMember(Name = "operation", IsRequired = true)]
		public CdpaMetricPropertyOperation Operation { get; set; }

		// Token: 0x06006070 RID: 24688 RVA: 0x001499D3 File Offset: 0x00147BD3
		public override int GetHashCode()
		{
			return base.Count + this.Order.GetHashCode() + this.Operation.GetHashCode();
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x001499F3 File Offset: 0x00147BF3
		public override bool Equals(CdpaLimits other)
		{
			return this.Equals(other as OrderedCdpaLimits);
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x00149A01 File Offset: 0x00147C01
		public bool Equals(OrderedCdpaLimits other)
		{
			return other != null && base.Count == other.Count && this.Order == other.Order && this.Operation.Equals(other.Operation);
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x00149A3C File Offset: 0x00147C3C
		public override CdpaLimits Intersect(CdpaLimits other)
		{
			OrderedCdpaLimits orderedCdpaLimits = other as OrderedCdpaLimits;
			if (orderedCdpaLimits != null && this.Order == orderedCdpaLimits.Order && this.Operation.Equals(orderedCdpaLimits.Operation))
			{
				return new OrderedCdpaLimits
				{
					Count = Math.Min(base.Count, orderedCdpaLimits.Count),
					Order = this.Order,
					Operation = this.Operation
				};
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x00149AB4 File Offset: 0x00147CB4
		public override CdpaLimits Union(CdpaLimits other)
		{
			OrderedCdpaLimits orderedCdpaLimits = other as OrderedCdpaLimits;
			if (orderedCdpaLimits != null && this.Order == orderedCdpaLimits.Order && this.Operation.Equals(orderedCdpaLimits.Operation))
			{
				return new OrderedCdpaLimits
				{
					Count = Math.Max(base.Count, orderedCdpaLimits.Count),
					Order = this.Order,
					Operation = this.Operation
				};
			}
			throw new NotSupportedException();
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DFE RID: 3582
	[DataContract]
	internal class CdpaTopConfiguration : IIntersectable<CdpaTopConfiguration>, IUnionable<CdpaTopConfiguration>
	{
		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06006085 RID: 24709 RVA: 0x00149DEA File Offset: 0x00147FEA
		// (set) Token: 0x06006086 RID: 24710 RVA: 0x00149DF2 File Offset: 0x00147FF2
		[DataMember(Name = "order", IsRequired = true)]
		public string Order { get; set; }

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x06006087 RID: 24711 RVA: 0x00149DFB File Offset: 0x00147FFB
		// (set) Token: 0x06006088 RID: 24712 RVA: 0x00149E03 File Offset: 0x00148003
		[DataMember(Name = "count", IsRequired = true)]
		public long Count { get; set; }

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06006089 RID: 24713 RVA: 0x00149E0C File Offset: 0x0014800C
		// (set) Token: 0x0600608A RID: 24714 RVA: 0x00149E14 File Offset: 0x00148014
		[DataMember(Name = "operation", IsRequired = true)]
		public CdpaOperation Operation { get; set; }

		// Token: 0x0600608B RID: 24715 RVA: 0x00149E20 File Offset: 0x00148020
		public CdpaTopConfiguration Intersect(CdpaTopConfiguration other)
		{
			if (this.Operation.Equals(other.Operation) && this.Order == other.Order)
			{
				return new CdpaTopConfiguration
				{
					Order = this.Order,
					Count = Math.Min(this.Count, other.Count),
					Operation = this.Operation
				};
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x00149E90 File Offset: 0x00148090
		public CdpaTopConfiguration Union(CdpaTopConfiguration other)
		{
			if (this.Operation.Equals(other.Operation) && this.Order == other.Order)
			{
				return new CdpaTopConfiguration
				{
					Order = this.Order,
					Count = Math.Max(this.Count, other.Count),
					Operation = this.Operation
				};
			}
			throw new NotSupportedException();
		}
	}
}

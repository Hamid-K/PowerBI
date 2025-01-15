using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF8 RID: 3576
	[DataContract]
	internal abstract class CdpaLimits : IEquatable<CdpaLimits>, IIntersectable<CdpaLimits>, IUnionable<CdpaLimits>
	{
		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x0600605E RID: 24670 RVA: 0x001498E7 File Offset: 0x00147AE7
		// (set) Token: 0x0600605F RID: 24671 RVA: 0x001498EF File Offset: 0x00147AEF
		[DataMember(Name = "count", IsRequired = true)]
		public int Count { get; set; }

		// Token: 0x06006060 RID: 24672
		public abstract override int GetHashCode();

		// Token: 0x06006061 RID: 24673
		public abstract bool Equals(CdpaLimits other);

		// Token: 0x06006062 RID: 24674 RVA: 0x001498F8 File Offset: 0x00147AF8
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaLimits);
		}

		// Token: 0x06006063 RID: 24675
		public abstract CdpaLimits Intersect(CdpaLimits other);

		// Token: 0x06006064 RID: 24676
		public abstract CdpaLimits Union(CdpaLimits other);
	}
}

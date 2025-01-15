using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF6 RID: 3574
	[DataContract]
	internal abstract class CdpaMetricSplit : IEquatable<CdpaMetricSplit>, IIntersectable<CdpaMetricSplit>, IUnionable<CdpaMetricSplit>
	{
		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x0600604A RID: 24650
		[DataMember(Name = "type", IsRequired = true)]
		public abstract string Type { get; }

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x00149780 File Offset: 0x00147980
		// (set) Token: 0x0600604C RID: 24652 RVA: 0x00149788 File Offset: 0x00147988
		[DataMember(Name = "propertyName", IsRequired = true)]
		public string PropertyName { get; set; }

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x0600604D RID: 24653
		public abstract bool IsRestricted { get; }

		// Token: 0x0600604E RID: 24654
		public abstract CdpaMetricSplit Intersect(CdpaMetricSplit other);

		// Token: 0x0600604F RID: 24655
		public abstract CdpaMetricSplit Union(CdpaMetricSplit other);

		// Token: 0x06006050 RID: 24656 RVA: 0x00149791 File Offset: 0x00147991
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaMetricSplit);
		}

		// Token: 0x06006051 RID: 24657
		public abstract bool Equals(CdpaMetricSplit other);

		// Token: 0x06006052 RID: 24658
		public abstract override int GetHashCode();
	}
}

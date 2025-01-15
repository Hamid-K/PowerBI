using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB1 RID: 3505
	[DataContract]
	internal abstract class CdpaTimeRange : IIntersectable<CdpaTimeRange>, IUnionable<CdpaTimeRange>
	{
		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x06005F43 RID: 24387
		[DataMember(Name = "timeSpan", IsRequired = true)]
		public abstract string TimeSpanKind { get; }

		// Token: 0x06005F44 RID: 24388
		public abstract TimePartsFilteredTimeIntervalCdpaTimeRange ToTimePartsFilteredTimeInterval();

		// Token: 0x06005F45 RID: 24389 RVA: 0x001484C2 File Offset: 0x001466C2
		public CdpaTimeRange Intersect(CdpaTimeRange other)
		{
			return this.ToTimePartsFilteredTimeInterval().Intersect(other.ToTimePartsFilteredTimeInterval());
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x001484D5 File Offset: 0x001466D5
		public CdpaTimeRange Union(CdpaTimeRange other)
		{
			return this.ToTimePartsFilteredTimeInterval().Union(other.ToTimePartsFilteredTimeInterval());
		}

		// Token: 0x04003469 RID: 13417
		public static readonly DateTime UtcEarliest = new DateTime(100, 1, 1, 0, 0, 0);
	}
}

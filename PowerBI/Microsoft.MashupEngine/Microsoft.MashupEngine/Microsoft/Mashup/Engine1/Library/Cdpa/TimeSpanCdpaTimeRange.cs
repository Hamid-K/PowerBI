using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB2 RID: 3506
	[DataContract]
	internal class TimeSpanCdpaTimeRange : CdpaTimeRange
	{
		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x06005F49 RID: 24393 RVA: 0x001484FB File Offset: 0x001466FB
		[DataMember(Name = "timeSpan", IsRequired = true)]
		public override string TimeSpanKind
		{
			get
			{
				return this.TimeSpan;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x06005F4A RID: 24394 RVA: 0x00148503 File Offset: 0x00146703
		// (set) Token: 0x06005F4B RID: 24395 RVA: 0x0014850B File Offset: 0x0014670B
		public string TimeSpan { get; set; }

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x00148514 File Offset: 0x00146714
		// (set) Token: 0x06005F4D RID: 24397 RVA: 0x0014851C File Offset: 0x0014671C
		[DataMember(Name = "now", IsRequired = false)]
		public DateTime? UtcNow { get; set; }

		// Token: 0x06005F4E RID: 24398 RVA: 0x000033E7 File Offset: 0x000015E7
		public override TimePartsFilteredTimeIntervalCdpaTimeRange ToTimePartsFilteredTimeInterval()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x001484FB File Offset: 0x001466FB
		public override string ToString()
		{
			return this.TimeSpan;
		}
	}
}

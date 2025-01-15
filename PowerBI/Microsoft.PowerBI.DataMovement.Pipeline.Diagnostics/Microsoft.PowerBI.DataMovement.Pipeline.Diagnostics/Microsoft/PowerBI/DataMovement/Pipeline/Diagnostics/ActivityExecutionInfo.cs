using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200000A RID: 10
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class ActivityExecutionInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021EF File Offset: 0x000003EF
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021F7 File Offset: 0x000003F7
		[DataMember(Name = "type")]
		public PipelineActivityType ActivityType { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002208 File Offset: 0x00000408
		[DataMember(Name = "caid")]
		public Guid ClientActivityId { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002211 File Offset: 0x00000411
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002219 File Offset: 0x00000419
		[DataMember(Name = "raid")]
		public Guid RootActivityId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002222 File Offset: 0x00000422
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000222A File Offset: 0x0000042A
		[DataMember(Name = "paid")]
		public Guid ParentActivityId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002233 File Offset: 0x00000433
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000223B File Offset: 0x0000043B
		[DataMember(Name = "aid")]
		public Guid ActivityId { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002244 File Offset: 0x00000444
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000224C File Offset: 0x0000044C
		[DataMember(Name = "res")]
		public ActivityResult Result { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002255 File Offset: 0x00000455
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000225D File Offset: 0x0000045D
		[DataMember(Name = "start")]
		public DateTime StartTime { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002266 File Offset: 0x00000466
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000226E File Offset: 0x0000046E
		[DataMember(Name = "dur")]
		public TimeSpan Duration { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002277 File Offset: 0x00000477
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000227F File Offset: 0x0000047F
		[DataMember(Name = "err")]
		public string Error { get; set; }
	}
}

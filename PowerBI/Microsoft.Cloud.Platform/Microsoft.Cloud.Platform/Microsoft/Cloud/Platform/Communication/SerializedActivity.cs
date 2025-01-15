using System;
using System.Runtime.Serialization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D6 RID: 1238
	[DataContract]
	public sealed class SerializedActivity
	{
		// Token: 0x0600259C RID: 9628 RVA: 0x000858BE File Offset: 0x00083ABE
		private SerializedActivity(bool isEmpty, Guid activityId, string activityTypeShortName, Guid rootActivityId, string clientActivityId)
		{
			this.IsEmpty = isEmpty;
			this.ActivityId = activityId;
			this.ActivityTypeShortName = activityTypeShortName;
			this.RootActivityId = rootActivityId;
			this.ClientActivityId = clientActivityId;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000858EB File Offset: 0x00083AEB
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x000858F3 File Offset: 0x00083AF3
		[DataMember]
		public bool IsEmpty { get; private set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000858FC File Offset: 0x00083AFC
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x00085904 File Offset: 0x00083B04
		[DataMember]
		public Guid ActivityId { get; private set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x0008590D File Offset: 0x00083B0D
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x00085915 File Offset: 0x00083B15
		[DataMember]
		public string ActivityTypeShortName { get; private set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0008591E File Offset: 0x00083B1E
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x00085926 File Offset: 0x00083B26
		[DataMember]
		public Guid RootActivityId { get; private set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0008592F File Offset: 0x00083B2F
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x00085937 File Offset: 0x00083B37
		[DataMember]
		public string ClientActivityId { get; private set; }

		// Token: 0x060025A7 RID: 9639 RVA: 0x00085940 File Offset: 0x00083B40
		public static SerializedActivity FromActivity(Activity activity)
		{
			return new SerializedActivity(activity.Equals(Activity.Empty), activity.ActivityId, activity.ActivityType.ShortName, activity.RootActivityId, activity.ClientActivityId);
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0008596F File Offset: 0x00083B6F
		public Activity ToActivity()
		{
			if (this.IsEmpty)
			{
				return Activity.Empty;
			}
			return new Activity(this.ActivityId, new ActivityType(this.ActivityTypeShortName), this.RootActivityId, this.ClientActivityId);
		}
	}
}

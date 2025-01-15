using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000169 RID: 361
	[CannotApplyEqualityOperator]
	public class Activity : IEquatable<Activity>
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00020567 File Offset: 0x0001E767
		public Activity(Guid activityId, ActivityType activityType)
			: this(activityId, activityType, Guid.Empty)
		{
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00020576 File Offset: 0x0001E776
		public Activity(Guid activityId, ActivityType activityType, Guid rootActivityId)
			: this(activityId, activityType, rootActivityId, Activity.s_emptyGuid)
		{
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00020586 File Offset: 0x0001E786
		public Activity(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			this.Set(activityId, activityType ?? ActivityType.Empty, rootActivityId, string.IsNullOrWhiteSpace(clientActivityId) ? Activity.s_emptyGuid : clientActivityId);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000205B3 File Offset: 0x0001E7B3
		public Activity Set(Guid activityId, ActivityType activityType, Guid rootActivityId, string clientActivityId)
		{
			this.ActivityId = activityId;
			this.ActivityType = activityType ?? ActivityType.Empty;
			this.RootActivityId = rootActivityId;
			this.ClientActivityId = (string.IsNullOrWhiteSpace(clientActivityId) ? Activity.s_emptyGuid : clientActivityId);
			return this;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000205EC File Offset: 0x0001E7EC
		protected Activity(SerializationInfo info, StreamingContext context)
		{
			object value = info.GetValue("Microsoft.Cloud.Platform.Utils.ActivityId", typeof(Guid));
			ExtendedDiagnostics.EnsureNotNull<object>(value, "Microsoft.Cloud.Platform.Utils.ActivityId");
			this.ActivityId = (Guid)value;
			object value2 = info.GetValue("Microsoft.Cloud.Platform.Utils.ActivityType", typeof(int));
			ExtendedDiagnostics.EnsureNotNull<object>(value2, "Microsoft.Cloud.Platform.Utils.ActivityType");
			this.ActivityType = new ActivityType((int)value2);
			object value3 = info.GetValue("Microsoft.Cloud.Platform.Utils.RootActivityId", typeof(Guid));
			ExtendedDiagnostics.EnsureNotNull<object>(value3, "Microsoft.Cloud.Platform.Utils.RootActivityId");
			this.RootActivityId = (Guid)value3;
			object value4 = info.GetValue("Microsoft.Cloud.Platform.Utils.ClientActivityId", typeof(string));
			ExtendedDiagnostics.EnsureNotNull<object>(value4, "Microsoft.Cloud.Platform.Utils.ClientActivityId");
			this.ClientActivityId = (string)value4;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000206B8 File Offset: 0x0001E8B8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Microsoft.Cloud.Platform.Utils.ActivityId", this.ActivityId, typeof(Guid));
			info.AddValue("Microsoft.Cloud.Platform.Utils.ActivityType", this.ActivityType.ShortNameId, typeof(int));
			info.AddValue("Microsoft.Cloud.Platform.Utils.RootActivityId", this.RootActivityId, typeof(Guid));
			info.AddValue("Microsoft.Cloud.Platform.Utils.ClientActivityId", this.ClientActivityId, typeof(string));
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00020745 File Offset: 0x0001E945
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x0002074D File Offset: 0x0001E94D
		public Guid ActivityId { get; protected set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00020756 File Offset: 0x0001E956
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x0002075E File Offset: 0x0001E95E
		public ActivityType ActivityType { get; protected set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x00020767 File Offset: 0x0001E967
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x0002076F File Offset: 0x0001E96F
		public Guid RootActivityId { get; internal set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00020778 File Offset: 0x0001E978
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00020780 File Offset: 0x0001E980
		public string ClientActivityId { get; internal set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00020789 File Offset: 0x0001E989
		public static Activity Empty
		{
			get
			{
				return Activity.s_emptyActivity;
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00020790 File Offset: 0x0001E990
		public virtual bool Equals(Activity other)
		{
			return other != null && (this == other || (this.ActivityId.Equals(other.ActivityId) && this.ActivityType.Equals(other.ActivityType) && this.RootActivityId.Equals(other.RootActivityId) && this.ClientActivityId.Equals(other.ClientActivityId, StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00020804 File Offset: 0x0001EA04
		public override int GetHashCode()
		{
			return this.ActivityId.GetHashCode();
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00020825 File Offset: 0x0001EA25
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Activity);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00020833 File Offset: 0x0001EA33
		public override string ToString()
		{
			return "Activity Id: {0}, Activity Type: {1}, Root Activity Id: {2}, Client Activity Id: {3}".FormatWithInvariantCulture(new object[] { this.ActivityId, this.ActivityType, this.RootActivityId, this.ClientActivityId });
		}

		// Token: 0x0400038E RID: 910
		private const string c_activityId = "Microsoft.Cloud.Platform.Utils.ActivityId";

		// Token: 0x0400038F RID: 911
		private const string c_activityType = "Microsoft.Cloud.Platform.Utils.ActivityType";

		// Token: 0x04000390 RID: 912
		private const string c_rootActivityId = "Microsoft.Cloud.Platform.Utils.RootActivityId";

		// Token: 0x04000391 RID: 913
		private const string c_clientActivityId = "Microsoft.Cloud.Platform.Utils.ClientActivityId";

		// Token: 0x04000392 RID: 914
		private static readonly Activity s_emptyActivity = new Activity(Guid.Empty, ActivityType.Empty, Guid.Empty, Guid.Empty.ToString());

		// Token: 0x04000393 RID: 915
		private static readonly string s_emptyGuid = Guid.Empty.ToString();
	}
}

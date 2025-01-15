using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ActivityInfo
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002290 File Offset: 0x00000490
		public ActivityInfo(string shortName, Guid clientActivityId, Guid rootActivityId, Guid activityId, Guid rootGatewayClientPipelineId = default(Guid), Guid currentGatewayClientPipelineId = default(Guid))
		{
			this.ShortName = shortName;
			this.ClientActivityId = clientActivityId;
			this.RootActivityId = rootActivityId;
			this.ActivityId = activityId;
			this.RootGatewayClientPipelineId = rootGatewayClientPipelineId;
			this.CurrentGatewayClientPipelineId = currentGatewayClientPipelineId;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022C5 File Offset: 0x000004C5
		public static ActivityInfo Empty
		{
			get
			{
				return ActivityInfo.s_empty;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000022D4 File Offset: 0x000004D4
		public string ShortName { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022DD File Offset: 0x000004DD
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022E5 File Offset: 0x000004E5
		public Guid ClientActivityId { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022EE File Offset: 0x000004EE
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022F6 File Offset: 0x000004F6
		public Guid RootActivityId { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022FF File Offset: 0x000004FF
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002307 File Offset: 0x00000507
		public Guid ActivityId { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002310 File Offset: 0x00000510
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002318 File Offset: 0x00000518
		public Guid RootGatewayClientPipelineId { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002321 File Offset: 0x00000521
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002329 File Offset: 0x00000529
		public Guid CurrentGatewayClientPipelineId { get; private set; }

		// Token: 0x0600002F RID: 47 RVA: 0x00002334 File Offset: 0x00000534
		public static ActivityInfo CreateRoot()
		{
			Guid guid = Guid.NewGuid();
			return new ActivityInfo(ActivityInfo.Empty.ShortName, guid, guid, Guid.Empty, default(Guid), default(Guid));
		}

		// Token: 0x04000029 RID: 41
		private static ActivityInfo s_empty = new ActivityInfo("0000", Guid.Empty, Guid.Empty, Guid.Empty, default(Guid), default(Guid));
	}
}

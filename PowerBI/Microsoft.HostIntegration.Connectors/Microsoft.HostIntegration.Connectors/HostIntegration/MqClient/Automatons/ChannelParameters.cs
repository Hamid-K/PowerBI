using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AD9 RID: 2777
	public class ChannelParameters
	{
		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06005888 RID: 22664 RVA: 0x0016C979 File Offset: 0x0016AB79
		// (set) Token: 0x06005889 RID: 22665 RVA: 0x0016C981 File Offset: 0x0016AB81
		public byte FapLevel { get; private set; }

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x0600588A RID: 22666 RVA: 0x0016C98A File Offset: 0x0016AB8A
		// (set) Token: 0x0600588B RID: 22667 RVA: 0x0016C992 File Offset: 0x0016AB92
		public short MaximumMessageBatch { get; private set; }

		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x0600588C RID: 22668 RVA: 0x0016C99B File Offset: 0x0016AB9B
		// (set) Token: 0x0600588D RID: 22669 RVA: 0x0016C9A3 File Offset: 0x0016ABA3
		public int MaximumTransmissionSize { get; private set; }

		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x0600588E RID: 22670 RVA: 0x0016C9AC File Offset: 0x0016ABAC
		// (set) Token: 0x0600588F RID: 22671 RVA: 0x0016C9B4 File Offset: 0x0016ABB4
		public int MaximumMessageSize { get; private set; }

		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06005890 RID: 22672 RVA: 0x0016C9BD File Offset: 0x0016ABBD
		// (set) Token: 0x06005891 RID: 22673 RVA: 0x0016C9C5 File Offset: 0x0016ABC5
		public int SequenceNumberWrap { get; private set; }

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06005892 RID: 22674 RVA: 0x0016C9CE File Offset: 0x0016ABCE
		// (set) Token: 0x06005893 RID: 22675 RVA: 0x0016C9D6 File Offset: 0x0016ABD6
		public int HeartBeatInterval { get; private set; }

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06005894 RID: 22676 RVA: 0x0016C9DF File Offset: 0x0016ABDF
		// (set) Token: 0x06005895 RID: 22677 RVA: 0x0016C9E7 File Offset: 0x0016ABE7
		public CapabilityFlag2 CapabilityFlag2 { get; private set; }

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06005896 RID: 22678 RVA: 0x0016C9F0 File Offset: 0x0016ABF0
		// (set) Token: 0x06005897 RID: 22679 RVA: 0x0016C9F8 File Offset: 0x0016ABF8
		public int NumberOfConversationsPerSocket { get; private set; }

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06005898 RID: 22680 RVA: 0x0016CA01 File Offset: 0x0016AC01
		// (set) Token: 0x06005899 RID: 22681 RVA: 0x0016CA09 File Offset: 0x0016AC09
		public int UsedMaximumTransmissionSize { get; private set; }

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x0600589A RID: 22682 RVA: 0x0016CA12 File Offset: 0x0016AC12
		// (set) Token: 0x0600589B RID: 22683 RVA: 0x0016CA1A File Offset: 0x0016AC1A
		public string MqmId { get; private set; }

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x0600589C RID: 22684 RVA: 0x0016CA23 File Offset: 0x0016AC23
		// (set) Token: 0x0600589D RID: 22685 RVA: 0x0016CA2B File Offset: 0x0016AC2B
		public bool NoDelay { get; private set; }

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x0600589E RID: 22686 RVA: 0x0016CA34 File Offset: 0x0016AC34
		// (set) Token: 0x0600589F RID: 22687 RVA: 0x0016CA3C File Offset: 0x0016AC3C
		public short Ccsid { get; private set; }

		// Token: 0x060058A0 RID: 22688 RVA: 0x0016CA48 File Offset: 0x0016AC48
		internal ChannelParameters(AutomatonQueueManagerContext context)
		{
			this.FapLevel = context.FapLevel;
			this.MaximumMessageBatch = context.MaximumMessageBatch;
			this.MaximumTransmissionSize = context.MaximumTransmissionSize;
			this.MaximumMessageSize = context.MaximumMessageSize;
			this.SequenceNumberWrap = context.SequenceNumberWrap;
			this.HeartBeatInterval = context.HeartBeatInterval;
			this.CapabilityFlag2 = context.CapabilityFlag2;
			this.NumberOfConversationsPerSocket = context.NumberOfConversationsPerSocket;
			this.UsedMaximumTransmissionSize = context.UsedMaximumTransmissionSize;
			this.MqmId = context.MqmId;
			this.NoDelay = (context.CapabilityFlag2 & CapabilityFlag2.FastMessageRequest) == CapabilityFlag2.FastMessageRequest;
			this.Ccsid = context.ServerCcsid;
		}

		// Token: 0x060058A1 RID: 22689 RVA: 0x0016CAF0 File Offset: 0x0016ACF0
		internal void FillContext(AutomatonQueueManagerContext context)
		{
			context.FapLevel = this.FapLevel;
			context.MaximumMessageBatch = this.MaximumMessageBatch;
			context.MaximumTransmissionSize = this.MaximumTransmissionSize;
			context.MaximumMessageSize = this.MaximumMessageSize;
			context.SequenceNumberWrap = this.SequenceNumberWrap;
			context.HeartBeatInterval = this.HeartBeatInterval;
			context.CapabilityFlag2 = this.CapabilityFlag2;
			context.NumberOfConversationsPerSocket = this.NumberOfConversationsPerSocket;
			context.UsedMaximumTransmissionSize = this.UsedMaximumTransmissionSize;
			context.MqmId = this.MqmId;
			context.ServerCcsid = this.Ccsid;
			if (this.NoDelay)
			{
				context.ServerCapabilityFlag2 |= CapabilityFlag2.FastMessageRequest;
			}
		}
	}
}

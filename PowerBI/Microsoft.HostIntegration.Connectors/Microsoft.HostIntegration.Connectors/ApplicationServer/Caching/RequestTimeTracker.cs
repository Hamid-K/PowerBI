using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200039C RID: 924
	[DataContract]
	internal class RequestTimeTracker : IRequestTracker
	{
		// Token: 0x060020C0 RID: 8384 RVA: 0x0006402D File Offset: 0x0006222D
		internal void GatewayStart()
		{
			this._gatewayStartTime = Stopwatch.GetTimestamp();
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0006403C File Offset: 0x0006223C
		internal void GatewayStop()
		{
			long timestamp = Stopwatch.GetTimestamp();
			this._timeSpentInGateway = TimeSpan.FromTicks(Utility.StopwatchTicksToSystemTicks(timestamp - this._gatewayStartTime));
			this._timeSpentInGatewayBeforeSendToClient = TimeSpan.FromTicks(Utility.StopwatchTicksToSystemTicks(timestamp - this._gatewayReceiveFromServer));
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0006407F File Offset: 0x0006227F
		internal void GatewayBeforeSendToPrimary()
		{
			this._timeSpentInGatewayBeforeSendToPrimary = TimeSpan.FromTicks(Utility.StopwatchTicksToSystemTicks(Stopwatch.GetTimestamp() - this._gatewayStartTime));
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000640A0 File Offset: 0x000622A0
		internal void GatewayAfterSendToPrimary()
		{
			TimeSpan timeSpan = TimeSpan.FromTicks(Utility.StopwatchTicksToSystemTicks(Stopwatch.GetTimestamp() - this._gatewayStartTime));
			this._timeSpentInGatewayAfterSendToPrimary = timeSpan - this._timeSpentInGatewayBeforeSendToPrimary;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000640D6 File Offset: 0x000622D6
		internal void GatewayReceiveFromPrimary()
		{
			this._gatewayReceiveFromServer = Stopwatch.GetTimestamp();
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000640E4 File Offset: 0x000622E4
		public override string ToString()
		{
			TimeSpan timeSpan = this.ClientEndToEnd - this.GatewayEndToEnd;
			TimeSpan timeSpan2 = ((this.PrimaryEndToEnd != TimeSpan.MaxValue) ? (this.GatewayEndToEnd - this.PrimaryEndToEnd) : TimeSpan.MaxValue);
			return string.Format(CultureInfo.CurrentCulture, "ClientEndToEnd {0}, GatewayEndToEnd {1}, PrimaryEndToEnd {2}, ClientToGatewayNetworkTime {3}, GatewayToPrimaryNetworkTime {4}, GatewayLatency {5}, GatewayLatencyAfterSend {6}, GatewayReturnLatency {7}, GatewayId {8}, GatewayDisplayFriendlyId {9}, PrimaryId {10}, PrimaryDisplayFriendlyId {11}", new object[]
			{
				this.ClientEndToEnd, this.GatewayEndToEnd, this.PrimaryEndToEnd, timeSpan, timeSpan2, this.GatewayLatency, this.GatewayLatencyAfterSend, this.GatewayReturnLatency, this.GatewayId, this.GatewayDisplayFriendlyId,
				this.PrimaryId, this.PrimaryDisplayFriendlyId
			});
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x000641D3 File Offset: 0x000623D3
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x000641DB File Offset: 0x000623DB
		public TimeSpan ClientEndToEnd
		{
			get
			{
				return this._timeSpentInClient;
			}
			internal set
			{
				this._timeSpentInClient = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x000641E4 File Offset: 0x000623E4
		public TimeSpan GatewayEndToEnd
		{
			get
			{
				return this._timeSpentInGateway;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x000641EC File Offset: 0x000623EC
		public TimeSpan GatewayLatency
		{
			get
			{
				return this._timeSpentInGatewayBeforeSendToPrimary;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000641F4 File Offset: 0x000623F4
		public TimeSpan GatewayLatencyAfterSend
		{
			get
			{
				return this._timeSpentInGatewayAfterSendToPrimary;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000641FC File Offset: 0x000623FC
		public TimeSpan GatewayReturnLatency
		{
			get
			{
				return this._timeSpentInGatewayBeforeSendToClient;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00064204 File Offset: 0x00062404
		public TimeSpan PrimaryEndToEnd
		{
			get
			{
				if (this.PrimaryTracker != null)
				{
					return this.PrimaryTracker.TimeSpentInPrimary;
				}
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0006421F File Offset: 0x0006241F
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x00064227 File Offset: 0x00062427
		public string GatewayId
		{
			get
			{
				return this._gatewayId;
			}
			set
			{
				this._gatewayId = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x00064230 File Offset: 0x00062430
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x00064238 File Offset: 0x00062438
		public string GatewayDisplayFriendlyId
		{
			get
			{
				return this.gatewayDisplayFriendlyId;
			}
			set
			{
				this.gatewayDisplayFriendlyId = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00064241 File Offset: 0x00062441
		public string PrimaryId
		{
			get
			{
				if (this.PrimaryTracker != null)
				{
					return this.PrimaryTracker.PrimaryId;
				}
				return null;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00064258 File Offset: 0x00062458
		public string PrimaryDisplayFriendlyId
		{
			get
			{
				if (this.PrimaryTracker != null)
				{
					return this.PrimaryTracker.DisplayFriendlyNodeId;
				}
				return null;
			}
		}

		// Token: 0x04001326 RID: 4902
		private long _gatewayStartTime;

		// Token: 0x04001327 RID: 4903
		private long _gatewayReceiveFromServer;

		// Token: 0x04001328 RID: 4904
		[DataMember]
		private TimeSpan _timeSpentInGateway;

		// Token: 0x04001329 RID: 4905
		[DataMember]
		private TimeSpan _timeSpentInGatewayBeforeSendToPrimary;

		// Token: 0x0400132A RID: 4906
		[DataMember]
		private TimeSpan _timeSpentInGatewayAfterSendToPrimary;

		// Token: 0x0400132B RID: 4907
		[DataMember]
		private TimeSpan _timeSpentInGatewayBeforeSendToClient;

		// Token: 0x0400132C RID: 4908
		private TimeSpan _timeSpentInClient;

		// Token: 0x0400132D RID: 4909
		[DataMember]
		private string _gatewayId;

		// Token: 0x0400132E RID: 4910
		[DataMember]
		private string gatewayDisplayFriendlyId;

		// Token: 0x0400132F RID: 4911
		[DataMember]
		internal RequestTrackerOnPrimary PrimaryTracker;
	}
}

using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B7 RID: 695
	internal class RequestReplyState
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x0004BB5C File Offset: 0x00049D5C
		internal Message ResponseMessage
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600197D RID: 6525 RVA: 0x0004BB64 File Offset: 0x00049D64
		internal EndpointID RemoteAddress
		{
			get
			{
				return this._remoteAddress;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x0004BB6C File Offset: 0x00049D6C
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x0004BB74 File Offset: 0x00049D74
		internal bool InTransit
		{
			get
			{
				return this._inTransit;
			}
			set
			{
				this._inTransit = value;
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0004BB7D File Offset: 0x00049D7D
		internal RequestReplyState(EndpointID endpoint)
		{
			this._handle = new LightWeightEventMonitorBased();
			this._remoteAddress = endpoint;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0004BB97 File Offset: 0x00049D97
		internal Message Wait(TimeSpan timeout)
		{
			this._handle.WaitOne(timeout);
			return this._message;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0004BBAC File Offset: 0x00049DAC
		internal void ProcessResponse(Message message)
		{
			this._message = message;
			this._inTransit = false;
			this._handle.Set();
		}

		// Token: 0x04000DC7 RID: 3527
		private Message _message;

		// Token: 0x04000DC8 RID: 3528
		private LightWeightEventMonitorBased _handle;

		// Token: 0x04000DC9 RID: 3529
		private EndpointID _remoteAddress;

		// Token: 0x04000DCA RID: 3530
		private bool _inTransit;
	}
}

using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000027 RID: 39
	internal sealed class PollerRequestContext
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00006B88 File Offset: 0x00004D88
		public PollerRequestContext(EndpointID endpoint, NotificationRequest request, CountDownLatch latch)
		{
			this._endpoint = endpoint;
			this._notificationRequest = request;
			this._countDownLatch = latch;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006BA5 File Offset: 0x00004DA5
		internal EndpointID Endpoint
		{
			get
			{
				return this._endpoint;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006BAD File Offset: 0x00004DAD
		public CountDownLatch CountDownLatch
		{
			get
			{
				return this._countDownLatch;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006BB5 File Offset: 0x00004DB5
		public NotificationRequest Request
		{
			get
			{
				return this._notificationRequest;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006BBD File Offset: 0x00004DBD
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006BC5 File Offset: 0x00004DC5
		public NotificationReply Reply { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006BCE File Offset: 0x00004DCE
		public bool IsReplyAvailable
		{
			get
			{
				return this.Reply != null;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006BDC File Offset: 0x00004DDC
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public ErrStatus ResponseCode { get; set; }

		// Token: 0x040000A4 RID: 164
		private readonly EndpointID _endpoint;

		// Token: 0x040000A5 RID: 165
		private readonly NotificationRequest _notificationRequest;

		// Token: 0x040000A6 RID: 166
		private readonly CountDownLatch _countDownLatch;
	}
}

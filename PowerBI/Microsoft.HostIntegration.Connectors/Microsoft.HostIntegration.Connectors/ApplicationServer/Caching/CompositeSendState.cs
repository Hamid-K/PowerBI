using System;
using System.ServiceModel.Channels;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A8 RID: 680
	internal class CompositeSendState
	{
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0004AF68 File Offset: 0x00049168
		// (set) Token: 0x060018E8 RID: 6376 RVA: 0x0004AF70 File Offset: 0x00049170
		public Message Message { get; private set; }

		// Token: 0x060018E9 RID: 6377 RVA: 0x0004AF79 File Offset: 0x00049179
		internal CompositeSendState(WaitCallback callback, object state, IDuplexSessionChannel channel, EndpointID endpoint, Message message)
		{
			this._callback = callback;
			this._state = state;
			this._channel = channel;
			this._endpoint = endpoint;
			this.Message = message;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0004AFA6 File Offset: 0x000491A6
		internal WaitCallback Callback
		{
			get
			{
				return this._callback;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0004AFAE File Offset: 0x000491AE
		internal object State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0004AFB6 File Offset: 0x000491B6
		internal IDuplexSessionChannel Channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0004AFBE File Offset: 0x000491BE
		internal EndpointID Endpoint
		{
			get
			{
				return this._endpoint;
			}
		}

		// Token: 0x04000D90 RID: 3472
		private WaitCallback _callback;

		// Token: 0x04000D91 RID: 3473
		private object _state;

		// Token: 0x04000D92 RID: 3474
		private IDuplexSessionChannel _channel;

		// Token: 0x04000D93 RID: 3475
		private EndpointID _endpoint;
	}
}

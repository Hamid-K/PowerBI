using System;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200044E RID: 1102
	internal class TcpInputSession : SafeCommunicationObject<IDuplexSessionChannel>, IInputSession, IInputConnection, ISession, ITransportConnection, ITransportObject
	{
		// Token: 0x06002684 RID: 9860 RVA: 0x00076059 File Offset: 0x00074259
		public TcpInputSession(IDuplexSessionChannel channel)
			: base(channel)
		{
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x00076062 File Offset: 0x00074262
		private IDuplexSessionChannel Channel
		{
			get
			{
				return base.WCFObject;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x0007606A File Offset: 0x0007426A
		public Uri RemoteAddress
		{
			get
			{
				return this.Channel.Via;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002687 RID: 9863 RVA: 0x00076077 File Offset: 0x00074277
		public Uri LocalAddress
		{
			get
			{
				return this.Channel.LocalAddress.Uri;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x00076089 File Offset: 0x00074289
		public string Id
		{
			get
			{
				return this.Channel.Session.Id;
			}
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0007609B File Offset: 0x0007429B
		public IInputContext Receive()
		{
			return this.Receive(TimeSpan.MaxValue);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x000760A8 File Offset: 0x000742A8
		public IInputContext Receive(TimeSpan timeout)
		{
			return this.EndReceive(this.BeginReceive(timeout, null, null));
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x000760B9 File Offset: 0x000742B9
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000760C8 File Offset: 0x000742C8
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.Channel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000760D8 File Offset: 0x000742D8
		public IInputContext EndReceive(IAsyncResult ar)
		{
			Message message;
			try
			{
				message = this.Channel.EndReceive(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				throw;
			}
			if (message == null)
			{
				return null;
			}
			return new InputContext(message, this, this.Channel);
		}
	}
}

using System;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000447 RID: 1095
	internal class TcpListener : SafeCommunicationObject<IChannelListener<IDuplexSessionChannel>>, ISessionListener, IListener, ITransportObject
	{
		// Token: 0x06002654 RID: 9812 RVA: 0x000757CF File Offset: 0x000739CF
		public TcpListener(IChannelListener<IDuplexSessionChannel> channelListener)
			: base(channelListener)
		{
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x000757D8 File Offset: 0x000739D8
		private IChannelListener<IDuplexSessionChannel> Listener
		{
			get
			{
				return base.WCFObject;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x000757E0 File Offset: 0x000739E0
		public Uri ListenAddress
		{
			get
			{
				return this.Listener.Uri;
			}
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000757ED File Offset: 0x000739ED
		public IInputSession Accept()
		{
			return this.Accept(TimeSpan.MaxValue);
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000757FA File Offset: 0x000739FA
		public IInputSession Accept(TimeSpan timeout)
		{
			return this.EndAccept(this.BeginAccept(timeout, null, null));
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x0007580B File Offset: 0x00073A0B
		public IAsyncResult BeginAccept(AsyncCallback callback, object state)
		{
			return this.BeginAccept(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0007581A File Offset: 0x00073A1A
		public IAsyncResult BeginAccept(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.Listener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x0007582C File Offset: 0x00073A2C
		public IInputSession EndAccept(IAsyncResult ar)
		{
			IDuplexSessionChannel duplexSessionChannel;
			try
			{
				duplexSessionChannel = this.Listener.EndAcceptChannel(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsException<TimeoutException>(ex) && !base.IsDead)
				{
					base.Fault(ex);
				}
				throw;
			}
			if (duplexSessionChannel == null)
			{
				return null;
			}
			return new TcpInputSession(duplexSessionChannel);
		}
	}
}

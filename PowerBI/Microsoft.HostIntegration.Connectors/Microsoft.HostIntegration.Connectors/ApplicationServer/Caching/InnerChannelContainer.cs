using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002B1 RID: 689
	internal class InnerChannelContainer : IChannelContainer
	{
		// Token: 0x06001942 RID: 6466 RVA: 0x0004B414 File Offset: 0x00049614
		internal InnerChannelContainer(IDuplexSessionChannel channel)
		{
			this._channel = channel;
			this._authorization = RemoteAuthorization.Unauthorized;
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x0004B42A File Offset: 0x0004962A
		internal bool IsUnauthorized
		{
			get
			{
				return this._authorization == RemoteAuthorization.Unauthorized;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x0004B435 File Offset: 0x00049635
		public IDuplexSessionChannel Channel
		{
			get
			{
				return this._channel;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0004B43D File Offset: 0x0004963D
		// (set) Token: 0x06001946 RID: 6470 RVA: 0x0004B445 File Offset: 0x00049645
		public RemoteAuthorization Authorization
		{
			get
			{
				return this._authorization;
			}
			set
			{
				this._authorization = value;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x0004B44E File Offset: 0x0004964E
		// (set) Token: 0x06001948 RID: 6472 RVA: 0x0004B456 File Offset: 0x00049656
		public ClientVersionInfo RemoteVersionInfo
		{
			get
			{
				return this._remoteVersionInfo;
			}
			set
			{
				this._remoteVersionInfo = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0004B45F File Offset: 0x0004965F
		CacheConnectionProperty IChannelContainer.ConnectionProperty
		{
			get
			{
				if (this.Channel != null)
				{
					return this.Channel.GetProperty<CacheConnectionProperty>();
				}
				return null;
			}
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0004B478 File Offset: 0x00049678
		T IChannelContainer.GetProperty<T>()
		{
			if (typeof(T) == typeof(ClientVersionInfo))
			{
				return (T)((object)this.RemoteVersionInfo);
			}
			if (typeof(T) == typeof(RemoteAuthorization))
			{
				return (T)((object)this.Authorization);
			}
			if (this.Channel != null)
			{
				return this.Channel.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x04000DA9 RID: 3497
		private readonly IDuplexSessionChannel _channel;

		// Token: 0x04000DAA RID: 3498
		private RemoteAuthorization _authorization;

		// Token: 0x04000DAB RID: 3499
		private ClientVersionInfo _remoteVersionInfo;
	}
}

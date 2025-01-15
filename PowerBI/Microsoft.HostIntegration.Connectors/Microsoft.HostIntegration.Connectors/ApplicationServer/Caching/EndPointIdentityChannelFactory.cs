using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002AC RID: 684
	internal class EndPointIdentityChannelFactory<TChannel> : IChannelFactory<TChannel>, IChannelFactory, ICommunicationObject
	{
		// Token: 0x0600190B RID: 6411 RVA: 0x0004B22F File Offset: 0x0004942F
		public EndPointIdentityChannelFactory(IChannelFactory<TChannel> innerFactory, IEndpointIdentityProvider endpointIdentityProvider)
		{
			if (innerFactory == null)
			{
				throw new ArgumentNullException("innerFactory");
			}
			if (endpointIdentityProvider == null)
			{
				throw new ArgumentNullException("endpointIdentityProvider");
			}
			this._innerFactory = innerFactory;
			this._endpointIdentityProvider = endpointIdentityProvider;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0004B264 File Offset: 0x00049464
		private EndpointAddress ApplyEndPointIdentity(EndpointAddress to)
		{
			Uri uri = to.Uri;
			EndpointIdentity endpointIdentity = this._endpointIdentityProvider.GetEndpointIdentity(uri.Host, uri.Port);
			AddressHeaderCollection headers = to.Headers;
			to = new EndpointAddress(uri, endpointIdentity, headers);
			return to;
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0004B2A2 File Offset: 0x000494A2
		TChannel IChannelFactory<TChannel>.CreateChannel(EndpointAddress to, Uri via)
		{
			to = this.ApplyEndPointIdentity(to);
			return this._innerFactory.CreateChannel(to, via);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0004B2BA File Offset: 0x000494BA
		TChannel IChannelFactory<TChannel>.CreateChannel(EndpointAddress to)
		{
			to = this.ApplyEndPointIdentity(to);
			return this._innerFactory.CreateChannel(to);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0004B2D1 File Offset: 0x000494D1
		T IChannelFactory.GetProperty<T>()
		{
			return this._innerFactory.GetProperty<T>();
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0004B2DE File Offset: 0x000494DE
		void ICommunicationObject.Abort()
		{
			this._innerFactory.Abort();
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x0004B2EB File Offset: 0x000494EB
		IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x0004B2FB File Offset: 0x000494FB
		IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginClose(callback, state);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0004B30A File Offset: 0x0004950A
		IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0004B31A File Offset: 0x0004951A
		IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginOpen(callback, state);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0004B329 File Offset: 0x00049529
		void ICommunicationObject.Close(TimeSpan timeout)
		{
			this._innerFactory.Close(timeout);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0004B337 File Offset: 0x00049537
		void ICommunicationObject.Close()
		{
			this._innerFactory.Close();
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001917 RID: 6423 RVA: 0x0004B344 File Offset: 0x00049544
		// (remove) Token: 0x06001918 RID: 6424 RVA: 0x0004B352 File Offset: 0x00049552
		event EventHandler ICommunicationObject.Closed
		{
			add
			{
				this._innerFactory.Closed += value;
			}
			remove
			{
				this._innerFactory.Closed -= value;
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001919 RID: 6425 RVA: 0x0004B360 File Offset: 0x00049560
		// (remove) Token: 0x0600191A RID: 6426 RVA: 0x0004B36E File Offset: 0x0004956E
		event EventHandler ICommunicationObject.Closing
		{
			add
			{
				this._innerFactory.Closing += value;
			}
			remove
			{
				this._innerFactory.Closing -= value;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0004B37C File Offset: 0x0004957C
		void ICommunicationObject.EndClose(IAsyncResult result)
		{
			this._innerFactory.EndClose(result);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0004B38A File Offset: 0x0004958A
		void ICommunicationObject.EndOpen(IAsyncResult result)
		{
			this._innerFactory.EndOpen(result);
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600191D RID: 6429 RVA: 0x0004B398 File Offset: 0x00049598
		// (remove) Token: 0x0600191E RID: 6430 RVA: 0x0004B3A6 File Offset: 0x000495A6
		event EventHandler ICommunicationObject.Faulted
		{
			add
			{
				this._innerFactory.Faulted += value;
			}
			remove
			{
				this._innerFactory.Faulted -= value;
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0004B3B4 File Offset: 0x000495B4
		void ICommunicationObject.Open(TimeSpan timeout)
		{
			this._innerFactory.Open(timeout);
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0004B3C2 File Offset: 0x000495C2
		void ICommunicationObject.Open()
		{
			this._innerFactory.Open();
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001921 RID: 6433 RVA: 0x0004B3CF File Offset: 0x000495CF
		// (remove) Token: 0x06001922 RID: 6434 RVA: 0x0004B3DD File Offset: 0x000495DD
		event EventHandler ICommunicationObject.Opened
		{
			add
			{
				this._innerFactory.Opened += value;
			}
			remove
			{
				this._innerFactory.Opened -= value;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001923 RID: 6435 RVA: 0x0004B3EB File Offset: 0x000495EB
		// (remove) Token: 0x06001924 RID: 6436 RVA: 0x0004B3F9 File Offset: 0x000495F9
		event EventHandler ICommunicationObject.Opening
		{
			add
			{
				this._innerFactory.Opening += value;
			}
			remove
			{
				this._innerFactory.Opening -= value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x0004B407 File Offset: 0x00049607
		CommunicationState ICommunicationObject.State
		{
			get
			{
				return this._innerFactory.State;
			}
		}

		// Token: 0x04000DA3 RID: 3491
		private IChannelFactory<TChannel> _innerFactory;

		// Token: 0x04000DA4 RID: 3492
		private IEndpointIdentityProvider _endpointIdentityProvider;
	}
}

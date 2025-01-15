using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200029F RID: 671
	internal class CacheResolverChannelFactory<TChannel> : IChannelFactory<TChannel>, IChannelFactory, ICommunicationObject
	{
		// Token: 0x0600188E RID: 6286 RVA: 0x0004A421 File Offset: 0x00048621
		public CacheResolverChannelFactory(IChannelFactory<TChannel> innerFactory, DataCacheSecurity dataCacheSecurity)
		{
			if (innerFactory == null)
			{
				throw new ArgumentNullException("innerFactory");
			}
			this._innerFactory = innerFactory;
			this._dataCacheSecurity = dataCacheSecurity;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0004A448 File Offset: 0x00048648
		TChannel IChannelFactory<TChannel>.CreateChannel(EndpointAddress to, Uri via)
		{
			TChannel tchannel = this._innerFactory.CreateChannel(to, via);
			return (TChannel)((object)new CacheResolverChannel(CacheResolverCommunicatorType.Client, (IDuplexSessionChannel)((object)tchannel), this._dataCacheSecurity));
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0004A480 File Offset: 0x00048680
		TChannel IChannelFactory<TChannel>.CreateChannel(EndpointAddress to)
		{
			TChannel tchannel = this._innerFactory.CreateChannel(to);
			return (TChannel)((object)new CacheResolverChannel(CacheResolverCommunicatorType.Client, (IDuplexSessionChannel)((object)tchannel), this._dataCacheSecurity));
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0004A4B6 File Offset: 0x000486B6
		T IChannelFactory.GetProperty<T>()
		{
			return this._innerFactory.GetProperty<T>();
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0004A4C3 File Offset: 0x000486C3
		void ICommunicationObject.Abort()
		{
			this._innerFactory.Abort();
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0004A4D0 File Offset: 0x000486D0
		IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginClose(timeout, callback, state);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0004A4E0 File Offset: 0x000486E0
		IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginClose(callback, state);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0004A4EF File Offset: 0x000486EF
		IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0004A4FF File Offset: 0x000486FF
		IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
		{
			return this._innerFactory.BeginOpen(callback, state);
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0004A50E File Offset: 0x0004870E
		void ICommunicationObject.Close(TimeSpan timeout)
		{
			this._innerFactory.Close(timeout);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0004A51C File Offset: 0x0004871C
		void ICommunicationObject.Close()
		{
			this._innerFactory.Close();
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001899 RID: 6297 RVA: 0x0004A529 File Offset: 0x00048729
		// (remove) Token: 0x0600189A RID: 6298 RVA: 0x0004A537 File Offset: 0x00048737
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

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600189B RID: 6299 RVA: 0x0004A545 File Offset: 0x00048745
		// (remove) Token: 0x0600189C RID: 6300 RVA: 0x0004A553 File Offset: 0x00048753
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

		// Token: 0x0600189D RID: 6301 RVA: 0x0004A561 File Offset: 0x00048761
		void ICommunicationObject.EndClose(IAsyncResult result)
		{
			this._innerFactory.EndClose(result);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0004A56F File Offset: 0x0004876F
		void ICommunicationObject.EndOpen(IAsyncResult result)
		{
			this._innerFactory.EndOpen(result);
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x0600189F RID: 6303 RVA: 0x0004A57D File Offset: 0x0004877D
		// (remove) Token: 0x060018A0 RID: 6304 RVA: 0x0004A58B File Offset: 0x0004878B
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

		// Token: 0x060018A1 RID: 6305 RVA: 0x0004A599 File Offset: 0x00048799
		void ICommunicationObject.Open(TimeSpan timeout)
		{
			this._innerFactory.Open(timeout);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x0004A5A7 File Offset: 0x000487A7
		void ICommunicationObject.Open()
		{
			this._innerFactory.Open();
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060018A3 RID: 6307 RVA: 0x0004A5B4 File Offset: 0x000487B4
		// (remove) Token: 0x060018A4 RID: 6308 RVA: 0x0004A5C2 File Offset: 0x000487C2
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

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060018A5 RID: 6309 RVA: 0x0004A5D0 File Offset: 0x000487D0
		// (remove) Token: 0x060018A6 RID: 6310 RVA: 0x0004A5DE File Offset: 0x000487DE
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0004A5EC File Offset: 0x000487EC
		CommunicationState ICommunicationObject.State
		{
			get
			{
				return this._innerFactory.State;
			}
		}

		// Token: 0x04000D76 RID: 3446
		private IChannelFactory<TChannel> _innerFactory;

		// Token: 0x04000D77 RID: 3447
		private DataCacheSecurity _dataCacheSecurity;
	}
}

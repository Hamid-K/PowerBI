using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A0 RID: 672
	internal class CacheResolverChannelListener<TChannel> : IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x060018A8 RID: 6312 RVA: 0x0004A5F9 File Offset: 0x000487F9
		public CacheResolverChannelListener(IChannelListener<TChannel> innerListener, DataCacheSecurity dataCacheSecurity)
		{
			if (innerListener == null)
			{
				throw new ArgumentNullException("innerListener");
			}
			this._dataCacheSecurity = dataCacheSecurity;
			this._innerListener = innerListener;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0004A61D File Offset: 0x0004881D
		public TChannel AcceptChannel(TimeSpan timeout)
		{
			return this.EndAcceptChannel(this.BeginAcceptChannel(timeout, null, null));
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0004A62E File Offset: 0x0004882E
		public TChannel AcceptChannel()
		{
			return this.EndAcceptChannel(this.BeginAcceptChannel(null, null));
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0004A63E File Offset: 0x0004883E
		public IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerListener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0004A64E File Offset: 0x0004884E
		public IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this._innerListener.BeginAcceptChannel(callback, state);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0004A660 File Offset: 0x00048860
		public TChannel EndAcceptChannel(IAsyncResult result)
		{
			TChannel tchannel = this._innerListener.EndAcceptChannel(result);
			return (TChannel)((object)new CacheResolverChannel(CacheResolverCommunicatorType.Service, (IDuplexSessionChannel)((object)tchannel), this._dataCacheSecurity));
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0004A696 File Offset: 0x00048896
		public IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0004A6A6 File Offset: 0x000488A6
		public bool EndWaitForChannel(IAsyncResult result)
		{
			return this._innerListener.EndWaitForChannel(result);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0004A6B4 File Offset: 0x000488B4
		T IChannelListener.GetProperty<T>()
		{
			return this._innerListener.GetProperty<T>();
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0004A6C1 File Offset: 0x000488C1
		public Uri Uri
		{
			get
			{
				return this._innerListener.Uri;
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0004A6CE File Offset: 0x000488CE
		public bool WaitForChannel(TimeSpan timeout)
		{
			return this._innerListener.WaitForChannel(timeout);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0004A6DC File Offset: 0x000488DC
		void ICommunicationObject.Abort()
		{
			this._innerListener.Abort();
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0004A6E9 File Offset: 0x000488E9
		IAsyncResult ICommunicationObject.BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerListener.BeginClose(timeout, callback, state);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0004A6F9 File Offset: 0x000488F9
		IAsyncResult ICommunicationObject.BeginClose(AsyncCallback callback, object state)
		{
			return this._innerListener.BeginClose(callback, state);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0004A708 File Offset: 0x00048908
		IAsyncResult ICommunicationObject.BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this._innerListener.BeginOpen(timeout, callback, state);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0004A718 File Offset: 0x00048918
		IAsyncResult ICommunicationObject.BeginOpen(AsyncCallback callback, object state)
		{
			return this._innerListener.BeginOpen(callback, state);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0004A727 File Offset: 0x00048927
		void ICommunicationObject.Close(TimeSpan timeout)
		{
			this._innerListener.Close(timeout);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0004A735 File Offset: 0x00048935
		void ICommunicationObject.Close()
		{
			this._innerListener.Close();
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060018BA RID: 6330 RVA: 0x0004A742 File Offset: 0x00048942
		// (remove) Token: 0x060018BB RID: 6331 RVA: 0x0004A750 File Offset: 0x00048950
		event EventHandler ICommunicationObject.Closed
		{
			add
			{
				this._innerListener.Closed += value;
			}
			remove
			{
				this._innerListener.Closed -= value;
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060018BC RID: 6332 RVA: 0x0004A75E File Offset: 0x0004895E
		// (remove) Token: 0x060018BD RID: 6333 RVA: 0x0004A76C File Offset: 0x0004896C
		event EventHandler ICommunicationObject.Closing
		{
			add
			{
				this._innerListener.Closing += value;
			}
			remove
			{
				this._innerListener.Closing -= value;
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0004A77A File Offset: 0x0004897A
		void ICommunicationObject.EndClose(IAsyncResult result)
		{
			this._innerListener.EndClose(result);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0004A788 File Offset: 0x00048988
		void ICommunicationObject.EndOpen(IAsyncResult result)
		{
			this._innerListener.EndOpen(result);
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060018C0 RID: 6336 RVA: 0x0004A796 File Offset: 0x00048996
		// (remove) Token: 0x060018C1 RID: 6337 RVA: 0x0004A7A4 File Offset: 0x000489A4
		event EventHandler ICommunicationObject.Faulted
		{
			add
			{
				this._innerListener.Faulted += value;
			}
			remove
			{
				this._innerListener.Faulted -= value;
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0004A7B2 File Offset: 0x000489B2
		void ICommunicationObject.Open(TimeSpan timeout)
		{
			this._innerListener.Open(timeout);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0004A7C0 File Offset: 0x000489C0
		void ICommunicationObject.Open()
		{
			this._innerListener.Open();
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060018C4 RID: 6340 RVA: 0x0004A7CD File Offset: 0x000489CD
		// (remove) Token: 0x060018C5 RID: 6341 RVA: 0x0004A7DB File Offset: 0x000489DB
		event EventHandler ICommunicationObject.Opened
		{
			add
			{
				this._innerListener.Opened += value;
			}
			remove
			{
				this._innerListener.Opened -= value;
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060018C6 RID: 6342 RVA: 0x0004A7E9 File Offset: 0x000489E9
		// (remove) Token: 0x060018C7 RID: 6343 RVA: 0x0004A7F7 File Offset: 0x000489F7
		event EventHandler ICommunicationObject.Opening
		{
			add
			{
				this._innerListener.Opening += value;
			}
			remove
			{
				this._innerListener.Opening -= value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0004A805 File Offset: 0x00048A05
		CommunicationState ICommunicationObject.State
		{
			get
			{
				return this._innerListener.State;
			}
		}

		// Token: 0x04000D78 RID: 3448
		private DataCacheSecurity _dataCacheSecurity;

		// Token: 0x04000D79 RID: 3449
		private IChannelListener<TChannel> _innerListener;
	}
}

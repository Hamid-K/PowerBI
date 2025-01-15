using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;

namespace NLog.LogReceiverService
{
	// Token: 0x0200009C RID: 156
	public abstract class WcfLogReceiverClientBase<TService> : ClientBase<TService>, IWcfLogReceiverClient, ICommunicationObject where TService : class
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001A95E File Offset: 0x00018B5E
		protected WcfLogReceiverClientBase()
		{
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001A966 File Offset: 0x00018B66
		protected WcfLogReceiverClientBase(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001A96F File Offset: 0x00018B6F
		protected WcfLogReceiverClientBase(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001A979 File Offset: 0x00018B79
		protected WcfLogReceiverClientBase(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001A983 File Offset: 0x00018B83
		protected WcfLogReceiverClientBase(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000A33 RID: 2611 RVA: 0x0001A990 File Offset: 0x00018B90
		// (remove) Token: 0x06000A34 RID: 2612 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		public event EventHandler<AsyncCompletedEventArgs> ProcessLogMessagesCompleted;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000A35 RID: 2613 RVA: 0x0001AA00 File Offset: 0x00018C00
		// (remove) Token: 0x06000A36 RID: 2614 RVA: 0x0001AA38 File Offset: 0x00018C38
		public event EventHandler<AsyncCompletedEventArgs> OpenCompleted;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000A37 RID: 2615 RVA: 0x0001AA70 File Offset: 0x00018C70
		// (remove) Token: 0x06000A38 RID: 2616 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		public event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0001AADD File Offset: 0x00018CDD
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		public CookieContainer CookieContainer
		{
			get
			{
				IHttpCookieContainerManager property = base.InnerChannel.GetProperty<IHttpCookieContainerManager>();
				if (property == null)
				{
					return null;
				}
				return property.CookieContainer;
			}
			set
			{
				IHttpCookieContainerManager property = base.InnerChannel.GetProperty<IHttpCookieContainerManager>();
				if (property != null)
				{
					property.CookieContainer = value;
					return;
				}
				throw new InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpCookieContainerBindingElement.");
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001AB26 File Offset: 0x00018D26
		public void OpenAsync()
		{
			this.OpenAsync(null);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0001AB2F File Offset: 0x00018D2F
		public void OpenAsync(object userState)
		{
			base.InvokeAsync(new ClientBase<TService>.BeginOperationDelegate(this.OnBeginOpen), null, new ClientBase<TService>.EndOperationDelegate(this.OnEndOpen), new SendOrPostCallback(this.OnOpenCompleted), userState);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001AB5D File Offset: 0x00018D5D
		public void CloseAsync()
		{
			this.CloseAsync(null);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0001AB66 File Offset: 0x00018D66
		public void CloseAsync(object userState)
		{
			base.InvokeAsync(new ClientBase<TService>.BeginOperationDelegate(this.OnBeginClose), null, new ClientBase<TService>.EndOperationDelegate(this.OnEndClose), new SendOrPostCallback(this.OnCloseCompleted), userState);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001AB94 File Offset: 0x00018D94
		public void ProcessLogMessagesAsync(NLogEvents events)
		{
			this.ProcessLogMessagesAsync(events, null);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001AB9E File Offset: 0x00018D9E
		public void ProcessLogMessagesAsync(NLogEvents events, object userState)
		{
			base.InvokeAsync(new ClientBase<TService>.BeginOperationDelegate(this.OnBeginProcessLogMessages), new object[] { events }, new ClientBase<TService>.EndOperationDelegate(this.OnEndProcessLogMessages), new SendOrPostCallback(this.OnProcessLogMessagesCompleted), userState);
		}

		// Token: 0x06000A41 RID: 2625
		public abstract IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x06000A42 RID: 2626
		public abstract void EndProcessLogMessages(IAsyncResult result);

		// Token: 0x06000A43 RID: 2627 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		private IAsyncResult OnBeginProcessLogMessages(object[] inValues, AsyncCallback callback, object asyncState)
		{
			NLogEvents nlogEvents = (NLogEvents)inValues[0];
			return this.BeginProcessLogMessages(nlogEvents, callback, asyncState);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0001ABF7 File Offset: 0x00018DF7
		private object[] OnEndProcessLogMessages(IAsyncResult result)
		{
			this.EndProcessLogMessages(result);
			return null;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0001AC04 File Offset: 0x00018E04
		private void OnProcessLogMessagesCompleted(object state)
		{
			if (this.ProcessLogMessagesCompleted != null)
			{
				ClientBase<TService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<TService>.InvokeAsyncCompletedEventArgs)state;
				this.ProcessLogMessagesCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0001AC43 File Offset: 0x00018E43
		private IAsyncResult OnBeginOpen(object[] inValues, AsyncCallback callback, object asyncState)
		{
			return ((ICommunicationObject)this).BeginOpen(callback, asyncState);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0001AC4D File Offset: 0x00018E4D
		private object[] OnEndOpen(IAsyncResult result)
		{
			((ICommunicationObject)this).EndOpen(result);
			return null;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0001AC58 File Offset: 0x00018E58
		private void OnOpenCompleted(object state)
		{
			if (this.OpenCompleted != null)
			{
				ClientBase<TService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<TService>.InvokeAsyncCompletedEventArgs)state;
				this.OpenCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001AC97 File Offset: 0x00018E97
		private IAsyncResult OnBeginClose(object[] inValues, AsyncCallback callback, object asyncState)
		{
			return ((ICommunicationObject)this).BeginClose(callback, asyncState);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0001ACA1 File Offset: 0x00018EA1
		private object[] OnEndClose(IAsyncResult result)
		{
			((ICommunicationObject)this).EndClose(result);
			return null;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001ACAC File Offset: 0x00018EAC
		private void OnCloseCompleted(object state)
		{
			if (this.CloseCompleted != null)
			{
				ClientBase<TService>.InvokeAsyncCompletedEventArgs invokeAsyncCompletedEventArgs = (ClientBase<TService>.InvokeAsyncCompletedEventArgs)state;
				this.CloseCompleted(this, new AsyncCompletedEventArgs(invokeAsyncCompletedEventArgs.Error, invokeAsyncCompletedEventArgs.Cancelled, invokeAsyncCompletedEventArgs.UserState));
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001ACEB File Offset: 0x00018EEB
		ClientCredentials IWcfLogReceiverClient.get_ClientCredentials()
		{
			return base.ClientCredentials;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001ACF3 File Offset: 0x00018EF3
		IClientChannel IWcfLogReceiverClient.get_InnerChannel()
		{
			return base.InnerChannel;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001ACFB File Offset: 0x00018EFB
		ServiceEndpoint IWcfLogReceiverClient.get_Endpoint()
		{
			return base.Endpoint;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001AD03 File Offset: 0x00018F03
		void IWcfLogReceiverClient.DisplayInitializationUI()
		{
			base.DisplayInitializationUI();
		}
	}
}

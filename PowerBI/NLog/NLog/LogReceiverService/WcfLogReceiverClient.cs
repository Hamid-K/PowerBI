using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace NLog.LogReceiverService
{
	// Token: 0x0200009B RID: 155
	public sealed class WcfLogReceiverClient : IWcfLogReceiverClient, ICommunicationObject
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001A5D2 File Offset: 0x000187D2
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0001A5DA File Offset: 0x000187DA
		public IWcfLogReceiverClient ProxiedClient { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001A5E3 File Offset: 0x000187E3
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0001A5EB File Offset: 0x000187EB
		public bool UseOneWay { get; private set; }

		// Token: 0x060009FE RID: 2558 RVA: 0x0001A5F4 File Offset: 0x000187F4
		public WcfLogReceiverClient(bool useOneWay)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient wcfLogReceiverClient2;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient();
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			else
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverOneWayClient();
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			this.ProxiedClient = wcfLogReceiverClient2;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001A628 File Offset: 0x00018828
		public WcfLogReceiverClient(bool useOneWay, string endpointConfigurationName)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient wcfLogReceiverClient2;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(endpointConfigurationName);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			else
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverOneWayClient(endpointConfigurationName);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			this.ProxiedClient = wcfLogReceiverClient2;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001A660 File Offset: 0x00018860
		public WcfLogReceiverClient(bool useOneWay, string endpointConfigurationName, string remoteAddress)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient wcfLogReceiverClient2;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(endpointConfigurationName, remoteAddress);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			else
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverOneWayClient(endpointConfigurationName, remoteAddress);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			this.ProxiedClient = wcfLogReceiverClient2;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001A698 File Offset: 0x00018898
		public WcfLogReceiverClient(bool useOneWay, string endpointConfigurationName, EndpointAddress remoteAddress)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient wcfLogReceiverClient2;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(endpointConfigurationName, remoteAddress);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			else
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverOneWayClient(endpointConfigurationName, remoteAddress);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			this.ProxiedClient = wcfLogReceiverClient2;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001A6D0 File Offset: 0x000188D0
		public WcfLogReceiverClient(bool useOneWay, Binding binding, EndpointAddress remoteAddress)
		{
			this.UseOneWay = useOneWay;
			IWcfLogReceiverClient wcfLogReceiverClient2;
			if (!useOneWay)
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverTwoWayClient(binding, remoteAddress);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			else
			{
				IWcfLogReceiverClient wcfLogReceiverClient = new WcfLogReceiverOneWayClient(binding, remoteAddress);
				wcfLogReceiverClient2 = wcfLogReceiverClient;
			}
			this.ProxiedClient = wcfLogReceiverClient2;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001A707 File Offset: 0x00018907
		public void Abort()
		{
			this.ProxiedClient.Abort();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001A714 File Offset: 0x00018914
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginClose(callback, state);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001A723 File Offset: 0x00018923
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginClose(timeout, callback, state);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001A733 File Offset: 0x00018933
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginOpen(callback, state);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0001A742 File Offset: 0x00018942
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ProxiedClient.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0001A752 File Offset: 0x00018952
		public IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return this.ProxiedClient.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001A762 File Offset: 0x00018962
		public ClientCredentials ClientCredentials
		{
			get
			{
				return this.ProxiedClient.ClientCredentials;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001A76F File Offset: 0x0001896F
		public void Close(TimeSpan timeout)
		{
			this.ProxiedClient.Close(timeout);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001A77D File Offset: 0x0001897D
		public void Close()
		{
			this.ProxiedClient.Close();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001A78A File Offset: 0x0001898A
		public void CloseAsync(object userState)
		{
			this.ProxiedClient.CloseAsync(userState);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001A798 File Offset: 0x00018998
		public void CloseAsync()
		{
			this.ProxiedClient.CloseAsync();
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000A0E RID: 2574 RVA: 0x0001A7A5 File Offset: 0x000189A5
		// (remove) Token: 0x06000A0F RID: 2575 RVA: 0x0001A7B3 File Offset: 0x000189B3
		public event EventHandler<AsyncCompletedEventArgs> CloseCompleted
		{
			add
			{
				this.ProxiedClient.CloseCompleted += value;
			}
			remove
			{
				this.ProxiedClient.CloseCompleted -= value;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000A10 RID: 2576 RVA: 0x0001A7C1 File Offset: 0x000189C1
		// (remove) Token: 0x06000A11 RID: 2577 RVA: 0x0001A7CF File Offset: 0x000189CF
		public event EventHandler Closed
		{
			add
			{
				this.ProxiedClient.Closed += value;
			}
			remove
			{
				this.ProxiedClient.Closed -= value;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000A12 RID: 2578 RVA: 0x0001A7DD File Offset: 0x000189DD
		// (remove) Token: 0x06000A13 RID: 2579 RVA: 0x0001A7EB File Offset: 0x000189EB
		public event EventHandler Closing
		{
			add
			{
				this.ProxiedClient.Closing += value;
			}
			remove
			{
				this.ProxiedClient.Closing -= value;
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001A7F9 File Offset: 0x000189F9
		public void DisplayInitializationUI()
		{
			this.ProxiedClient.DisplayInitializationUI();
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x0001A806 File Offset: 0x00018A06
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x0001A813 File Offset: 0x00018A13
		public CookieContainer CookieContainer
		{
			get
			{
				return this.ProxiedClient.CookieContainer;
			}
			set
			{
				this.ProxiedClient.CookieContainer = value;
			}
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001A821 File Offset: 0x00018A21
		public void EndClose(IAsyncResult result)
		{
			this.ProxiedClient.EndClose(result);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001A82F File Offset: 0x00018A2F
		public void EndOpen(IAsyncResult result)
		{
			this.ProxiedClient.EndOpen(result);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001A83D File Offset: 0x00018A3D
		public ServiceEndpoint Endpoint
		{
			get
			{
				return this.ProxiedClient.Endpoint;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001A84A File Offset: 0x00018A4A
		public void EndProcessLogMessages(IAsyncResult result)
		{
			this.ProxiedClient.EndProcessLogMessages(result);
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000A1B RID: 2587 RVA: 0x0001A858 File Offset: 0x00018A58
		// (remove) Token: 0x06000A1C RID: 2588 RVA: 0x0001A866 File Offset: 0x00018A66
		public event EventHandler Faulted
		{
			add
			{
				this.ProxiedClient.Faulted += value;
			}
			remove
			{
				this.ProxiedClient.Faulted -= value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0001A874 File Offset: 0x00018A74
		public IClientChannel InnerChannel
		{
			get
			{
				return this.ProxiedClient.InnerChannel;
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001A881 File Offset: 0x00018A81
		public void Open()
		{
			this.ProxiedClient.Open();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001A88E File Offset: 0x00018A8E
		public void Open(TimeSpan timeout)
		{
			this.ProxiedClient.Open(timeout);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001A89C File Offset: 0x00018A9C
		public void OpenAsync()
		{
			this.ProxiedClient.OpenAsync();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001A8A9 File Offset: 0x00018AA9
		public void OpenAsync(object userState)
		{
			this.ProxiedClient.OpenAsync(userState);
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000A22 RID: 2594 RVA: 0x0001A8B7 File Offset: 0x00018AB7
		// (remove) Token: 0x06000A23 RID: 2595 RVA: 0x0001A8C5 File Offset: 0x00018AC5
		public event EventHandler<AsyncCompletedEventArgs> OpenCompleted
		{
			add
			{
				this.ProxiedClient.OpenCompleted += value;
			}
			remove
			{
				this.ProxiedClient.OpenCompleted -= value;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000A24 RID: 2596 RVA: 0x0001A8D3 File Offset: 0x00018AD3
		// (remove) Token: 0x06000A25 RID: 2597 RVA: 0x0001A8E1 File Offset: 0x00018AE1
		public event EventHandler Opened
		{
			add
			{
				this.ProxiedClient.Opened += value;
			}
			remove
			{
				this.ProxiedClient.Opened -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000A26 RID: 2598 RVA: 0x0001A8EF File Offset: 0x00018AEF
		// (remove) Token: 0x06000A27 RID: 2599 RVA: 0x0001A8FD File Offset: 0x00018AFD
		public event EventHandler Opening
		{
			add
			{
				this.ProxiedClient.Opening += value;
			}
			remove
			{
				this.ProxiedClient.Opening -= value;
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001A90B File Offset: 0x00018B0B
		public void ProcessLogMessagesAsync(NLogEvents events)
		{
			this.ProxiedClient.ProcessLogMessagesAsync(events);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001A919 File Offset: 0x00018B19
		public void ProcessLogMessagesAsync(NLogEvents events, object userState)
		{
			this.ProxiedClient.ProcessLogMessagesAsync(events, userState);
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000A2A RID: 2602 RVA: 0x0001A928 File Offset: 0x00018B28
		// (remove) Token: 0x06000A2B RID: 2603 RVA: 0x0001A936 File Offset: 0x00018B36
		public event EventHandler<AsyncCompletedEventArgs> ProcessLogMessagesCompleted
		{
			add
			{
				this.ProxiedClient.ProcessLogMessagesCompleted += value;
			}
			remove
			{
				this.ProxiedClient.ProcessLogMessagesCompleted -= value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0001A944 File Offset: 0x00018B44
		public CommunicationState State
		{
			get
			{
				return this.ProxiedClient.State;
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001A951 File Offset: 0x00018B51
		public void CloseCommunicationObject()
		{
			this.ProxiedClient.Close();
		}
	}
}

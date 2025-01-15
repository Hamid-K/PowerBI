using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002BE RID: 702
	public sealed class StreamingSubscriptionConnection : IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060018F9 RID: 6393 RVA: 0x00044079 File Offset: 0x00043079
		// (remove) Token: 0x060018FA RID: 6394 RVA: 0x00044092 File Offset: 0x00043092
		public event StreamingSubscriptionConnection.NotificationEventDelegate OnNotificationEvent;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060018FB RID: 6395 RVA: 0x000440AB File Offset: 0x000430AB
		// (remove) Token: 0x060018FC RID: 6396 RVA: 0x000440C4 File Offset: 0x000430C4
		public event StreamingSubscriptionConnection.SubscriptionErrorDelegate OnSubscriptionError;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060018FD RID: 6397 RVA: 0x000440DD File Offset: 0x000430DD
		// (remove) Token: 0x060018FE RID: 6398 RVA: 0x000440F6 File Offset: 0x000430F6
		public event StreamingSubscriptionConnection.SubscriptionErrorDelegate OnDisconnect;

		// Token: 0x060018FF RID: 6399 RVA: 0x00044110 File Offset: 0x00043110
		public StreamingSubscriptionConnection(ExchangeService service, int lifetime)
		{
			EwsUtilities.ValidateParam(service, "service");
			EwsUtilities.ValidateClassVersion(service, ExchangeVersion.Exchange2010_SP1, base.GetType().Name);
			if (lifetime < 1 || lifetime > 30)
			{
				throw new ArgumentOutOfRangeException("lifetime");
			}
			this.session = service;
			this.subscriptions = new Dictionary<string, StreamingSubscription>();
			this.connectionTimeout = lifetime;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00044178 File Offset: 0x00043178
		public StreamingSubscriptionConnection(ExchangeService service, IEnumerable<StreamingSubscription> subscriptions, int lifetime)
			: this(service, lifetime)
		{
			EwsUtilities.ValidateParamCollection(subscriptions, "subscriptions");
			foreach (StreamingSubscription streamingSubscription in subscriptions)
			{
				this.subscriptions.Add(streamingSubscription.Id, streamingSubscription);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001901 RID: 6401 RVA: 0x000441E0 File Offset: 0x000431E0
		public IEnumerable<StreamingSubscription> CurrentSubscriptions
		{
			get
			{
				List<StreamingSubscription> list = new List<StreamingSubscription>();
				lock (this.lockObject)
				{
					list.AddRange(this.subscriptions.Values);
				}
				return list;
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0004422C File Offset: 0x0004322C
		public void AddSubscription(StreamingSubscription subscription)
		{
			this.ThrowIfDisposed();
			EwsUtilities.ValidateParam(subscription, "subscription");
			this.ValidateConnectionState(false, Strings.CannotAddSubscriptionToLiveConnection);
			lock (this.lockObject)
			{
				if (!this.subscriptions.ContainsKey(subscription.Id))
				{
					this.subscriptions.Add(subscription.Id, subscription);
				}
			}
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000442A8 File Offset: 0x000432A8
		public void RemoveSubscription(StreamingSubscription subscription)
		{
			this.ThrowIfDisposed();
			EwsUtilities.ValidateParam(subscription, "subscription");
			this.ValidateConnectionState(false, Strings.CannotRemoveSubscriptionFromLiveConnection);
			lock (this.lockObject)
			{
				this.subscriptions.Remove(subscription.Id);
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00044310 File Offset: 0x00043310
		public void Open()
		{
			lock (this.lockObject)
			{
				this.ThrowIfDisposed();
				this.ValidateConnectionState(false, Strings.CannotCallConnectDuringLiveConnection);
				if (this.subscriptions.Count == 0)
				{
					throw new ServiceLocalException(Strings.NoSubscriptionsOnConnection);
				}
				this.currentHangingRequest = new GetStreamingEventsRequest(this.session, new HangingServiceRequestBase.HandleResponseObject(this.HandleServiceResponseObject), this.subscriptions.Keys, this.connectionTimeout);
				this.currentHangingRequest.OnDisconnect += this.OnRequestDisconnect;
				this.currentHangingRequest.InternalExecute();
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000443C8 File Offset: 0x000433C8
		private void OnRequestDisconnect(object sender, HangingRequestDisconnectEventArgs args)
		{
			this.InternalOnDisconnect(args.Exception);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000443D8 File Offset: 0x000433D8
		public void Close()
		{
			lock (this.lockObject)
			{
				this.ThrowIfDisposed();
				this.ValidateConnectionState(true, Strings.CannotCallDisconnectWithNoLiveConnection);
				this.currentHangingRequest.Disconnect();
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00044430 File Offset: 0x00043430
		private void InternalOnDisconnect(Exception ex)
		{
			if (this.OnDisconnect != null)
			{
				this.OnDisconnect(this, new SubscriptionErrorEventArgs(null, ex));
			}
			this.currentHangingRequest = null;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x00044454 File Offset: 0x00043454
		public bool IsOpen
		{
			get
			{
				this.ThrowIfDisposed();
				return this.currentHangingRequest != null && this.currentHangingRequest.IsConnected;
			}
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00044471 File Offset: 0x00043471
		private void ValidateConnectionState(bool isConnectedExpected, string errorMessage)
		{
			if ((isConnectedExpected && !this.IsOpen) || (!isConnectedExpected && this.IsOpen))
			{
				throw new ServiceLocalException(errorMessage);
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00044490 File Offset: 0x00043490
		private void HandleServiceResponseObject(object response)
		{
			GetStreamingEventsResponse getStreamingEventsResponse = response as GetStreamingEventsResponse;
			if (getStreamingEventsResponse == null)
			{
				throw new ArgumentException();
			}
			if (getStreamingEventsResponse.Result == ServiceResult.Success || getStreamingEventsResponse.Result == ServiceResult.Warning)
			{
				if (getStreamingEventsResponse.Results.Notifications.Count > 0)
				{
					this.IssueNotificationEvents(getStreamingEventsResponse);
					return;
				}
			}
			else if (getStreamingEventsResponse.Result == ServiceResult.Error)
			{
				if (getStreamingEventsResponse.ErrorSubscriptionIds == null || getStreamingEventsResponse.ErrorSubscriptionIds.Count == 0)
				{
					this.IssueGeneralFailure(getStreamingEventsResponse);
					return;
				}
				this.IssueSubscriptionFailures(getStreamingEventsResponse);
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00044508 File Offset: 0x00043508
		private void IssueSubscriptionFailures(GetStreamingEventsResponse gseResponse)
		{
			ServiceResponseException ex = new ServiceResponseException(gseResponse);
			foreach (string text in gseResponse.ErrorSubscriptionIds)
			{
				StreamingSubscription streamingSubscription = null;
				lock (this.lockObject)
				{
					if (this.subscriptions != null && this.subscriptions.ContainsKey(text))
					{
						streamingSubscription = this.subscriptions[text];
					}
				}
				if (streamingSubscription != null)
				{
					SubscriptionErrorEventArgs subscriptionErrorEventArgs = new SubscriptionErrorEventArgs(streamingSubscription, ex);
					if (this.OnSubscriptionError != null)
					{
						this.OnSubscriptionError(this, subscriptionErrorEventArgs);
					}
				}
				if (gseResponse.ErrorCode != ServiceError.ErrorMissedNotificationEvents)
				{
					lock (this.lockObject)
					{
						if (this.subscriptions != null && this.subscriptions.ContainsKey(text))
						{
							this.subscriptions.Remove(text);
						}
					}
				}
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00044620 File Offset: 0x00043620
		private void IssueGeneralFailure(GetStreamingEventsResponse gseResponse)
		{
			SubscriptionErrorEventArgs subscriptionErrorEventArgs = new SubscriptionErrorEventArgs(null, new ServiceResponseException(gseResponse));
			if (this.OnSubscriptionError != null)
			{
				this.OnSubscriptionError(this, subscriptionErrorEventArgs);
			}
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00044650 File Offset: 0x00043650
		private void IssueNotificationEvents(GetStreamingEventsResponse gseResponse)
		{
			foreach (GetStreamingEventsResults.NotificationGroup notificationGroup in gseResponse.Results.Notifications)
			{
				StreamingSubscription streamingSubscription = null;
				lock (this.lockObject)
				{
					if (this.subscriptions != null && this.subscriptions.ContainsKey(notificationGroup.SubscriptionId))
					{
						streamingSubscription = this.subscriptions[notificationGroup.SubscriptionId];
					}
				}
				if (streamingSubscription != null)
				{
					NotificationEventArgs notificationEventArgs = new NotificationEventArgs(streamingSubscription, notificationGroup.Events);
					if (this.OnNotificationEvent != null)
					{
						this.OnNotificationEvent(this, notificationEventArgs);
					}
				}
			}
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00044718 File Offset: 0x00043718
		~StreamingSubscriptionConnection()
		{
			this.Dispose(false);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00044748 File Offset: 0x00043748
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00044754 File Offset: 0x00043754
		private void Dispose(bool suppressFinalizer)
		{
			if (suppressFinalizer)
			{
				GC.SuppressFinalize(this);
			}
			lock (this.lockObject)
			{
				if (!this.isDisposed)
				{
					if (this.currentHangingRequest != null)
					{
						this.currentHangingRequest = null;
					}
					this.subscriptions = null;
					this.session = null;
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000447BC File Offset: 0x000437BC
		private void ThrowIfDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x040013E2 RID: 5090
		private Dictionary<string, StreamingSubscription> subscriptions;

		// Token: 0x040013E3 RID: 5091
		private int connectionTimeout;

		// Token: 0x040013E4 RID: 5092
		private ExchangeService session;

		// Token: 0x040013E5 RID: 5093
		private bool isDisposed;

		// Token: 0x040013E6 RID: 5094
		private GetStreamingEventsRequest currentHangingRequest;

		// Token: 0x040013E7 RID: 5095
		private object lockObject = new object();

		// Token: 0x020002BF RID: 703
		// (Invoke) Token: 0x06001913 RID: 6419
		public delegate void NotificationEventDelegate(object sender, NotificationEventArgs args);

		// Token: 0x020002C0 RID: 704
		// (Invoke) Token: 0x06001917 RID: 6423
		public delegate void SubscriptionErrorDelegate(object sender, SubscriptionErrorEventArgs args);
	}
}

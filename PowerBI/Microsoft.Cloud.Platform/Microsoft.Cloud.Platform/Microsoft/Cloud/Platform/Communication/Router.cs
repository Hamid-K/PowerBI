using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F2 RID: 1266
	public abstract class Router : IRouter, IRouterPolicy, IIdentifiable
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06002671 RID: 9841 RVA: 0x0008957A File Offset: 0x0008777A
		// (set) Token: 0x06002672 RID: 9842 RVA: 0x00089582 File Offset: 0x00087782
		private protected ServiceDetails ServiceDetails { protected get; private set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06002673 RID: 9843 RVA: 0x0008958B File Offset: 0x0008778B
		// (set) Token: 0x06002674 RID: 9844 RVA: 0x00089593 File Offset: 0x00087793
		protected WorkTicketManager WorkTicketManager { get; set; }

		// Token: 0x06002675 RID: 9845 RVA: 0x0008959C File Offset: 0x0008779C
		protected Router(bool isUnicast, [NotNull] IRetryPolicy retryPolicy, IEventsKitFactory eventsKitFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IRetryPolicy>(retryPolicy, "retryPolicy");
			this.m_retryPolicy = retryPolicy;
			this.m_isUnicast = isUnicast;
			if (eventsKitFactory != null)
			{
				this.m_communicationFrameworkEventsKit = eventsKitFactory.CreateEventsKit<ICommunicationFrameworkEventsKit>();
			}
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x000895CC File Offset: 0x000877CC
		protected Router(bool isUnicast, IRetryPolicy retryPolicy)
			: this(isUnicast, retryPolicy, null)
		{
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x000895D7 File Offset: 0x000877D7
		public void Register(ServiceDetails serviceDetails, WorkTicketManager workTicketManager)
		{
			if (this.ServiceDetails != null || this.WorkTicketManager != null)
			{
				throw new CommunicationFrameworkArgumentException("The load balancer must be unique per each service");
			}
			this.ServiceDetails = serviceDetails;
			this.WorkTicketManager = workTicketManager;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x00089602 File Offset: 0x00087802
		public virtual string Identifier
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0008960C File Offset: 0x0008780C
		public virtual IAsyncResult BeginCreateRequest(object[] keys, AsyncCallback callback, object state)
		{
			WorkTicket workTicket = this.WorkTicketManager.CreateWorkTicket(this);
			IAsyncResult asyncResult;
			using (DisposeController disposeController = new DisposeController(workTicket))
			{
				ChainedAsyncResult<WorkTicket> chainedAsyncResult = new ChainedAsyncResult<WorkTicket>(callback, state, workTicket);
				chainedAsyncResult.InnerResult = this.BeginGetEndpoints(keys, new AsyncCallback(chainedAsyncResult.BeginAsyncFunctionCallback), null);
				disposeController.PreventDispose();
				asyncResult = chainedAsyncResult;
			}
			return asyncResult;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x00089678 File Offset: 0x00087878
		public virtual IRouterRequest EndCreateRequest(IAsyncResult ar)
		{
			ChainedAsyncResult<WorkTicket> chainedAsyncResult = (ChainedAsyncResult<WorkTicket>)ar;
			IRouterRequest routerRequest;
			using (chainedAsyncResult.WorkTicket)
			{
				chainedAsyncResult.End();
				IEnumerable<Uri> enumerable = this.EndGetEndpoints(chainedAsyncResult.InnerResult);
				if (enumerable == null)
				{
					throw new CommunicationFrameworkRouterException("Router returned illegal Uris list");
				}
				routerRequest = new RouterRequest(enumerable.Select((Uri uri) => new EndpointIdentifier(uri, string.Empty, this.m_retryPolicy.CreateInitialState())), !this.m_isUnicast, this, this.m_retryPolicy);
			}
			return routerRequest;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x000896F8 File Offset: 0x000878F8
		[CanBeNull]
		public IEnumerable<EndpointIdentifier> GetNextEndpoints(IEnumerable<EndpointIdentifier> potentialEndpoints, bool singleResult)
		{
			if (this.m_isUnicast)
			{
				int i = Randomizer.GetI32(potentialEndpoints.Count<EndpointIdentifier>());
				return new List<EndpointIdentifier> { potentialEndpoints.ElementAt(i) };
			}
			if (!singleResult)
			{
				return potentialEndpoints;
			}
			return null;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x00089732 File Offset: 0x00087932
		public IAsyncResult BeginCompleteRequest(AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x0008973B File Offset: 0x0008793B
		public void EndCompleteRequest(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x00089743 File Offset: 0x00087943
		protected void RaiseResolutionException(string destination, MonitoredException mex)
		{
			if (this.m_communicationFrameworkEventsKit != null)
			{
				this.m_communicationFrameworkEventsKit.NotifyRouterResolveError(destination, base.GetType().Name, mex);
			}
			throw mex;
		}

		// Token: 0x06002680 RID: 9856
		public abstract IAsyncResult BeginGetEndpoints(object[] keys, AsyncCallback callback, object state);

		// Token: 0x06002681 RID: 9857
		public abstract IEnumerable<Uri> EndGetEndpoints(IAsyncResult result);

		// Token: 0x04000DAA RID: 3498
		private bool m_isUnicast;

		// Token: 0x04000DAB RID: 3499
		private IRetryPolicy m_retryPolicy;

		// Token: 0x04000DAC RID: 3500
		private ICommunicationFrameworkEventsKit m_communicationFrameworkEventsKit;
	}
}

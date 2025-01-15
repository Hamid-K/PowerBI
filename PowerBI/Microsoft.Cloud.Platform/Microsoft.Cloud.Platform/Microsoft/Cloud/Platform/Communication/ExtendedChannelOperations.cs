using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200049F RID: 1183
	public class ExtendedChannelOperations<T> : ChannelOperationsBase<T>
	{
		// Token: 0x0600247E RID: 9342 RVA: 0x00083270 File Offset: 0x00081470
		public ExtendedChannelOperations(IEnumerable<Type> knownTypesList, EndpointInfo endpointInfo, ICertificateProvider certificateProvider, string certificateKey, bool isInternalContract, bool doubleWrapExceptions, bool allowImpersonation, ICommunicationFrameworkEventsKit eventsKit, IActivityFactory activityFactory = null, OnProxyCreating onProxyCreating = null)
			: base(knownTypesList, endpointInfo, certificateProvider, certificateKey, eventsKit)
		{
			this.m_activityFactory = activityFactory;
			this.m_onProxyCreating = onProxyCreating;
			this.m_isInternalContract = isInternalContract;
			this.m_doubleWrapExceptions = doubleWrapExceptions;
			this.m_allowImpersonation = allowImpersonation;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000832A8 File Offset: 0x000814A8
		protected override void ExtendWcfBehaviors(ChannelFactory channelFactory, IBindingData bindingData)
		{
			channelFactory.Endpoint.Behaviors.Add(new ParameterInspectorBehavior());
			if (this.m_activityFactory != null && this.m_isInternalContract)
			{
				channelFactory.Endpoint.Behaviors.Add(new AddContextToHeaderEndpointBehavior(this.m_activityFactory));
			}
			if (this.m_doubleWrapExceptions)
			{
				channelFactory.Endpoint.Behaviors.Add(new ClientExceptionHandlingBehavior());
			}
			if (this.m_allowImpersonation)
			{
				channelFactory.Credentials.Windows.AllowedImpersonationLevel = TokenImpersonationLevel.Impersonation;
			}
			ExceptionHandler.AsynchronousThreadExceptionHandler = new CommunicationFrameworkExceptionHandler();
			if (this.m_onProxyCreating != null)
			{
				this.m_proxyExtender = new ProxyExtender(channelFactory);
				this.m_onProxyCreating(this.m_proxyExtender);
			}
		}

		// Token: 0x04000CE2 RID: 3298
		private readonly IActivityFactory m_activityFactory;

		// Token: 0x04000CE3 RID: 3299
		private readonly OnProxyCreating m_onProxyCreating;

		// Token: 0x04000CE4 RID: 3300
		private ProxyExtender m_proxyExtender;

		// Token: 0x04000CE5 RID: 3301
		private readonly bool m_isInternalContract;

		// Token: 0x04000CE6 RID: 3302
		private readonly bool m_doubleWrapExceptions;

		// Token: 0x04000CE7 RID: 3303
		private readonly bool m_allowImpersonation;
	}
}

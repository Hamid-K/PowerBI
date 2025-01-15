using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004D7 RID: 1239
	internal class ServiceErrorHandlerBehavior : IServiceBehavior
	{
		// Token: 0x060025A9 RID: 9641 RVA: 0x000859A1 File Offset: 0x00083BA1
		internal ServiceErrorHandlerBehavior(bool disableDefaultErrorHandler)
		{
			if (!disableDefaultErrorHandler)
			{
				this.m_errorHandler = new CommunicationFrameworkErrorHandler();
				return;
			}
			this.m_errorHandler = new CommunicationFrameworkExternalErrorHandler();
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000859C4 File Offset: 0x00083BC4
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				((ChannelDispatcher)channelDispatcherBase).ErrorHandlers.Add(this.m_errorHandler);
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		// Token: 0x04000D54 RID: 3412
		private IErrorHandler m_errorHandler;
	}
}

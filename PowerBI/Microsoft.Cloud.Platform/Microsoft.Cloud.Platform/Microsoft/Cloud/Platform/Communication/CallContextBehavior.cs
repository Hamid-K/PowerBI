using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B8 RID: 1208
	public class CallContextBehavior : IOperationBehavior
	{
		// Token: 0x06002506 RID: 9478 RVA: 0x00083CF2 File Offset: 0x00081EF2
		public CallContextBehavior()
		{
			throw new InvalidOperationException("Default constructor must not be used. Please use the constructor accepting an activity factory");
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00083D04 File Offset: 0x00081F04
		public CallContextBehavior(IActivityFactory activityFactory)
		{
			this.m_activityFactory = activityFactory;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00083D13 File Offset: 0x00081F13
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			dispatchOperation.CallContextInitializers.Add(new CallContextInitializer(this.m_activityFactory));
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Validate(OperationDescription operationDescription)
		{
		}

		// Token: 0x04000D11 RID: 3345
		private IActivityFactory m_activityFactory;
	}
}

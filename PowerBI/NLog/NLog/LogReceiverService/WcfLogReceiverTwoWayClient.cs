using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NLog.LogReceiverService
{
	// Token: 0x0200009E RID: 158
	public sealed class WcfLogReceiverTwoWayClient : WcfLogReceiverClientBase<ILogReceiverTwoWayClient>, ILogReceiverTwoWayClient
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001AD58 File Offset: 0x00018F58
		public WcfLogReceiverTwoWayClient()
		{
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001AD60 File Offset: 0x00018F60
		public WcfLogReceiverTwoWayClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0001AD69 File Offset: 0x00018F69
		public WcfLogReceiverTwoWayClient(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0001AD73 File Offset: 0x00018F73
		public WcfLogReceiverTwoWayClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001AD7D File Offset: 0x00018F7D
		public WcfLogReceiverTwoWayClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001AD87 File Offset: 0x00018F87
		public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001AD97 File Offset: 0x00018F97
		public override void EndProcessLogMessages(IAsyncResult result)
		{
			base.Channel.EndProcessLogMessages(result);
		}
	}
}

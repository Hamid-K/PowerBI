using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NLog.LogReceiverService
{
	// Token: 0x0200009D RID: 157
	public sealed class WcfLogReceiverOneWayClient : WcfLogReceiverClientBase<ILogReceiverOneWayClient>, ILogReceiverOneWayClient
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x0001AD0B File Offset: 0x00018F0B
		public WcfLogReceiverOneWayClient()
		{
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001AD13 File Offset: 0x00018F13
		public WcfLogReceiverOneWayClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001AD1C File Offset: 0x00018F1C
		public WcfLogReceiverOneWayClient(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001AD26 File Offset: 0x00018F26
		public WcfLogReceiverOneWayClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001AD30 File Offset: 0x00018F30
		public WcfLogReceiverOneWayClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001AD3A File Offset: 0x00018F3A
		public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001AD4A File Offset: 0x00018F4A
		public override void EndProcessLogMessages(IAsyncResult result)
		{
			base.Channel.EndProcessLogMessages(result);
		}
	}
}

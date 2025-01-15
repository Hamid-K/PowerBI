using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NLog.LogReceiverService
{
	// Token: 0x0200009A RID: 154
	[Obsolete("Use WcfLogReceiverOneWayClient class instead. Marked obsolete before v4.3.11 and it may be removed in a future release.")]
	public sealed class WcfILogReceiverClient : WcfLogReceiverClientBase<ILogReceiverClient>, ILogReceiverClient
	{
		// Token: 0x060009F3 RID: 2547 RVA: 0x0001A585 File Offset: 0x00018785
		public WcfILogReceiverClient()
		{
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001A58D File Offset: 0x0001878D
		public WcfILogReceiverClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001A596 File Offset: 0x00018796
		public WcfILogReceiverClient(string endpointConfigurationName, string remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001A5A0 File Offset: 0x000187A0
		public WcfILogReceiverClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0001A5AA File Offset: 0x000187AA
		public WcfILogReceiverClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0001A5B4 File Offset: 0x000187B4
		public override IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState)
		{
			return base.Channel.BeginProcessLogMessages(events, callback, asyncState);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001A5C4 File Offset: 0x000187C4
		public override void EndProcessLogMessages(IAsyncResult result)
		{
			base.Channel.EndProcessLogMessages(result);
		}
	}
}

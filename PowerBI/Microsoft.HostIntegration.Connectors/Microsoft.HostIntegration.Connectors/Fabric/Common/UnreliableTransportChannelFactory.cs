using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000430 RID: 1072
	internal class UnreliableTransportChannelFactory<TChannel> : ChannelFactoryBase<TChannel>
	{
		// Token: 0x06002540 RID: 9536 RVA: 0x000724C6 File Offset: 0x000706C6
		public UnreliableTransportChannelFactory(IChannelFactory<TChannel> nestedFactory, Binding binding)
			: base(binding)
		{
			ReleaseAssert.IsTrue(typeof(TChannel) == typeof(IDuplexSessionChannel));
			this.m_nestedFactory = nestedFactory;
		}

		// Token: 0x06002541 RID: 9537 RVA: 0x000724F4 File Offset: 0x000706F4
		protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			TChannel tchannel = this.m_nestedFactory.CreateChannel(address, via);
			return (TChannel)((object)new UnreliableDuplexSessionChannel(this, (IDuplexSessionChannel)((object)tchannel)));
		}

		// Token: 0x06002542 RID: 9538 RVA: 0x00072525 File Offset: 0x00070725
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.m_nestedFactory.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06002543 RID: 9539 RVA: 0x00072535 File Offset: 0x00070735
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.m_nestedFactory.EndOpen(result);
		}

		// Token: 0x06002544 RID: 9540 RVA: 0x00072543 File Offset: 0x00070743
		protected override void OnOpen(TimeSpan timeout)
		{
			this.m_nestedFactory.Open();
		}

		// Token: 0x06002545 RID: 9541 RVA: 0x00072550 File Offset: 0x00070750
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.m_nestedFactory.BeginClose(timeout, callback, state);
		}

		// Token: 0x06002546 RID: 9542 RVA: 0x00072560 File Offset: 0x00070760
		protected override void OnEndClose(IAsyncResult result)
		{
			this.m_nestedFactory.EndClose(result);
		}

		// Token: 0x06002547 RID: 9543 RVA: 0x0007256E File Offset: 0x0007076E
		protected override void OnClose(TimeSpan timeout)
		{
			this.m_nestedFactory.Close(timeout);
		}

		// Token: 0x06002548 RID: 9544 RVA: 0x0007257C File Offset: 0x0007077C
		protected override void OnAbort()
		{
			this.m_nestedFactory.Abort();
		}

		// Token: 0x06002549 RID: 9545 RVA: 0x00072589 File Offset: 0x00070789
		public override T GetProperty<T>()
		{
			return this.m_nestedFactory.GetProperty<T>();
		}

		// Token: 0x040016A8 RID: 5800
		private IChannelFactory<TChannel> m_nestedFactory;
	}
}

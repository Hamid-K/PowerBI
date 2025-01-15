using System;
using System.ServiceModel.Channels;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200042F RID: 1071
	internal class UnreliableTransportBindingElement : TransportBindingElement
	{
		// Token: 0x06002536 RID: 9526 RVA: 0x000723D0 File Offset: 0x000705D0
		public UnreliableTransportBindingElement(TransportBindingElement nestedBindingElement)
		{
			this.m_nestedBindingElement = nestedBindingElement;
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000723DF File Offset: 0x000705DF
		public UnreliableTransportBindingElement(UnreliableTransportBindingElement element)
		{
			this.m_nestedBindingElement = (TransportBindingElement)element.m_nestedBindingElement.Clone();
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000723FD File Offset: 0x000705FD
		public override string Scheme
		{
			get
			{
				return this.m_nestedBindingElement.Scheme;
			}
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0007240A File Offset: 0x0007060A
		public override BindingElement Clone()
		{
			return new UnreliableTransportBindingElement(this);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00072412 File Offset: 0x00070612
		public override T GetProperty<T>(BindingContext context)
		{
			if (typeof(T) == typeof(UnreliableTransportBindingElement))
			{
				return (T)((object)this);
			}
			return this.m_nestedBindingElement.GetProperty<T>(context);
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0007243D File Offset: 0x0007063D
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return this.m_nestedBindingElement.CanBuildChannelFactory<TChannel>(context);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x0007244B File Offset: 0x0007064B
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return this.m_nestedBindingElement.CanBuildChannelListener<TChannel>(context);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x0007245C File Offset: 0x0007065C
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			IChannelFactory<TChannel> channelFactory = this.m_nestedBindingElement.BuildChannelFactory<TChannel>(context);
			if (!UnreliableTransportBindingElement.Enabled || typeof(TChannel) != typeof(IDuplexSessionChannel))
			{
				return channelFactory;
			}
			return new UnreliableTransportChannelFactory<TChannel>(channelFactory, context.Binding);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000724A1 File Offset: 0x000706A1
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			return this.m_nestedBindingElement.BuildChannelListener<TChannel>(context);
		}

		// Token: 0x040016A6 RID: 5798
		private TransportBindingElement m_nestedBindingElement;

		// Token: 0x040016A7 RID: 5799
		public static bool Enabled = ConfigFile.Config.GetValue<bool>("EnableUnreliableTransport", false);
	}
}

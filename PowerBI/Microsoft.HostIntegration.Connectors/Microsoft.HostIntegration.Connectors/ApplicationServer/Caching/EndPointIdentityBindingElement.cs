using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002AB RID: 683
	internal class EndPointIdentityBindingElement : BindingElement
	{
		// Token: 0x06001907 RID: 6407 RVA: 0x0004B1E5 File Offset: 0x000493E5
		public EndPointIdentityBindingElement(IEndpointIdentityProvider endpointIdentityProvider)
		{
			this._endpointIdentityProvider = endpointIdentityProvider;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0004B1F4 File Offset: 0x000493F4
		public override BindingElement Clone()
		{
			return new EndPointIdentityBindingElement(this._endpointIdentityProvider);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00049C28 File Offset: 0x00047E28
		public override T GetProperty<T>(BindingContext context)
		{
			return context.GetInnerProperty<T>();
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0004B204 File Offset: 0x00049404
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			IChannelFactory<TChannel> channelFactory = base.BuildChannelFactory<TChannel>(context);
			if (this._endpointIdentityProvider == null)
			{
				return channelFactory;
			}
			return new EndPointIdentityChannelFactory<TChannel>(channelFactory, this._endpointIdentityProvider);
		}

		// Token: 0x04000DA2 RID: 3490
		private IEndpointIdentityProvider _endpointIdentityProvider;
	}
}

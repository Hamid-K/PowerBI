using System;
using System.ServiceModel.Channels;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200029C RID: 668
	internal class CacheResolverBindingElement : BindingElement
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x00049C0C File Offset: 0x00047E0C
		public CacheResolverBindingElement(DataCacheSecurity dataCacheSecurity)
		{
			this.dataCacheSecurity = dataCacheSecurity;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00049C1B File Offset: 0x00047E1B
		public override BindingElement Clone()
		{
			return new CacheResolverBindingElement(this.dataCacheSecurity);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00049C28 File Offset: 0x00047E28
		public override T GetProperty<T>(BindingContext context)
		{
			return context.GetInnerProperty<T>();
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00049C30 File Offset: 0x00047E30
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			IChannelFactory<TChannel> channelFactory = base.BuildChannelFactory<TChannel>(context);
			return new CacheResolverChannelFactory<TChannel>(channelFactory, this.dataCacheSecurity);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00049C54 File Offset: 0x00047E54
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			IChannelListener<TChannel> channelListener = base.BuildChannelListener<TChannel>(context);
			return new CacheResolverChannelListener<TChannel>(channelListener, this.dataCacheSecurity);
		}

		// Token: 0x04000D69 RID: 3433
		private DataCacheSecurity dataCacheSecurity;
	}
}

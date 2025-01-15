using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C4 RID: 196
	public interface IBlockServiceManager
	{
		// Token: 0x0600059C RID: 1436
		BlockServiceTicket GetService(RequestedBlockService request);

		// Token: 0x0600059D RID: 1437
		BlockServiceTicket TryGetService(RequestedBlockService request);

		// Token: 0x0600059E RID: 1438
		BlockServiceTicket GetService(IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context);

		// Token: 0x0600059F RID: 1439
		BlockServiceTicket GetService(string name, IBlock serviceConsumer, Type serviceType, BlockServiceProviderIdentity serviceIdentity, object context);

		// Token: 0x060005A0 RID: 1440
		bool PublishService(string serviceIdentity, object service, Type serviceType, BlockServiceProviderIdentity serviceLevel, IBlock serviceProvider);

		// Token: 0x060005A1 RID: 1441
		bool PublishService(object service, Type serviceType, BlockServiceProviderIdentity serviceIdentity, IBlock serviceProvider);
	}
}

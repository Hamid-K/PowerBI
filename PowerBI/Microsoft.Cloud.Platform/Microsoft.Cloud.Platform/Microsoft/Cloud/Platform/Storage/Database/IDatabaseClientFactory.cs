using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000023 RID: 35
	public interface IDatabaseClientFactory
	{
		// Token: 0x060000BA RID: 186
		TInterface Create<TInterface>() where TInterface : IDatabaseClient;

		// Token: 0x060000BB RID: 187
		ReplicatedDatabaseClient<TInterface> CreateReplicated<TInterface>() where TInterface : IDatabaseClient;

		// Token: 0x060000BC RID: 188
		TInterface Create<TInterface>(string identification) where TInterface : IDatabaseClient;

		// Token: 0x060000BD RID: 189
		ReplicatedDatabaseClient<TInterface> CreateReplicated<TInterface>(string identification) where TInterface : IDatabaseClient;
	}
}

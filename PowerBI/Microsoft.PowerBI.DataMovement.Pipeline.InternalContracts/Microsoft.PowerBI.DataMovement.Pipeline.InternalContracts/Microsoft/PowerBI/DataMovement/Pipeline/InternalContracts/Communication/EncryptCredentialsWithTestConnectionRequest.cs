using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200002A RID: 42
	[DataContract]
	internal sealed class EncryptCredentialsWithTestConnectionRequest : TestDataSourceConnectionRequest
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00002627 File Offset: 0x00000827
		[NullableContext(1)]
		internal EncryptCredentialsWithTestConnectionRequest WithDataSource(DataSourceGatewayDetails dataSource)
		{
			return new EncryptCredentialsWithTestConnectionRequest
			{
				ConnectionString = base.ConnectionString,
				ProviderName = base.ProviderName,
				DataSource = dataSource,
				ConnectTimeout = base.ConnectTimeout
			};
		}
	}
}

using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200001B RID: 27
	public interface IDbConnection : IDisposable
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000075 RID: 117
		bool IsOpen { get; }

		// Token: 0x06000076 RID: 118
		Task OpenAsync();

		// Token: 0x06000077 RID: 119
		Task<bool> IsAliveAsync();

		// Token: 0x06000078 RID: 120
		IDbCommand CreateCommand(string commandText);

		// Token: 0x06000079 RID: 121
		IDbSchemaCommand CreateSchemaCommand();

		// Token: 0x0600007A RID: 122
		Task CloseAsync();
	}
}

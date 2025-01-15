using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecutionCommon
{
	// Token: 0x0200006D RID: 109
	internal interface ICommandExecutor
	{
		// Token: 0x06000290 RID: 656
		bool IsClosed();

		// Token: 0x06000291 RID: 657
		Task CloseAsync(bool shouldNotThrow);

		// Token: 0x06000292 RID: 658
		Task ExecuteAsync(IDbConnection connection, CancellationToken cancellationToken, CancellationToken internalCancelToken);
	}
}

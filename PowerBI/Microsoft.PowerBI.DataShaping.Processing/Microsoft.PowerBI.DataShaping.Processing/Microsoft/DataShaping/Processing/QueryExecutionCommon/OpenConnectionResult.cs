using System;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.DataShaping.Processing.QueryExecutionCommon
{
	// Token: 0x0200006C RID: 108
	internal sealed class OpenConnectionResult
	{
		// Token: 0x0600028D RID: 653 RVA: 0x00007847 File Offset: 0x00005A47
		internal OpenConnectionResult(IDbConnection connection, bool isFromPool)
		{
			this._connection = connection;
			this._isFromPool = isFromPool;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000785D File Offset: 0x00005A5D
		internal IDbConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007865 File Offset: 0x00005A65
		internal bool IsFromPool
		{
			get
			{
				return this._isFromPool;
			}
		}

		// Token: 0x04000191 RID: 401
		private readonly IDbConnection _connection;

		// Token: 0x04000192 RID: 402
		private readonly bool _isFromPool;
	}
}

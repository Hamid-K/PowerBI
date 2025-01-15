using System;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003AB RID: 939
	internal interface ISqlClient
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060020F1 RID: 8433
		string ProviderName { get; }

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060020F2 RID: 8434
		DbProviderFactory ProviderFactory { get; }

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060020F3 RID: 8435
		bool SupportsAad { get; }

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060020F4 RID: 8436
		bool SupportsMultiSubnetFailover { get; }

		// Token: 0x060020F5 RID: 8437
		void SetAccessToken(DbConnection connection, string accessToken);

		// Token: 0x060020F6 RID: 8438
		void ClearAllPools();

		// Token: 0x060020F7 RID: 8439
		void ClearPool(DbConnection dbConnection);

		// Token: 0x060020F8 RID: 8440
		bool TryGetErrorInfo(DbException exception, bool wasEncrypted, out SqlErrorInfo errorInfo);

		// Token: 0x060020F9 RID: 8441
		ISqlBulkCopy CreateBulkCopy(DbConnection connection, bool keepIdentity, DbTransaction transaction);

		// Token: 0x060020FA RID: 8442
		SqlClassification[][] GetClassifications(DbDataReader reader);

		// Token: 0x060020FB RID: 8443
		void AddInfoMessageListener(IEngineHost engineHost, DbConnection connection, string connectionId);

		// Token: 0x060020FC RID: 8444
		bool InitializeAadDiscovery();

		// Token: 0x060020FD RID: 8445
		bool TryGetClientConnectionId(DbConnection connection, out string clientConnectionId);

		// Token: 0x060020FE RID: 8446
		void TraceClientConnectionId(IHostTrace trace, DbException exception);

		// Token: 0x060020FF RID: 8447
		void TraceRequestIds(IHostTrace trace, DbException exception);
	}
}

using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000169 RID: 361
	internal abstract class DbConnectionClosed : DbConnectionInternal
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x0006E01A File Offset: 0x0006C21A
		protected DbConnectionClosed(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
			: base(state, hidePassword, allowSetConnectionString)
		{
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001ADE RID: 6878 RVA: 0x0006E025 File Offset: 0x0006C225
		public override string ServerVersion
		{
			get
			{
				throw ADP.ClosedConnectionError();
			}
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x0006E025 File Offset: 0x0006C225
		protected override void Activate(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x0006E025 File Offset: 0x0006C225
		public override DbTransaction BeginTransaction(global::System.Data.IsolationLevel il)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0006E025 File Offset: 0x0006C225
		public override void ChangeDatabase(string database)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal override void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0006E025 File Offset: 0x0006C225
		protected override void Deactivate()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0006E025 File Offset: 0x0006C225
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006E025 File Offset: 0x0006C225
		protected internal override DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0006E025 File Offset: 0x0006C225
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.ClosedConnectionError();
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0004C2E7 File Offset: 0x0004A4E7
		internal override bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}
	}
}

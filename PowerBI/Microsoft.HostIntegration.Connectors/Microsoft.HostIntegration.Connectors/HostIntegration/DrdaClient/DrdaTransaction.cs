using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A39 RID: 2617
	public sealed class DrdaTransaction : DbTransaction
	{
		// Token: 0x060051D6 RID: 20950 RVA: 0x0014DD8C File Offset: 0x0014BF8C
		internal DrdaTransaction(DrdaConnection connection, IsolationLevel isolationLevel)
		{
			if (TransactionState.GlobalStarted == connection.TransactionState)
			{
				throw DrdaException.NoLocalTransactionInDistributedContext();
			}
			this._connection = connection;
			this._isolationLevel = isolationLevel;
			this._isComplete = false;
			this._connection.TransactionState = TransactionState.LocalStarted;
		}

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x0014DDEA File Offset: 0x0014BFEA
		public new DrdaConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x0014DDF2 File Offset: 0x0014BFF2
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x0014DDFA File Offset: 0x0014BFFA
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.AssertNotCompleted();
				return this._isolationLevel;
			}
		}

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x0014DE08 File Offset: 0x0014C008
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x0014DE10 File Offset: 0x0014C010
		internal bool IsComplete
		{
			get
			{
				return this._isComplete;
			}
		}

		// Token: 0x060051DC RID: 20956 RVA: 0x0014DE18 File Offset: 0x0014C018
		private async Task InternalCommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			DrdaConnection.DrdaPermission.Demand();
			this.AssertNotCompleted();
			await this.Connection.CommitAsync(isAsync, cancellationToken);
			this._connection = null;
			this.Dispose(true);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x0014DE70 File Offset: 0x0014C070
		public override void Commit()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalCommitAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0014DEA8 File Offset: 0x0014C0A8
		private async Task InternalRollbackAsync(bool isAsync, CancellationToken cancellationToken)
		{
			DrdaConnection.DrdaPermission.Demand();
			this.AssertNotCompleted();
			await this.Connection.RollbackAsync(isAsync, cancellationToken);
			this._connection = null;
			this.Dispose(true);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x0014DF00 File Offset: 0x0014C100
		public override void Rollback()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalRollbackAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x0014DF36 File Offset: 0x0014C136
		public Task CommitAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalCommitAsync(true, cancellationToken);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x0014DF50 File Offset: 0x0014C150
		public Task RollbackAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalRollbackAsync(true, cancellationToken);
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x0014DF6A File Offset: 0x0014C16A
		private void AssertNotCompleted()
		{
			if (this.Connection == null)
			{
				throw DrdaException.TransactionCompleted();
			}
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x0014DF7C File Offset: 0x0014C17C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.Connection != null)
				{
					this.Connection.RollbackAsync(true, CancellationToken.None).GetAwaiter().GetResult();
				}
				this._connection = null;
			}
			this._isComplete = true;
		}

		// Token: 0x0400405C RID: 16476
		private DrdaConnection _connection;

		// Token: 0x0400405D RID: 16477
		private IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

		// Token: 0x0400405E RID: 16478
		private bool _isComplete;

		// Token: 0x0400405F RID: 16479
		private static int _objectTypeCount;

		// Token: 0x04004060 RID: 16480
		internal readonly int _objectID = Interlocked.Increment(ref DrdaTransaction._objectTypeCount);
	}
}

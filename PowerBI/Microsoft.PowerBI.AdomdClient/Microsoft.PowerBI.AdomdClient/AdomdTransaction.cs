using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006A RID: 106
	public sealed class AdomdTransaction : IDbTransaction, IDisposable
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00023687 File Offset: 0x00021887
		internal bool IsCompleted
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0002368F File Offset: 0x0002188F
		internal AdomdTransaction(AdomdConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.connection = connection;
			this.ExecuteMdx("BEGIN TRANSACTION");
			this.complete = false;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000236C8 File Offset: 0x000218C8
		~AdomdTransaction()
		{
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000236F0 File Offset: 0x000218F0
		public void Rollback()
		{
			this.CheckDisposed();
			if (!this.complete)
			{
				this.ExecuteMdx("ROLLBACK TRANSACTION");
				this.complete = true;
				return;
			}
			throw new InvalidOperationException(SR.TransactionAlreadyComplete);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0002371D File Offset: 0x0002191D
		public void Commit()
		{
			this.CheckDisposed();
			if (!this.complete)
			{
				this.ExecuteMdx("COMMIT TRANSACTION");
				this.complete = true;
				return;
			}
			throw new InvalidOperationException(SR.TransactionAlreadyComplete);
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0002374A File Offset: 0x0002194A
		public AdomdConnection Connection
		{
			get
			{
				this.CheckDisposed();
				return this.connection;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00023758 File Offset: 0x00021958
		public IsolationLevel IsolationLevel
		{
			get
			{
				this.CheckDisposed();
				return IsolationLevel.ReadCommitted;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00023768 File Offset: 0x00021968
		public void Dispose()
		{
			try
			{
				if (!this.complete && this.connection != null)
				{
					this.Rollback();
				}
				this.connection = null;
			}
			catch (AdomdException)
			{
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x000237A8 File Offset: 0x000219A8
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000237B0 File Offset: 0x000219B0
		private void ExecuteMdx(string mdx)
		{
			new AdomdCommand(mdx, this.connection).ExecuteNonQuery();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000237C4 File Offset: 0x000219C4
		private void CheckDisposed()
		{
			if (this.connection == null)
			{
				throw new ObjectDisposedException("AdomdTransaction");
			}
		}

		// Token: 0x040004E4 RID: 1252
		private AdomdConnection connection;

		// Token: 0x040004E5 RID: 1253
		private bool complete = true;

		// Token: 0x040004E6 RID: 1254
		private const string mdxBeginTransaction = "BEGIN TRANSACTION";

		// Token: 0x040004E7 RID: 1255
		private const string mdxCommitTransaction = "COMMIT TRANSACTION";

		// Token: 0x040004E8 RID: 1256
		private const string mdxRollbackTransaction = "ROLLBACK TRANSACTION";
	}
}

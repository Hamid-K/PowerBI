using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006A RID: 106
	public sealed class AdomdTransaction : IDbTransaction, IDisposable
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000239B7 File Offset: 0x00021BB7
		internal bool IsCompleted
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000239BF File Offset: 0x00021BBF
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

		// Token: 0x060006FA RID: 1786 RVA: 0x000239F8 File Offset: 0x00021BF8
		~AdomdTransaction()
		{
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00023A20 File Offset: 0x00021C20
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

		// Token: 0x060006FC RID: 1788 RVA: 0x00023A4D File Offset: 0x00021C4D
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00023A7A File Offset: 0x00021C7A
		public AdomdConnection Connection
		{
			get
			{
				this.CheckDisposed();
				return this.connection;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00023A88 File Offset: 0x00021C88
		public IsolationLevel IsolationLevel
		{
			get
			{
				this.CheckDisposed();
				return IsolationLevel.ReadCommitted;
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00023A98 File Offset: 0x00021C98
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

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00023AD8 File Offset: 0x00021CD8
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00023AE0 File Offset: 0x00021CE0
		private void ExecuteMdx(string mdx)
		{
			new AdomdCommand(mdx, this.connection).ExecuteNonQuery();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00023AF4 File Offset: 0x00021CF4
		private void CheckDisposed()
		{
			if (this.connection == null)
			{
				throw new ObjectDisposedException("AdomdTransaction");
			}
		}

		// Token: 0x040004F1 RID: 1265
		private AdomdConnection connection;

		// Token: 0x040004F2 RID: 1266
		private bool complete = true;

		// Token: 0x040004F3 RID: 1267
		private const string mdxBeginTransaction = "BEGIN TRANSACTION";

		// Token: 0x040004F4 RID: 1268
		private const string mdxCommitTransaction = "COMMIT TRANSACTION";

		// Token: 0x040004F5 RID: 1269
		private const string mdxRollbackTransaction = "ROLLBACK TRANSACTION";
	}
}

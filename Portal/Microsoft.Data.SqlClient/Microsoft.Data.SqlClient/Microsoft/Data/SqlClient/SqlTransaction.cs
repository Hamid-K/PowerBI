using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000047 RID: 71
	public sealed class SqlTransaction : DbTransaction
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		internal SqlTransaction(SqlInternalConnection internalConnection, SqlConnection con, IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			this._isolationLevel = iso;
			this._connection = con;
			if (internalTransaction == null)
			{
				this._internalTransaction = new SqlInternalTransaction(internalConnection, TransactionType.LocalFromAPI, this);
				return;
			}
			this._internalTransaction = internalTransaction;
			this._internalTransaction.InitParent(this);
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x0001005F File Offset: 0x0000E25F
		public new SqlConnection Connection
		{
			get
			{
				if (this.IsZombied)
				{
					return null;
				}
				return this._connection;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00010071 File Offset: 0x0000E271
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00010079 File Offset: 0x0000E279
		internal SqlInternalTransaction InternalTransaction
		{
			get
			{
				return this._internalTransaction;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00010081 File Offset: 0x0000E281
		public override IsolationLevel IsolationLevel
		{
			get
			{
				this.ZombieCheck();
				return this._isolationLevel;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001008F File Offset: 0x0000E28F
		private bool Is2005PartialZombie
		{
			get
			{
				return this._internalTransaction != null && this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000100A6 File Offset: 0x0000E2A6
		internal bool IsZombied
		{
			get
			{
				return this._internalTransaction == null || this._internalTransaction.IsCompleted;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000100BD File Offset: 0x0000E2BD
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x000100C5 File Offset: 0x0000E2C5
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000100EC File Offset: 0x0000E2EC
		internal void Zombie()
		{
			SqlInternalConnection sqlInternalConnection = this._connection.InnerConnection as SqlInternalConnection;
			if (sqlInternalConnection != null && sqlInternalConnection.Is2005OrNewer && !this._isFromAPI)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("SqlTransaction.Zombie | ADV | Object Id {0} yukon deferred zombie", this.ObjectID);
				return;
			}
			this._internalTransaction = null;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001013A File Offset: 0x0000E33A
		private void ZombieCheck()
		{
			if (this.IsZombied)
			{
				if (this.Is2005PartialZombie)
				{
					this._internalTransaction = null;
				}
				throw ADP.TransactionZombied(this);
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001015C File Offset: 0x0000E35C
		public override void Commit()
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int>("<sc.SqlTransaction.Commit|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlTransaction.Commit|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				TdsParser tdsParser = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this._isFromAPI = true;
					this._internalTransaction.Commit();
				}
				catch (OutOfMemoryException ex)
				{
					this._connection.Abort(ex);
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._connection.Abort(ex2);
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._connection.Abort(ex3);
					SqlInternalConnection.BestEffortCleanup(tdsParser);
					throw;
				}
				catch (SqlException ex4)
				{
					Win32Exception ex5 = ex4.InnerException as Win32Exception;
					if (ex5 != null && (long)ex5.NativeErrorCode == 258L)
					{
						this._connection.Abort(ex4);
					}
					throw;
				}
				finally
				{
					this._isFromAPI = false;
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000102A8 File Offset: 0x0000E4A8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				TdsParser tdsParser = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
					if (!this.IsZombied && !this.Is2005PartialZombie)
					{
						this._internalTransaction.Dispose();
					}
				}
				catch (OutOfMemoryException ex)
				{
					this._connection.Abort(ex);
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._connection.Abort(ex2);
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._connection.Abort(ex3);
					SqlInternalConnection.BestEffortCleanup(tdsParser);
					throw;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001034C File Offset: 0x0000E54C
		public override void Rollback()
		{
			if (this.Is2005PartialZombie)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlTransaction.Rollback|ADV> {0} partial zombie no rollback required", this.ObjectID);
				this._internalTransaction = null;
				return;
			}
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int>("<sc.SqlTransaction.Rollback|API> {0}", this.ObjectID))
			{
				SqlClientEventSource.Log.TryCorrelationTraceEvent<int, ActivityCorrelator.ActivityId>("<sc.SqlTransaction.Rollback|API|Correlation> ObjectID {0}, ActivityID {1}", this.ObjectID, ActivityCorrelator.Current);
				TdsParser tdsParser = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this._isFromAPI = true;
					this._internalTransaction.Rollback();
				}
				catch (OutOfMemoryException ex)
				{
					this._connection.Abort(ex);
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._connection.Abort(ex2);
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._connection.Abort(ex3);
					SqlInternalConnection.BestEffortCleanup(tdsParser);
					throw;
				}
				finally
				{
					this._isFromAPI = false;
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00010474 File Offset: 0x0000E674
		public void Rollback(string transactionName)
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int, string>("<sc.SqlTransaction.Rollback|API> {0} transactionName='{1}'", this.ObjectID, transactionName))
			{
				TdsParser tdsParser = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this._isFromAPI = true;
					this._internalTransaction.Rollback(transactionName);
				}
				catch (OutOfMemoryException ex)
				{
					this._connection.Abort(ex);
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._connection.Abort(ex2);
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._connection.Abort(ex3);
					SqlInternalConnection.BestEffortCleanup(tdsParser);
					throw;
				}
				finally
				{
					this._isFromAPI = false;
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00010568 File Offset: 0x0000E768
		public void Save(string savePointName)
		{
			SqlConnection.ExecutePermission.Demand();
			this.ZombieCheck();
			SqlStatistics sqlStatistics = null;
			using (TryEventScope.Create<int, string>("<sc.SqlTransaction.Save|API> {0} savePointName='{1}'", this.ObjectID, savePointName))
			{
				TdsParser tdsParser = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this._connection);
					sqlStatistics = SqlStatistics.StartTimer(this.Statistics);
					this._internalTransaction.Save(savePointName);
				}
				catch (OutOfMemoryException ex)
				{
					this._connection.Abort(ex);
					throw;
				}
				catch (StackOverflowException ex2)
				{
					this._connection.Abort(ex2);
					throw;
				}
				catch (ThreadAbortException ex3)
				{
					this._connection.Abort(ex3);
					SqlInternalConnection.BestEffortCleanup(tdsParser);
					throw;
				}
				finally
				{
					SqlStatistics.StopTimer(sqlStatistics);
				}
			}
		}

		// Token: 0x040000E2 RID: 226
		private static int s_objectTypeCount;

		// Token: 0x040000E3 RID: 227
		internal readonly int _objectID = Interlocked.Increment(ref SqlTransaction.s_objectTypeCount);

		// Token: 0x040000E4 RID: 228
		internal readonly IsolationLevel _isolationLevel = IsolationLevel.ReadCommitted;

		// Token: 0x040000E5 RID: 229
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x040000E6 RID: 230
		private readonly SqlConnection _connection;

		// Token: 0x040000E7 RID: 231
		private bool _isFromAPI;
	}
}

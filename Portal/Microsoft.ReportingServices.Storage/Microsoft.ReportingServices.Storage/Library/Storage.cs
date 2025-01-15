using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000035 RID: 53
	internal class Storage : IRSStorage
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00008B65 File Offset: 0x00006D65
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00008B44 File Offset: 0x00006D44
		public virtual ConnectionManager ConnectionManager
		{
			get
			{
				return this.m_connectionManager;
			}
			set
			{
				if (this.m_connectionManager != null)
				{
					RSTrace.CatalogTrace.Assert(false, "Connection manager already initialized");
				}
				this.m_connectionManager = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00008B6D File Offset: 0x00006D6D
		public SqlConnection Connection
		{
			get
			{
				return this.m_connectionManager.Connection;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008B7A File Offset: 0x00006D7A
		public SqlConnection UnverifiedConnection
		{
			get
			{
				return this.m_connectionManager.UnverifiedConnection;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00008B88 File Offset: 0x00006D88
		public SqlTransaction Transaction
		{
			get
			{
				SqlTransaction transaction;
				using (this.m_connectionManager.EnterThreadSafeContext())
				{
					if (this.m_connectionManager.Transaction == null)
					{
						this.m_connectionManager.BeginTransaction();
					}
					transaction = this.m_connectionManager.Transaction;
				}
				return transaction;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public void DisconnectStorage()
		{
			if (this.m_connectionManager != null)
			{
				using (this.m_connectionManager.EnterThreadSafeContext())
				{
					this.m_connectionManager.DisconnectStorage();
				}
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00008C2C File Offset: 0x00006E2C
		public void AbortTransaction()
		{
			using (this.m_connectionManager.EnterThreadSafeContext())
			{
				this.m_connectionManager.AbortTransaction();
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008C6C File Offset: 0x00006E6C
		public void Commit()
		{
			using (this.m_connectionManager.EnterThreadSafeContext())
			{
				this.m_connectionManager.CommitTransaction();
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008CAC File Offset: 0x00006EAC
		public void Disconnect()
		{
			if (this.m_connectionManager != null)
			{
				using (this.m_connectionManager.EnterThreadSafeContext())
				{
					this.m_connectionManager.DisconnectStorage();
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public int SqlCommandTimeout
		{
			get
			{
				return ConnectionManager.SqlCommandTimeoutSeconds;
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008CFC File Offset: 0x00006EFC
		public CancelableSqlCommand NewCancelableStandardSqlCommand(string storedProcedureName)
		{
			IDisposable disposable = null;
			CancelableSqlCommand cancelableSqlCommand;
			try
			{
				disposable = this.ConnectionManager.EnterThreadSafeContext();
				cancelableSqlCommand = CancelableSqlCommand.GetCancelableSqlCommand(new SqlCommand(storedProcedureName, this.Connection, this.Transaction)
				{
					CommandType = CommandType.StoredProcedure,
					CommandTimeout = this.SqlCommandTimeout
				}, disposable);
			}
			catch (Exception)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				throw;
			}
			return cancelableSqlCommand;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008D64 File Offset: 0x00006F64
		public virtual InstrumentedSqlCommand NewStandardSqlCommand(string storedProcedureName, SqlConnection connection = null)
		{
			IDisposable disposable = null;
			InstrumentedSqlCommand instrumentedSqlCommand2;
			try
			{
				disposable = this.ConnectionManager.EnterThreadSafeContext();
				connection = connection ?? this.Connection;
				InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(storedProcedureName, connection, this.Transaction), disposable);
				instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
				instrumentedSqlCommand.CommandTimeout = this.SqlCommandTimeout;
				instrumentedSqlCommand2 = instrumentedSqlCommand;
			}
			catch (Exception)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				throw;
			}
			return instrumentedSqlCommand2;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008DD4 File Offset: 0x00006FD4
		public InstrumentedSqlCommand NewStandardSqlCommandQuery(string query)
		{
			IDisposable disposable = null;
			InstrumentedSqlCommand instrumentedSqlCommand2;
			try
			{
				disposable = this.ConnectionManager.EnterThreadSafeContext();
				InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(query, this.Connection, this.Transaction), disposable);
				instrumentedSqlCommand.CommandType = CommandType.Text;
				instrumentedSqlCommand.CommandTimeout = this.SqlCommandTimeout;
				instrumentedSqlCommand2 = instrumentedSqlCommand;
			}
			catch (Exception)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				throw;
			}
			return instrumentedSqlCommand2;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008E3C File Offset: 0x0000703C
		[Obsolete("Use non-static methods")]
		protected static InstrumentedSqlCommand NewSqlCommand(string storedProcedureName, CommandType commandType, SqlConnection connection, SqlTransaction transaction, int timeoutSeconds)
		{
			InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(storedProcedureName, connection, transaction), null);
			instrumentedSqlCommand.CommandType = commandType;
			instrumentedSqlCommand.CommandTimeout = timeoutSeconds;
			return instrumentedSqlCommand;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008E5C File Offset: 0x0000705C
		public static string EncodeForLike(string s)
		{
			if (s == null)
			{
				return null;
			}
			char[] array = new char[] { '%', '_', '[' };
			if (s.IndexOfAny(array) >= 0)
			{
				s = s.Replace("%", "*%");
				s = s.Replace("[", "*[");
				s = s.Replace("_", "*_");
			}
			return s;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008EC4 File Offset: 0x000070C4
		public static string EncodeForSearch(string s, bool forContains)
		{
			if (s == null)
			{
				return null;
			}
			if (s == string.Empty)
			{
				if (forContains)
				{
					return "%";
				}
				return string.Empty;
			}
			else
			{
				char[] array = new char[] { '%', '_', '[', '*' };
				if (s.IndexOfAny(array) >= 0)
				{
					s = s.Replace(Storage.m_LikeEscapeCharString, Storage.m_TwoLikeEscapeChars);
					s = s.Replace("%", "*%");
					s = s.Replace("[", "*[");
					s = s.Replace("_", "*_");
				}
				if (forContains)
				{
					return "%" + s + "%";
				}
				return s;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008F6B File Offset: 0x0000716B
		public static void WrapAndThrowKnownExceptionTypes(Exception e)
		{
			if (e == null)
			{
				return;
			}
			if (e is InvalidOperationException)
			{
				if (string.CompareOrdinal(e.Source, "System.Data") == 0)
				{
					throw new ReportServerStorageException(e, null);
				}
			}
			else if (e is SqlException)
			{
				throw new ReportServerStorageException(e, null);
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008FA4 File Offset: 0x000071A4
		protected static void BindItemPathToCommand(CatalogItemPath itemPath, InstrumentedSqlCommand command, bool allowEditSession, UserContext user)
		{
			command.AddParameter("@Path", SqlDbType.NVarChar, itemPath.Value);
			if (itemPath.IsEditSession)
			{
				RSTrace.CatalogTrace.Assert(allowEditSession, "attempt to use edit session on unsupporteded method");
				command.AddParameter("@EditSessionID", SqlDbType.VarChar, itemPath.EditSessionID);
				if (user != null)
				{
					byte[] array = null;
					if (user.AuthenticationType == AuthenticationType.Windows)
					{
						array = Native.NameToSid(user.UserName);
					}
					command.Parameters.Add("@OwnerSid", SqlDbType.VarBinary, 85).Value = array;
					command.Parameters.Add("@OwnerName", SqlDbType.NVarChar, 260).Value = user.UserName;
				}
			}
		}

		// Token: 0x0400015E RID: 350
		protected const char m_LikeEscapeChar = '*';

		// Token: 0x0400015F RID: 351
		protected static readonly string m_LikeEscapeCharString = '*'.ToString();

		// Token: 0x04000160 RID: 352
		protected static readonly string m_TwoLikeEscapeChars = Storage.m_LikeEscapeCharString + "*";

		// Token: 0x04000161 RID: 353
		private ConnectionManager m_connectionManager;
	}
}

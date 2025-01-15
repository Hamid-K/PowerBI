using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000276 RID: 630
	public class DatabaseLogFormatter : IDbCommandInterceptor, IDbInterceptor, IDbConnectionInterceptor, IDbTransactionInterceptor
	{
		// Token: 0x06001F9E RID: 8094 RVA: 0x0005A198 File Offset: 0x00058398
		public DatabaseLogFormatter(Action<string> writeAction)
		{
			Check.NotNull<Action<string>>(writeAction, "writeAction");
			this._writeAction = writeAction;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0005A1BE File Offset: 0x000583BE
		public DatabaseLogFormatter(DbContext context, Action<string> writeAction)
		{
			Check.NotNull<Action<string>>(writeAction, "writeAction");
			this._context = new WeakReference(context);
			this._writeAction = writeAction;
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x0005A1F0 File Offset: 0x000583F0
		protected internal DbContext Context
		{
			get
			{
				if (this._context == null || !this._context.IsAlive)
				{
					return null;
				}
				return (DbContext)this._context.Target;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x0005A219 File Offset: 0x00058419
		internal Action<string> WriteAction
		{
			get
			{
				return this._writeAction;
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0005A221 File Offset: 0x00058421
		protected virtual void Write(string output)
		{
			this._writeAction(output);
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0005A22F File Offset: 0x0005842F
		[Obsolete("This stopwatch can give incorrect times. Use 'GetStopwatch' instead.")]
		protected internal Stopwatch Stopwatch
		{
			get
			{
				return this._stopwatch;
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x0005A238 File Offset: 0x00058438
		protected internal Stopwatch GetStopwatch(DbCommandInterceptionContext interceptionContext)
		{
			if (this._context != null)
			{
				return this._stopwatch;
			}
			IDbMutableInterceptionContext dbMutableInterceptionContext = (IDbMutableInterceptionContext)interceptionContext;
			Stopwatch stopwatch = (Stopwatch)dbMutableInterceptionContext.MutableData.FindUserState("__LoggingStopwatch__");
			if (stopwatch == null)
			{
				stopwatch = new Stopwatch();
				dbMutableInterceptionContext.MutableData.SetUserState("__LoggingStopwatch__", stopwatch);
			}
			return stopwatch;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x0005A28C File Offset: 0x0005848C
		private void RestartStopwatch(DbCommandInterceptionContext interceptionContext)
		{
			Stopwatch stopwatch = this.GetStopwatch(interceptionContext);
			stopwatch.Restart();
			if (stopwatch != this._stopwatch)
			{
				this._stopwatch.Restart();
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0005A2AE File Offset: 0x000584AE
		private void StopStopwatch(DbCommandInterceptionContext interceptionContext)
		{
			Stopwatch stopwatch = this.GetStopwatch(interceptionContext);
			stopwatch.Stop();
			if (stopwatch != this._stopwatch)
			{
				this._stopwatch.Stop();
			}
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0005A2D0 File Offset: 0x000584D0
		public virtual void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<int>>(interceptionContext, "interceptionContext");
			this.Executing<int>(command, interceptionContext);
			this.RestartStopwatch(interceptionContext);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0005A2F9 File Offset: 0x000584F9
		public virtual void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<int>>(interceptionContext, "interceptionContext");
			this.StopStopwatch(interceptionContext);
			this.Executed<int>(command, interceptionContext);
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x0005A322 File Offset: 0x00058522
		public virtual void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<DbDataReader>>(interceptionContext, "interceptionContext");
			this.Executing<DbDataReader>(command, interceptionContext);
			this.RestartStopwatch(interceptionContext);
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0005A34B File Offset: 0x0005854B
		public virtual void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<DbDataReader>>(interceptionContext, "interceptionContext");
			this.StopStopwatch(interceptionContext);
			this.Executed<DbDataReader>(command, interceptionContext);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0005A374 File Offset: 0x00058574
		public virtual void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<object>>(interceptionContext, "interceptionContext");
			this.Executing<object>(command, interceptionContext);
			this.RestartStopwatch(interceptionContext);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0005A39D File Offset: 0x0005859D
		public virtual void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<object>>(interceptionContext, "interceptionContext");
			this.StopStopwatch(interceptionContext);
			this.Executed<object>(command, interceptionContext);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0005A3C8 File Offset: 0x000585C8
		public virtual void Executing<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this.LogCommand<TResult>(command, interceptionContext);
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0005A41C File Offset: 0x0005861C
		public virtual void Executed<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this.LogResult<TResult>(command, interceptionContext);
			}
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0005A470 File Offset: 0x00058670
		public virtual void LogCommand<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			string text = command.CommandText ?? "<null>";
			if (text.EndsWith(Environment.NewLine, StringComparison.Ordinal))
			{
				this.Write(text);
			}
			else
			{
				this.Write(text);
				this.Write(Environment.NewLine);
			}
			if (command.Parameters != null)
			{
				foreach (DbParameter dbParameter in command.Parameters.OfType<DbParameter>())
				{
					this.LogParameter<TResult>(command, interceptionContext, dbParameter);
				}
			}
			this.Write(interceptionContext.IsAsync ? Strings.CommandLogAsync(DateTimeOffset.Now, Environment.NewLine) : Strings.CommandLogNonAsync(DateTimeOffset.Now, Environment.NewLine));
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0005A558 File Offset: 0x00058758
		public virtual void LogParameter<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext, DbParameter parameter)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			Check.NotNull<DbParameter>(parameter, "parameter");
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("-- ").Append(parameter.ParameterName).Append(": '")
				.Append((parameter.Value == null || parameter.Value == DBNull.Value) ? "null" : parameter.Value)
				.Append("' (Type = ")
				.Append(parameter.DbType);
			if (parameter.Direction != ParameterDirection.Input)
			{
				stringBuilder.Append(", Direction = ").Append(parameter.Direction);
			}
			if (!parameter.IsNullable)
			{
				stringBuilder.Append(", IsNullable = false");
			}
			if (parameter.Size != 0)
			{
				stringBuilder.Append(", Size = ").Append(parameter.Size);
			}
			if (((IDbDataParameter)parameter).Precision != 0)
			{
				stringBuilder.Append(", Precision = ").Append(((IDbDataParameter)parameter).Precision);
			}
			if (((IDbDataParameter)parameter).Scale != 0)
			{
				stringBuilder.Append(", Scale = ").Append(((IDbDataParameter)parameter).Scale);
			}
			stringBuilder.Append(")").Append(Environment.NewLine);
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0005A6AC File Offset: 0x000588AC
		public virtual void LogResult<TResult>(DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext<TResult>>(interceptionContext, "interceptionContext");
			Stopwatch stopwatch = this._stopwatch;
			if (this._context == null)
			{
				Stopwatch stopwatch2 = (Stopwatch)((IDbMutableInterceptionContext)interceptionContext).MutableData.FindUserState("__LoggingStopwatch__");
				if (stopwatch2 != null)
				{
					stopwatch = stopwatch2;
				}
			}
			if (interceptionContext.Exception != null)
			{
				this.Write(Strings.CommandLogFailed(stopwatch.ElapsedMilliseconds, interceptionContext.Exception.Message, Environment.NewLine));
			}
			else if (interceptionContext.TaskStatus.HasFlag(TaskStatus.Canceled))
			{
				this.Write(Strings.CommandLogCanceled(stopwatch.ElapsedMilliseconds, Environment.NewLine));
			}
			else
			{
				TResult result = interceptionContext.Result;
				string text = ((result == null) ? "null" : ((result is DbDataReader) ? result.GetType().Name : result.ToString()));
				this.Write(Strings.CommandLogComplete(stopwatch.ElapsedMilliseconds, text, Environment.NewLine));
			}
			this.Write(Environment.NewLine);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0005A7CF File Offset: 0x000589CF
		public virtual void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0005A7D4 File Offset: 0x000589D4
		public virtual void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<BeginTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.TransactionStartErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.TransactionStartedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0005A868 File Offset: 0x00058A68
		public virtual void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0005A86A File Offset: 0x00058A6A
		public virtual void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0005A86C File Offset: 0x00058A6C
		public virtual void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0005A870 File Offset: 0x00058A70
		public virtual void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(interceptionContext.IsAsync ? Strings.ConnectionOpenErrorLogAsync(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine) : Strings.ConnectionOpenErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				if (interceptionContext.TaskStatus.HasFlag(TaskStatus.Canceled))
				{
					this.Write(Strings.ConnectionOpenCanceledLog(DateTimeOffset.Now, Environment.NewLine));
					return;
				}
				this.Write(interceptionContext.IsAsync ? Strings.ConnectionOpenedLogAsync(DateTimeOffset.Now, Environment.NewLine) : Strings.ConnectionOpenedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0005A981 File Offset: 0x00058B81
		public virtual void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0005A984 File Offset: 0x00058B84
		public virtual void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.ConnectionCloseErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.ConnectionClosedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0005AA18 File Offset: 0x00058C18
		public virtual void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0005AA1A File Offset: 0x00058C1A
		public virtual void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0005AA1C File Offset: 0x00058C1C
		public virtual void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0005AA1E File Offset: 0x00058C1E
		public virtual void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0005AA20 File Offset: 0x00058C20
		public virtual void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0005AA22 File Offset: 0x00058C22
		public virtual void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0005AA24 File Offset: 0x00058C24
		public virtual void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0005AA26 File Offset: 0x00058C26
		public virtual void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0005AA28 File Offset: 0x00058C28
		public virtual void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0005AA2A File Offset: 0x00058C2A
		public virtual void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0005AA2C File Offset: 0x00058C2C
		public virtual void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionInterceptionContext>(interceptionContext, "interceptionContext");
			if ((this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals))) && connection.State == ConnectionState.Open)
			{
				this.Write(Strings.ConnectionDisposedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0005AA9B File Offset: 0x00058C9B
		public virtual void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0005AA9D File Offset: 0x00058C9D
		public virtual void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0005AA9F File Offset: 0x00058C9F
		public virtual void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0005AAA1 File Offset: 0x00058CA1
		public virtual void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0005AAA3 File Offset: 0x00058CA3
		public virtual void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0005AAA5 File Offset: 0x00058CA5
		public virtual void ConnectionGetting(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0005AAA7 File Offset: 0x00058CA7
		public virtual void ConnectionGot(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0005AAA9 File Offset: 0x00058CA9
		public virtual void IsolationLevelGetting(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0005AAAB File Offset: 0x00058CAB
		public virtual void IsolationLevelGot(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0005AAAD File Offset: 0x00058CAD
		public virtual void Committing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0005AAB0 File Offset: 0x00058CB0
		public virtual void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.TransactionCommitErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.TransactionCommittedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0005AB44 File Offset: 0x00058D44
		public virtual void Disposing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if ((this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals))) && transaction.Connection != null)
			{
				this.Write(Strings.TransactionDisposedLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0005ABB2 File Offset: 0x00058DB2
		public virtual void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0005ABB4 File Offset: 0x00058DB4
		public virtual void RollingBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0005ABB8 File Offset: 0x00058DB8
		public virtual void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			if (this.Context == null || interceptionContext.DbContexts.Contains(this.Context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				if (interceptionContext.Exception != null)
				{
					this.Write(Strings.TransactionRollbackErrorLog(DateTimeOffset.Now, interceptionContext.Exception.Message, Environment.NewLine));
					return;
				}
				this.Write(Strings.TransactionRolledBackLog(DateTimeOffset.Now, Environment.NewLine));
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0005AC4C File Offset: 0x00058E4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0005AC54 File Offset: 0x00058E54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0005AC5D File Offset: 0x00058E5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0005AC65 File Offset: 0x00058E65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B6E RID: 2926
		private const string StopwatchStateKey = "__LoggingStopwatch__";

		// Token: 0x04000B6F RID: 2927
		private readonly WeakReference _context;

		// Token: 0x04000B70 RID: 2928
		private readonly Action<string> _writeAction;

		// Token: 0x04000B71 RID: 2929
		private readonly Stopwatch _stopwatch = new Stopwatch();
	}
}

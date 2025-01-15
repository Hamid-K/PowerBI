using System;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000038 RID: 56
	internal class InstrumentedSqlCommand : ThreadSafeSqlCommand, IDbCommand, IDisposable
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00009230 File Offset: 0x00007430
		protected InstrumentedSqlCommand(SqlCommand command)
			: this(command, null)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000923A File Offset: 0x0000743A
		protected InstrumentedSqlCommand(SqlCommand cmd, IDisposable connectionLockContext)
			: base(cmd, connectionLockContext)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009244 File Offset: 0x00007444
		[Obsolete("Use factory method with lock context instead")]
		internal static InstrumentedSqlCommand GetInstrumentedSqlCommand(SqlCommand command)
		{
			return new InstrumentedSqlCommand(command);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000924C File Offset: 0x0000744C
		internal static InstrumentedSqlCommand GetInstrumentedSqlCommand(SqlCommand command, IDisposable connectionLockContext)
		{
			return new InstrumentedSqlCommand(command, connectionLockContext);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009258 File Offset: 0x00007458
		public virtual int ExecuteNonQuery()
		{
			int num;
			using (new MeasureSql())
			{
				try
				{
					num = base.Command.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					Storage.WrapAndThrowKnownExceptionTypes(ex);
					throw;
				}
			}
			return num;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000092A8 File Offset: 0x000074A8
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000092B0 File Offset: 0x000074B0
		public virtual IDataReader ExecuteReader()
		{
			SqlDataReader sqlDataReader = null;
			using (new MeasureSql())
			{
				try
				{
					sqlDataReader = base.Command.ExecuteReader();
				}
				catch (Exception ex)
				{
					Storage.WrapAndThrowKnownExceptionTypes(ex);
					throw;
				}
			}
			return new HandledSqlDataReader(sqlDataReader);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00009308 File Offset: 0x00007508
		IDataReader IDbCommand.ExecuteReader(CommandBehavior beh)
		{
			return this.ExecuteReader(beh);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009314 File Offset: 0x00007514
		public virtual IDataReader ExecuteReader(CommandBehavior beh)
		{
			SqlDataReader sqlDataReader = null;
			using (new MeasureSql())
			{
				try
				{
					sqlDataReader = base.Command.ExecuteReader(beh);
				}
				catch (Exception ex)
				{
					Storage.WrapAndThrowKnownExceptionTypes(ex);
					throw;
				}
			}
			return new HandledSqlDataReader(sqlDataReader);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000936C File Offset: 0x0000756C
		public virtual object ExecuteScalar()
		{
			object obj;
			using (new MeasureSql())
			{
				try
				{
					obj = base.Command.ExecuteScalar();
				}
				catch (Exception ex)
				{
					Storage.WrapAndThrowKnownExceptionTypes(ex);
					throw;
				}
			}
			return obj;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000093BC File Offset: 0x000075BC
		public virtual void Cancel()
		{
			using (new MeasureSql())
			{
				try
				{
					base.Command.Cancel();
				}
				catch (Exception ex)
				{
					Storage.WrapAndThrowKnownExceptionTypes(ex);
					throw;
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000940C File Offset: 0x0000760C
		public override void Dispose()
		{
			using (new MeasureSql())
			{
				base.Dispose();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009444 File Offset: 0x00007644
		public void Prepare()
		{
			using (new MeasureSql())
			{
				try
				{
					base.Command.Prepare();
				}
				catch (Exception ex)
				{
					Storage.WrapAndThrowKnownExceptionTypes(ex);
					throw;
				}
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009494 File Offset: 0x00007694
		public IDbDataParameter CreateParameter()
		{
			IDbDataParameter dbDataParameter;
			using (new MeasureSql())
			{
				dbDataParameter = base.Command.CreateParameter();
			}
			return dbDataParameter;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000094D0 File Offset: 0x000076D0
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000094DD File Offset: 0x000076DD
		public IDbConnection Connection
		{
			get
			{
				return base.Command.Connection;
			}
			set
			{
				base.Command.Connection = (SqlConnection)value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000094F0 File Offset: 0x000076F0
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000094FD File Offset: 0x000076FD
		public IDbTransaction Transaction
		{
			get
			{
				return base.Command.Transaction;
			}
			set
			{
				base.Command.Transaction = (SqlTransaction)value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00009510 File Offset: 0x00007710
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000951D File Offset: 0x0000771D
		public string CommandText
		{
			get
			{
				return base.Command.CommandText;
			}
			set
			{
				base.Command.CommandText = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000952B File Offset: 0x0000772B
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00009538 File Offset: 0x00007738
		public int CommandTimeout
		{
			get
			{
				return base.Command.CommandTimeout;
			}
			set
			{
				base.Command.CommandTimeout = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00009546 File Offset: 0x00007746
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00009553 File Offset: 0x00007753
		public CommandType CommandType
		{
			get
			{
				return base.Command.CommandType;
			}
			set
			{
				base.Command.CommandType = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009561 File Offset: 0x00007761
		public SqlParameterCollection Parameters
		{
			get
			{
				return base.Command.Parameters;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009561 File Offset: 0x00007761
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return base.Command.Parameters;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000956E File Offset: 0x0000776E
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000957B File Offset: 0x0000777B
		public UpdateRowSource UpdatedRowSource
		{
			get
			{
				return base.Command.UpdatedRowSource;
			}
			set
			{
				base.Command.UpdatedRowSource = value;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009589 File Offset: 0x00007789
		public SqlParameter AddParameter(string paramName, SqlDbType type, object val)
		{
			SqlParameter sqlParameter = this.Parameters.Add(paramName, type);
			sqlParameter.Value = val;
			return sqlParameter;
		}

		// Token: 0x02000056 RID: 86
		private sealed class InstrumentedSqlCommandDebug : InstrumentedSqlCommand
		{
			// Token: 0x06000292 RID: 658 RVA: 0x00009230 File Offset: 0x00007430
			internal InstrumentedSqlCommandDebug(SqlCommand command)
				: base(command, null)
			{
			}

			// Token: 0x06000293 RID: 659 RVA: 0x000090F5 File Offset: 0x000072F5
			internal InstrumentedSqlCommandDebug(SqlCommand cmd, IDisposable connectionLockContext)
				: base(cmd, connectionLockContext)
			{
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000B080 File Offset: 0x00009280
			~InstrumentedSqlCommandDebug()
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Detected finalization of InstrumentedSqlCommandDebug. This is an error condition and should never happen");
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000B070 File Offset: 0x00009270
			public override void Dispose()
			{
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}
	}
}

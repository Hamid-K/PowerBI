using System;
using System.Collections;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200011B RID: 283
	internal sealed class InterceptableDbCommand : DbCommand
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x00032B7E File Offset: 0x00030D7E
		public InterceptableDbCommand(DbCommand command, DbInterceptionContext context, DbDispatchers dispatchers = null)
		{
			GC.SuppressFinalize(this);
			this._command = command;
			this._interceptionContext = context;
			this._dispatchers = dispatchers ?? DbInterception.Dispatch;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x00032BAA File Offset: 0x00030DAA
		public DbInterceptionContext InterceptionContext
		{
			get
			{
				return this._interceptionContext;
			}
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00032BB2 File Offset: 0x00030DB2
		public override void Prepare()
		{
			this._command.Prepare();
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600137D RID: 4989 RVA: 0x00032BBF File Offset: 0x00030DBF
		// (set) Token: 0x0600137E RID: 4990 RVA: 0x00032BCC File Offset: 0x00030DCC
		public override string CommandText
		{
			get
			{
				return this._command.CommandText;
			}
			set
			{
				this._command.CommandText = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00032BDA File Offset: 0x00030DDA
		// (set) Token: 0x06001380 RID: 4992 RVA: 0x00032BE7 File Offset: 0x00030DE7
		public override int CommandTimeout
		{
			get
			{
				return this._command.CommandTimeout;
			}
			set
			{
				this._command.CommandTimeout = value;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x00032BF5 File Offset: 0x00030DF5
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x00032C02 File Offset: 0x00030E02
		public override CommandType CommandType
		{
			get
			{
				return this._command.CommandType;
			}
			set
			{
				this._command.CommandType = value;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00032C10 File Offset: 0x00030E10
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x00032C1D File Offset: 0x00030E1D
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._command.UpdatedRowSource;
			}
			set
			{
				this._command.UpdatedRowSource = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x00032C2B File Offset: 0x00030E2B
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x00032C38 File Offset: 0x00030E38
		protected override DbConnection DbConnection
		{
			get
			{
				return this._command.Connection;
			}
			set
			{
				this._command.Connection = value;
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x00032C46 File Offset: 0x00030E46
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this._command.Parameters;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00032C53 File Offset: 0x00030E53
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x00032C60 File Offset: 0x00030E60
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this._command.Transaction;
			}
			set
			{
				this._command.Transaction = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00032C6E File Offset: 0x00030E6E
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x00032C7B File Offset: 0x00030E7B
		public override bool DesignTimeVisible
		{
			get
			{
				return this._command.DesignTimeVisible;
			}
			set
			{
				this._command.DesignTimeVisible = value;
			}
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00032C89 File Offset: 0x00030E89
		public override void Cancel()
		{
			this._command.Cancel();
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00032C96 File Offset: 0x00030E96
		protected override DbParameter CreateDbParameter()
		{
			return this._command.CreateParameter();
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00032CA4 File Offset: 0x00030EA4
		public override int ExecuteNonQuery()
		{
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return 1;
			}
			return this._dispatchers.Command.NonQuery(this._command, new DbCommandInterceptionContext(this._interceptionContext));
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00032CF4 File Offset: 0x00030EF4
		public override object ExecuteScalar()
		{
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return null;
			}
			return this._dispatchers.Command.Scalar(this._command, new DbCommandInterceptionContext(this._interceptionContext));
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00032D44 File Offset: 0x00030F44
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new InterceptableDbCommand.NullDataReader();
			}
			DbCommandInterceptionContext dbCommandInterceptionContext = new DbCommandInterceptionContext(this._interceptionContext);
			if (behavior != CommandBehavior.Default)
			{
				dbCommandInterceptionContext = dbCommandInterceptionContext.WithCommandBehavior(behavior);
			}
			return this._dispatchers.Command.Reader(this._command, dbCommandInterceptionContext);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00032DA4 File Offset: 0x00030FA4
		public override Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new Task<int>(() => 1);
			}
			return this._dispatchers.Command.NonQueryAsync(this._command, new DbCommandInterceptionContext(this._interceptionContext), cancellationToken);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00032E20 File Offset: 0x00031020
		public override Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new Task<object>(() => null);
			}
			return this._dispatchers.Command.ScalarAsync(this._command, new DbCommandInterceptionContext(this._interceptionContext), cancellationToken);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x00032E9C File Offset: 0x0003109C
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (!this._dispatchers.CancelableCommand.Executing(this._command, this._interceptionContext))
			{
				return new Task<DbDataReader>(() => new InterceptableDbCommand.NullDataReader());
			}
			DbCommandInterceptionContext dbCommandInterceptionContext = new DbCommandInterceptionContext(this._interceptionContext);
			if (behavior != CommandBehavior.Default)
			{
				dbCommandInterceptionContext = dbCommandInterceptionContext.WithCommandBehavior(behavior);
			}
			return this._dispatchers.Command.ReaderAsync(this._command, dbCommandInterceptionContext, cancellationToken);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x00032F22 File Offset: 0x00031122
		protected override void Dispose(bool disposing)
		{
			if (disposing && this._command != null)
			{
				this._command.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000955 RID: 2389
		private readonly DbCommand _command;

		// Token: 0x04000956 RID: 2390
		private readonly DbInterceptionContext _interceptionContext;

		// Token: 0x04000957 RID: 2391
		private readonly DbDispatchers _dispatchers;

		// Token: 0x020007FD RID: 2045
		private class NullDataReader : DbDataReader
		{
			// Token: 0x06005921 RID: 22817 RVA: 0x0013A22A File Offset: 0x0013842A
			public override void Close()
			{
			}

			// Token: 0x06005922 RID: 22818 RVA: 0x0013A22C File Offset: 0x0013842C
			public override bool NextResult()
			{
				int resultCount = this._resultCount;
				this._resultCount = resultCount + 1;
				return resultCount == 0;
			}

			// Token: 0x06005923 RID: 22819 RVA: 0x0013A250 File Offset: 0x00138450
			public override bool Read()
			{
				int readCount = this._readCount;
				this._readCount = readCount + 1;
				return readCount == 0;
			}

			// Token: 0x17001056 RID: 4182
			// (get) Token: 0x06005924 RID: 22820 RVA: 0x0013A271 File Offset: 0x00138471
			public override bool IsClosed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001057 RID: 4183
			// (get) Token: 0x06005925 RID: 22821 RVA: 0x0013A274 File Offset: 0x00138474
			public override int FieldCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06005926 RID: 22822 RVA: 0x0013A277 File Offset: 0x00138477
			public override int GetOrdinal(string name)
			{
				return -1;
			}

			// Token: 0x06005927 RID: 22823 RVA: 0x0013A27A File Offset: 0x0013847A
			public override object GetValue(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005928 RID: 22824 RVA: 0x0013A281 File Offset: 0x00138481
			public override DataTable GetSchemaTable()
			{
				throw new NotImplementedException();
			}

			// Token: 0x17001058 RID: 4184
			// (get) Token: 0x06005929 RID: 22825 RVA: 0x0013A288 File Offset: 0x00138488
			public override int Depth
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x17001059 RID: 4185
			// (get) Token: 0x0600592A RID: 22826 RVA: 0x0013A28F File Offset: 0x0013848F
			public override int RecordsAffected
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600592B RID: 22827 RVA: 0x0013A292 File Offset: 0x00138492
			public override bool GetBoolean(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600592C RID: 22828 RVA: 0x0013A299 File Offset: 0x00138499
			public override byte GetByte(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600592D RID: 22829 RVA: 0x0013A2A0 File Offset: 0x001384A0
			public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600592E RID: 22830 RVA: 0x0013A2A7 File Offset: 0x001384A7
			public override char GetChar(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600592F RID: 22831 RVA: 0x0013A2AE File Offset: 0x001384AE
			public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005930 RID: 22832 RVA: 0x0013A2B5 File Offset: 0x001384B5
			public override Guid GetGuid(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005931 RID: 22833 RVA: 0x0013A2BC File Offset: 0x001384BC
			public override short GetInt16(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005932 RID: 22834 RVA: 0x0013A2C3 File Offset: 0x001384C3
			public override int GetInt32(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005933 RID: 22835 RVA: 0x0013A2CA File Offset: 0x001384CA
			public override long GetInt64(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005934 RID: 22836 RVA: 0x0013A2D1 File Offset: 0x001384D1
			public override DateTime GetDateTime(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005935 RID: 22837 RVA: 0x0013A2D8 File Offset: 0x001384D8
			public override string GetString(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005936 RID: 22838 RVA: 0x0013A2DF File Offset: 0x001384DF
			public override decimal GetDecimal(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005937 RID: 22839 RVA: 0x0013A2E6 File Offset: 0x001384E6
			public override double GetDouble(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005938 RID: 22840 RVA: 0x0013A2ED File Offset: 0x001384ED
			public override float GetFloat(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005939 RID: 22841 RVA: 0x0013A2F4 File Offset: 0x001384F4
			public override string GetName(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600593A RID: 22842 RVA: 0x0013A2FB File Offset: 0x001384FB
			public override int GetValues(object[] values)
			{
				return 0;
			}

			// Token: 0x0600593B RID: 22843 RVA: 0x0013A2FE File Offset: 0x001384FE
			public override bool IsDBNull(int ordinal)
			{
				return true;
			}

			// Token: 0x1700105A RID: 4186
			public override object this[int ordinal]
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700105B RID: 4187
			public override object this[string name]
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700105C RID: 4188
			// (get) Token: 0x0600593E RID: 22846 RVA: 0x0013A30F File Offset: 0x0013850F
			public override bool HasRows
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600593F RID: 22847 RVA: 0x0013A316 File Offset: 0x00138516
			public override string GetDataTypeName(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005940 RID: 22848 RVA: 0x0013A31D File Offset: 0x0013851D
			public override Type GetFieldType(int ordinal)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005941 RID: 22849 RVA: 0x0013A324 File Offset: 0x00138524
			public override IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}

			// Token: 0x040021EA RID: 8682
			private int _resultCount;

			// Token: 0x040021EB RID: 8683
			private int _readCount;
		}
	}
}

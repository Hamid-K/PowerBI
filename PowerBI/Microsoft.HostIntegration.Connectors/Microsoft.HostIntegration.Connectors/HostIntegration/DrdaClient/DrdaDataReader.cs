using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009F6 RID: 2550
	public sealed class DrdaDataReader : DbDataReader
	{
		// Token: 0x06004F7F RID: 20351 RVA: 0x0013EE74 File Offset: 0x0013D074
		internal DrdaDataReader(DrdaConnection connection, DbCommand command, ISqlStatement statement, string sqlCommandText, CommandBehavior behavior, int recordsAffected, DrdaResultSet[] resultSets)
		{
			this._connection = connection;
			this._command = command;
			this._statement = statement;
			this._sqlCommandText = sqlCommandText;
			this._recordsAffected = recordsAffected;
			this._resultSets = resultSets;
			this._behavior = behavior;
			this._open = true;
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06004F80 RID: 20352 RVA: 0x0013EED3 File Offset: 0x0013D0D3
		public override int RecordsAffected
		{
			get
			{
				return this._recordsAffected;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x0013EEDB File Offset: 0x0013D0DB
		internal CommandBehavior CommandBehavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06004F82 RID: 20354 RVA: 0x0013EEE3 File Offset: 0x0013D0E3
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw DrdaException.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x0013EEF9 File Offset: 0x0013D0F9
		public override bool IsClosed
		{
			get
			{
				return !this._open;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x0013EF04 File Offset: 0x0013D104
		public bool IsScrollable
		{
			get
			{
				bool flag = false;
				if (this._resultSets.Length != 0)
				{
					DrdaResultSet drdaResultSet = this._resultSets[this._currentResultSet];
					if (drdaResultSet != null && drdaResultSet.ResultSet != null)
					{
						flag = drdaResultSet.ResultSet.IsCursorScrollable;
					}
				}
				return flag;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x0013EF42 File Offset: 0x0013D142
		// (set) Token: 0x06004F86 RID: 20358 RVA: 0x0013EF4A File Offset: 0x0013D14A
		internal ISqlStatement Statement
		{
			get
			{
				return this._statement;
			}
			set
			{
				this._statement = value;
				this._command = null;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x0013EF5A File Offset: 0x0013D15A
		internal IRequester Requester
		{
			get
			{
				return this._connection.Requester;
			}
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x0013EF67 File Offset: 0x0013D167
		private void AssertReaderIsOpen(string methodName)
		{
			if (this.IsClosed)
			{
				throw DrdaException.DataReaderClosed(methodName);
			}
			if (this._connection == null || ConnectionState.Open != this._connection.State)
			{
				throw DrdaException.ClosedConnectionError();
			}
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x0013EF94 File Offset: 0x0013D194
		public override IEnumerator GetEnumerator()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			throw new NotSupportedException();
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x0013EFAC File Offset: 0x0013D1AC
		private void StartTimeout()
		{
			DrdaCommand drdaCommand = this._command as DrdaCommand;
			if (drdaCommand != null)
			{
				drdaCommand.StartTimeout();
			}
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x0013EFD0 File Offset: 0x0013D1D0
		private void StopTimeout()
		{
			DrdaCommand drdaCommand = this._command as DrdaCommand;
			if (drdaCommand != null)
			{
				drdaCommand.StopTimeout();
			}
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x0013EFF4 File Offset: 0x0013D1F4
		public DataTable GetSchemaTable(bool retrieveKeys)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			DataTable schemaTable;
			try
			{
				this.StartTimeout();
				if (this.IsClosed)
				{
					throw DrdaException.DataReaderClosed("GetSchemaTable");
				}
				schemaTable = this._resultSets[this._currentResultSet].GetSchemaTable(retrieveKeys);
			}
			finally
			{
				this.StopTimeout();
			}
			return schemaTable;
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x0013F058 File Offset: 0x0013D258
		public override DataTable GetSchemaTable()
		{
			return this.GetSchemaTable(true);
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x0013F061 File Offset: 0x0013D261
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen("FieldCount");
				if (this._resultSets.Length != 0)
				{
					return this._resultSets[this._currentResultSet].FieldCount;
				}
				return 0;
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x0013F08C File Offset: 0x0013D28C
		public override bool HasRows
		{
			get
			{
				if (this._resultSets.Length != 0 && this._resultSets[this._currentResultSet].ResultSet.CurrentRowIndex == -1 && this._resultSets[this._currentResultSet].ResultSet.RowsCount > 0)
				{
					return this._resultSets[this._currentResultSet].HasRows;
				}
				if (this._resultSets.Length != 0 && this._resultSets[this._currentResultSet].ResultSet.CurrentRowIndex < this._resultSets[this._currentResultSet].ResultSet.RowsCount - 1 && this._resultSets[this._currentResultSet].ResultSet.RowsCount != 0)
				{
					return this._resultSets[this._currentResultSet].HasRows;
				}
				return this._resultSets.Length != 0 && !this._resultSets[this._currentResultSet].ResultSet.EndOfQuery && this._resultSets[this._currentResultSet].HasRows;
			}
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x0013F188 File Offset: 0x0013D388
		internal async Task InternalCloseAsync(bool isAsync, CancellationToken cancellationToken)
		{
			try
			{
				this.StartTimeout();
				if (!this.IsClosed)
				{
					for (int i = 0; i < this._resultSets.Length; i++)
					{
						this._resultSets[i].Close();
					}
					await this.Statement.CloseAsync(isAsync, cancellationToken);
					if (this._command != null)
					{
						if (this._command is DrdaCommand)
						{
							await ((DrdaCommand)this._command).RemoveDataReaderAsync(this, isAsync, cancellationToken);
						}
						else if (this._command is DrdaSchemaCommand)
						{
							await ((DrdaSchemaCommand)this._command).RemoveDataReaderAsync(this, isAsync, cancellationToken);
						}
					}
					else if (this._statement != null)
					{
						await this._connection.CheckInStatementAsync(this._statement, isAsync, cancellationToken);
					}
					this._statement = null;
					this._open = false;
					if ((this.CommandBehavior & CommandBehavior.CloseConnection) > CommandBehavior.Default)
					{
						this._connection.Close();
					}
				}
			}
			finally
			{
				this.StopTimeout();
			}
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x0013F1E0 File Offset: 0x0013D3E0
		public override void Close()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			this.InternalCloseAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x0013F216 File Offset: 0x0013D416
		public Task CloseAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalCloseAsync(true, cancellationToken);
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0013F230 File Offset: 0x0013D430
		public override bool NextResult()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			bool flag;
			try
			{
				DrdaCommand drdaCommand = this._command as DrdaCommand;
				if (drdaCommand != null && drdaCommand.Cancelled)
				{
					this.Close();
					flag = false;
				}
				else
				{
					this.StartTimeout();
					this.AssertReaderIsOpen("NextResult");
					if (this._currentResultSet >= this._resultSets.Length - 1 || (this.CommandBehavior & CommandBehavior.SingleResult) > CommandBehavior.Default)
					{
						flag = false;
					}
					else
					{
						this._currentResultSet++;
						flag = true;
					}
				}
			}
			finally
			{
				this.StopTimeout();
			}
			return flag;
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0013F2CC File Offset: 0x0013D4CC
		private async Task<bool> InternalReadAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				this.StartTimeout();
				DrdaCommand drdaCommand = this._command as DrdaCommand;
				if (drdaCommand != null && drdaCommand.Cancelled)
				{
					await this.InternalCloseAsync(isAsync, cancellationToken);
					flag = false;
				}
				else
				{
					this.AssertReaderIsOpen("Read");
					if (this._resultSets.Length != 0 && ((this.CommandBehavior & CommandBehavior.SingleRow) == CommandBehavior.Default || this._rowsRead == 0))
					{
						this._rowsRead++;
						flag = await this._resultSets[this._currentResultSet].ReadAsync(orientation, number, isAsync, cancellationToken);
					}
					else
					{
						flag = false;
					}
				}
			}
			finally
			{
				this.StopTimeout();
			}
			return flag;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0013F334 File Offset: 0x0013D534
		public override bool Read()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Next, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0013F36D File Offset: 0x0013D56D
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Next, 0L, true, cancellationToken);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x0013F38C File Offset: 0x0013D58C
		public bool ReadPrior()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Prior, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x0013F3C5 File Offset: 0x0013D5C5
		public Task<bool> ReadPriorAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Prior, 0L, true, cancellationToken);
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x0013F3E4 File Offset: 0x0013D5E4
		public bool ReadFirst()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.First, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x0013F41D File Offset: 0x0013D61D
		public Task<bool> ReadFirstAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.First, 0L, true, cancellationToken);
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x0013F43C File Offset: 0x0013D63C
		public bool MoveLast()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.After, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x0013F475 File Offset: 0x0013D675
		public Task<bool> MoveLastAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.After, 0L, true, cancellationToken);
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0013F494 File Offset: 0x0013D694
		public bool MoveFirst()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Before, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0013F4CD File Offset: 0x0013D6CD
		public Task<bool> MoveFirstAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Before, 0L, true, cancellationToken);
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0013F4EC File Offset: 0x0013D6EC
		public bool ReadLast()
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Last, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x0013F525 File Offset: 0x0013D725
		public Task<bool> ReadLastAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Last, 0L, true, cancellationToken);
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x0013F544 File Offset: 0x0013D744
		public bool ReadRelative(long relative)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Relative, relative, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x0013F57C File Offset: 0x0013D77C
		public Task<bool> ReadRelativeAsync(long relative, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Relative, relative, true, cancellationToken);
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0013F598 File Offset: 0x0013D798
		public bool ReadAbsolute(long absolute)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Absolute, absolute, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x0013F5D0 File Offset: 0x0013D7D0
		public Task<bool> ReadAbsoluteAsync(long absolute, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalReadAsync(QueryScrollOrientation.Absolute, absolute, true, cancellationToken);
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x0013F5EC File Offset: 0x0013D7EC
		public override string GetName(int i)
		{
			this.AssertReaderIsOpen("GetName");
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetName(i);
			}
			return string.Empty;
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x0013F61B File Offset: 0x0013D81B
		public override string GetDataTypeName(int i)
		{
			this.AssertReaderIsOpen("GetName");
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetDataTypeName(i);
			}
			return string.Empty;
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0013F64A File Offset: 0x0013D84A
		public override Type GetFieldType(int i)
		{
			this.AssertReaderIsOpen("GetName");
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetFieldType(i);
			}
			return null;
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0013F675 File Offset: 0x0013D875
		public override object GetValue(int i)
		{
			this.AssertReaderIsOpen("GetValue");
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetValue(i);
			}
			return null;
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0013F6A0 File Offset: 0x0013D8A0
		public override int GetValues(object[] values)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetValues(values);
			}
			return 0;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x0013F6C0 File Offset: 0x0013D8C0
		public override int GetOrdinal(string name)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetOrdinal(name);
			}
			return -1;
		}

		// Token: 0x1700135D RID: 4957
		public override object this[int i]
		{
			get
			{
				this.AssertReaderIsOpen("this[int i]");
				if (this._resultSets.Length != 0)
				{
					return this._resultSets[this._currentResultSet].GetValue(i);
				}
				return null;
			}
		}

		// Token: 0x1700135E RID: 4958
		public override object this[string name]
		{
			get
			{
				this.AssertReaderIsOpen("this[String name]");
				if (this._resultSets.Length != 0)
				{
					return this._resultSets[this._currentResultSet].GetValue(this._resultSets[this._currentResultSet].GetOrdinal(name));
				}
				return null;
			}
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x0013F748 File Offset: 0x0013D948
		public override bool GetBoolean(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetBoolean(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x0013F76C File Offset: 0x0013D96C
		public override byte GetByte(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetByte(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x0013F790 File Offset: 0x0013D990
		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetBytes(i, fieldOffset, buffer, bufferOffset, length);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0013F7BA File Offset: 0x0013D9BA
		public override char GetChar(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetChar(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x0013F7DE File Offset: 0x0013D9DE
		public override long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetChars(i, fieldOffset, buffer, bufferOffset, length);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0013F808 File Offset: 0x0013DA08
		public override Guid GetGuid(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetGuid(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x0013F82C File Offset: 0x0013DA2C
		public override short GetInt16(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetInt16(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0013F850 File Offset: 0x0013DA50
		public override int GetInt32(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetInt32(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0013F874 File Offset: 0x0013DA74
		public override long GetInt64(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetInt64(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0013F898 File Offset: 0x0013DA98
		public override float GetFloat(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetFloat(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0013F8BC File Offset: 0x0013DABC
		public override double GetDouble(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetDouble(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0013F8E0 File Offset: 0x0013DAE0
		public override string GetString(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetString(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0013F904 File Offset: 0x0013DB04
		public override decimal GetDecimal(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetDecimal(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0013F928 File Offset: 0x0013DB28
		public override DateTime GetDateTime(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetDateTime(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x0013F94C File Offset: 0x0013DB4C
		public TimeSpan GetTimeSpan(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].GetTimeSpan(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x0013F970 File Offset: 0x0013DB70
		public override bool IsDBNull(int i)
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet].IsDBNull(i);
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x0013F994 File Offset: 0x0013DB94
		public XmlDocument GetXml(int i)
		{
			return this.GetXml(i, Encoding.UTF8);
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x0013F9A4 File Offset: 0x0013DBA4
		public XmlDocument GetXml(int i, Encoding encoder)
		{
			if (this._resultSets.Length != 0)
			{
				object value = this.GetValue(i);
				try
				{
					string text;
					if (value is byte[])
					{
						byte[] array = (byte[])value;
						text = encoder.GetString(array, 0, array.Length);
					}
					else if (value is string)
					{
						text = (string)value;
					}
					else
					{
						if (value == null || value == DBNull.Value)
						{
							return null;
						}
						throw new InvalidCastException();
					}
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
					xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
					xmlReaderSettings.XmlResolver = null;
					XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.XmlResolver = null;
					xmlDocument.Load(xmlReader);
					return xmlDocument;
				}
				catch (Exception ex)
				{
					throw new DrdaException(ex.Message, null, 0);
				}
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0013FA6C File Offset: 0x0013DC6C
		internal DrdaResultSet GetResultSet()
		{
			if (this._resultSets.Length != 0)
			{
				return this._resultSets[this._currentResultSet];
			}
			throw DrdaException.DataReaderNoData();
		}

		// Token: 0x04003F06 RID: 16134
		private static int _objectTypeCount;

		// Token: 0x04003F07 RID: 16135
		internal readonly int _objectID = Interlocked.Increment(ref DrdaDataReader._objectTypeCount);

		// Token: 0x04003F08 RID: 16136
		private bool _open;

		// Token: 0x04003F09 RID: 16137
		private int _recordsAffected;

		// Token: 0x04003F0A RID: 16138
		private int _currentResultSet;

		// Token: 0x04003F0B RID: 16139
		private int _rowsRead;

		// Token: 0x04003F0C RID: 16140
		private DrdaConnection _connection;

		// Token: 0x04003F0D RID: 16141
		private DbCommand _command;

		// Token: 0x04003F0E RID: 16142
		private DrdaResultSet[] _resultSets;

		// Token: 0x04003F0F RID: 16143
		private ISqlStatement _statement;

		// Token: 0x04003F10 RID: 16144
		private CommandBehavior _behavior;

		// Token: 0x04003F11 RID: 16145
		private string _sqlCommandText;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000436 RID: 1078
	internal class BufferedDataReader : DbDataReader
	{
		// Token: 0x06003468 RID: 13416 RVA: 0x000A8E6C File Offset: 0x000A706C
		public BufferedDataReader(DbDataReader reader)
		{
			this._underlyingReader = reader;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000A8E86 File Offset: 0x000A7086
		public override int RecordsAffected
		{
			get
			{
				return this._recordsAffected;
			}
		}

		// Token: 0x17000A29 RID: 2601
		public override object this[string name]
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A2A RID: 2602
		public override object this[int ordinal]
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x0600346C RID: 13420 RVA: 0x000A8E9C File Offset: 0x000A709C
		public override int Depth
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x000A8EA3 File Offset: 0x000A70A3
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._currentResultSet.FieldCount;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600346E RID: 13422 RVA: 0x000A8EB6 File Offset: 0x000A70B6
		public override bool HasRows
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._currentResultSet.HasRows;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000A8EC9 File Offset: 0x000A70C9
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x000A8ED1 File Offset: 0x000A70D1
		private void AssertReaderIsOpen()
		{
			if (this._isClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x000A8EE1 File Offset: 0x000A70E1
		private void AssertReaderIsOpenWithData()
		{
			if (this._isClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
			if (!this._currentResultSet.IsDataReady)
			{
				throw Error.ADP_NoData();
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x000A8F04 File Offset: 0x000A7104
		[Conditional("DEBUG")]
		private void AssertFieldIsReady(int ordinal)
		{
			if (this._isClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
			if (!this._currentResultSet.IsDataReady)
			{
				throw Error.ADP_NoData();
			}
			if (0 > ordinal || ordinal > this._currentResultSet.FieldCount)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000A8F40 File Offset: 0x000A7140
		internal void Initialize(string providerManifestToken, DbProviderServices providerServices, Type[] columnTypes, bool[] nullableColumns)
		{
			DbDataReader underlyingReader = this._underlyingReader;
			if (underlyingReader == null)
			{
				return;
			}
			this._underlyingReader = null;
			try
			{
				if (columnTypes != null && underlyingReader.GetType().Name != "SqlDataReader")
				{
					this._bufferedDataRecords.Add(ShapedBufferedDataRecord.Initialize(providerManifestToken, providerServices, underlyingReader, columnTypes, nullableColumns));
				}
				else
				{
					this._bufferedDataRecords.Add(ShapelessBufferedDataRecord.Initialize(providerManifestToken, providerServices, underlyingReader));
				}
				while (underlyingReader.NextResult())
				{
					this._bufferedDataRecords.Add(ShapelessBufferedDataRecord.Initialize(providerManifestToken, providerServices, underlyingReader));
				}
				this._recordsAffected = underlyingReader.RecordsAffected;
				this._currentResultSet = this._bufferedDataRecords[this._currentResultSetNumber];
			}
			finally
			{
				underlyingReader.Dispose();
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000A9000 File Offset: 0x000A7200
		internal async Task InitializeAsync(string providerManifestToken, DbProviderServices providerServices, Type[] columnTypes, bool[] nullableColumns, CancellationToken cancellationToken)
		{
			if (this._underlyingReader != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				DbDataReader reader = this._underlyingReader;
				this._underlyingReader = null;
				try
				{
					if (columnTypes != null && reader.GetType().Name != "SqlDataReader")
					{
						List<BufferedDataRecord> list = this._bufferedDataRecords;
						BufferedDataRecord bufferedDataRecord = await ShapedBufferedDataRecord.InitializeAsync(providerManifestToken, providerServices, reader, columnTypes, nullableColumns, cancellationToken).WithCurrentCulture<BufferedDataRecord>();
						list.Add(bufferedDataRecord);
						list = null;
					}
					else
					{
						List<BufferedDataRecord> list = this._bufferedDataRecords;
						list.Add(await ShapelessBufferedDataRecord.InitializeAsync(providerManifestToken, providerServices, reader, cancellationToken).WithCurrentCulture<ShapelessBufferedDataRecord>());
						list = null;
					}
					for (;;)
					{
						global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = reader.NextResultAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
						if (!cultureAwaiter.IsCompleted)
						{
							await cultureAwaiter;
							global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
							cultureAwaiter = cultureAwaiter2;
							cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
						}
						if (!cultureAwaiter.GetResult())
						{
							break;
						}
						List<BufferedDataRecord> list = this._bufferedDataRecords;
						list.Add(await ShapelessBufferedDataRecord.InitializeAsync(providerManifestToken, providerServices, reader, cancellationToken).WithCurrentCulture<ShapelessBufferedDataRecord>());
						list = null;
					}
					this._recordsAffected = reader.RecordsAffected;
					this._currentResultSet = this._bufferedDataRecords[this._currentResultSetNumber];
				}
				finally
				{
					reader.Dispose();
				}
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000A9070 File Offset: 0x000A7270
		public override void Close()
		{
			this._bufferedDataRecords = null;
			this._isClosed = true;
			DbDataReader underlyingReader = this._underlyingReader;
			if (underlyingReader != null)
			{
				this._underlyingReader = null;
				underlyingReader.Dispose();
			}
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000A90A2 File Offset: 0x000A72A2
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing && !this.IsClosed)
			{
				this.Close();
			}
			this._disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000A90CD File Offset: 0x000A72CD
		public override bool GetBoolean(int ordinal)
		{
			return this._currentResultSet.GetBoolean(ordinal);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000A90DB File Offset: 0x000A72DB
		public override byte GetByte(int ordinal)
		{
			return this._currentResultSet.GetByte(ordinal);
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000A90E9 File Offset: 0x000A72E9
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000A90F0 File Offset: 0x000A72F0
		public override char GetChar(int ordinal)
		{
			return this._currentResultSet.GetChar(ordinal);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000A90FE File Offset: 0x000A72FE
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000A9105 File Offset: 0x000A7305
		public override DateTime GetDateTime(int ordinal)
		{
			return this._currentResultSet.GetDateTime(ordinal);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000A9113 File Offset: 0x000A7313
		public override decimal GetDecimal(int ordinal)
		{
			return this._currentResultSet.GetDecimal(ordinal);
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000A9121 File Offset: 0x000A7321
		public override double GetDouble(int ordinal)
		{
			return this._currentResultSet.GetDouble(ordinal);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000A912F File Offset: 0x000A732F
		public override float GetFloat(int ordinal)
		{
			return this._currentResultSet.GetFloat(ordinal);
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000A913D File Offset: 0x000A733D
		public override Guid GetGuid(int ordinal)
		{
			return this._currentResultSet.GetGuid(ordinal);
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000A914B File Offset: 0x000A734B
		public override short GetInt16(int ordinal)
		{
			return this._currentResultSet.GetInt16(ordinal);
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000A9159 File Offset: 0x000A7359
		public override int GetInt32(int ordinal)
		{
			return this._currentResultSet.GetInt32(ordinal);
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000A9167 File Offset: 0x000A7367
		public override long GetInt64(int ordinal)
		{
			return this._currentResultSet.GetInt64(ordinal);
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000A9175 File Offset: 0x000A7375
		public override string GetString(int ordinal)
		{
			return this._currentResultSet.GetString(ordinal);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000A9183 File Offset: 0x000A7383
		public override T GetFieldValue<T>(int ordinal)
		{
			return this._currentResultSet.GetFieldValue<T>(ordinal);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000A9191 File Offset: 0x000A7391
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return this._currentResultSet.GetFieldValueAsync<T>(ordinal, cancellationToken);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000A91A0 File Offset: 0x000A73A0
		public override object GetValue(int ordinal)
		{
			return this._currentResultSet.GetValue(ordinal);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000A91AE File Offset: 0x000A73AE
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			this.AssertReaderIsOpenWithData();
			return this._currentResultSet.GetValues(values);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000A91CE File Offset: 0x000A73CE
		public override string GetDataTypeName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetDataTypeName(ordinal);
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000A91E2 File Offset: 0x000A73E2
		public override Type GetFieldType(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetFieldType(ordinal);
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000A91F6 File Offset: 0x000A73F6
		public override string GetName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetName(ordinal);
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000A920A File Offset: 0x000A740A
		public override int GetOrdinal(string name)
		{
			Check.NotNull<string>(name, "name");
			this.AssertReaderIsOpen();
			return this._currentResultSet.GetOrdinal(name);
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000A922A File Offset: 0x000A742A
		public override bool IsDBNull(int ordinal)
		{
			return this._currentResultSet.IsDBNull(ordinal);
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000A9238 File Offset: 0x000A7438
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return this._currentResultSet.IsDBNullAsync(ordinal, cancellationToken);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000A9247 File Offset: 0x000A7447
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000A924F File Offset: 0x000A744F
		public override DataTable GetSchemaTable()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000A9258 File Offset: 0x000A7458
		public override bool NextResult()
		{
			this.AssertReaderIsOpen();
			int num = this._currentResultSetNumber + 1;
			this._currentResultSetNumber = num;
			if (num < this._bufferedDataRecords.Count)
			{
				this._currentResultSet = this._bufferedDataRecords[this._currentResultSetNumber];
				return true;
			}
			this._currentResultSet = null;
			return false;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000A92AA File Offset: 0x000A74AA
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			return Task.FromResult<bool>(this.NextResult());
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000A92BE File Offset: 0x000A74BE
		public override bool Read()
		{
			this.AssertReaderIsOpen();
			return this._currentResultSet.Read();
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000A92D1 File Offset: 0x000A74D1
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.AssertReaderIsOpen();
			return this._currentResultSet.ReadAsync(cancellationToken);
		}

		// Token: 0x040010EA RID: 4330
		private DbDataReader _underlyingReader;

		// Token: 0x040010EB RID: 4331
		private List<BufferedDataRecord> _bufferedDataRecords = new List<BufferedDataRecord>();

		// Token: 0x040010EC RID: 4332
		private BufferedDataRecord _currentResultSet;

		// Token: 0x040010ED RID: 4333
		private int _currentResultSetNumber;

		// Token: 0x040010EE RID: 4334
		private int _recordsAffected;

		// Token: 0x040010EF RID: 4335
		private bool _disposed;

		// Token: 0x040010F0 RID: 4336
		private bool _isClosed;
	}
}

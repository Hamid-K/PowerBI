using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Query.ResultAssembly
{
	// Token: 0x0200032A RID: 810
	internal sealed class BridgeDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord
	{
		// Token: 0x060026AD RID: 9901 RVA: 0x0006F8B6 File Offset: 0x0006DAB6
		internal BridgeDataRecord(Shaper<RecordState> shaper, int depth)
		{
			this._shaper = shaper;
			this.Depth = depth;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0006F8CC File Offset: 0x0006DACC
		internal void CloseExplicitly()
		{
			this.Close<object>(BridgeDataRecord.Status.ClosedExplicitly, new Func<object>(this.CloseNestedObjectImplicitly));
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0006F8E4 File Offset: 0x0006DAE4
		internal Task CloseExplicitlyAsync(CancellationToken cancellationToken)
		{
			return this.Close<Task>(BridgeDataRecord.Status.ClosedExplicitly, () => this.CloseNestedObjectImplicitlyAsync(cancellationToken));
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0006F918 File Offset: 0x0006DB18
		internal void CloseImplicitly()
		{
			this.Close<object>(BridgeDataRecord.Status.ClosedImplicitly, new Func<object>(this.CloseNestedObjectImplicitly));
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0006F930 File Offset: 0x0006DB30
		internal Task CloseImplicitlyAsync(CancellationToken cancellationToken)
		{
			return this.Close<Task>(BridgeDataRecord.Status.ClosedImplicitly, () => this.CloseNestedObjectImplicitlyAsync(cancellationToken));
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0006F964 File Offset: 0x0006DB64
		private T Close<T>(BridgeDataRecord.Status status, Func<T> close)
		{
			this._status = status;
			this._source = null;
			return close();
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0006F97C File Offset: 0x0006DB7C
		private object CloseNestedObjectImplicitly()
		{
			BridgeDataRecord currentNestedRecord = this._currentNestedRecord;
			if (currentNestedRecord != null)
			{
				this._currentNestedRecord = null;
				currentNestedRecord.CloseImplicitly();
			}
			BridgeDataReader currentNestedReader = this._currentNestedReader;
			if (currentNestedReader != null)
			{
				this._currentNestedReader = null;
				currentNestedReader.CloseImplicitly();
			}
			return null;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x0006F9B8 File Offset: 0x0006DBB8
		private async Task CloseNestedObjectImplicitlyAsync(CancellationToken cancellationToken)
		{
			BridgeDataRecord currentNestedRecord = this._currentNestedRecord;
			if (currentNestedRecord != null)
			{
				this._currentNestedRecord = null;
				await currentNestedRecord.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
			}
			BridgeDataReader currentNestedReader = this._currentNestedReader;
			if (currentNestedReader != null)
			{
				this._currentNestedReader = null;
				await currentNestedReader.CloseImplicitlyAsync(cancellationToken).WithCurrentCulture();
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x0006FA05 File Offset: 0x0006DC05
		internal void SetRecordSource(RecordState newSource, bool hasData)
		{
			if (hasData)
			{
				this._source = newSource;
			}
			else
			{
				this._source = null;
			}
			this._status = BridgeDataRecord.Status.Open;
			this._lastColumnRead = -1;
			this._lastDataOffsetRead = -1L;
			this._lastOrdinalCheckedForNull = -1;
			this._lastValueCheckedForNull = null;
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0006FA3E File Offset: 0x0006DC3E
		private void AssertReaderIsOpen()
		{
			if (this.IsExplicitlyClosed)
			{
				throw Error.ADP_ClosedDataReaderError();
			}
			if (this.IsImplicitlyClosed)
			{
				throw Error.ADP_ImplicitlyClosedDataReaderError();
			}
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0006FA5C File Offset: 0x0006DC5C
		private void AssertReaderIsOpenWithData()
		{
			this.AssertReaderIsOpen();
			if (!this.HasData)
			{
				throw Error.ADP_NoData();
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0006FA74 File Offset: 0x0006DC74
		private void AssertSequentialAccess(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			if (this._lastColumnRead >= ordinal)
			{
				throw new InvalidOperationException(Strings.ADP_NonSequentialColumnAccess(ordinal.ToString(CultureInfo.InvariantCulture), (this._lastColumnRead + 1).ToString(CultureInfo.InvariantCulture)));
			}
			this._lastColumnRead = ordinal;
			this._lastDataOffsetRead = long.MaxValue;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0006FAEC File Offset: 0x0006DCEC
		private void AssertSequentialAccess(int ordinal, long dataOffset, string methodName)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			if (this._lastColumnRead > ordinal || (this._lastColumnRead == ordinal && this._lastDataOffsetRead == 9223372036854775807L))
			{
				throw new InvalidOperationException(Strings.ADP_NonSequentialColumnAccess(ordinal.ToString(CultureInfo.InvariantCulture), (this._lastColumnRead + 1).ToString(CultureInfo.InvariantCulture)));
			}
			if (this._lastColumnRead == ordinal)
			{
				if (this._lastDataOffsetRead >= dataOffset)
				{
					throw new InvalidOperationException(Strings.ADP_NonSequentialChunkAccess(dataOffset.ToString(CultureInfo.InvariantCulture), (this._lastDataOffsetRead + 1L).ToString(CultureInfo.InvariantCulture), methodName));
				}
			}
			else
			{
				this._lastColumnRead = ordinal;
				this._lastDataOffsetRead = -1L;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0006FBB4 File Offset: 0x0006DDB4
		internal bool HasData
		{
			get
			{
				return this._source != null;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x0006FBBF File Offset: 0x0006DDBF
		internal bool IsClosed
		{
			get
			{
				return this._status > BridgeDataRecord.Status.Open;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x0006FBCA File Offset: 0x0006DDCA
		internal bool IsExplicitlyClosed
		{
			get
			{
				return this._status == BridgeDataRecord.Status.ClosedExplicitly;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x0006FBD5 File Offset: 0x0006DDD5
		internal bool IsImplicitlyClosed
		{
			get
			{
				return this._status == BridgeDataRecord.Status.ClosedImplicitly;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0006FBE0 File Offset: 0x0006DDE0
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._source.DataRecordInfo;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x0006FBF3 File Offset: 0x0006DDF3
		public override int FieldCount
		{
			get
			{
				this.AssertReaderIsOpen();
				return this._source.ColumnCount;
			}
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x0006FC08 File Offset: 0x0006DE08
		private TypeUsage GetTypeUsage(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this._source.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			RecordState recordState = this._source.CurrentColumnValues[ordinal] as RecordState;
			TypeUsage typeUsage;
			if (recordState != null)
			{
				typeUsage = recordState.DataRecordInfo.RecordType;
			}
			else
			{
				typeUsage = this._source.GetTypeUsage(ordinal);
			}
			return typeUsage;
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x0006FC64 File Offset: 0x0006DE64
		public override string GetDataTypeName(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			return this.GetTypeUsage(ordinal).ToString();
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x0006FC78 File Offset: 0x0006DE78
		public override Type GetFieldType(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			return BridgeDataReader.GetClrTypeFromTypeMetadata(this.GetTypeUsage(ordinal));
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0006FC8C File Offset: 0x0006DE8C
		public override string GetName(int ordinal)
		{
			this.AssertReaderIsOpen();
			return this._source.GetName(ordinal);
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x0006FCA0 File Offset: 0x0006DEA0
		public override int GetOrdinal(string name)
		{
			this.AssertReaderIsOpen();
			return this._source.GetOrdinal(name);
		}

		// Token: 0x17000832 RID: 2098
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000833 RID: 2099
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x0006FCCC File Offset: 0x0006DECC
		public override object GetValue(int ordinal)
		{
			this.AssertReaderIsOpenWithData();
			this.AssertSequentialAccess(ordinal);
			object obj;
			if (ordinal == this._lastOrdinalCheckedForNull)
			{
				obj = this._lastValueCheckedForNull;
			}
			else
			{
				this._lastOrdinalCheckedForNull = -1;
				this._lastValueCheckedForNull = null;
				this.CloseNestedObjectImplicitly();
				obj = this._source.CurrentColumnValues[ordinal];
				if (this._source.IsNestedObject(ordinal))
				{
					obj = this.GetNestedObjectValue(obj);
				}
			}
			return obj;
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x0006FD34 File Offset: 0x0006DF34
		private object GetNestedObjectValue(object result)
		{
			if (result != DBNull.Value)
			{
				RecordState recordState = result as RecordState;
				if (recordState != null)
				{
					if (recordState.IsNull)
					{
						result = DBNull.Value;
					}
					else
					{
						BridgeDataRecord bridgeDataRecord = new BridgeDataRecord(this._shaper, this.Depth + 1);
						bridgeDataRecord.SetRecordSource(recordState, true);
						result = bridgeDataRecord;
						this._currentNestedRecord = bridgeDataRecord;
						this._currentNestedReader = null;
					}
				}
				else
				{
					Coordinator<RecordState> coordinator = result as Coordinator<RecordState>;
					if (coordinator != null)
					{
						BridgeDataReader bridgeDataReader = new BridgeDataReader(this._shaper, coordinator.TypedCoordinatorFactory, this.Depth + 1, null);
						result = bridgeDataReader;
						this._currentNestedRecord = null;
						this._currentNestedReader = bridgeDataReader;
					}
				}
			}
			return result;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0006FDD0 File Offset: 0x0006DFD0
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x0006FE0F File Offset: 0x0006E00F
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this.GetValue(ordinal);
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x0006FE1D File Offset: 0x0006E01D
		public override byte GetByte(int ordinal)
		{
			return (byte)this.GetValue(ordinal);
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x0006FE2B File Offset: 0x0006E02B
		public override char GetChar(int ordinal)
		{
			return (char)this.GetValue(ordinal);
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x0006FE39 File Offset: 0x0006E039
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x0006FE47 File Offset: 0x0006E047
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x0006FE55 File Offset: 0x0006E055
		public override double GetDouble(int ordinal)
		{
			return (double)this.GetValue(ordinal);
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x0006FE63 File Offset: 0x0006E063
		public override float GetFloat(int ordinal)
		{
			return (float)this.GetValue(ordinal);
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x0006FE71 File Offset: 0x0006E071
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this.GetValue(ordinal);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0006FE7F File Offset: 0x0006E07F
		public override short GetInt16(int ordinal)
		{
			return (short)this.GetValue(ordinal);
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0006FE8D File Offset: 0x0006E08D
		public override int GetInt32(int ordinal)
		{
			return (int)this.GetValue(ordinal);
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0006FE9B File Offset: 0x0006E09B
		public override long GetInt64(int ordinal)
		{
			return (long)this.GetValue(ordinal);
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0006FEA9 File Offset: 0x0006E0A9
		public override string GetString(int ordinal)
		{
			return (string)this.GetValue(ordinal);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0006FEB8 File Offset: 0x0006E0B8
		public override bool IsDBNull(int ordinal)
		{
			object value = this.GetValue(ordinal);
			this._lastColumnRead--;
			this._lastDataOffsetRead = -1L;
			this._lastValueCheckedForNull = value;
			this._lastOrdinalCheckedForNull = ordinal;
			return DBNull.Value == value;
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0006FEFC File Offset: 0x0006E0FC
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.AssertReaderIsOpenWithData();
			this.AssertSequentialAccess(ordinal, dataOffset, "GetBytes");
			long bytes = this._source.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
			if (buffer != null)
			{
				this._lastDataOffsetRead = dataOffset + bytes - 1L;
			}
			return bytes;
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0006FF40 File Offset: 0x0006E140
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			this.AssertReaderIsOpenWithData();
			this.AssertSequentialAccess(ordinal, dataOffset, "GetChars");
			long chars = this._source.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
			if (buffer != null)
			{
				this._lastDataOffsetRead = dataOffset + chars - 1L;
			}
			return chars;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0006FF83 File Offset: 0x0006E183
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return (DbDataReader)this.GetValue(ordinal);
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0006FF91 File Offset: 0x0006E191
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this.GetValue(ordinal);
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0006FF9F File Offset: 0x0006E19F
		public DbDataReader GetDataReader(int ordinal)
		{
			return this.GetDbDataReader(ordinal);
		}

		// Token: 0x04000D7B RID: 3451
		internal readonly int Depth;

		// Token: 0x04000D7C RID: 3452
		private readonly Shaper<RecordState> _shaper;

		// Token: 0x04000D7D RID: 3453
		private RecordState _source;

		// Token: 0x04000D7E RID: 3454
		private BridgeDataRecord.Status _status;

		// Token: 0x04000D7F RID: 3455
		private int _lastColumnRead;

		// Token: 0x04000D80 RID: 3456
		private long _lastDataOffsetRead;

		// Token: 0x04000D81 RID: 3457
		private int _lastOrdinalCheckedForNull;

		// Token: 0x04000D82 RID: 3458
		private object _lastValueCheckedForNull;

		// Token: 0x04000D83 RID: 3459
		private BridgeDataReader _currentNestedReader;

		// Token: 0x04000D84 RID: 3460
		private BridgeDataRecord _currentNestedRecord;

		// Token: 0x020009CC RID: 2508
		private enum Status
		{
			// Token: 0x0400284C RID: 10316
			Open,
			// Token: 0x0400284D RID: 10317
			ClosedImplicitly,
			// Token: 0x0400284E RID: 10318
			ClosedExplicitly
		}
	}
}

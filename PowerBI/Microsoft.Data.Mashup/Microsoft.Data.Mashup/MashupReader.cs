using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator;
using Microsoft.OleDb;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200003F RID: 63
	public sealed class MashupReader : DbDataReader, IDataRecordWithMetadata, IDataRecord
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000BE5C File Offset: 0x0000A05C
		internal MashupReader(MashupCommand command, CommandBehavior commandBehavior, IDataReaderSource dataReaderSource)
		{
			this.command = command;
			this.readerId = Interlocked.Increment(ref MashupReader.lastId);
			MashupConnection connection = this.command.Connection;
			Guid? guid = ((connection != null) ? connection.ActivityId : null);
			if (guid != null)
			{
				this.evaluationConstants = new EvaluationConstants(guid.Value, this.command.CorrelationId, null);
			}
			using (IHostTrace hostTrace = ProviderTracing.CreateTrace("MashupReader/.ctor", this.evaluationConstants, TraceEventType.Information, null))
			{
				hostTrace.Add("readerId", this.readerId, false);
			}
			this.treatErrorsAsNull = this.command.Connection.ReturnErrorValuesAsNull;
			this.commandBehavior = commandBehavior;
			this.dataReaderSource = dataReaderSource;
			this.internalReader = this.CreateDataReader();
			this.lockObject = new object();
			this.SetFieldTypes();
			if (this.command.CommandTimeout != 0)
			{
				this.timeout = this.command.CommandTimeout;
				this.timer = new Timer(SafeThread2.CreateTimerCallback(new TimerCallback(this.CheckTimeout)), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		public override void Close()
		{
			if (!this.isClosed)
			{
				using (IHostTrace hostTrace = ProviderTracing.CreateTrace("MashupReader/Close", this.evaluationConstants, TraceEventType.Information, null))
				{
					hostTrace.Add("readerId", this.readerId, false);
				}
			}
			this.isClosed = true;
			this.internalReader.Close();
			if (this.command != null)
			{
				this.command.NotifyDataReaderClosing();
				if (Util.IsSet(this.commandBehavior, CommandBehavior.CloseConnection))
				{
					this.command.Connection.Close();
				}
				this.command = null;
			}
			this.hasReadyData = false;
			this.dataReaderSource.Dispose();
			if (this.timer != null)
			{
				this.timer.Dispose();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C070 File Offset: 0x0000A270
		public override int Depth
		{
			get
			{
				this.ThrowOnClosedReader();
				return 0;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000C079 File Offset: 0x0000A279
		public override int FieldCount
		{
			get
			{
				this.ThrowOnClosedReader();
				return this.internalReader.FieldCount;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C08C File Offset: 0x0000A28C
		public override bool GetBoolean(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetBoolean(ordinal);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C0A1 File Offset: 0x0000A2A1
		public override byte GetByte(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetByte(ordinal);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			this.ThrowOnNotValidGet(ordinal);
			byte[] array = (byte[])this.GetValue(ordinal);
			if (dataOffset < 0L)
			{
				throw new ArgumentOutOfRangeException("dataOffset", ProviderErrorStrings.DataOffsetNegative);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (bufferOffset < 0)
			{
				throw new ArgumentOutOfRangeException("bufferOffset", ProviderErrorStrings.BufferOffsetNegative);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", ProviderErrorStrings.LengthNegative);
			}
			if (dataOffset >= (long)array.Length)
			{
				return 0L;
			}
			long num = Math.Min((long)array.Length - dataOffset, (long)length);
			if (num > (long)(buffer.Length - bufferOffset))
			{
				throw new InvalidOperationException(ProviderErrorStrings.BufferTooSmall(bufferOffset, num));
			}
			Array.Copy(array, dataOffset, buffer, (long)bufferOffset, num);
			return num;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C16E File Offset: 0x0000A36E
		public override char GetChar(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C175 File Offset: 0x0000A375
		public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C17C File Offset: 0x0000A37C
		public override decimal GetDecimal(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			if (this.fieldTypes[ordinal] == typeof(decimal))
			{
				return this.internalReader.GetDecimal(ordinal);
			}
			return (decimal)this.GetValue(ordinal);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
		public override double GetDouble(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetDouble(ordinal);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public override DateTime GetDateTime(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return (DateTime)this.GetValue(ordinal);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C1E1 File Offset: 0x0000A3E1
		public override float GetFloat(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetFloat(ordinal);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C1F6 File Offset: 0x0000A3F6
		public override Guid GetGuid(int ordinal)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C1FD File Offset: 0x0000A3FD
		public override short GetInt16(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetInt16(ordinal);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000C212 File Offset: 0x0000A412
		public override int GetInt32(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetInt32(ordinal);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C227 File Offset: 0x0000A427
		public override long GetInt64(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetInt64(ordinal);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000C23C File Offset: 0x0000A43C
		public override string GetString(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.internalReader.GetString(ordinal);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000C254 File Offset: 0x0000A454
		public IDataRecordWithMetadata GetMetadata(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			object value = this.internalReader.GetValue(ordinal);
			if (!(value is ValueWithMetadata))
			{
				return DataRecordWithMetadata.Empty;
			}
			return new DataRecordWithMetadata(((ValueWithMetadata)value).Metadata);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C295 File Offset: 0x0000A495
		public override object GetValue(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return this.UncheckedGetValue(ordinal);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C2A8 File Offset: 0x0000A4A8
		private object UncheckedGetValue(int ordinal)
		{
			object obj = this.internalReader.GetValue(ordinal);
			if (this.hasMetadata[ordinal])
			{
				obj = ((ValueWithMetadata)obj).Value;
			}
			if (obj is Guid)
			{
				obj = ((Guid)obj).ToString();
			}
			else if (obj is Time)
			{
				obj = ((Time)obj).TimeSpan;
			}
			else if (obj is Date)
			{
				obj = ((Date)obj).DateTime;
			}
			else if (obj is Number)
			{
				Number number = (Number)obj;
				obj = this.ToDecimal(number);
			}
			else if (obj is Currency)
			{
				obj = ((Currency)obj).Value;
			}
			else if (obj is IDataRecord)
			{
				obj = new DataRecordWithMetadata((IDataRecord)obj);
			}
			return obj;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C388 File Offset: 0x0000A588
		public override int GetValues(object[] values)
		{
			this.ThrowOnClosedReader();
			this.ThrowOnNoReadyData();
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = Math.Min(this.FieldCount, values.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.UncheckedGetValue(i);
			}
			return num;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000C3D5 File Offset: 0x0000A5D5
		public override string GetName(int ordinal)
		{
			this.ThrowOnClosedReader();
			this.ThrowOnOrdinalOutOfRange(ordinal);
			return this.internalReader.GetName(ordinal);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public override int GetOrdinal(string name)
		{
			this.ThrowOnClosedReader();
			return this.GetOrdinalWithArgumentValidation(name);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000C400 File Offset: 0x0000A600
		public override DataTable GetSchemaTable()
		{
			this.ThrowOnClosedReader();
			DataTable dataTable = this.internalReader.Schema.ToDataTable();
			dataTable.TableName = "SchemaTable";
			DataColumn dataColumn = dataTable.Columns[InformationSchemaTableColumnName.DataType];
			DataColumn dataColumn2 = dataTable.Columns.Add(InformationSchemaTableColumnName.ProviderSpecificDataType, typeof(Type));
			DataColumn dataColumn3 = (dataTable.Columns.Contains(InformationSchemaTableColumnName.ColumnSize) ? dataTable.Columns[InformationSchemaTableColumnName.ColumnSize] : dataTable.Columns.Add(InformationSchemaTableColumnName.ColumnSize, typeof(int)));
			DataColumn dataColumn4 = dataTable.Columns.Add("IsLong", typeof(bool));
			for (int i = 0; i < this.FieldCount; i++)
			{
				Type fieldType = this.GetFieldType(i);
				Type providerSpecificFieldType = this.GetProviderSpecificFieldType(i);
				dataTable.Rows[i][dataColumn] = fieldType;
				dataTable.Rows[i][dataColumn2] = providerSpecificFieldType;
				dataTable.Rows[i][dataColumn3] = this.GetSize(fieldType);
				dataTable.Rows[i][dataColumn4] = false;
			}
			return dataTable;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000C547 File Offset: 0x0000A747
		public override IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C54E File Offset: 0x0000A74E
		public override bool HasRows
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000C555 File Offset: 0x0000A755
		public override string GetDataTypeName(int ordinal)
		{
			this.ThrowOnClosedReader();
			return this.internalReader.GetDataTypeName(ordinal);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000C56C File Offset: 0x0000A76C
		public override Type GetFieldType(int ordinal)
		{
			this.ThrowOnClosedReader();
			this.ThrowOnOrdinalOutOfRange(ordinal);
			Type type = this.fieldTypes[ordinal];
			if (type == typeof(Guid))
			{
				type = typeof(string);
			}
			else if (type == typeof(Time))
			{
				type = typeof(TimeSpan);
			}
			else if (type == typeof(Date))
			{
				type = typeof(DateTime);
			}
			else if (type == typeof(Number))
			{
				type = typeof(decimal);
			}
			else if (type == typeof(Currency))
			{
				type = typeof(decimal);
			}
			else if (type == typeof(UnsupportedType))
			{
				type = typeof(object);
			}
			return type;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000C64B File Offset: 0x0000A84B
		public override bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C654 File Offset: 0x0000A854
		public override bool IsDBNull(int ordinal)
		{
			this.ThrowOnClosedReader();
			bool flag = this.internalReader.IsDBNull(ordinal);
			if (!flag && this.treatErrorsAsNull && this.fieldTypes[ordinal] == typeof(Number))
			{
				flag = this.GetValue(ordinal) == null;
			}
			return flag;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public override bool NextResult()
		{
			this.ThrowOnClosedReader();
			bool flag;
			try
			{
				using (new MashupReader.TimeoutChecker(this))
				{
					this.hasReadyData = false;
					if (this.internalReader.NextResult())
					{
						this.SetFieldTypes();
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
			}
			catch (Exception ex)
			{
				this.command.TryCheckException(ex);
				throw;
			}
			return flag;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C71C File Offset: 0x0000A91C
		private void CheckTimeout(object obj)
		{
			if (this.command == null)
			{
				return;
			}
			MashupCommand mashupCommand = null;
			object obj2 = this.lockObject;
			lock (obj2)
			{
				if (this.checkTimer && DateTime.UtcNow - this.lastTimestamp > TimeSpan.FromSeconds((double)this.timeout))
				{
					this.timedOut = true;
					mashupCommand = this.command;
				}
			}
			if (mashupCommand != null)
			{
				mashupCommand.Cancel();
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public override bool Read()
		{
			this.ThrowOnClosedReader();
			try
			{
				using (new MashupReader.TimeoutChecker(this))
				{
					this.hasReadyData = this.internalReader.Read();
				}
			}
			catch (Exception ex)
			{
				this.command.TryCheckException(ex);
				throw;
			}
			return this.hasReadyData;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000C814 File Offset: 0x0000AA14
		public override int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170000F2 RID: 242
		public override object this[string name]
		{
			get
			{
				this.ThrowOnClosedReader();
				this.ThrowOnNoReadyData();
				return this.GetValue(this.GetOrdinalWithArgumentValidation(name));
			}
		}

		// Token: 0x170000F3 RID: 243
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C83C File Offset: 0x0000AA3C
		public override Type GetProviderSpecificFieldType(int ordinal)
		{
			this.ThrowOnClosedReader();
			this.ThrowOnOrdinalOutOfRange(ordinal);
			Type type = this.fieldTypes[ordinal];
			if (type == typeof(Guid))
			{
				type = typeof(string);
			}
			else if (type == typeof(Time))
			{
				type = typeof(Time);
			}
			else if (type == typeof(Date))
			{
				type = typeof(Date);
			}
			else if (type == typeof(Number))
			{
				type = typeof(decimal);
			}
			else if (type == typeof(Currency))
			{
				type = typeof(decimal);
			}
			else if (type == typeof(UnsupportedType))
			{
				type = typeof(object);
			}
			return type;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C91C File Offset: 0x0000AB1C
		public override object GetProviderSpecificValue(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			object obj = this.internalReader.GetValue(ordinal);
			if (this.hasMetadata[ordinal])
			{
				obj = ((ValueWithMetadata)obj).Value;
			}
			if (obj is Guid)
			{
				obj = ((Guid)obj).ToString();
			}
			else if (obj is Time)
			{
				obj = new Time(((Time)obj).TimeSpan);
			}
			else if (obj is Date)
			{
				obj = new Date(((Date)obj).DateTime);
			}
			else if (obj is Number)
			{
				obj = ((Number)obj).ToDecimal();
			}
			else if (obj is Currency)
			{
				obj = ((Currency)obj).Value;
			}
			else if (obj is IDataRecord)
			{
				obj = new DataRecordWithMetadata((IDataRecord)obj);
			}
			return obj;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public override int GetProviderSpecificValues(object[] values)
		{
			this.ThrowOnClosedReader();
			this.ThrowOnNoReadyData();
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = ((values.Length < this.FieldCount) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetProviderSpecificValue(i);
			}
			return num;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000CA65 File Offset: 0x0000AC65
		public DateTimeOffset GetDateTimeOffset(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return (DateTimeOffset)this.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000CA7A File Offset: 0x0000AC7A
		public TimeSpan GetTimeSpan(int ordinal)
		{
			this.ThrowOnNotValidGet(ordinal);
			return (TimeSpan)this.GetValue(ordinal);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000CA8F File Offset: 0x0000AC8F
		public Date GetDate(int ordinal)
		{
			return (Date)this.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		public Time GetTime(int ordinal)
		{
			return (Time)this.GetProviderSpecificValue(ordinal);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000CAAB File Offset: 0x0000ACAB
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.command != null)
			{
				this.command.StopUpdatingProgress();
			}
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		private void SetFieldTypes()
		{
			this.fieldTypes = new Type[this.internalReader.FieldCount];
			this.hasMetadata = new bool[this.fieldTypes.Length];
			for (int i = 0; i < this.internalReader.FieldCount; i++)
			{
				this.hasMetadata[i] = ValueWithMetadata.HasMetadata(this.internalReader.GetFieldType(i), out this.fieldTypes[i]);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CB44 File Offset: 0x0000AD44
		private object ToDecimal(Number value)
		{
			object obj;
			try
			{
				obj = value.ToDecimal();
			}
			catch (OverflowException)
			{
				if (!this.treatErrorsAsNull)
				{
					throw;
				}
				obj = null;
			}
			return obj;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private int GetSize(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Object:
				if (type == typeof(object) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan) || type == typeof(UnsupportedType) || type == typeof(byte[]) || type == typeof(IDataRecord))
				{
					return -1;
				}
				break;
			case TypeCode.Boolean:
				return 1;
			case TypeCode.Char:
				return 2;
			case TypeCode.SByte:
				return 1;
			case TypeCode.Byte:
				return 1;
			case TypeCode.Int16:
				return 2;
			case TypeCode.UInt16:
				return 2;
			case TypeCode.Int32:
				return 4;
			case TypeCode.UInt32:
				return 4;
			case TypeCode.Int64:
				return 8;
			case TypeCode.UInt64:
				return 8;
			case TypeCode.Single:
				return 4;
			case TypeCode.Double:
				return 8;
			case TypeCode.Decimal:
				return 16;
			case TypeCode.DateTime:
				return -1;
			case TypeCode.String:
				return -1;
			}
			throw new InvalidOperationException(type.FullName);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000CC84 File Offset: 0x0000AE84
		private int GetOrdinalWithArgumentValidation(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(ProviderErrorStrings.ColumnNameCannotBeEmptyString, "name");
			}
			int ordinal;
			try
			{
				ordinal = this.internalReader.GetOrdinal(name);
			}
			catch (IndexOutOfRangeException)
			{
				throw new ArgumentException(ProviderErrorStrings.ColumnNameNotValid, "name");
			}
			return ordinal;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		private IDataReaderWithTableSchema CreateDataReader()
		{
			IDataReaderWithTableSchema dataReaderWithTableSchema;
			try
			{
				dataReaderWithTableSchema = new PageReaderDataReader(this.dataReaderSource.PageReader, this.GetErrorHandler(!this.treatErrorsAsNull), this.GetErrorHandler(this.command.Connection.ThrowEnumerationErrors));
			}
			catch (Exception ex)
			{
				this.command.TryCheckException(ex);
				throw;
			}
			return dataReaderWithTableSchema;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000CD44 File Offset: 0x0000AF44
		private void ThrowOnClosedReader()
		{
			if (this.isClosed)
			{
				throw new InvalidOperationException(ProviderErrorStrings.DataReaderClosed);
			}
			if (this.command.IsCanceled)
			{
				throw new InvalidOperationException(ProviderErrorStrings.CancelledCommand);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000CD71 File Offset: 0x0000AF71
		private void ThrowOnNotValidGet(int ordinal)
		{
			this.ThrowOnClosedReader();
			this.ThrowOnNoReadyData();
			this.ThrowOnOrdinalOutOfRange(ordinal);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000CD86 File Offset: 0x0000AF86
		private void ThrowOnNoReadyData()
		{
			if (!this.hasReadyData)
			{
				throw new InvalidOperationException(ProviderErrorStrings.CannotReadWhenNoData);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		private void ThrowOnOrdinalOutOfRange(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this.internalReader.FieldCount)
			{
				throw new ArgumentException(ProviderErrorStrings.OrdinalOutOfRange, "ordinal");
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		private Func<ISerializedException, Exception> GetErrorHandler(bool enabled)
		{
			if (enabled)
			{
				return new Func<ISerializedException, Exception>(this.HandleError);
			}
			return null;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		private Exception HandleError(ISerializedException properties)
		{
			ValueException2 valueException = MashupEngines.Version1.CreateExceptionFromProperties(properties) as ValueException2;
			if (valueException == null)
			{
				return new MashupValueException(properties["Message"], properties["Reason"]);
			}
			return AdoNetProviderContext.Instance.CreateValueKindException(valueException);
		}

		// Token: 0x04000190 RID: 400
		private static long lastId = -1L;

		// Token: 0x04000191 RID: 401
		private MashupCommand command;

		// Token: 0x04000192 RID: 402
		private readonly long readerId;

		// Token: 0x04000193 RID: 403
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x04000194 RID: 404
		private readonly bool treatErrorsAsNull;

		// Token: 0x04000195 RID: 405
		private readonly CommandBehavior commandBehavior;

		// Token: 0x04000196 RID: 406
		private readonly IDataReaderWithTableSchema internalReader;

		// Token: 0x04000197 RID: 407
		private readonly object lockObject;

		// Token: 0x04000198 RID: 408
		private readonly IDataReaderSource dataReaderSource;

		// Token: 0x04000199 RID: 409
		private readonly int timeout;

		// Token: 0x0400019A RID: 410
		private Type[] fieldTypes;

		// Token: 0x0400019B RID: 411
		private bool[] hasMetadata;

		// Token: 0x0400019C RID: 412
		private bool hasReadyData;

		// Token: 0x0400019D RID: 413
		private bool isClosed;

		// Token: 0x0400019E RID: 414
		private DateTime lastTimestamp;

		// Token: 0x0400019F RID: 415
		private Timer timer;

		// Token: 0x040001A0 RID: 416
		private bool timedOut;

		// Token: 0x040001A1 RID: 417
		private bool checkTimer;

		// Token: 0x0200007E RID: 126
		private struct TimeoutChecker : IDisposable
		{
			// Token: 0x060004DD RID: 1245 RVA: 0x00011EE4 File Offset: 0x000100E4
			public TimeoutChecker(MashupReader reader)
			{
				object lockObject = reader.lockObject;
				lock (lockObject)
				{
					reader.lastTimestamp = DateTime.UtcNow;
					reader.checkTimer = true;
				}
				this.reader = reader;
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00011F38 File Offset: 0x00010138
			public void Dispose()
			{
				MashupReader mashupReader = this.reader;
				if (mashupReader != null)
				{
					this.reader = null;
					object lockObject = mashupReader.lockObject;
					lock (lockObject)
					{
						mashupReader.checkTimer = false;
						if (mashupReader.timedOut)
						{
							throw new MashupHostingException(ProviderErrorStrings.CommandTimeoutExpired, "Timeout");
						}
					}
				}
			}

			// Token: 0x04000295 RID: 661
			private MashupReader reader;
		}
	}
}

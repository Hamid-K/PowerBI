using System;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;
using Microsoft.OleDb.Serialization;

namespace Microsoft.OleDb
{
	// Token: 0x02001E59 RID: 7769
	public abstract class Column : IOleDbColumn, IColumn
	{
		// Token: 0x0600BEC8 RID: 48840 RVA: 0x0026963C File Offset: 0x0026783C
		public static Column[] CreateColumns(TableSchema schema, int maxRowCount)
		{
			Column[] array = new Column[schema.ColumnCount];
			for (int i = 0; i < array.Length; i++)
			{
				SchemaColumn column = schema.GetColumn(i);
				array[i] = Column.Create(column, maxRowCount);
			}
			return array;
		}

		// Token: 0x0600BEC9 RID: 48841 RVA: 0x00269678 File Offset: 0x00267878
		public static Column Create(SchemaColumn column, int maxRowCount)
		{
			bool flag;
			ColumnType columnType = Column.GetColumnType(column.DataType, out flag);
			if (column.ColumnSchema != null)
			{
				return Column.AugmentColumn(new TypedRecordColumn(maxRowCount, column.ColumnSchema), column.Nullable, flag, maxRowCount);
			}
			return Column.Create(columnType, column.Nullable, flag, maxRowCount);
		}

		// Token: 0x0600BECA RID: 48842 RVA: 0x002696C4 File Offset: 0x002678C4
		public static Column Create(Type type, bool nullable, int maxRowCount)
		{
			bool flag;
			return Column.Create(Column.GetColumnType(type, out flag), nullable, flag, maxRowCount);
		}

		// Token: 0x0600BECB RID: 48843 RVA: 0x002696E1 File Offset: 0x002678E1
		public static Column Create(ColumnType type, bool nullable, bool hasMetadata, int maxRowCount)
		{
			return Column.AugmentColumn(Column.CreateColumn(type, hasMetadata, maxRowCount), nullable, hasMetadata, maxRowCount);
		}

		// Token: 0x0600BECC RID: 48844 RVA: 0x002696F3 File Offset: 0x002678F3
		protected static Column AugmentColumn(Column column, bool nullable, bool hasMetadata, int maxRowCount)
		{
			if (nullable)
			{
				column = new NullableColumn(column, maxRowCount);
			}
			if (hasMetadata)
			{
				column = new ColumnWithMetadata(column, maxRowCount);
			}
			return column;
		}

		// Token: 0x0600BECD RID: 48845 RVA: 0x00269710 File Offset: 0x00267910
		private static Column CreateColumn(ColumnType type, bool hasMetadata, int maxRowCount)
		{
			switch (type)
			{
			case ColumnType.Boolean:
				return new BooleanColumn(maxRowCount);
			case ColumnType.SByte:
				return new SByteColumn(maxRowCount);
			case ColumnType.Int16:
				return new Int16Column(maxRowCount);
			case ColumnType.Int32:
				return new Int32Column(maxRowCount);
			case ColumnType.Int64:
				return new Int64Column(maxRowCount);
			case ColumnType.Byte:
				return new ByteColumn(maxRowCount);
			case ColumnType.UInt16:
				return new UInt16Column(maxRowCount);
			case ColumnType.UInt32:
				return new UInt32Column(maxRowCount);
			case ColumnType.UInt64:
				return new UInt64Column(maxRowCount);
			case ColumnType.Single:
				return new SingleColumn(maxRowCount);
			case ColumnType.Double:
				return new DoubleColumn(maxRowCount);
			case ColumnType.Decimal:
				return new DecimalColumn(maxRowCount);
			case ColumnType.Currency:
				return new CurrencyColumn(maxRowCount);
			case ColumnType.Numeric:
				return new NumericColumn(maxRowCount);
			case ColumnType.Time:
				return new TimeColumn(maxRowCount);
			case ColumnType.Date:
				return new DateColumn(maxRowCount);
			case ColumnType.DateTime:
				return new DateTimeColumn(maxRowCount);
			case ColumnType.DateTimeOffset:
				return new DateTimeOffsetColumn(maxRowCount);
			case ColumnType.TimeSpan:
				return new TimeSpanColumn(maxRowCount);
			case ColumnType.String:
				return new StringColumn(maxRowCount);
			case ColumnType.Binary:
				return new BinaryColumn(maxRowCount);
			case ColumnType.Object:
				return new ObjectColumn(maxRowCount);
			case ColumnType.TypedRecord:
				throw new InvalidOperationException();
			case ColumnType.UntypedRecord:
				return new UntypedRecordColumn(hasMetadata, maxRowCount);
			case ColumnType.Guid:
				return new GuidColumn(maxRowCount);
			case ColumnType.Error:
				return new ErrorColumn(maxRowCount);
			case ColumnType.Null:
				return new NullColumn(0);
			case ColumnType.Unsupported:
				return new UnsupportedTypeColumn();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600BECE RID: 48846 RVA: 0x00269860 File Offset: 0x00267A60
		public static Type GetType(ColumnType columnType)
		{
			switch (columnType)
			{
			case ColumnType.Boolean:
				return typeof(bool);
			case ColumnType.SByte:
				return typeof(sbyte);
			case ColumnType.Int16:
				return typeof(short);
			case ColumnType.Int32:
				return typeof(int);
			case ColumnType.Int64:
				return typeof(long);
			case ColumnType.Byte:
				return typeof(byte);
			case ColumnType.UInt16:
				return typeof(ushort);
			case ColumnType.UInt32:
				return typeof(uint);
			case ColumnType.UInt64:
				return typeof(ulong);
			case ColumnType.Single:
				return typeof(float);
			case ColumnType.Double:
				return typeof(double);
			case ColumnType.Decimal:
				return typeof(decimal);
			case ColumnType.Currency:
				return typeof(Currency);
			case ColumnType.Numeric:
				return typeof(Number);
			case ColumnType.Time:
				return typeof(Time);
			case ColumnType.Date:
				return typeof(Date);
			case ColumnType.DateTime:
				return typeof(DateTime);
			case ColumnType.DateTimeOffset:
				return typeof(DateTimeOffset);
			case ColumnType.TimeSpan:
				return typeof(TimeSpan);
			case ColumnType.String:
				return typeof(string);
			case ColumnType.Binary:
				return typeof(byte[]);
			case ColumnType.Object:
				return typeof(object);
			case ColumnType.TypedRecord:
			case ColumnType.UntypedRecord:
				return typeof(IDataRecord);
			case ColumnType.Guid:
				return typeof(Guid);
			case ColumnType.Error:
				return typeof(ErrorWrapper);
			case ColumnType.Null:
				return null;
			case ColumnType.Unsupported:
				return typeof(UnsupportedType);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600BECF RID: 48847 RVA: 0x00269A10 File Offset: 0x00267C10
		public static ColumnType GetColumnType(Type type)
		{
			bool flag;
			return Column.GetColumnType(type, out flag);
		}

		// Token: 0x0600BED0 RID: 48848 RVA: 0x00269A28 File Offset: 0x00267C28
		public static ColumnType GetColumnType(Type type, out bool hasMetadata)
		{
			hasMetadata = false;
			switch (global::System.Type.GetTypeCode(type))
			{
			case TypeCode.DBNull:
				return ColumnType.Null;
			case TypeCode.Boolean:
				return ColumnType.Boolean;
			case TypeCode.SByte:
				return ColumnType.SByte;
			case TypeCode.Byte:
				return ColumnType.Byte;
			case TypeCode.Int16:
				return ColumnType.Int16;
			case TypeCode.UInt16:
				return ColumnType.UInt16;
			case TypeCode.Int32:
				return ColumnType.Int32;
			case TypeCode.UInt32:
				return ColumnType.UInt32;
			case TypeCode.Int64:
				return ColumnType.Int64;
			case TypeCode.UInt64:
				return ColumnType.UInt64;
			case TypeCode.Single:
				return ColumnType.Single;
			case TypeCode.Double:
				return ColumnType.Double;
			case TypeCode.Decimal:
				return ColumnType.Decimal;
			case TypeCode.DateTime:
				return ColumnType.DateTime;
			case TypeCode.String:
				return ColumnType.String;
			}
			if (type == typeof(Guid))
			{
				return ColumnType.Guid;
			}
			if (type == typeof(Date))
			{
				return ColumnType.Date;
			}
			if (type == typeof(Time))
			{
				return ColumnType.Time;
			}
			if (type == typeof(DateTimeOffset))
			{
				return ColumnType.DateTimeOffset;
			}
			if (type == typeof(TimeSpan))
			{
				return ColumnType.TimeSpan;
			}
			if (type == typeof(ErrorWrapper))
			{
				return ColumnType.Error;
			}
			if (type == typeof(Number))
			{
				return ColumnType.Numeric;
			}
			if (type == typeof(byte[]))
			{
				return ColumnType.Binary;
			}
			if (type == typeof(Currency))
			{
				return ColumnType.Currency;
			}
			if (type == typeof(object))
			{
				return ColumnType.Object;
			}
			if (typeof(IDataRecord).IsAssignableFrom(type))
			{
				return ColumnType.UntypedRecord;
			}
			if (type == null)
			{
				return ColumnType.Null;
			}
			hasMetadata = ValueWithMetadata.HasMetadata(type, out type);
			if (hasMetadata)
			{
				return Column.GetColumnType(type);
			}
			return ColumnType.Unsupported;
		}

		// Token: 0x0600BED1 RID: 48849 RVA: 0x00269BBC File Offset: 0x00267DBC
		public static ColumnType GetColumnType(DBTYPE dbType)
		{
			if (dbType <= DBTYPE.DBTIMESTAMP)
			{
				switch (dbType)
				{
				case DBTYPE.NULL:
					return ColumnType.Null;
				case DBTYPE.I2:
					return ColumnType.Int16;
				case DBTYPE.I4:
					return ColumnType.Int32;
				case DBTYPE.R4:
					return ColumnType.Single;
				case DBTYPE.R8:
					return ColumnType.Double;
				case DBTYPE.CY:
				case DBTYPE.IDISPATCH:
				case (DBTYPE)15:
					break;
				case DBTYPE.DATE:
					return ColumnType.DateTime;
				case DBTYPE.BSTR:
					return ColumnType.String;
				case DBTYPE.ERROR:
					return ColumnType.Error;
				case DBTYPE.BOOL:
					return ColumnType.Boolean;
				case DBTYPE.VARIANT:
					return ColumnType.Object;
				case DBTYPE.IUNKNOWN:
					return ColumnType.Binary;
				case DBTYPE.DECIMAL:
					return ColumnType.Decimal;
				case DBTYPE.I1:
					return ColumnType.SByte;
				case DBTYPE.UI1:
					return ColumnType.Byte;
				case DBTYPE.UI2:
					return ColumnType.UInt16;
				case DBTYPE.UI4:
					return ColumnType.UInt32;
				case DBTYPE.I8:
					return ColumnType.Int64;
				case DBTYPE.UI8:
					return ColumnType.UInt64;
				default:
					if (dbType == DBTYPE.GUID)
					{
						return ColumnType.Guid;
					}
					switch (dbType)
					{
					case DBTYPE.BYTES:
						return ColumnType.Binary;
					case DBTYPE.STR:
						return ColumnType.String;
					case DBTYPE.WSTR:
						return ColumnType.String;
					case DBTYPE.NUMERIC:
						return ColumnType.Numeric;
					case DBTYPE.DBDATE:
						return ColumnType.Date;
					case DBTYPE.DBTIME:
						return ColumnType.Time;
					case DBTYPE.DBTIMESTAMP:
						return ColumnType.DateTime;
					}
					break;
				}
			}
			else
			{
				if (dbType == DBTYPE.DBTIME2)
				{
					return ColumnType.Time;
				}
				if (dbType == DBTYPE.DBTIMESTAMPOFFSET)
				{
					return ColumnType.DateTimeOffset;
				}
				if (dbType == DBTYPE.DBDURATION)
				{
					return ColumnType.TimeSpan;
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600BED2 RID: 48850 RVA: 0x00269CD0 File Offset: 0x00267ED0
		public unsafe static int GetColumnSizeEstimate(ColumnType type, int length)
		{
			switch (type)
			{
			case ColumnType.Boolean:
				return 1;
			case ColumnType.SByte:
				return 1;
			case ColumnType.Int16:
				return 2;
			case ColumnType.Int32:
				return 4;
			case ColumnType.Int64:
				return 8;
			case ColumnType.Byte:
				return 1;
			case ColumnType.UInt16:
				return 2;
			case ColumnType.UInt32:
				return 4;
			case ColumnType.UInt64:
				return 8;
			case ColumnType.Single:
				return 4;
			case ColumnType.Double:
				return 8;
			case ColumnType.Decimal:
				return 16;
			case ColumnType.Currency:
				return 16;
			case ColumnType.Numeric:
				return sizeof(Number);
			case ColumnType.Time:
				return sizeof(TimeSpan);
			case ColumnType.Date:
				return sizeof(DateTime);
			case ColumnType.DateTime:
				return sizeof(DateTime);
			case ColumnType.DateTimeOffset:
				return sizeof(DateTimeOffset);
			case ColumnType.TimeSpan:
				return sizeof(TimeSpan);
			case ColumnType.String:
				return 2 * Math.Min(length, 1073741823);
			case ColumnType.Binary:
				return Math.Min(length, 1073741823);
			case ColumnType.Object:
			case ColumnType.Error:
			case ColumnType.Unsupported:
				return sizeof(IntPtr);
			case ColumnType.TypedRecord:
				return 2 * Math.Min(length, 1073741823);
			case ColumnType.UntypedRecord:
				return 4 * Math.Min(length, 1073741823);
			case ColumnType.Guid:
				return sizeof(Guid);
			case ColumnType.Null:
				return 0;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600BED3 RID: 48851 RVA: 0x00269DEC File Offset: 0x00267FEC
		protected void CheckNull(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", string.Format(CultureInfo.InvariantCulture, "Null was added at row {0}.", this.RowCount.ToString(CultureInfo.InvariantCulture)));
			}
		}

		// Token: 0x0600BED4 RID: 48852 RVA: 0x00269E29 File Offset: 0x00268029
		protected static void AddValueToColumn(Column column, object value)
		{
			if (value == DBNull.Value)
			{
				column.AddNull();
				return;
			}
			column.AddValue(value);
		}

		// Token: 0x0600BED5 RID: 48853 RVA: 0x00269E41 File Offset: 0x00268041
		private Exception CreateCastException(string toType)
		{
			return new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "Unable to cast {0} to type {1}.", base.GetType().Name, toType));
		}

		// Token: 0x0600BED6 RID: 48854 RVA: 0x00269E63 File Offset: 0x00268063
		private Exception CreateAddException(string fromType)
		{
			return new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "Unable to add {0} to a {1}.", fromType, base.GetType().Name));
		}

		// Token: 0x17002EF0 RID: 12016
		// (get) Token: 0x0600BED7 RID: 48855
		public abstract ColumnType Type { get; }

		// Token: 0x17002EF1 RID: 12017
		// (get) Token: 0x0600BED8 RID: 48856
		public abstract int RowCount { get; }

		// Token: 0x0600BED9 RID: 48857
		public abstract void AddValue(object value);

		// Token: 0x0600BEDA RID: 48858 RVA: 0x00269E85 File Offset: 0x00268085
		public virtual void AddBoolean(bool value)
		{
			throw this.CreateAddException("Boolean");
		}

		// Token: 0x0600BEDB RID: 48859 RVA: 0x00269E92 File Offset: 0x00268092
		public virtual void AddByte(byte value)
		{
			throw this.CreateAddException("Byte");
		}

		// Token: 0x0600BEDC RID: 48860 RVA: 0x00269E9F File Offset: 0x0026809F
		public virtual void AddUInt16(ushort value)
		{
			throw this.CreateAddException("UInt16");
		}

		// Token: 0x0600BEDD RID: 48861 RVA: 0x00269EAC File Offset: 0x002680AC
		public virtual void AddUInt32(uint value)
		{
			throw this.CreateAddException("UInt32");
		}

		// Token: 0x0600BEDE RID: 48862 RVA: 0x00269EB9 File Offset: 0x002680B9
		public virtual void AddUInt64(ulong value)
		{
			throw this.CreateAddException("UInt64");
		}

		// Token: 0x0600BEDF RID: 48863 RVA: 0x00269EC6 File Offset: 0x002680C6
		public virtual void AddSByte(sbyte value)
		{
			throw this.CreateAddException("SByte");
		}

		// Token: 0x0600BEE0 RID: 48864 RVA: 0x00269ED3 File Offset: 0x002680D3
		public virtual void AddInt16(short value)
		{
			throw this.CreateAddException("Int16");
		}

		// Token: 0x0600BEE1 RID: 48865 RVA: 0x00269EE0 File Offset: 0x002680E0
		public virtual void AddInt32(int value)
		{
			throw this.CreateAddException("Int32");
		}

		// Token: 0x0600BEE2 RID: 48866 RVA: 0x00269EED File Offset: 0x002680ED
		public virtual void AddInt64(long value)
		{
			throw this.CreateAddException("Int64");
		}

		// Token: 0x0600BEE3 RID: 48867 RVA: 0x00269EFA File Offset: 0x002680FA
		public virtual void AddFloat(float value)
		{
			throw this.CreateAddException("Float");
		}

		// Token: 0x0600BEE4 RID: 48868 RVA: 0x00269F07 File Offset: 0x00268107
		public virtual void AddDouble(double value)
		{
			throw this.CreateAddException("Double");
		}

		// Token: 0x0600BEE5 RID: 48869 RVA: 0x00269F14 File Offset: 0x00268114
		public virtual void AddDecimal(decimal value)
		{
			throw this.CreateAddException("Decimal");
		}

		// Token: 0x0600BEE6 RID: 48870 RVA: 0x00269F21 File Offset: 0x00268121
		public virtual void AddNumber(Number number)
		{
			throw this.CreateAddException("Number");
		}

		// Token: 0x0600BEE7 RID: 48871 RVA: 0x00269F2E File Offset: 0x0026812E
		public virtual void AddCurrency(Currency value)
		{
			throw this.CreateAddException("Currency");
		}

		// Token: 0x0600BEE8 RID: 48872 RVA: 0x00269F3B File Offset: 0x0026813B
		public virtual void AddDate(Date value)
		{
			throw this.CreateAddException("Date");
		}

		// Token: 0x0600BEE9 RID: 48873 RVA: 0x00269F48 File Offset: 0x00268148
		public virtual void AddTime(Time value)
		{
			throw this.CreateAddException("Time");
		}

		// Token: 0x0600BEEA RID: 48874 RVA: 0x00269F55 File Offset: 0x00268155
		public virtual void AddDateTime(DateTime value)
		{
			throw this.CreateAddException("DateTime");
		}

		// Token: 0x0600BEEB RID: 48875 RVA: 0x00269F62 File Offset: 0x00268162
		public virtual void AddDateTimeOffset(DateTimeOffset value)
		{
			throw this.CreateAddException("DateTimeOffset");
		}

		// Token: 0x0600BEEC RID: 48876 RVA: 0x00269F6F File Offset: 0x0026816F
		public virtual void AddTimeSpan(TimeSpan value)
		{
			throw this.CreateAddException("TimeSpan");
		}

		// Token: 0x0600BEED RID: 48877 RVA: 0x00269F7C File Offset: 0x0026817C
		public virtual void AddGuid(Guid value)
		{
			throw this.CreateAddException("Guid");
		}

		// Token: 0x0600BEEE RID: 48878
		public unsafe abstract void AddValue(DBTYPE type, void* value, int length);

		// Token: 0x0600BEEF RID: 48879
		public abstract bool TryAddValue(object value);

		// Token: 0x0600BEF0 RID: 48880
		public abstract void AddNull();

		// Token: 0x0600BEF1 RID: 48881
		public abstract bool IsNull(int row);

		// Token: 0x0600BEF2 RID: 48882
		public abstract void Clear();

		// Token: 0x0600BEF3 RID: 48883
		public abstract object GetObject(int row);

		// Token: 0x0600BEF4 RID: 48884 RVA: 0x00269F89 File Offset: 0x00268189
		public virtual bool GetBoolean(int row)
		{
			throw this.CreateCastException("Boolean");
		}

		// Token: 0x0600BEF5 RID: 48885 RVA: 0x00269F96 File Offset: 0x00268196
		public virtual byte GetByte(int row)
		{
			throw this.CreateCastException("Byte");
		}

		// Token: 0x0600BEF6 RID: 48886 RVA: 0x00269FA3 File Offset: 0x002681A3
		public virtual short GetInt16(int row)
		{
			throw this.CreateCastException("Int16");
		}

		// Token: 0x0600BEF7 RID: 48887 RVA: 0x00269FB0 File Offset: 0x002681B0
		public virtual int GetInt32(int row)
		{
			throw this.CreateCastException("Int32");
		}

		// Token: 0x0600BEF8 RID: 48888 RVA: 0x00269FBD File Offset: 0x002681BD
		public virtual long GetInt64(int row)
		{
			throw this.CreateCastException("Int64");
		}

		// Token: 0x0600BEF9 RID: 48889 RVA: 0x00269FCA File Offset: 0x002681CA
		public virtual float GetFloat(int row)
		{
			throw this.CreateCastException("Float");
		}

		// Token: 0x0600BEFA RID: 48890 RVA: 0x00269FD7 File Offset: 0x002681D7
		public virtual Guid GetGuid(int row)
		{
			throw this.CreateCastException("Guid");
		}

		// Token: 0x0600BEFB RID: 48891 RVA: 0x00269FE4 File Offset: 0x002681E4
		public virtual double GetDouble(int row)
		{
			throw this.CreateCastException("Double");
		}

		// Token: 0x0600BEFC RID: 48892 RVA: 0x00269FF1 File Offset: 0x002681F1
		public virtual decimal GetDecimal(int row)
		{
			throw this.CreateCastException("Decimal");
		}

		// Token: 0x0600BEFD RID: 48893 RVA: 0x00269FFE File Offset: 0x002681FE
		public virtual DateTime GetDateTime(int row)
		{
			throw this.CreateCastException("DateTime");
		}

		// Token: 0x0600BEFE RID: 48894 RVA: 0x0026A00B File Offset: 0x0026820B
		public virtual string GetString(int row)
		{
			throw this.CreateCastException("String");
		}

		// Token: 0x0600BEFF RID: 48895
		public unsafe abstract DBSTATUS GetValue(int row, IManagedDataConvert dataConvert, Binding binding, byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600BF00 RID: 48896
		public abstract void Serialize(PageWriter writer);

		// Token: 0x0600BF01 RID: 48897
		public abstract void Deserialize(PageReader reader);
	}
}

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000010 RID: 16
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class Column : IColumn
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000044 RID: 68
		public abstract ColumnType Type { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69
		public abstract int RowCount { get; }

		// Token: 0x06000046 RID: 70
		public abstract void AddValue(object value);

		// Token: 0x06000047 RID: 71
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe abstract void AddValue(DBTYPE type, void* value, int length);

		// Token: 0x06000048 RID: 72
		public abstract bool TryAddValue(object value);

		// Token: 0x06000049 RID: 73
		public abstract void AddNull();

		// Token: 0x0600004A RID: 74
		public abstract bool IsNull(int row);

		// Token: 0x0600004B RID: 75
		public abstract void Clear();

		// Token: 0x0600004C RID: 76
		public abstract object GetObject(int row);

		// Token: 0x0600004D RID: 77
		public unsafe abstract DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength);

		// Token: 0x0600004E RID: 78
		public abstract void Serialize(PageWriter writer);

		// Token: 0x0600004F RID: 79
		public abstract void Deserialize(PageReader reader);

		// Token: 0x06000050 RID: 80 RVA: 0x00002A4E File Offset: 0x00000C4E
		public static Column Create(Type type, bool nullable, int maxRowCount)
		{
			return Column.Create(Column.GetColumnType(type), nullable, maxRowCount);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A60 File Offset: 0x00000C60
		public static Column Create(ColumnType type, bool nullable, int maxRowCount)
		{
			Column column = Column.Create(type, maxRowCount);
			if (nullable)
			{
				column = new NullableColumn(column, maxRowCount);
			}
			return column;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A84 File Offset: 0x00000C84
		private static Column Create(ColumnType type, int maxRowCount)
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
			case ColumnType.Guid:
				return new GuidColumn(maxRowCount);
			case ColumnType.Error:
				return new ErrorColumn(maxRowCount);
			case ColumnType.Null:
				return new NullColumn();
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BB4 File Offset: 0x00000DB4
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
			case ColumnType.Guid:
				return typeof(Guid);
			case ColumnType.Error:
				return typeof(ErrorWrapper);
			case ColumnType.Null:
				return null;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002D40 File Offset: 0x00000F40
		public static ColumnType GetColumnType(Type type)
		{
			switch (global::System.Type.GetTypeCode(type))
			{
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
			if (type == null)
			{
				return ColumnType.Null;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002EA4 File Offset: 0x000010A4
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

		// Token: 0x06000056 RID: 86 RVA: 0x00002FB8 File Offset: 0x000011B8
		private Exception CreateCastException(string toType)
		{
			return new InvalidCastException(string.Format(CultureInfo.InvariantCulture, "Unable to cast {0} to type {1}.", base.GetType().Name, toType));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FDA File Offset: 0x000011DA
		public virtual bool GetBoolean(int row)
		{
			throw this.CreateCastException("Boolean");
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FE7 File Offset: 0x000011E7
		public virtual byte GetByte(int row)
		{
			throw this.CreateCastException("Byte");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FF4 File Offset: 0x000011F4
		public virtual short GetInt16(int row)
		{
			throw this.CreateCastException("Int16");
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003001 File Offset: 0x00001201
		public virtual int GetInt32(int row)
		{
			throw this.CreateCastException("Int32");
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000300E File Offset: 0x0000120E
		public virtual long GetInt64(int row)
		{
			throw this.CreateCastException("Int64");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000301B File Offset: 0x0000121B
		public virtual float GetFloat(int row)
		{
			throw this.CreateCastException("Float");
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003028 File Offset: 0x00001228
		public virtual Guid GetGuid(int row)
		{
			throw this.CreateCastException("Guid");
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003035 File Offset: 0x00001235
		public virtual double GetDouble(int row)
		{
			throw this.CreateCastException("Double");
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003042 File Offset: 0x00001242
		public virtual decimal GetDecimal(int row)
		{
			throw this.CreateCastException("Decimal");
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000304F File Offset: 0x0000124F
		public virtual DateTime GetDateTime(int row)
		{
			throw this.CreateCastException("DateTime");
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000305C File Offset: 0x0000125C
		public virtual string GetString(int row)
		{
			throw this.CreateCastException("String");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000306C File Offset: 0x0000126C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		protected unsafe DBSTATUS ConvertDateTimeToDouble(DateTime dateTime, byte* destValue, out DBLENGTH destLength)
		{
			destLength = DbLength.Double;
			double? num = OleDbConvert.SafeToOADate(dateTime);
			if (num != null)
			{
				*(double*)destValue = num.Value;
				return DBSTATUS.S_OK;
			}
			*(double*)destValue = 0.0;
			return DBSTATUS.S_ISNULL;
		}
	}
}

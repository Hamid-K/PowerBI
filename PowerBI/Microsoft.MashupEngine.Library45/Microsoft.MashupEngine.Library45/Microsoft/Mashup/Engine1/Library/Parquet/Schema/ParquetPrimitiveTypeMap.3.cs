using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Parquet;
using Microsoft.OleDb;
using ParquetSharp;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F80 RID: 8064
	internal sealed class ParquetPrimitiveTypeMap<TPhysical, TLogical> : ParquetPrimitiveTypeMap<TPhysical>
	{
		// Token: 0x06010EAB RID: 69291 RVA: 0x003A4BE5 File Offset: 0x003A2DE5
		public ParquetPrimitiveTypeMap(LogicalTypeEnum logicalTypeEnum, Func<LogicalType> logicalTypeCtor, Func<TPhysical, TLogical> toLogical, Func<IAllocator, TLogical, TPhysical> fromLogical, TypeFacets facets = null, int? typeLength = null)
			: this(logicalTypeEnum, logicalTypeCtor, ParquetPrimitiveTypeMap<TPhysical, TLogical>.GetMType(typeof(TLogical)), toLogical, fromLogical, facets, typeLength)
		{
		}

		// Token: 0x06010EAC RID: 69292 RVA: 0x003A4C08 File Offset: 0x003A2E08
		private ParquetPrimitiveTypeMap(LogicalTypeEnum logicalTypeEnum, Func<LogicalType> logicalTypeCtor, TypeValue typeValue, Func<TPhysical, TLogical> toLogical, Func<IAllocator, TLogical, TPhysical> fromLogical, TypeFacets facets = null, int? typeLength = null)
			: base(logicalTypeEnum, logicalTypeCtor, typeValue, facets, typeLength)
		{
			this.toLogical = toLogical;
			this.fromLogical = fromLogical;
			if (this.IsOleDbCompatible)
			{
				this.columnIndexer = this.GetColumnIndexerMethod();
				return;
			}
			this.columnIndexer = delegate(IColumn c, int i)
			{
				throw new NotSupportedException();
			};
		}

		// Token: 0x17002CCF RID: 11471
		// (get) Token: 0x06010EAD RID: 69293 RVA: 0x003A4C6C File Offset: 0x003A2E6C
		public override bool IsOleDbCompatible
		{
			get
			{
				Type typeFromHandle = typeof(TLogical);
				return typeFromHandle == typeof(UnsupportedType) || ParquetPrimitiveTypeMap<TPhysical, TLogical>.GetOleDbColumnType(typeFromHandle) != ColumnType.Unsupported;
			}
		}

		// Token: 0x17002CD0 RID: 11472
		// (get) Token: 0x06010EAE RID: 69294 RVA: 0x003A4CA5 File Offset: 0x003A2EA5
		public override Type Type
		{
			get
			{
				return Microsoft.OleDb.Column.GetType(this.ColumnType);
			}
		}

		// Token: 0x17002CD1 RID: 11473
		// (get) Token: 0x06010EAF RID: 69295 RVA: 0x003A4CB4 File Offset: 0x003A2EB4
		private ColumnType ColumnType
		{
			get
			{
				Type typeFromHandle = typeof(TLogical);
				if (typeFromHandle == typeof(UnsupportedType))
				{
					return ColumnType.Unsupported;
				}
				ColumnType oleDbColumnType = ParquetPrimitiveTypeMap<TPhysical, TLogical>.GetOleDbColumnType(typeFromHandle);
				if (oleDbColumnType == ColumnType.Unsupported)
				{
					throw new NotSupportedException();
				}
				return oleDbColumnType;
			}
		}

		// Token: 0x06010EB0 RID: 69296 RVA: 0x003A4CF2 File Offset: 0x003A2EF2
		public TLogical ToLogical(TPhysical physical)
		{
			return this.toLogical(physical);
		}

		// Token: 0x06010EB1 RID: 69297 RVA: 0x003A4D00 File Offset: 0x003A2F00
		public TPhysical FromLogical(IAllocator allocator, TLogical logical)
		{
			return this.fromLogical(allocator, logical);
		}

		// Token: 0x06010EB2 RID: 69298 RVA: 0x003A4D0F File Offset: 0x003A2F0F
		public override Action<TPhysical> GetColumnLoader(Microsoft.OleDb.Column column)
		{
			Action<TLogical> addMethod = this.GetColumnAddMethod(column);
			return delegate(TPhysical value)
			{
				try
				{
					addMethod(this.ToLogical(value));
				}
				catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex) && !(ex is ValueException))
				{
					throw ValueException.NewDataFormatError<Message1>(Resources.LoadError(ex.Message), Value.Null, ex);
				}
			};
		}

		// Token: 0x06010EB3 RID: 69299 RVA: 0x003A4D35 File Offset: 0x003A2F35
		public override TPhysical GetFromColumn(IAllocator allocator, IColumn column, int row)
		{
			return this.fromLogical(allocator, this.columnIndexer(column, row));
		}

		// Token: 0x06010EB4 RID: 69300 RVA: 0x003A4D50 File Offset: 0x003A2F50
		public override Value ToValue(TPhysical physical)
		{
			Value value;
			try
			{
				value = ParquetPrimitiveTypeMap<TPhysical, TLogical>.MarshalFromClr(this.ToLogical(physical));
			}
			catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex) && !(ex is ValueException))
			{
				throw ValueException.NewDataFormatError<Message1>(Resources.LoadError(ex.Message), Value.Null, ex);
			}
			return value;
		}

		// Token: 0x06010EB5 RID: 69301 RVA: 0x003A4DC4 File Offset: 0x003A2FC4
		public override TPhysical FromValue(IAllocator allocator, Value value)
		{
			TPhysical tphysical;
			try
			{
				tphysical = this.FromLogical(allocator, (TLogical)((object)ParquetPrimitiveTypeMap<TPhysical, TLogical>.MarshalToClr(value, base.TypeValue)));
			}
			catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex) && !(ex is ValueException))
			{
				throw ValueException.NewDataFormatError<Message1>(Resources.WriteError(ex.Message), Value.Null, ex);
			}
			return tphysical;
		}

		// Token: 0x06010EB6 RID: 69302 RVA: 0x003A4E40 File Offset: 0x003A3040
		private Action<TLogical> GetColumnAddMethod(Microsoft.OleDb.Column column)
		{
			switch (this.ColumnType)
			{
			case ColumnType.Boolean:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<bool>(new Action<bool>(column.AddBoolean));
			case ColumnType.SByte:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<sbyte>(new Action<sbyte>(column.AddSByte));
			case ColumnType.Int16:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<short>(new Action<short>(column.AddInt16));
			case ColumnType.Int32:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<int>(new Action<int>(column.AddInt32));
			case ColumnType.Int64:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<long>(new Action<long>(column.AddInt64));
			case ColumnType.Byte:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<byte>(new Action<byte>(column.AddByte));
			case ColumnType.UInt16:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<ushort>(new Action<ushort>(column.AddUInt16));
			case ColumnType.UInt32:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<uint>(new Action<uint>(column.AddUInt32));
			case ColumnType.UInt64:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<ulong>(new Action<ulong>(column.AddUInt64));
			case ColumnType.Single:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<float>(new Action<float>(column.AddFloat));
			case ColumnType.Double:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<double>(new Action<double>(column.AddDouble));
			case ColumnType.Decimal:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<decimal>(new Action<decimal>(column.AddDecimal));
			case ColumnType.Currency:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Currency>(new Action<Currency>(column.AddCurrency));
			case ColumnType.Numeric:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Number>(new Action<Number>(column.AddNumber));
			case ColumnType.Time:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Time>(new Action<Time>(column.AddTime));
			case ColumnType.Date:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Microsoft.OleDb.Date>(new Action<Microsoft.OleDb.Date>(column.AddDate));
			case ColumnType.DateTime:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<DateTime>(new Action<DateTime>(column.AddDateTime));
			case ColumnType.DateTimeOffset:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<DateTimeOffset>(new Action<DateTimeOffset>(column.AddDateTimeOffset));
			case ColumnType.TimeSpan:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<TimeSpan>(new Action<TimeSpan>(column.AddTimeSpan));
			case ColumnType.Guid:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Guid>(new Action<Guid>(column.AddGuid));
			}
			return delegate(TLogical value)
			{
				column.AddValue(value);
			};
		}

		// Token: 0x06010EB7 RID: 69303 RVA: 0x003A50BC File Offset: 0x003A32BC
		private Func<IColumn, int, TLogical> GetColumnIndexerMethod()
		{
			ColumnType columnType = this.ColumnType;
			switch (columnType)
			{
			case ColumnType.Boolean:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<bool>((IColumn c, int i) => c.GetBoolean(i));
			case ColumnType.SByte:
			case ColumnType.UInt16:
			case ColumnType.UInt32:
			case ColumnType.UInt64:
			case ColumnType.Single:
			case ColumnType.Currency:
			case ColumnType.Time:
			case ColumnType.Date:
				break;
			case ColumnType.Int16:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<short>((IColumn c, int i) => c.GetInt16(i));
			case ColumnType.Int32:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<int>((IColumn c, int i) => c.GetInt32(i));
			case ColumnType.Int64:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<long>((IColumn c, int i) => c.GetInt64(i));
			case ColumnType.Byte:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<byte>((IColumn c, int i) => c.GetByte(i));
			case ColumnType.Double:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<double>((IColumn c, int i) => c.GetDouble(i));
			case ColumnType.Decimal:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<decimal>((IColumn c, int i) => c.GetDecimal(i));
			case ColumnType.Numeric:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Number>(delegate(IColumn c, int i)
				{
					IConformingColumn<Number> conformingColumn = c as IConformingColumn<Number>;
					if (conformingColumn != null)
					{
						return conformingColumn.GetValue(i);
					}
					return (Number)c.GetObject(i);
				});
			case ColumnType.DateTime:
				return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<DateTime>((IColumn c, int i) => c.GetDateTime(i));
			default:
				if (columnType == ColumnType.Guid)
				{
					return ParquetPrimitiveTypeMap<TPhysical, TLogical>.Cast<Guid>((IColumn c, int i) => c.GetGuid(i));
				}
				break;
			}
			return (IColumn c, int i) => (TLogical)((object)c.GetObject(i));
		}

		// Token: 0x06010EB8 RID: 69304 RVA: 0x003A52B8 File Offset: 0x003A34B8
		public static Value MarshalFromClr(object instance)
		{
			Value value = instance as Value;
			if (value != null)
			{
				return value;
			}
			if (instance is TimeSpan)
			{
				return DurationValue.New((TimeSpan)instance);
			}
			if (instance is ParquetInterval)
			{
				ParquetInterval parquetInterval = (ParquetInterval)instance;
				return RecordValue.New(ParquetTypeMap.IntervalType, new Value[]
				{
					NumberValue.New((long)((ulong)parquetInterval.Months)),
					NumberValue.New((long)((ulong)parquetInterval.Days)),
					NumberValue.New((long)((ulong)parquetInterval.Milliseconds))
				});
			}
			return ValueMarshaller.MarshalFromClr(instance);
		}

		// Token: 0x06010EB9 RID: 69305 RVA: 0x003A5338 File Offset: 0x003A3538
		public static object MarshalToClr(Value value, TypeValue intendedType)
		{
			if (value == null)
			{
				return null;
			}
			ValueKind kind = value.Kind;
			if (kind <= ValueKind.Date)
			{
				if (kind == ValueKind.Time)
				{
					return new Time(value.AsTime.AsClrTimeSpan);
				}
				if (kind == ValueKind.Date)
				{
					return new Microsoft.OleDb.Date(value.AsDate.AsClrDateTime);
				}
			}
			else if (kind != ValueKind.Number)
			{
				if (kind != ValueKind.Text)
				{
					if (kind == ValueKind.Record)
					{
						if (intendedType != null && intendedType.Equals(ParquetTypeMap.IntervalType))
						{
							RecordValue asRecord = value.AsRecord;
							long asInteger = asRecord["Months"].AsNumber.AsInteger64;
							long asInteger2 = asRecord["Days"].AsNumber.AsInteger64;
							long asInteger3 = asRecord["Milliseconds"].AsNumber.AsInteger64;
							if (asInteger < 0L || asInteger > (long)((ulong)(-1)) || asInteger2 < 0L || asInteger2 > (long)((ulong)(-1)) || asInteger3 < 0L || asInteger3 > (long)((ulong)(-1)))
							{
								throw ValueException.NewDataFormatError<Message0>(Resources.IntervalComponentOutOfRangeError, asRecord, null);
							}
							return new ParquetInterval((uint)asInteger, (uint)asInteger2, (uint)asInteger3);
						}
					}
				}
				else if (intendedType != null && intendedType.Equals(TypeValue.Guid))
				{
					Guid guid;
					if (Guid.TryParse(value.AsString, out guid))
					{
						return guid;
					}
					throw ParquetTypeErrors.NotAGuid(value);
				}
			}
			else if (intendedType != null && intendedType.Equals(TypeValue.Number))
			{
				return value.AsNumber.ToOleDb(typeof(Number));
			}
			return ValueMarshaller.MarshalToClr(value, intendedType);
		}

		// Token: 0x06010EBA RID: 69306 RVA: 0x003A54AF File Offset: 0x003A36AF
		private static Func<IColumn, int, TLogical> Cast<T>(Func<IColumn, int, T> columnIndexer)
		{
			if (typeof(T) == typeof(TLogical))
			{
				return (Func<IColumn, int, TLogical>)columnIndexer;
			}
			throw new InvalidCastException();
		}

		// Token: 0x06010EBB RID: 69307 RVA: 0x003A54D8 File Offset: 0x003A36D8
		private static Action<TLogical> Cast<T>(Action<T> columnLoader)
		{
			if (typeof(T) == typeof(TLogical))
			{
				return (Action<TLogical>)columnLoader;
			}
			throw new InvalidCastException();
		}

		// Token: 0x06010EBC RID: 69308 RVA: 0x003A5504 File Offset: 0x003A3704
		private static TypeValue GetMType(Type clrType)
		{
			string fullName = clrType.FullName;
			if (fullName == "System.DBNull")
			{
				return TypeValue.None;
			}
			if (fullName == "System.TimeSpan")
			{
				return TypeValue.Duration;
			}
			if (fullName == "Microsoft.Mashup.Engine1.Library.Parquet.ParquetInterval")
			{
				return ParquetTypeMap.IntervalType;
			}
			if (fullName == "System.Guid")
			{
				return TypeValue.Guid;
			}
			if (fullName == "Microsoft.OleDb.Number")
			{
				return TypeValue.Number;
			}
			if (!(fullName == "Microsoft.OleDb.UnsupportedType"))
			{
				return ValueMarshaller.GetMType(clrType);
			}
			return TypeValue.None;
		}

		// Token: 0x06010EBD RID: 69309 RVA: 0x003A5592 File Offset: 0x003A3792
		private static ColumnType GetOleDbColumnType(Type clrType)
		{
			if (clrType == typeof(DBNull))
			{
				return ColumnType.Null;
			}
			return Microsoft.OleDb.Column.GetColumnType(clrType);
		}

		// Token: 0x06010EBE RID: 69310 RVA: 0x003A55B0 File Offset: 0x003A37B0
		public override ListStatistics GetStatistics(Statistics statistics)
		{
			Value value;
			Value value2;
			if (statistics.NumValues > 0L)
			{
				MinMax<TPhysical> minMax = MinMax<TPhysical>.GetMinMax(statistics);
				value = this.ToValue(minMax.Min);
				value2 = this.ToValue(minMax.Max);
			}
			else
			{
				value = Value.Null;
				value2 = Value.Null;
			}
			return new ListStatistics(base.TypeValue, value, value2, statistics.NumValues + statistics.NullCount, statistics.NullCount);
		}

		// Token: 0x040065DF RID: 26079
		private readonly Func<TPhysical, TLogical> toLogical;

		// Token: 0x040065E0 RID: 26080
		private readonly Func<IAllocator, TLogical, TPhysical> fromLogical;

		// Token: 0x040065E1 RID: 26081
		private readonly Func<IColumn, int, TLogical> columnIndexer;
	}
}

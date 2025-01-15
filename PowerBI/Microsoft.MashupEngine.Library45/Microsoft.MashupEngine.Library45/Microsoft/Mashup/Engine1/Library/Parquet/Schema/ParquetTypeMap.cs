using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F7D RID: 8061
	internal abstract class ParquetTypeMap
	{
		// Token: 0x06010E89 RID: 69257 RVA: 0x003A44C7 File Offset: 0x003A26C7
		protected ParquetTypeMap(PhysicalType physicalType, LogicalTypeEnum logicalTypeType, Func<LogicalType> logicalTypeCtor, TypeFacets facets = null)
		{
			this.physicalType = physicalType;
			this.logicalTypeType = logicalTypeType;
			this.logicalTypeCtor = logicalTypeCtor;
			this.facets = facets;
		}

		// Token: 0x17002CC6 RID: 11462
		// (get) Token: 0x06010E8A RID: 69258
		public abstract ICollection<ValueKind> TypeKinds { get; }

		// Token: 0x17002CC7 RID: 11463
		// (get) Token: 0x06010E8B RID: 69259 RVA: 0x003A44EC File Offset: 0x003A26EC
		public PhysicalType PhysicalType
		{
			get
			{
				return this.physicalType;
			}
		}

		// Token: 0x17002CC8 RID: 11464
		// (get) Token: 0x06010E8C RID: 69260 RVA: 0x003A44F4 File Offset: 0x003A26F4
		public LogicalTypeEnum LogicalTypeType
		{
			get
			{
				return this.logicalTypeType;
			}
		}

		// Token: 0x17002CC9 RID: 11465
		// (get) Token: 0x06010E8D RID: 69261 RVA: 0x003A44FC File Offset: 0x003A26FC
		public TypeFacets Facets
		{
			get
			{
				TypeFacets typeFacets = this.facets ?? TypeFacets.None;
				string typeName = ParquetTypeMap.GetTypeName(this.PhysicalType, this.LogicalTypeType);
				if (typeName != null)
				{
					typeFacets = typeFacets.AddNative(typeName, null, null);
				}
				return typeFacets;
			}
		}

		// Token: 0x06010E8E RID: 69262
		public abstract Func<object, Value> GetToValue(TypeValue expectedTypeValue);

		// Token: 0x06010E8F RID: 69263
		public abstract Func<IAllocator, Value, object> GetFromValue(TypeValue expectedTypeValue);

		// Token: 0x06010E90 RID: 69264 RVA: 0x003A4539 File Offset: 0x003A2739
		public LogicalType CreateLogicalType()
		{
			return this.logicalTypeCtor();
		}

		// Token: 0x06010E91 RID: 69265 RVA: 0x003A4546 File Offset: 0x003A2746
		public static ParquetTypeMap Map(Node node, TypeValue typeValue, SchemaConfig config)
		{
			return ParquetTypeMaps.Map(node, typeValue, config);
		}

		// Token: 0x06010E92 RID: 69266 RVA: 0x003A4550 File Offset: 0x003A2750
		public static ParquetPrimitiveTypeMap<TPhysical, TLogical> BaseOn<TPhysical, TLogical>(LogicalTypeEnum logicalTypeEnum, Func<LogicalType> logicalTypeCtor, ParquetTypeMap underlying)
		{
			return new ParquetPrimitiveTypeMap<TPhysical, TLogical>(logicalTypeEnum, logicalTypeCtor, new Func<TPhysical, TLogical>(((ParquetPrimitiveTypeMap<TPhysical, TLogical>)underlying).ToLogical), new Func<IAllocator, TLogical, TPhysical>(((ParquetPrimitiveTypeMap<TPhysical, TLogical>)underlying).FromLogical), underlying.Facets, ((ParquetPrimitiveTypeMap)underlying).TypeLength);
		}

		// Token: 0x06010E93 RID: 69267 RVA: 0x003A458C File Offset: 0x003A278C
		public static PhysicalType GetPhysicalType(Type type)
		{
			if (type == typeof(bool))
			{
				return PhysicalType.Boolean;
			}
			if (type == typeof(int))
			{
				return PhysicalType.Int32;
			}
			if (type == typeof(long))
			{
				return PhysicalType.Int64;
			}
			if (type == typeof(Int96))
			{
				return PhysicalType.Int96;
			}
			if (type == typeof(float))
			{
				return PhysicalType.Float;
			}
			if (type == typeof(double))
			{
				return PhysicalType.Double;
			}
			if (type == typeof(ByteArray))
			{
				return PhysicalType.ByteArray;
			}
			if (type == typeof(FixedLenByteArray))
			{
				return PhysicalType.FixedLenByteArray;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06010E94 RID: 69268 RVA: 0x003A4640 File Offset: 0x003A2840
		public static PhysicalType GetPhysicalTypeOrSentinel(Node node)
		{
			PrimitiveNode primitiveNode = node as PrimitiveNode;
			if (primitiveNode != null)
			{
				return primitiveNode.PhysicalType;
			}
			return (PhysicalType)(-1);
		}

		// Token: 0x06010E95 RID: 69269 RVA: 0x003A4660 File Offset: 0x003A2860
		public static string GetLogicalTypeName(LogicalTypeEnum logicalTypeEnum)
		{
			switch (logicalTypeEnum)
			{
			case LogicalTypeEnum.Undefined:
				return null;
			case LogicalTypeEnum.String:
				return "StringType";
			case LogicalTypeEnum.Map:
				return "MapType";
			case LogicalTypeEnum.List:
				return "ListType";
			case LogicalTypeEnum.Enum:
				return "EnumType";
			case LogicalTypeEnum.Decimal:
				return "DecimalType";
			case LogicalTypeEnum.Date:
				return "DateType";
			case LogicalTypeEnum.Time:
				return "TimeType";
			case LogicalTypeEnum.Timestamp:
				return "TimestampType";
			case LogicalTypeEnum.Interval:
				return "INTERVAL";
			case LogicalTypeEnum.Int:
				return "IntType";
			case LogicalTypeEnum.Nil:
				return "NullType";
			case LogicalTypeEnum.Json:
				return "JsonType";
			case LogicalTypeEnum.Bson:
				return "BsonType";
			case LogicalTypeEnum.Uuid:
				return "UUIDType";
			case LogicalTypeEnum.None:
				return null;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06010E96 RID: 69270 RVA: 0x003A4714 File Offset: 0x003A2914
		public static LogicalTypeEnum GetLogicalTypeFromName(string logicalTypeName)
		{
			if (logicalTypeName != null)
			{
				if (logicalTypeName != null)
				{
					switch (logicalTypeName.Length)
					{
					case 7:
					{
						char c = logicalTypeName[0];
						if (c != 'I')
						{
							if (c == 'M')
							{
								if (logicalTypeName == "MapType")
								{
									return LogicalTypeEnum.Map;
								}
							}
						}
						else if (logicalTypeName == "IntType")
						{
							return LogicalTypeEnum.Int;
						}
						break;
					}
					case 8:
					{
						char c = logicalTypeName[0];
						switch (c)
						{
						case 'B':
							if (logicalTypeName == "BsonType")
							{
								return LogicalTypeEnum.Bson;
							}
							break;
						case 'C':
						case 'F':
						case 'G':
						case 'H':
						case 'K':
						case 'M':
							break;
						case 'D':
							if (logicalTypeName == "DateType")
							{
								return LogicalTypeEnum.Date;
							}
							break;
						case 'E':
							if (logicalTypeName == "EnumType")
							{
								return LogicalTypeEnum.Enum;
							}
							break;
						case 'I':
							if (logicalTypeName == "INTERVAL")
							{
								return LogicalTypeEnum.Interval;
							}
							break;
						case 'J':
							if (logicalTypeName == "JsonType")
							{
								return LogicalTypeEnum.Json;
							}
							break;
						case 'L':
							if (logicalTypeName == "ListType")
							{
								return LogicalTypeEnum.List;
							}
							break;
						case 'N':
							if (logicalTypeName == "NullType")
							{
								return LogicalTypeEnum.Nil;
							}
							break;
						default:
							if (c != 'T')
							{
								if (c == 'U')
								{
									if (logicalTypeName == "UUIDType")
									{
										return LogicalTypeEnum.Uuid;
									}
								}
							}
							else if (logicalTypeName == "TimeType")
							{
								return LogicalTypeEnum.Time;
							}
							break;
						}
						break;
					}
					case 10:
						if (logicalTypeName == "StringType")
						{
							return LogicalTypeEnum.String;
						}
						break;
					case 11:
						if (logicalTypeName == "DecimalType")
						{
							return LogicalTypeEnum.Decimal;
						}
						break;
					case 13:
						if (logicalTypeName == "TimestampType")
						{
							return LogicalTypeEnum.Timestamp;
						}
						break;
					}
				}
				return LogicalTypeEnum.Undefined;
			}
			return LogicalTypeEnum.None;
		}

		// Token: 0x06010E97 RID: 69271 RVA: 0x003A48F4 File Offset: 0x003A2AF4
		public static string GetPhysicalTypeName(PhysicalType physicalType)
		{
			switch (physicalType)
			{
			case (PhysicalType)(-1):
				return "group";
			case PhysicalType.Boolean:
				return "BOOLEAN";
			case PhysicalType.Int32:
				return "INT32";
			case PhysicalType.Int64:
				return "INT64";
			case PhysicalType.Int96:
				return "INT96";
			case PhysicalType.Float:
				return "FLOAT";
			case PhysicalType.Double:
				return "DOUBLE";
			case PhysicalType.ByteArray:
				return "BYTE_ARRAY";
			case PhysicalType.FixedLenByteArray:
				return "FIXED_LEN_BYTE_ARRAY";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06010E98 RID: 69272 RVA: 0x003A496C File Offset: 0x003A2B6C
		public static PhysicalType? GetPhysicalTypeFromName(string physicalTypeName)
		{
			if (physicalTypeName != null)
			{
				int length = physicalTypeName.Length;
				switch (length)
				{
				case 5:
				{
					char c = physicalTypeName[3];
					if (c <= '6')
					{
						if (c != '3')
						{
							if (c == '6')
							{
								if (physicalTypeName == "INT64")
								{
									return new PhysicalType?(PhysicalType.Int64);
								}
							}
						}
						else if (physicalTypeName == "INT32")
						{
							return new PhysicalType?(PhysicalType.Int32);
						}
					}
					else if (c != '9')
					{
						if (c == 'A')
						{
							if (physicalTypeName == "FLOAT")
							{
								return new PhysicalType?(PhysicalType.Float);
							}
						}
					}
					else if (physicalTypeName == "INT96")
					{
						return new PhysicalType?(PhysicalType.Int96);
					}
					break;
				}
				case 6:
					if (physicalTypeName == "DOUBLE")
					{
						return new PhysicalType?(PhysicalType.Double);
					}
					break;
				case 7:
					if (physicalTypeName == "BOOLEAN")
					{
						return new PhysicalType?(PhysicalType.Boolean);
					}
					break;
				case 8:
				case 9:
					break;
				case 10:
					if (physicalTypeName == "BYTE_ARRAY")
					{
						return new PhysicalType?(PhysicalType.ByteArray);
					}
					break;
				default:
					if (length == 20)
					{
						if (physicalTypeName == "FIXED_LEN_BYTE_ARRAY")
						{
							return new PhysicalType?(PhysicalType.FixedLenByteArray);
						}
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x06010E99 RID: 69273 RVA: 0x003A4AA0 File Offset: 0x003A2CA0
		private static string GetTypeName(PhysicalType physicalType, LogicalTypeEnum logicalTypeEnum)
		{
			if (logicalTypeEnum == LogicalTypeEnum.Undefined)
			{
				return null;
			}
			if (logicalTypeEnum != LogicalTypeEnum.None)
			{
				return ParquetTypeMap.GetLogicalTypeName(logicalTypeEnum);
			}
			if (physicalType == (PhysicalType)(-1))
			{
				return null;
			}
			return ParquetTypeMap.GetPhysicalTypeName(physicalType);
		}

		// Token: 0x040065D3 RID: 26067
		public const PhysicalType GroupSentinel = (PhysicalType)(-1);

		// Token: 0x040065D4 RID: 26068
		public const string IntervalMonthsKey = "Months";

		// Token: 0x040065D5 RID: 26069
		public const string IntervalDaysKey = "Days";

		// Token: 0x040065D6 RID: 26070
		public const string IntervalMillisecondsKey = "Milliseconds";

		// Token: 0x040065D7 RID: 26071
		private static readonly Keys IntervalKeys = Keys.New("Months", "Days", "Milliseconds");

		// Token: 0x040065D8 RID: 26072
		public static readonly RecordTypeValue IntervalType = RecordTypeValue.New(RecordValue.New(ParquetTypeMap.IntervalKeys, new Value[]
		{
			RecordTypeValue.NewField(TypeValue.Int64, null),
			RecordTypeValue.NewField(TypeValue.Int64, null),
			RecordTypeValue.NewField(TypeValue.Int64, null)
		})).NewFacets(TypeFacets.None.AddNative(ParquetTypeMap.GetLogicalTypeName(LogicalTypeEnum.Interval), null, null)).AsRecordType;

		// Token: 0x040065D9 RID: 26073
		private readonly PhysicalType physicalType;

		// Token: 0x040065DA RID: 26074
		private readonly LogicalTypeEnum logicalTypeType;

		// Token: 0x040065DB RID: 26075
		private readonly Func<LogicalType> logicalTypeCtor;

		// Token: 0x040065DC RID: 26076
		private readonly TypeFacets facets;
	}
}

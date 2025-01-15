using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ParquetSharp.Schema;

namespace ParquetSharp
{
	// Token: 0x02000071 RID: 113
	[NullableContext(1)]
	[Nullable(0)]
	public class LogicalTypeFactory
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000A724 File Offset: 0x00008924
		public LogicalTypeFactory()
			: this(LogicalTypeFactory.DefaultPrimitiveMapping)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000A734 File Offset: 0x00008934
		public LogicalTypeFactory([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "logicalType", "repetition", "physicalType" })] [Nullable(new byte[] { 1, 1, 0, 2 })] IReadOnlyDictionary<Type, global::System.ValueTuple<LogicalType, Repetition, PhysicalType>> primitiveMapping)
		{
			this._primitiveMapping = primitiveMapping;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000A744 File Offset: 0x00008944
		public virtual bool TryGetParquetTypes(Type logicalSystemType, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "logicalType", "repetition", "physicalType" })] [Nullable(new byte[] { 0, 2 })] out global::System.ValueTuple<LogicalType, Repetition, PhysicalType> entry)
		{
			return this._primitiveMapping.TryGetValue(logicalSystemType, out entry);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000A754 File Offset: 0x00008954
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "physicalType", "logicalType" })]
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public virtual global::System.ValueTuple<Type, Type> GetSystemTypes(ColumnDescriptor descriptor, [Nullable(2)] Type columnLogicalTypeOverride)
		{
			global::System.ValueTuple<Type, Type> systemTypes = this.GetSystemTypes(descriptor);
			return new global::System.ValueTuple<Type, Type>(systemTypes.Item1, columnLogicalTypeOverride ?? systemTypes.Item2);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000A788 File Offset: 0x00008988
		public virtual bool IsSupported(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			while (!this._primitiveMapping.ContainsKey(type))
			{
				if (!type.IsArray)
				{
					return false;
				}
				type = type.GetElementType();
			}
			return true;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000A7DC File Offset: 0x000089DC
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "physicalType", "logicalType" })]
		[return: Nullable(new byte[] { 0, 1, 1 })]
		public unsafe virtual global::System.ValueTuple<Type, Type> GetSystemTypes(ColumnDescriptor descriptor)
		{
			PhysicalType physicalType = descriptor.PhysicalType;
			global::System.ValueTuple<Type, Type> valueTuple;
			using (LogicalType logicalType = descriptor.LogicalType)
			{
				using (Node schemaNode = descriptor.SchemaNode)
				{
					Repetition repetition = schemaNode.Repetition;
					bool flag = repetition == Repetition.Optional;
					KeyValuePair<Type, global::System.ValueTuple<LogicalType, Repetition, PhysicalType>> keyValuePair = this._primitiveMapping.FirstOrDefault(delegate([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "logicalType", "repetition", "physicalType" })] KeyValuePair<Type, global::System.ValueTuple<LogicalType, Repetition, PhysicalType>> e)
					{
						if (e.Value.Item3 == physicalType && e.Value.Item2 == repetition)
						{
							LogicalType item = e.Value.Item1;
							return item != null && item.Equals(logicalType);
						}
						return false;
					});
					if (keyValuePair.Key != null)
					{
						valueTuple = new global::System.ValueTuple<Type, Type>(LogicalTypeFactory.DefaultPhysicalTypeMapping[physicalType], keyValuePair.Key);
					}
					else
					{
						bool flag2 = logicalType is NoneLogicalType || logicalType is NullLogicalType;
						if (flag2)
						{
							if (!flag && logicalType is NullLogicalType)
							{
								throw new ArgumentOutOfRangeException("logicalType", "The null logical type may only be used with optional columns");
							}
							switch (physicalType)
							{
							case PhysicalType.Boolean:
								return new global::System.ValueTuple<Type, Type>(typeof(bool), flag ? typeof(bool?) : typeof(bool));
							case PhysicalType.Int32:
								return new global::System.ValueTuple<Type, Type>(typeof(int), flag ? typeof(int?) : typeof(int));
							case PhysicalType.Int64:
								return new global::System.ValueTuple<Type, Type>(typeof(long), flag ? typeof(long?) : typeof(long));
							case PhysicalType.Int96:
								return new global::System.ValueTuple<Type, Type>(typeof(Int96), flag ? typeof(Int96?) : typeof(Int96));
							case PhysicalType.Float:
								return new global::System.ValueTuple<Type, Type>(typeof(float), flag ? typeof(float?) : typeof(float));
							case PhysicalType.Double:
								return new global::System.ValueTuple<Type, Type>(typeof(double), flag ? typeof(double?) : typeof(double));
							}
						}
						if (logicalType is DecimalLogicalType)
						{
							PhysicalType physicalType2 = physicalType;
							if (physicalType2 != PhysicalType.Int32)
							{
								if (physicalType2 != PhysicalType.Int64)
								{
									if (physicalType2 == PhysicalType.FixedLenByteArray)
									{
										if (descriptor.TypeLength != sizeof(Decimal128))
										{
											throw new NotSupportedException(string.Format("only {0} bytes of decimal length is supported with fixed-length byte array data", sizeof(Decimal128)));
										}
										if (descriptor.TypePrecision > 29)
										{
											throw new NotSupportedException("only max 29 digits of decimal precision is supported with fixed-length byte array data");
										}
										return new global::System.ValueTuple<Type, Type>(typeof(FixedLenByteArray), flag ? typeof(decimal?) : typeof(decimal));
									}
								}
								else
								{
									if (descriptor.TypePrecision > 18)
									{
										throw new NotSupportedException("A maximum of 18 digits of decimal precision is supported with int64 data");
									}
									return new global::System.ValueTuple<Type, Type>(typeof(long), flag ? typeof(decimal?) : typeof(decimal));
								}
							}
							else
							{
								if (descriptor.TypePrecision > 9)
								{
									throw new NotSupportedException("A maximum of 9 digits of decimal precision is supported with int32 data");
								}
								return new global::System.ValueTuple<Type, Type>(typeof(int), flag ? typeof(decimal?) : typeof(decimal));
							}
						}
						TimeLogicalType timeLogicalType = logicalType as TimeLogicalType;
						if (timeLogicalType != null)
						{
							switch (timeLogicalType.TimeUnit)
							{
							case TimeUnit.Millis:
								return new global::System.ValueTuple<Type, Type>(typeof(int), flag ? typeof(TimeSpan?) : typeof(TimeSpan));
							case TimeUnit.Micros:
								return new global::System.ValueTuple<Type, Type>(typeof(long), flag ? typeof(TimeSpan?) : typeof(TimeSpan));
							case TimeUnit.Nanos:
								return new global::System.ValueTuple<Type, Type>(typeof(long), flag ? typeof(TimeSpanNanos?) : typeof(TimeSpanNanos));
							}
						}
						TimestampLogicalType timestampLogicalType = logicalType as TimestampLogicalType;
						if (timestampLogicalType != null)
						{
							TimeUnit timeUnit = timestampLogicalType.TimeUnit;
							if (timeUnit - TimeUnit.Millis <= 1)
							{
								return new global::System.ValueTuple<Type, Type>(typeof(long), flag ? typeof(DateTime?) : typeof(DateTime));
							}
							if (timeUnit == TimeUnit.Nanos)
							{
								return new global::System.ValueTuple<Type, Type>(typeof(long), flag ? typeof(DateTimeNanos?) : typeof(DateTimeNanos));
							}
						}
						if (logicalType.Type == LogicalTypeEnum.String)
						{
							valueTuple = new global::System.ValueTuple<Type, Type>(typeof(ByteArray), typeof(string));
						}
						else if (logicalType.Type == LogicalTypeEnum.Json)
						{
							valueTuple = new global::System.ValueTuple<Type, Type>(typeof(ByteArray), typeof(string));
						}
						else
						{
							if (logicalType.Type != LogicalTypeEnum.Bson)
							{
								throw new ArgumentOutOfRangeException("logicalType", string.Format("unsupported logical type {0} with physical type {1}", logicalType, physicalType));
							}
							valueTuple = new global::System.ValueTuple<Type, Type>(typeof(ByteArray), typeof(byte[]));
						}
					}
				}
			}
			return valueTuple;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000AE04 File Offset: 0x00009004
		[NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "logicalType", "physicalType" })]
		[return: Nullable(new byte[] { 0, 1 })]
		public virtual global::System.ValueTuple<LogicalType, PhysicalType> GetTypesOverride(LogicalType logicalTypeOverride, LogicalType logicalType, PhysicalType physicalType)
		{
			bool flag = logicalTypeOverride == null || logicalTypeOverride is NoneLogicalType;
			if (!flag)
			{
				TimeLogicalType timeLogicalType = logicalTypeOverride as TimeLogicalType;
				if (timeLogicalType != null && timeLogicalType.TimeUnit == TimeUnit.Millis)
				{
					physicalType = PhysicalType.Int32;
				}
				return new global::System.ValueTuple<LogicalType, PhysicalType>(logicalTypeOverride, physicalType);
			}
			if (logicalType == null)
			{
				throw new ArgumentNullException("logicalType", "both logicalType and logicalTypeOverride are null");
			}
			return new global::System.ValueTuple<LogicalType, PhysicalType>(logicalType, physicalType);
		}

		// Token: 0x040000D3 RID: 211
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "logicalType", "repetition", "physicalType" })]
		[Nullable(new byte[] { 1, 1, 0, 2 })]
		public static readonly IReadOnlyDictionary<Type, global::System.ValueTuple<LogicalType, Repetition, PhysicalType>> DefaultPrimitiveMapping = new Dictionary<Type, global::System.ValueTuple<LogicalType, Repetition, PhysicalType>>
		{
			{
				typeof(bool),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Required, PhysicalType.Boolean)
			},
			{
				typeof(bool?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Optional, PhysicalType.Boolean)
			},
			{
				typeof(sbyte),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(8, true), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(sbyte?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(8, true), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(byte),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(8, false), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(byte?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(8, false), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(short),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(16, true), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(short?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(16, true), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(ushort),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(16, false), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(ushort?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(16, false), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(int),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(32, true), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(int?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(32, true), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(uint),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(32, false), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(uint?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(32, false), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(long),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(64, true), Repetition.Required, PhysicalType.Int64)
			},
			{
				typeof(long?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(64, true), Repetition.Optional, PhysicalType.Int64)
			},
			{
				typeof(ulong),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(64, false), Repetition.Required, PhysicalType.Int64)
			},
			{
				typeof(ulong?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Int(64, false), Repetition.Optional, PhysicalType.Int64)
			},
			{
				typeof(Int96),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Required, PhysicalType.Int96)
			},
			{
				typeof(Int96?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Optional, PhysicalType.Int96)
			},
			{
				typeof(float),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Required, PhysicalType.Float)
			},
			{
				typeof(float?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Optional, PhysicalType.Float)
			},
			{
				typeof(double),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Required, PhysicalType.Double)
			},
			{
				typeof(double?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Optional, PhysicalType.Double)
			},
			{
				typeof(decimal),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(null, Repetition.Required, PhysicalType.FixedLenByteArray)
			},
			{
				typeof(decimal?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(null, Repetition.Optional, PhysicalType.FixedLenByteArray)
			},
			{
				typeof(Guid),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Uuid(), Repetition.Required, PhysicalType.FixedLenByteArray)
			},
			{
				typeof(Guid?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Uuid(), Repetition.Optional, PhysicalType.FixedLenByteArray)
			},
			{
				typeof(Date),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Date(), Repetition.Required, PhysicalType.Int32)
			},
			{
				typeof(Date?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Date(), Repetition.Optional, PhysicalType.Int32)
			},
			{
				typeof(DateTime),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Timestamp(true, TimeUnit.Micros), Repetition.Required, PhysicalType.Int64)
			},
			{
				typeof(DateTime?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Timestamp(true, TimeUnit.Micros), Repetition.Optional, PhysicalType.Int64)
			},
			{
				typeof(DateTimeNanos),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Timestamp(true, TimeUnit.Nanos), Repetition.Required, PhysicalType.Int64)
			},
			{
				typeof(DateTimeNanos?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Timestamp(true, TimeUnit.Nanos), Repetition.Optional, PhysicalType.Int64)
			},
			{
				typeof(TimeSpan),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Time(true, TimeUnit.Micros), Repetition.Required, PhysicalType.Int64)
			},
			{
				typeof(TimeSpan?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Time(true, TimeUnit.Micros), Repetition.Optional, PhysicalType.Int64)
			},
			{
				typeof(TimeSpanNanos),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Time(true, TimeUnit.Nanos), Repetition.Required, PhysicalType.Int64)
			},
			{
				typeof(TimeSpanNanos?),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.Time(true, TimeUnit.Nanos), Repetition.Optional, PhysicalType.Int64)
			},
			{
				typeof(string),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.String(), Repetition.Optional, PhysicalType.ByteArray)
			},
			{
				typeof(byte[]),
				new global::System.ValueTuple<LogicalType, Repetition, PhysicalType>(LogicalType.None(), Repetition.Optional, PhysicalType.ByteArray)
			}
		};

		// Token: 0x040000D4 RID: 212
		public static readonly IReadOnlyDictionary<PhysicalType, Type> DefaultPhysicalTypeMapping = new Dictionary<PhysicalType, Type>
		{
			{
				PhysicalType.Boolean,
				typeof(bool)
			},
			{
				PhysicalType.Int32,
				typeof(int)
			},
			{
				PhysicalType.Int64,
				typeof(long)
			},
			{
				PhysicalType.Int96,
				typeof(Int96)
			},
			{
				PhysicalType.Float,
				typeof(float)
			},
			{
				PhysicalType.Double,
				typeof(double)
			},
			{
				PhysicalType.ByteArray,
				typeof(ByteArray)
			},
			{
				PhysicalType.FixedLenByteArray,
				typeof(FixedLenByteArray)
			}
		};

		// Token: 0x040000D5 RID: 213
		public static readonly LogicalTypeFactory Default = new LogicalTypeFactory();

		// Token: 0x040000D6 RID: 214
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "logicalType", "repetition", "physicalType" })]
		[Nullable(new byte[] { 1, 1, 0, 2 })]
		private readonly IReadOnlyDictionary<Type, global::System.ValueTuple<LogicalType, Repetition, PhysicalType>> _primitiveMapping;
	}
}

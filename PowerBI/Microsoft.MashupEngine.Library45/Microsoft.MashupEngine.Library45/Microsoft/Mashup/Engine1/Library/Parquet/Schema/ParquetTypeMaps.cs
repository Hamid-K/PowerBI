using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Parquet.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.Mashup.Parquet;
using Microsoft.OleDb;
using ParquetSharp;
using ParquetSharp.Schema;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001F9C RID: 8092
	internal static class ParquetTypeMaps
	{
		// Token: 0x06010F29 RID: 69417 RVA: 0x003A622C File Offset: 0x003A442C
		public static ParquetTypeMap Map(Node node, TypeValue typeValue, SchemaConfig config)
		{
			ParquetTypeMap parquetTypeMap;
			try
			{
				if (!ParquetTypeMaps.All.TryMap(node, typeValue, config, out parquetTypeMap))
				{
					throw ParquetTypeErrors.UnmappedTypeError(typeValue, Array.Empty<NamedValue>());
				}
			}
			catch (ValueException)
			{
				if (typeValue == null)
				{
					ParquetTypeMaps.UnrecognizedLogicalTypeErrors.TryMap(node, typeValue, config, out parquetTypeMap);
					return parquetTypeMap;
				}
				throw;
			}
			if (node != null)
			{
				PhysicalType physicalTypeOrSentinel = ParquetTypeMap.GetPhysicalTypeOrSentinel(node);
				if (parquetTypeMap.PhysicalType != physicalTypeOrSentinel)
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "PhysicalType", TextValue.New(ParquetTypeMap.GetPhysicalTypeName(parquetTypeMap.PhysicalType)), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(physicalTypeOrSentinel)));
				}
			}
			if (node != null && typeValue != null && !typeValue.Facets.IsEmpty)
			{
				RecordValue recordValue = typeValue.Facets.ToRecord();
				RecordValue recordValue2 = parquetTypeMap.Facets.ToRecord();
				List<NamedValue> list = new List<NamedValue>();
				foreach (string text in recordValue.Keys)
				{
					Value value = recordValue[text];
					Value value2;
					if (!value.IsNull && (!recordValue2.TryGetValue(text, out value2) || !value.Equals(value2)))
					{
						list.Add(new NamedValue(text, value));
					}
					if (list.Count > 0)
					{
						throw ParquetTypeErrors.UnmappedTypeError(typeValue, list.ToArray());
					}
				}
			}
			return parquetTypeMap;
		}

		// Token: 0x06010F2A RID: 69418 RVA: 0x003A6390 File Offset: 0x003A4590
		private static ParquetPrimitiveTypeMap<T> Null<T>()
		{
			return new ParquetPrimitiveTypeMap<T, DBNull>(LogicalTypeEnum.Nil, new Func<LogicalType>(LogicalType.Null), delegate(T value)
			{
				throw ValueException.NewDataFormatError<Message0>(Resources.NonNullInNullColumn, Value.Null, null);
			}, delegate(IAllocator allocator, DBNull value)
			{
				throw ValueException.NewDataFormatError<Message0>(Resources.NonNullInNullColumn, Value.Null, null);
			}, null, null);
		}

		// Token: 0x06010F2B RID: 69419 RVA: 0x003A63F8 File Offset: 0x003A45F8
		private static SingleTypeParquetTypeMapper Error<T>(Func<Node, TypeValue, SchemaConfig, ValueException> getException)
		{
			return new SingleTypeParquetTypeMapper(new ValueKind[] { ValueKind.None }, ParquetTypeMap.GetPhysicalType(typeof(T)), LogicalTypeEnum.Undefined, ParquetTypeMapper.MapTo((Node node, TypeValue typeValue, SchemaConfig config) => ParquetTypeMaps.Error<T>(getException(node, typeValue, config))));
		}

		// Token: 0x06010F2C RID: 69420 RVA: 0x003A6444 File Offset: 0x003A4644
		private static SingleTypeParquetTypeMapper Error(Func<Node, TypeValue, SchemaConfig, ValueException> getException)
		{
			return new SingleTypeParquetTypeMapper(new ValueKind[] { ValueKind.None }, (PhysicalType)(-1), LogicalTypeEnum.Undefined, ParquetTypeMapper.MapTo((Node node, TypeValue typeValue, SchemaConfig config) => ParquetTypeMaps.Error(getException(node, typeValue, config))));
		}

		// Token: 0x06010F2D RID: 69421 RVA: 0x003A6484 File Offset: 0x003A4684
		private static ParquetTypeMap Error<T>(ValueException exception)
		{
			return new ParquetPrimitiveTypeMap<T, UnsupportedType>(LogicalTypeEnum.Undefined, delegate
			{
				throw exception;
			}, delegate(T value)
			{
				throw exception;
			}, delegate(IAllocator allocator, UnsupportedType value)
			{
				throw exception;
			}, null, null);
		}

		// Token: 0x06010F2E RID: 69422 RVA: 0x003A64D4 File Offset: 0x003A46D4
		private static ParquetTypeMap Error(ValueException exception)
		{
			Func<RecordValue, Value> <>9__6;
			Func<IAllocator, Value, RecordValue> <>9__7;
			return new ParquetGroupTypeMap(new ValueKind[] { ValueKind.None }, LogicalTypeEnum.Undefined, delegate
			{
				throw exception;
			}, delegate(TypeValue expectedTypeValue)
			{
				Func<RecordValue, Value> func;
				if ((func = <>9__6) == null)
				{
					func = (<>9__6 = delegate(RecordValue value)
					{
						throw exception;
					});
				}
				return func;
			}, delegate(TypeValue expectedTypeValue)
			{
				Func<IAllocator, Value, RecordValue> func2;
				if ((func2 = <>9__7) == null)
				{
					func2 = (<>9__7 = delegate(IAllocator allocator, Value value)
					{
						throw exception;
					});
				}
				return func2;
			}, (RecordTypeValue recordType) => TypeValue.None, (TypeValue typeValue) => TypeValue.Record, (NestedColumnSelection columnSelection) => new NestedColumnSelection(new ColumnSelection(Keys.Empty), null));
		}

		// Token: 0x06010F2F RID: 69423 RVA: 0x003A6580 File Offset: 0x003A4780
		private static ParquetTypeMapper Errors(Func<Node, TypeValue, SchemaConfig, ValueException> getException)
		{
			return ParquetTypeMapper.Cased<PhysicalType>(delegate(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (node != null)
				{
					return ParquetTypeMap.GetPhysicalTypeOrSentinel(node);
				}
				return PhysicalType.Boolean;
			}, new SingleTypeParquetTypeMapper[]
			{
				ParquetTypeMaps.Error<bool>(getException),
				ParquetTypeMaps.Error<int>(getException),
				ParquetTypeMaps.Error<long>(getException),
				ParquetTypeMaps.Error<Int96>(getException),
				ParquetTypeMaps.Error<float>(getException),
				ParquetTypeMaps.Error<double>(getException),
				ParquetTypeMaps.Error<ByteArray>(getException),
				ParquetTypeMaps.Error<FixedLenByteArray>(getException),
				ParquetTypeMaps.Error(getException)
			}.ToDictionary((SingleTypeParquetTypeMapper typeMap) => typeMap.PhysicalType, (SingleTypeParquetTypeMapper typeMap) => typeMap));
		}

		// Token: 0x06010F30 RID: 69424 RVA: 0x003A664C File Offset: 0x003A484C
		private static ParquetTypeMapper LogicalTypesOfKind(ValueKind valueKind)
		{
			return ParquetTypeMapper.CheckType(ParquetTypeMapper.If(ParquetTypeMapper.If(ParquetTypeMapper.Matching(new ParquetTypeMapper[]
			{
				ParquetTypeMapper.If(ParquetTypeMaps.LogicalTypes, null, null, new bool?(true), null),
				ParquetTypeMapper.If(ParquetTypeMapper.Try(ParquetTypeMaps.LogicalTypes), null, null, new bool?(false), null)
			}), delegate(Node node, TypeValue typeValue)
			{
				if (node == null)
				{
					object obj;
					if (typeValue == null)
					{
						obj = null;
					}
					else
					{
						TypeFacets facets = typeValue.Facets;
						obj = ((facets != null) ? facets.NativeTypeName : null);
					}
					return obj != null;
				}
				return true;
			}), (ParquetTypeMap typeMap) => typeMap.TypeKinds.Contains(valueKind)), new ValueKind[] { valueKind });
		}

		// Token: 0x06010F31 RID: 69425 RVA: 0x003A66FC File Offset: 0x003A48FC
		private static ParquetTypeMapper LogicalTypesOfType(TypeValue typeValue)
		{
			return ParquetTypeMapper.If<ParquetPrimitiveTypeMap>(ParquetTypeMaps.LogicalTypesOfKind(typeValue.TypeKind), (ParquetPrimitiveTypeMap typeMap) => typeMap.TypeValue.Equals(typeValue));
		}

		// Token: 0x06010F32 RID: 69426 RVA: 0x003A6738 File Offset: 0x003A4938
		private static int GetTypeLength(PrimitiveNode node, TypeValue typeValue)
		{
			int? num = null;
			if (node != null)
			{
				num = new int?(node.TypeLength);
			}
			if (typeValue != null)
			{
				bool? isVariableLength = typeValue.Facets.IsVariableLength;
				bool flag = true;
				if ((isVariableLength.GetValueOrDefault() == flag) & (isVariableLength != null))
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "IsVariableLength", LogicalValue.False, LogicalValue.True);
				}
			}
			if (typeValue != null && typeValue.Facets.MaxLength != null)
			{
				if (num != null)
				{
					int? num2 = num;
					long? num3 = ((num2 != null) ? new long?((long)num2.GetValueOrDefault()) : null);
					long? maxLength = typeValue.Facets.MaxLength;
					if (!((num3.GetValueOrDefault() == maxLength.GetValueOrDefault()) & (num3 != null == (maxLength != null))))
					{
						throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "MaxLength", NumberValue.New(typeValue.Facets.MaxLength.Value), NumberValue.New(num.Value));
					}
				}
				if (typeValue.Facets.MaxLength.Value > 2147483647L)
				{
					throw ParquetTypeErrors.LengthOutOfRange(typeValue.Facets.MaxLength.Value);
				}
				num = new int?((int)typeValue.Facets.MaxLength.Value);
			}
			if (num == null)
			{
				throw ParquetTypeErrors.InsufficientTypeError(typeValue, new string[] { "MaxLength" });
			}
			return num.Value;
		}

		// Token: 0x06010F33 RID: 69427 RVA: 0x003A68C0 File Offset: 0x003A4AC0
		private static Func<Node, TypeValue, SchemaConfig, TResult> Chooser<TChoice, TResult>(Func<Node, SchemaConfig, TChoice> getNodeChoice, Func<TypeValue, SchemaConfig, TChoice> getTypeChoice, Func<Node, TypeValue, SchemaConfig, TChoice, TResult> getResult, Func<Node, TChoice, TypeValue, TChoice, TChoice> choose)
		{
			return delegate(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (node != null && typeValue != null)
				{
					TChoice tchoice = getNodeChoice(node, schemaConfig);
					TChoice tchoice2 = getTypeChoice(typeValue, schemaConfig);
					TChoice tchoice3;
					if (tchoice.Equals(tchoice2))
					{
						tchoice3 = tchoice;
					}
					else
					{
						tchoice3 = choose(node, tchoice, typeValue, tchoice2);
					}
					return getResult(node, typeValue, schemaConfig, tchoice3);
				}
				if (node != null)
				{
					return getResult(node, typeValue, schemaConfig, getNodeChoice(node, schemaConfig));
				}
				if (typeValue != null)
				{
					return getResult(node, typeValue, schemaConfig, getTypeChoice(typeValue, schemaConfig));
				}
				throw new InvalidOperationException();
			};
		}

		// Token: 0x06010F34 RID: 69428 RVA: 0x003A68F0 File Offset: 0x003A4AF0
		// Note: this type is marked as 'beforefieldinit'.
		static ParquetTypeMaps()
		{
			ParquetTypeMapper[] array = new ParquetTypeMapper[4];
			array[0] = ParquetTypeMapper.If<PrimitiveNode>(ParquetTypeMaps.FixedLenByteArrayUuid, null, (TypeValue type) => type.NewFacets(TypeFacets.None).Equals(TypeValue.Guid), null, null, null, null);
			int num = 1;
			ParquetTypeMapper parquetTypeMapper = ParquetTypeMapper.Try(ParquetTypeMaps.FixedLenByteArrayUuid);
			Func<Node, bool> func = null;
			Func<TypeValue, bool> func2 = null;
			bool? flag = new bool?(true);
			bool? flag2 = null;
			array[num] = ParquetTypeMapper.If(parquetTypeMapper, func, func2, flag, flag2);
			array[2] = ParquetTypeMaps.LogicalTypesOfKind(ValueKind.Text);
			array[3] = ParquetTypeMaps.ByteArrayString;
			ParquetTypeMaps.Text = ParquetTypeMapper.Matching(array);
			ParquetTypeMaps.Date = ParquetTypeMaps.Int32Date;
			ParquetTypeMaps.DateTime = ParquetTypeMapper.Matching(new ParquetTypeMapper[]
			{
				ParquetTypeMaps.LogicalTypesOfKind(ValueKind.DateTime),
				ParquetTypeMaps.Timestamp
			});
			ParquetTypeMaps.DateTimeZone = ParquetTypeMapper.Cased<TypeMapping>((Node node, TypeValue typeValue, SchemaConfig schemaConfig) => schemaConfig.DefaultTypeMapping, new Dictionary<TypeMapping, ParquetTypeMapper>
			{
				{
					TypeMapping.Default,
					ParquetTypeMaps.Timestamp
				},
				{
					TypeMapping.Sql,
					ParquetTypeMapper.Fail((Node n, TypeValue t) => ValueException.NewExpressionError<Message0>(Resources.WriteDatetimezoneError, null, null))
				}
			});
			ParquetTypeMaps.Duration = ParquetTypeMapper.Fail((Node node, TypeValue typeValue) => ValueException.NewExpressionError<Message0>(Resources.WriteDurationError, null, null));
			ParquetTypeMapper[] array2 = new ParquetTypeMapper[5];
			int num2 = 0;
			ParquetTypeMapper fixedLenByteArrayNone = ParquetTypeMaps.FixedLenByteArrayNone;
			Func<PrimitiveNode, bool> func3 = null;
			Func<TypeValue, bool> func4 = delegate(TypeValue type)
			{
				bool? isVariableLength = type.Facets.IsVariableLength;
				bool flag3 = false;
				return ((isVariableLength.GetValueOrDefault() == flag3) & (isVariableLength != null)) && type.Facets.MaxLength != null;
			};
			flag2 = new bool?(true);
			array2[num2] = ParquetTypeMapper.If<PrimitiveNode>(fixedLenByteArrayNone, func3, func4, null, flag2, null, null);
			int num3 = 1;
			ParquetTypeMapper parquetTypeMapper2 = ParquetTypeMaps.ByteArrayNone;
			Func<PrimitiveNode, bool> func5 = null;
			Func<TypeValue, bool> func6 = delegate(TypeValue type)
			{
				bool? isVariableLength2 = type.Facets.IsVariableLength;
				bool flag4 = true;
				return (isVariableLength2.GetValueOrDefault() == flag4) & (isVariableLength2 != null);
			};
			flag2 = new bool?(true);
			array2[num3] = ParquetTypeMapper.If<PrimitiveNode>(parquetTypeMapper2, func5, func6, null, flag2, null, null);
			array2[2] = ParquetTypeMaps.LogicalTypesOfKind(ValueKind.Binary);
			int num4 = 3;
			ParquetTypeMapper parquetTypeMapper3 = ParquetTypeMaps.ByteArrayNone;
			Func<PrimitiveNode, bool> func7 = null;
			Func<TypeValue, bool> func8 = null;
			flag2 = new bool?(true);
			array2[num4] = ParquetTypeMapper.If<PrimitiveNode>(parquetTypeMapper3, func7, func8, new bool?(false), flag2, null, null);
			array2[4] = ParquetTypeMapper.Fail((Node node, TypeValue type) => ParquetTypeErrors.IncompatibleTypeError(type, "PhysicalType", ListValue.New(new string[]
			{
				ParquetTypeMap.GetPhysicalTypeName(PhysicalType.ByteArray),
				ParquetTypeMap.GetPhysicalTypeName(PhysicalType.FixedLenByteArray)
			}), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(ParquetTypeMap.GetPhysicalTypeOrSentinel(node)))));
			ParquetTypeMaps.Binary = ParquetTypeMapper.Matching(array2);
			ParquetTypeMaps.Table = ParquetTypeMapper.Matching(new ParquetTypeMapper[]
			{
				ParquetTypeMaps.LogicalTypesOfKind(ValueKind.Table),
				ParquetTypeMapper.If(ParquetTypeMaps._Map, null, delegate(TypeValue type)
				{
					TableTypeValue asTableType = type.AsTableType;
					Keys fieldKeys = asTableType.ItemType.FieldKeys;
					if (!fieldKeys.Equals(ParquetTypeMaps.Maps.KvpKeys) && !fieldKeys.Equals(ParquetTypeMaps.Maps.KeyOnlyKeys))
					{
						return false;
					}
					return asTableType.TableKeys.Any((TableKey tableKey) => tableKey.Columns.Length == 1 && tableKey.Columns[0] == 0);
				}, null, null),
				ParquetTypeMaps.List
			});
			ParquetTypeMaps.Record = ParquetTypeMapper.Matching(new ParquetTypeMapper[]
			{
				ParquetTypeMaps.LogicalTypesOfKind(ValueKind.Record),
				ParquetTypeMaps.GroupNone
			});
			ParquetTypeMaps.Any = ParquetTypeMapper.Matching(new ParquetTypeMapper[]
			{
				ParquetTypeMapper.If(ParquetTypeMaps.LogicalTypes, null, null, new bool?(true), null),
				ParquetTypeMapper.If(ParquetTypeMapper.Fail((Node node, TypeValue typeValue) => ParquetTypeErrors.InsufficientTypeError(typeValue, Array.Empty<string>())), null, null, new bool?(false), null)
			});
			ParquetTypeMaps.None = ParquetTypeMaps.Nulls;
			ParquetTypeMaps.All = ParquetTypeMapper.If(ParquetTypeMapper.Cased<ValueKind>(delegate(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (typeValue == null)
				{
					return ValueKind.Any;
				}
				return typeValue.TypeKind;
			}, new Dictionary<ValueKind, ParquetTypeMapper>
			{
				{
					ValueKind.Time,
					ParquetTypeMaps.Time
				},
				{
					ValueKind.Date,
					ParquetTypeMaps.Date
				},
				{
					ValueKind.DateTime,
					ParquetTypeMaps.DateTime
				},
				{
					ValueKind.DateTimeZone,
					ParquetTypeMaps.DateTimeZone
				},
				{
					ValueKind.Duration,
					ParquetTypeMaps.Duration
				},
				{
					ValueKind.Number,
					ParquetTypeMaps.Number
				},
				{
					ValueKind.Logical,
					ParquetTypeMaps.Logical
				},
				{
					ValueKind.Text,
					ParquetTypeMaps.Text
				},
				{
					ValueKind.Binary,
					ParquetTypeMaps.Binary
				},
				{
					ValueKind.Record,
					ParquetTypeMaps.Record
				},
				{
					ValueKind.List,
					ParquetTypeMaps.List
				},
				{
					ValueKind.Table,
					ParquetTypeMaps.Table
				},
				{
					ValueKind.Any,
					ParquetTypeMaps.Any
				},
				{
					ValueKind.None,
					ParquetTypeMaps.None
				}
			}.ToDictionary((KeyValuePair<ValueKind, ParquetTypeMapper> kvp) => kvp.Key, (KeyValuePair<ValueKind, ParquetTypeMapper> kvp) => ParquetTypeMapper.CheckType(kvp.Value, new ValueKind[] { kvp.Key }))), null, null, null, null);
		}

		// Token: 0x0400661B RID: 26139
		public static readonly ParquetTypeMap BooleanNone = new ParquetPrimitiveTypeMap<bool, bool>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<bool, bool>(LogicalTypeConversions.None<bool>), new Func<IAllocator, bool, bool>(LogicalTypeConversions.None<bool>), null, null);

		// Token: 0x0400661C RID: 26140
		public static readonly ParquetTypeMap Int32None = new ParquetPrimitiveTypeMap<int, int>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<int, int>(LogicalTypeConversions.None<int>), new Func<IAllocator, int, int>(LogicalTypeConversions.None<int>), null, null);

		// Token: 0x0400661D RID: 26141
		public static readonly ParquetTypeMap Int64None = new ParquetPrimitiveTypeMap<long, long>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<long, long>(LogicalTypeConversions.None<long>), new Func<IAllocator, long, long>(LogicalTypeConversions.None<long>), null, null);

		// Token: 0x0400661E RID: 26142
		public static readonly ParquetTypeMap Int96None = new ParquetPrimitiveTypeMap<Int96, DateTime>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<Int96, DateTime>(LogicalTypeConversions.Int96ToTimestampNanos), new Func<IAllocator, DateTime, Int96>(LogicalTypeConversions.Int96FromTimestampNanos), null, null);

		// Token: 0x0400661F RID: 26143
		public static readonly ParquetTypeMap FloatNone = new ParquetPrimitiveTypeMap<float, float>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<float, float>(LogicalTypeConversions.None<float>), new Func<IAllocator, float, float>(LogicalTypeConversions.None<float>), null, null);

		// Token: 0x04006620 RID: 26144
		public static readonly ParquetTypeMap DoubleNone = new ParquetPrimitiveTypeMap<double, double>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<double, double>(LogicalTypeConversions.None<double>), new Func<IAllocator, double, double>(LogicalTypeConversions.None<double>), null, null);

		// Token: 0x04006621 RID: 26145
		public static readonly ParquetTypeMap ByteArrayNone = new ParquetPrimitiveTypeMap<ByteArray, byte[]>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), new Func<ByteArray, byte[]>(LogicalTypeConversions.ByteArrayToBytes), new Func<IAllocator, byte[], ByteArray>(LogicalTypeConversions.ByteArrayFromBytes), TypeFacets.NewBinary(null, new bool?(true), null), null);

		// Token: 0x04006622 RID: 26146
		private static readonly SingleTypeParquetTypeMapper FixedLenByteArrayNone = new SingleTypeParquetTypeMapper(new ValueKind[] { ValueKind.Binary }, PhysicalType.FixedLenByteArray, LogicalTypeEnum.None, ParquetTypeMapper.MapTo<PrimitiveNode>(delegate(PrimitiveNode node, TypeValue typeValue)
		{
			int typeLength = ParquetTypeMaps.GetTypeLength(node, typeValue);
			return new ParquetPrimitiveTypeMap<FixedLenByteArray, byte[]>(LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), LogicalTypeConversions.FixedLenByteArrayToBytes(typeLength), LogicalTypeConversions.FixedLenByteArrayFromBytes(typeLength), TypeFacets.NewBinary(new long?((long)typeLength), new bool?(false), null), new int?(typeLength));
		}));

		// Token: 0x04006623 RID: 26147
		private static readonly ParquetTypeMap ByteArrayString = new ParquetPrimitiveTypeMap<ByteArray, string>(LogicalTypeEnum.String, new Func<LogicalType>(LogicalType.String), new Func<ByteArray, string>(LogicalTypeConversions.ByteArrayToString), new Func<IAllocator, string, ByteArray>(LogicalTypeConversions.ByteArrayFromString), TypeFacets.NewText(null, new bool?(true), null), null);

		// Token: 0x04006624 RID: 26148
		private static readonly ParquetTypeMap ByteArrayEnum = ParquetTypeMap.BaseOn<ByteArray, string>(LogicalTypeEnum.Enum, new Func<LogicalType>(LogicalType.Enum), ParquetTypeMaps.ByteArrayString);

		// Token: 0x04006625 RID: 26149
		private static readonly ParquetTypeMap FixedLenByteArrayUuid = new ParquetPrimitiveTypeMap<FixedLenByteArray, Guid>(LogicalTypeEnum.Uuid, new Func<LogicalType>(LogicalType.Uuid), new Func<FixedLenByteArray, Guid>(LogicalTypeConversions.FixedLenByteArrayToUuid), new Func<IAllocator, Guid, FixedLenByteArray>(LogicalTypeConversions.FixedLenByteArrayFromUuid), null, new int?(16));

		// Token: 0x04006626 RID: 26150
		private static readonly ParquetTypeMapper Int = ParquetTypeMaps.Ints.Mapper;

		// Token: 0x04006627 RID: 26151
		private static readonly ParquetTypeMapper Decimal = ParquetTypeMaps.Decimals.Mapper;

		// Token: 0x04006628 RID: 26152
		private static readonly ParquetTypeMap Int32Date = new ParquetPrimitiveTypeMap<int, Microsoft.OleDb.Date>(LogicalTypeEnum.Date, new Func<LogicalType>(LogicalType.Date), new Func<int, Microsoft.OleDb.Date>(LogicalTypeConversions.Int32ToDate), new Func<IAllocator, Microsoft.OleDb.Date, int>(LogicalTypeConversions.Int32FromDate), null, null);

		// Token: 0x04006629 RID: 26153
		private static readonly ParquetTypeMapper Time = ParquetTypeMaps.Times.Mapper;

		// Token: 0x0400662A RID: 26154
		private static readonly ParquetTypeMapper Timestamp = ParquetTypeMaps.Timestamps.Mapper;

		// Token: 0x0400662B RID: 26155
		private static readonly ParquetTypeMapper Interval = ParquetTypeMaps.Intervals.Mapper;

		// Token: 0x0400662C RID: 26156
		private static readonly ParquetTypeMapper Json = ParquetTypeMaps.JsonDocuments.Mapper;

		// Token: 0x0400662D RID: 26157
		private static readonly ParquetTypeMapper Bson = ParquetTypeMaps.BsonDocuments.Mapper;

		// Token: 0x0400662E RID: 26158
		private static readonly ParquetTypeMap GroupNone = new ParquetGroupTypeMap(new ValueKind[] { ValueKind.Record }, LogicalTypeEnum.None, new Func<LogicalType>(LogicalType.None), (TypeValue expectedTypeValue) => (RecordValue record) => record, (TypeValue expectedTypeValue) => (IAllocator allocator, Value value) => value.AsRecord, (RecordTypeValue typeValue) => typeValue, delegate(TypeValue typeValue)
		{
			if (typeValue.TypeKind != ValueKind.Any)
			{
				return typeValue.AsRecordType;
			}
			return TypeValue.Record;
		}, (NestedColumnSelection columnSelection) => columnSelection);

		// Token: 0x0400662F RID: 26159
		private static readonly ParquetTypeMapper List = ParquetTypeMapper.If(ParquetTypeMaps.Lists.Mapper, (Node node) => node.LogicalTypeType() == LogicalTypeEnum.List, null, null, null);

		// Token: 0x04006630 RID: 26160
		private static readonly ParquetTypeMapper _Map = ParquetTypeMapper.If(ParquetTypeMaps.Maps.Mapper, (Node node) => node.LogicalTypeType() == LogicalTypeEnum.Map, null, null, null);

		// Token: 0x04006631 RID: 26161
		private static readonly ParquetTypeMapper Nulls = ParquetTypeMapper.Cased<PhysicalType>(delegate(Node node, TypeValue type, SchemaConfig schemaConfig)
		{
			if (node != null)
			{
				return ParquetTypeMap.GetPhysicalTypeOrSentinel(node);
			}
			return PhysicalType.Boolean;
		}, new SingleTypeParquetTypeMapper[]
		{
			ParquetTypeMaps.Null<bool>(),
			ParquetTypeMaps.Null<int>(),
			ParquetTypeMaps.Null<long>(),
			ParquetTypeMaps.Null<Int96>(),
			ParquetTypeMaps.Null<float>(),
			ParquetTypeMaps.Null<double>(),
			ParquetTypeMaps.Null<ByteArray>(),
			ParquetTypeMaps.Null<FixedLenByteArray>()
		}.ToDictionary((SingleTypeParquetTypeMapper typeMap) => typeMap.PhysicalType, (SingleTypeParquetTypeMapper typeMap) => typeMap));

		// Token: 0x04006632 RID: 26162
		private static readonly ParquetTypeMapper UnrecognizedLogicalTypeErrors = ParquetTypeMaps.Errors(new Func<Node, TypeValue, SchemaConfig, ValueException>(ParquetTypeErrors.UnrecognizedLogicalType));

		// Token: 0x04006633 RID: 26163
		private static readonly ParquetTypeMapper PhysicalTypes = ParquetTypeMapper.Cased<PhysicalType>(ParquetTypeMaps.Chooser<PhysicalType?, PhysicalType>((Node node, SchemaConfig schemaConfig) => new PhysicalType?(ParquetTypeMap.GetPhysicalTypeOrSentinel(node)), (TypeValue typeValue, SchemaConfig schemaConfig) => ParquetTypeMap.GetPhysicalTypeFromName(typeValue.Facets.NativeTypeName), delegate(Node node, TypeValue typeValue, SchemaConfig schemaConfig, PhysicalType? choice)
		{
			if (choice == null)
			{
				throw ParquetTypeErrors.InsufficientTypeError(typeValue, Array.Empty<string>());
			}
			return choice.Value;
		}, delegate(Node node, PhysicalType? nodeChoice, TypeValue type, PhysicalType? typeChoice)
		{
			if (nodeChoice == null)
			{
				return typeChoice;
			}
			if (typeChoice != null)
			{
				PhysicalType? physicalType = nodeChoice;
				PhysicalType? physicalType2 = typeChoice;
				if (!((physicalType.GetValueOrDefault() == physicalType2.GetValueOrDefault()) & (physicalType != null == (physicalType2 != null))))
				{
					throw ParquetTypeErrors.IncompatibleTypeError(type, "NativeTypeName", TextValue.New(ParquetTypeMap.GetPhysicalTypeName(typeChoice.Value)), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(nodeChoice.Value)));
				}
			}
			return nodeChoice;
		}), new SingleTypeParquetTypeMapper[]
		{
			ParquetTypeMaps.BooleanNone,
			ParquetTypeMaps.Int32None,
			ParquetTypeMaps.Int64None,
			ParquetTypeMaps.Int96None,
			ParquetTypeMaps.FloatNone,
			ParquetTypeMaps.DoubleNone,
			ParquetTypeMaps.ByteArrayNone,
			ParquetTypeMaps.FixedLenByteArrayNone,
			ParquetTypeMaps.GroupNone
		}.ToDictionary((SingleTypeParquetTypeMapper typeMap) => typeMap.PhysicalType, (SingleTypeParquetTypeMapper typeMap) => ParquetTypeMapper.CheckType(typeMap, typeMap.TypeKinds.ToArray<ValueKind>())));

		// Token: 0x04006634 RID: 26164
		private static readonly ParquetTypeMapper LogicalTypes = ParquetTypeMapper.Cased<LogicalTypeEnum>(ParquetTypeMaps.Chooser<LogicalTypeEnum?, LogicalTypeEnum>((Node node, SchemaConfig schemaConfig) => new LogicalTypeEnum?(node.LogicalTypeType()), delegate(TypeValue typeValue, SchemaConfig schemaConfig)
		{
			string nativeTypeName = typeValue.Facets.NativeTypeName;
			if (nativeTypeName == null)
			{
				return null;
			}
			LogicalTypeEnum logicalTypeFromName = ParquetTypeMap.GetLogicalTypeFromName(nativeTypeName);
			if (logicalTypeFromName != LogicalTypeEnum.Undefined)
			{
				return new LogicalTypeEnum?(logicalTypeFromName);
			}
			if (ParquetTypeMap.GetPhysicalTypeFromName(nativeTypeName) != null)
			{
				return new LogicalTypeEnum?(LogicalTypeEnum.None);
			}
			throw ParquetTypeErrors.UnmappedTypeError(typeValue, "NativeTypeName", TextValue.New(nativeTypeName));
		}, (Node node, TypeValue typeValue, SchemaConfig schemaConfig, LogicalTypeEnum? choice) => choice.GetValueOrDefault(LogicalTypeEnum.None), delegate(Node node, LogicalTypeEnum? nodeChoice, TypeValue typeValue, LogicalTypeEnum? typeChoice)
		{
			if (typeChoice == null)
			{
				return nodeChoice;
			}
			LogicalTypeEnum? logicalTypeEnum = nodeChoice;
			LogicalTypeEnum logicalTypeEnum2 = LogicalTypeEnum.None;
			if (!((logicalTypeEnum.GetValueOrDefault() == logicalTypeEnum2) & (logicalTypeEnum != null)))
			{
				logicalTypeEnum = typeChoice;
				logicalTypeEnum2 = LogicalTypeEnum.None;
				if (!((logicalTypeEnum.GetValueOrDefault() == logicalTypeEnum2) & (logicalTypeEnum != null)))
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "NativeTypeName", TextValue.New(ParquetTypeMap.GetLogicalTypeName(typeChoice.Value)), TextValue.New(ParquetTypeMap.GetLogicalTypeName(nodeChoice.Value)));
				}
			}
			return typeChoice;
		}), new Dictionary<LogicalTypeEnum, ParquetTypeMapper>
		{
			{
				LogicalTypeEnum.None,
				ParquetTypeMaps.PhysicalTypes
			},
			{
				LogicalTypeEnum.Nil,
				ParquetTypeMaps.Nulls
			},
			{
				LogicalTypeEnum.String,
				ParquetTypeMaps.ByteArrayString
			},
			{
				LogicalTypeEnum.Uuid,
				ParquetTypeMaps.FixedLenByteArrayUuid
			},
			{
				LogicalTypeEnum.Enum,
				ParquetTypeMaps.ByteArrayEnum
			},
			{
				LogicalTypeEnum.Date,
				ParquetTypeMaps.Int32Date
			},
			{
				LogicalTypeEnum.Time,
				ParquetTypeMaps.Time
			},
			{
				LogicalTypeEnum.Timestamp,
				ParquetTypeMaps.Timestamp
			},
			{
				LogicalTypeEnum.Int,
				ParquetTypeMaps.Int
			},
			{
				LogicalTypeEnum.Decimal,
				ParquetTypeMaps.Decimal
			},
			{
				LogicalTypeEnum.Interval,
				ParquetTypeMaps.Interval
			},
			{
				LogicalTypeEnum.Json,
				ParquetTypeMaps.Json
			},
			{
				LogicalTypeEnum.Bson,
				ParquetTypeMaps.Bson
			},
			{
				LogicalTypeEnum.List,
				ParquetTypeMaps.List
			},
			{
				LogicalTypeEnum.Map,
				ParquetTypeMaps._Map
			}
		});

		// Token: 0x04006635 RID: 26165
		private static readonly ParquetTypeMapper Number = ParquetTypeMapper.Cased<TypeValue>((Node node, TypeValue typeValue, SchemaConfig schemaConfig) => ((typeValue != null) ? typeValue.NewFacets(TypeFacets.None) : null) ?? TypeValue.Number, new Dictionary<TypeValue, ParquetTypeMapper>
		{
			{
				TypeValue.Byte,
				ParquetTypeMaps.Int
			},
			{
				TypeValue.Int8,
				ParquetTypeMaps.Int
			},
			{
				TypeValue.Int16,
				ParquetTypeMaps.Int
			},
			{
				TypeValue.Int32,
				ParquetTypeMapper.Matching(new ParquetTypeMapper[]
				{
					ParquetTypeMaps.LogicalTypesOfType(TypeValue.Int32),
					ParquetTypeMapper.Try(ParquetTypeMaps.Int),
					ParquetTypeMaps.Int32None
				})
			},
			{
				TypeValue.Int64,
				ParquetTypeMapper.Matching(new ParquetTypeMapper[]
				{
					ParquetTypeMaps.LogicalTypesOfType(TypeValue.Int64),
					ParquetTypeMapper.Try(ParquetTypeMaps.Int),
					ParquetTypeMaps.Int64None
				})
			},
			{
				TypeValue.Single,
				ParquetTypeMaps.FloatNone
			},
			{
				TypeValue.Double,
				ParquetTypeMaps.DoubleNone
			},
			{
				TypeValue.Decimal,
				ParquetTypeMapper.Matching(new ParquetTypeMapper[]
				{
					ParquetTypeMapper.If(ParquetTypeMaps.Int, (Node node) => node.LogicalTypeType() == LogicalTypeEnum.Int, null, new bool?(true), null),
					ParquetTypeMaps.Decimal
				})
			},
			{
				TypeValue.Currency,
				ParquetTypeMaps.Decimal
			},
			{
				TypeValue.Percentage,
				ParquetTypeMaps.Decimal
			},
			{
				TypeValue.Number,
				ParquetTypeMapper.Matching(new ParquetTypeMapper[]
				{
					ParquetTypeMapper.If(ParquetTypeMaps.Decimal, null, delegate(TypeValue typeValue)
					{
						int? numericPrecisionBase = typeValue.Facets.NumericPrecisionBase;
						int num5 = 10;
						return ((numericPrecisionBase.GetValueOrDefault() == num5) & (numericPrecisionBase != null)) && (typeValue.Facets.NumericPrecision != null || typeValue.Facets.NumericScale != null);
					}, null, null),
					ParquetTypeMaps.LogicalTypesOfKind(ValueKind.Number),
					ParquetTypeMapper.If(ParquetTypeMaps.DoubleNone, null, null, new bool?(false), null)
				})
			}
		});

		// Token: 0x04006636 RID: 26166
		private static readonly ParquetTypeMapper Logical = ParquetTypeMaps.BooleanNone;

		// Token: 0x04006637 RID: 26167
		private static readonly ParquetTypeMapper Text;

		// Token: 0x04006638 RID: 26168
		private static readonly ParquetTypeMapper Date;

		// Token: 0x04006639 RID: 26169
		private static readonly ParquetTypeMapper DateTime;

		// Token: 0x0400663A RID: 26170
		private static readonly ParquetTypeMapper DateTimeZone;

		// Token: 0x0400663B RID: 26171
		private static readonly ParquetTypeMapper Duration;

		// Token: 0x0400663C RID: 26172
		private static readonly ParquetTypeMapper Binary;

		// Token: 0x0400663D RID: 26173
		private static readonly ParquetTypeMapper Table;

		// Token: 0x0400663E RID: 26174
		private static readonly ParquetTypeMapper Record;

		// Token: 0x0400663F RID: 26175
		private static readonly ParquetTypeMapper Any;

		// Token: 0x04006640 RID: 26176
		private static readonly ParquetTypeMapper None;

		// Token: 0x04006641 RID: 26177
		private static readonly ParquetTypeMapper All;

		// Token: 0x02001F9D RID: 8093
		private static class Ints
		{
			// Token: 0x06010F35 RID: 69429 RVA: 0x003A753E File Offset: 0x003A573E
			private static ValueTuple<int, bool> GetKey(int bitWidth, bool isSigned)
			{
				return new ValueTuple<int, bool>(bitWidth, isSigned);
			}

			// Token: 0x06010F36 RID: 69430 RVA: 0x003A7548 File Offset: 0x003A5748
			private static ValueTuple<int, bool> GetKey(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (typeValue == null)
				{
					return ParquetTypeMaps.Ints.GetKey(node);
				}
				TypeValue typeValue2 = typeValue.NewFacets(TypeFacets.None);
				if (node == null || node.LogicalTypeType() != LogicalTypeEnum.Int)
				{
					ValueTuple<int, bool> valueTuple;
					if (ParquetTypeMaps.Ints.IntTypes.TryGetValue(typeValue2, out valueTuple))
					{
						return valueTuple;
					}
					if (typeValue2.TypeKind != ValueKind.Any && !typeValue2.Equals(TypeValue.Number))
					{
						throw ParquetTypeErrors.UnmappedTypeError(typeValue, Array.Empty<NamedValue>());
					}
					if (node == null)
					{
						return ParquetTypeMaps.Ints.GetKey(64, true);
					}
					PhysicalType physicalTypeOrSentinel = ParquetTypeMap.GetPhysicalTypeOrSentinel(node);
					if (physicalTypeOrSentinel == PhysicalType.Int32)
					{
						return ParquetTypeMaps.Ints.GetKey(32, true);
					}
					if (physicalTypeOrSentinel != PhysicalType.Int64)
					{
						throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "NativeTypeName", TextValue.New(ParquetTypeMap.GetLogicalTypeName(LogicalTypeEnum.Int)), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(physicalTypeOrSentinel)));
					}
					return ParquetTypeMaps.Ints.GetKey(64, true);
				}
				else
				{
					ValueTuple<int, bool> key = ParquetTypeMaps.Ints.GetKey(node);
					TypeValue typeValue3 = ParquetTypeMaps.Ints.ByKey[key].TypeValue;
					if (typeValue2.Kind != ValueKind.Any && !typeValue2.Equals(TypeValue.Number) && !typeValue2.Equals(typeValue3))
					{
						throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "Type", typeValue, typeValue3);
					}
					return key;
				}
			}

			// Token: 0x06010F37 RID: 69431 RVA: 0x003A764C File Offset: 0x003A584C
			private static ValueTuple<int, bool> GetKey(Node node)
			{
				ValueTuple<int, bool> key;
				using (LogicalType logicalType = node.LogicalType)
				{
					key = ParquetTypeMaps.Ints.GetKey(logicalType);
				}
				return key;
			}

			// Token: 0x06010F38 RID: 69432 RVA: 0x003A7684 File Offset: 0x003A5884
			private static ValueTuple<int, bool> GetKey(ParquetTypeMap typeMap)
			{
				ValueTuple<int, bool> key;
				using (LogicalType logicalType = typeMap.CreateLogicalType())
				{
					key = ParquetTypeMaps.Ints.GetKey(logicalType);
				}
				return key;
			}

			// Token: 0x06010F39 RID: 69433 RVA: 0x003A76BC File Offset: 0x003A58BC
			private static ValueTuple<int, bool> GetKey(LogicalType logicalType)
			{
				IntLogicalType intLogicalType = (IntLogicalType)logicalType;
				return new ValueTuple<int, bool>(intLogicalType.BitWidth, intLogicalType.IsSigned);
			}

			// Token: 0x04006642 RID: 26178
			private static readonly ParquetTypeMap Int32Int8 = new ParquetPrimitiveTypeMap<int, sbyte>(LogicalTypeEnum.Int, () => LogicalType.Int(8, true), (int value) => (sbyte)value, (IAllocator allocator, sbyte value) => (int)((byte)value), null, null);

			// Token: 0x04006643 RID: 26179
			private static readonly ParquetTypeMap Int32UInt8 = new ParquetPrimitiveTypeMap<int, byte>(LogicalTypeEnum.Int, () => LogicalType.Int(8, false), (int value) => (byte)value, (IAllocator allocator, byte value) => (int)value, null, null);

			// Token: 0x04006644 RID: 26180
			private static readonly ParquetTypeMap Int32Int16 = new ParquetPrimitiveTypeMap<int, short>(LogicalTypeEnum.Int, () => LogicalType.Int(16, true), (int value) => (short)value, (IAllocator allocator, short value) => (int)((ushort)value), null, null);

			// Token: 0x04006645 RID: 26181
			private static readonly ParquetTypeMap Int32UInt16 = new ParquetPrimitiveTypeMap<int, ushort>(LogicalTypeEnum.Int, () => LogicalType.Int(16, false), (int value) => (ushort)value, (IAllocator allocator, ushort value) => (int)value, null, null);

			// Token: 0x04006646 RID: 26182
			private static readonly ParquetTypeMap Int32Int32 = new ParquetPrimitiveTypeMap<int, int>(LogicalTypeEnum.Int, () => LogicalType.Int(32, true), new Func<int, int>(LogicalTypeConversions.None<int>), new Func<IAllocator, int, int>(LogicalTypeConversions.None<int>), null, null);

			// Token: 0x04006647 RID: 26183
			private static readonly ParquetTypeMap Int32UInt32 = new ParquetPrimitiveTypeMap<int, uint>(LogicalTypeEnum.Int, () => LogicalType.Int(32, false), (int value) => (uint)value, (IAllocator allocator, uint value) => (int)value, null, null);

			// Token: 0x04006648 RID: 26184
			private static readonly ParquetTypeMap Int64Int64 = new ParquetPrimitiveTypeMap<long, long>(LogicalTypeEnum.Int, () => LogicalType.Int(64, true), new Func<long, long>(LogicalTypeConversions.None<long>), new Func<IAllocator, long, long>(LogicalTypeConversions.None<long>), null, null);

			// Token: 0x04006649 RID: 26185
			private static readonly ParquetTypeMap Int64UInt64 = new ParquetPrimitiveTypeMap<long, ulong>(LogicalTypeEnum.Int, () => LogicalType.Int(64, false), (long value) => (ulong)value, (IAllocator allocator, ulong value) => (long)value, null, null);

			// Token: 0x0400664A RID: 26186
			private static readonly Dictionary<ValueTuple<int, bool>, ParquetPrimitiveTypeMap> ByKey = new ParquetTypeMap[]
			{
				ParquetTypeMaps.Ints.Int32Int8,
				ParquetTypeMaps.Ints.Int32UInt8,
				ParquetTypeMaps.Ints.Int32Int16,
				ParquetTypeMaps.Ints.Int32UInt16,
				ParquetTypeMaps.Ints.Int32Int32,
				ParquetTypeMaps.Ints.Int32UInt32,
				ParquetTypeMaps.Ints.Int64Int64,
				ParquetTypeMaps.Ints.Int64UInt64
			}.ToDictionary(new Func<ParquetTypeMap, ValueTuple<int, bool>>(ParquetTypeMaps.Ints.GetKey), (ParquetTypeMap typeMap) => (ParquetPrimitiveTypeMap)typeMap);

			// Token: 0x0400664B RID: 26187
			public static readonly ParquetTypeMapper Mapper = ParquetTypeMapper.CheckType(ParquetTypeMapper.Cased<ValueTuple<int, bool>>(new Func<Node, TypeValue, SchemaConfig, ValueTuple<int, bool>>(ParquetTypeMaps.Ints.GetKey), ParquetTypeMaps.Ints.ByKey.ToDictionary((KeyValuePair<ValueTuple<int, bool>, ParquetPrimitiveTypeMap> kvp) => kvp.Key, (KeyValuePair<ValueTuple<int, bool>, ParquetPrimitiveTypeMap> kvp) => kvp.Value)), new ValueKind[] { ValueKind.Number });

			// Token: 0x0400664C RID: 26188
			private static readonly Dictionary<TypeValue, ValueTuple<int, bool>> IntTypes = new Dictionary<TypeValue, ValueTuple<int, bool>>
			{
				{
					TypeValue.Byte,
					new ValueTuple<int, bool>(8, false)
				},
				{
					TypeValue.Int8,
					new ValueTuple<int, bool>(8, true)
				},
				{
					TypeValue.Int16,
					new ValueTuple<int, bool>(16, true)
				},
				{
					TypeValue.Int32,
					new ValueTuple<int, bool>(32, true)
				},
				{
					TypeValue.Int64,
					new ValueTuple<int, bool>(64, true)
				}
			};
		}

		// Token: 0x02001F9F RID: 8095
		private static class Times
		{
			// Token: 0x06010F54 RID: 69460 RVA: 0x003A7AC4 File Offset: 0x003A5CC4
			public static TimeUnit? GetKey(TypeValue typeValue, SchemaConfig schemaConfig)
			{
				int? num;
				if (typeValue == null)
				{
					num = null;
				}
				else
				{
					TypeFacets facets = typeValue.Facets;
					num = ((facets != null) ? facets.DateTimePrecision : null);
				}
				return ParquetTypeMaps.Times.GetTimeUnit(num);
			}

			// Token: 0x06010F55 RID: 69461 RVA: 0x003A7B00 File Offset: 0x003A5D00
			public static TimeUnit? GetTimeUnit(int? precision)
			{
				if (precision == null)
				{
					return null;
				}
				switch (precision.GetValueOrDefault())
				{
				case 0:
				case 1:
				case 2:
				case 3:
					return new TimeUnit?(TimeUnit.Millis);
				case 4:
				case 5:
				case 6:
					return new TimeUnit?(TimeUnit.Micros);
				case 7:
				case 8:
				case 9:
					return new TimeUnit?(TimeUnit.Nanos);
				default:
					return new TimeUnit?(TimeUnit.Nanos);
				}
			}

			// Token: 0x06010F56 RID: 69462 RVA: 0x003A7B73 File Offset: 0x003A5D73
			public static int GetPrecision(TimeUnit timeUnit)
			{
				switch (timeUnit)
				{
				case TimeUnit.Millis:
					return 3;
				case TimeUnit.Micros:
					return 6;
				case TimeUnit.Nanos:
					return 9;
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06010F57 RID: 69463 RVA: 0x003A7B98 File Offset: 0x003A5D98
			private static TimeUnit? GetKey(Node node, SchemaConfig schemaConfig)
			{
				TimeUnit? timeUnit;
				using (LogicalType logicalType = node.LogicalType)
				{
					TimeLogicalType timeLogicalType = logicalType as TimeLogicalType;
					if (timeLogicalType != null)
					{
						timeUnit = new TimeUnit?(timeLogicalType.TimeUnit);
					}
					else
					{
						timeUnit = null;
					}
				}
				return timeUnit;
			}

			// Token: 0x06010F58 RID: 69464 RVA: 0x003A7BEC File Offset: 0x003A5DEC
			private static Func<LogicalType> CopyLogicalType(LogicalType logicalType)
			{
				TimeLogicalType timeLogicalType = (TimeLogicalType)logicalType;
				bool isAdjustedForUtc = timeLogicalType.IsAdjustedToUtc;
				TimeUnit timeUnit = timeLogicalType.TimeUnit;
				return () => LogicalType.Time(isAdjustedForUtc, timeUnit);
			}

			// Token: 0x06010F59 RID: 69465 RVA: 0x003A7C28 File Offset: 0x003A5E28
			private static Func<LogicalType> GetLogicalType(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (node != null && node.LogicalTypeType() == LogicalTypeEnum.Time)
				{
					return node.WithLogicalType(new Func<LogicalType, Func<LogicalType>>(ParquetTypeMaps.Times.CopyLogicalType));
				}
				TimeUnit timeUnit = ParquetTypeMaps.Times.GetKey(typeValue, schemaConfig) ?? schemaConfig.DefaultTimeUnit;
				return () => LogicalType.Time(schemaConfig.AllTimesUtcAdjusted, timeUnit);
			}

			// Token: 0x0400664E RID: 26190
			private static readonly ParquetTypeMapper Int32TimeMillis = ParquetTypeMapper.MapTo<PrimitiveNode>((PrimitiveNode node, TypeValue typeValue, SchemaConfig schemaConfig) => new ParquetPrimitiveTypeMap<int, Time>(LogicalTypeEnum.Time, ParquetTypeMaps.Times.GetLogicalType(node, typeValue, schemaConfig), new Func<int, Time>(LogicalTypeConversions.Int32ToTimeMillis), new Func<IAllocator, Time, int>(LogicalTypeConversions.Int32FromTimeMillis), TypeFacets.NewDateTime(new int?(3), null), null));

			// Token: 0x0400664F RID: 26191
			private static readonly ParquetTypeMapper Int64TimeMicros = ParquetTypeMapper.MapTo<PrimitiveNode>((PrimitiveNode node, TypeValue typeValue, SchemaConfig schemaConfig) => new ParquetPrimitiveTypeMap<long, Time>(LogicalTypeEnum.Time, ParquetTypeMaps.Times.GetLogicalType(node, typeValue, schemaConfig), new Func<long, Time>(LogicalTypeConversions.Int64ToTimeMicros), new Func<IAllocator, Time, long>(LogicalTypeConversions.Int64FromTimeMicros), TypeFacets.NewDateTime(new int?(6), null), null));

			// Token: 0x04006650 RID: 26192
			private static readonly ParquetTypeMapper Int64TimeNanos = ParquetTypeMapper.MapTo<PrimitiveNode>((PrimitiveNode node, TypeValue typeValue, SchemaConfig schemaConfig) => new ParquetPrimitiveTypeMap<long, Time>(LogicalTypeEnum.Time, ParquetTypeMaps.Times.GetLogicalType(node, typeValue, schemaConfig), new Func<long, Time>(LogicalTypeConversions.Int64ToTimeNanos), new Func<IAllocator, Time, long>(LogicalTypeConversions.Int64FromTimeNanos), TypeFacets.NewDateTime(new int?(9), null), null));

			// Token: 0x04006651 RID: 26193
			public static readonly ParquetTypeMapper Mapper = ParquetTypeMapper.CheckType(ParquetTypeMapper.Cased<TimeUnit>(ParquetTypeMaps.Chooser<TimeUnit?, TimeUnit>(new Func<Node, SchemaConfig, TimeUnit?>(ParquetTypeMaps.Times.GetKey), new Func<TypeValue, SchemaConfig, TimeUnit?>(ParquetTypeMaps.Times.GetKey), delegate(Node node, TypeValue type, SchemaConfig schemaConfig, TimeUnit? choice)
			{
				if (node == null)
				{
					TimeUnit? timeUnit = choice;
					if (timeUnit == null)
					{
						return schemaConfig.DefaultTimeUnit;
					}
					return timeUnit.GetValueOrDefault();
				}
				else
				{
					if (choice == null)
					{
						throw ParquetTypeErrors.InsufficientTypeError(type, new string[] { "DateTimePrecision" });
					}
					return choice.Value;
				}
			}, delegate(Node node, TimeUnit? nodeChoice, TypeValue type, TimeUnit? typeChoice)
			{
				if (nodeChoice != null)
				{
					TimeUnit? timeUnit2 = nodeChoice;
					TimeUnit timeUnit3 = TimeUnit.Unknown;
					if (!((timeUnit2.GetValueOrDefault() == timeUnit3) & (timeUnit2 != null)))
					{
						if (typeChoice == null)
						{
							return nodeChoice;
						}
						throw ParquetTypeErrors.IncompatibleTypeError(type, "DateTimePrecision", NumberValue.New(ParquetTypeMaps.Times.GetPrecision(typeChoice.Value)), NumberValue.New(ParquetTypeMaps.Times.GetPrecision(nodeChoice.Value)));
					}
				}
				return typeChoice;
			}), new Dictionary<TimeUnit, ParquetTypeMapper>
			{
				{
					TimeUnit.Millis,
					ParquetTypeMaps.Times.Int32TimeMillis
				},
				{
					TimeUnit.Micros,
					ParquetTypeMaps.Times.Int64TimeMicros
				},
				{
					TimeUnit.Nanos,
					ParquetTypeMaps.Times.Int64TimeNanos
				}
			}), new ValueKind[] { ValueKind.Time });
		}

		// Token: 0x02001FA3 RID: 8099
		private static class Timestamps
		{
			// Token: 0x06010F66 RID: 69478 RVA: 0x003A7F48 File Offset: 0x003A6148
			private static ValueTuple<bool, TimeUnit> GetKey(bool isAdjustedToUtc, TimeUnit timeUnit)
			{
				return new ValueTuple<bool, TimeUnit>(isAdjustedToUtc, timeUnit);
			}

			// Token: 0x06010F67 RID: 69479 RVA: 0x003A7F54 File Offset: 0x003A6154
			private static ValueTuple<bool?, TimeUnit?> GetKey(Node node, SchemaConfig schemaConfig)
			{
				ValueTuple<bool?, TimeUnit?> key;
				using (LogicalType logicalType = node.LogicalType)
				{
					key = ParquetTypeMaps.Timestamps.GetKey(logicalType, schemaConfig);
				}
				return key;
			}

			// Token: 0x06010F68 RID: 69480 RVA: 0x003A7F90 File Offset: 0x003A6190
			private static ValueTuple<bool?, TimeUnit?> GetKey(TypeValue typeValue, SchemaConfig schemaConfig)
			{
				TimeUnit? key = ParquetTypeMaps.Times.GetKey(typeValue, schemaConfig);
				ValueKind typeKind = typeValue.TypeKind;
				bool? flag;
				if (typeKind != ValueKind.Any)
				{
					if (typeKind != ValueKind.DateTime)
					{
						if (typeKind != ValueKind.DateTimeZone)
						{
							throw new InvalidOperationException();
						}
						flag = new bool?(true);
					}
					else
					{
						flag = new bool?(false);
					}
				}
				else
				{
					flag = null;
				}
				return new ValueTuple<bool?, TimeUnit?>(flag, key);
			}

			// Token: 0x06010F69 RID: 69481 RVA: 0x003A7FE8 File Offset: 0x003A61E8
			private static ValueTuple<bool?, TimeUnit?> GetKey(LogicalType logicalType, SchemaConfig schemaConfig)
			{
				TimestampLogicalType timestampLogicalType = logicalType as TimestampLogicalType;
				if (timestampLogicalType != null)
				{
					return new ValueTuple<bool?, TimeUnit?>(new bool?(schemaConfig.AllTimesUtcAdjusted || timestampLogicalType.IsAdjustedToUtc), new TimeUnit?(timestampLogicalType.TimeUnit));
				}
				return new ValueTuple<bool?, TimeUnit?>(null, null);
			}

			// Token: 0x06010F6A RID: 69482 RVA: 0x003A8040 File Offset: 0x003A6240
			private static Func<LogicalType> CopyLogicalType(LogicalType logicalType)
			{
				TimestampLogicalType timestampLogicalType = (TimestampLogicalType)logicalType;
				bool isAdjustedForUtc = timestampLogicalType.IsAdjustedToUtc;
				TimeUnit timeUnit = timestampLogicalType.TimeUnit;
				return () => LogicalType.Timestamp(isAdjustedForUtc, timeUnit);
			}

			// Token: 0x06010F6B RID: 69483 RVA: 0x003A807C File Offset: 0x003A627C
			private static Func<LogicalType> GetLogicalType(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (node != null && node.LogicalTypeType() == LogicalTypeEnum.Timestamp)
				{
					return node.WithLogicalType(new Func<LogicalType, Func<LogicalType>>(ParquetTypeMaps.Timestamps.CopyLogicalType));
				}
				TimeUnit timeUnit = ParquetTypeMaps.Times.GetKey(typeValue, schemaConfig) ?? schemaConfig.DefaultTimeUnit;
				bool isAdjustedToUtc = (typeValue != null && typeValue.TypeKind == ValueKind.DateTimeZone) || schemaConfig.AllTimesUtcAdjusted;
				return () => LogicalType.Timestamp(isAdjustedToUtc, timeUnit);
			}

			// Token: 0x06010F6C RID: 69484 RVA: 0x003A80F9 File Offset: 0x003A62F9
			private static Func<long, DateTimeOffset> UtcConversion(Func<long, DateTime> dateTimeConversion)
			{
				return (long value) => global::System.DateTime.SpecifyKind(dateTimeConversion(value), DateTimeKind.Utc);
			}

			// Token: 0x06010F6D RID: 69485 RVA: 0x003A8112 File Offset: 0x003A6312
			private static Func<IAllocator, DateTimeOffset, long> UtcConversion(Func<IAllocator, DateTime, long> dateTimeConversion)
			{
				return (IAllocator allocator, DateTimeOffset value) => dateTimeConversion(allocator, value.UtcDateTime);
			}

			// Token: 0x04006657 RID: 26199
			private static readonly ParquetTypeMapper Int64TimestampMillisLocal = new ParquetPrimitiveTypeMap<long, DateTime>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(false, TimeUnit.Millis), new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampMillis), new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampMillis), TypeFacets.NewDateTime(new int?(3), null), null);

			// Token: 0x04006658 RID: 26200
			private static readonly ParquetTypeMapper Int64TimestampMicrosLocal = new ParquetPrimitiveTypeMap<long, DateTime>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(false, TimeUnit.Micros), new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampMicros), new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampMicros), TypeFacets.NewDateTime(new int?(6), null), null);

			// Token: 0x04006659 RID: 26201
			private static readonly ParquetTypeMapper Int64TimestampNanosLocal = new ParquetPrimitiveTypeMap<long, DateTime>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(false, TimeUnit.Nanos), new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampNanos), new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampNanos), TypeFacets.NewDateTime(new int?(9), null), null);

			// Token: 0x0400665A RID: 26202
			private static readonly ParquetTypeMapper Int64TimestampMillisUtcSql = new ParquetPrimitiveTypeMap<long, DateTime>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(false, TimeUnit.Millis), new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampMillis), new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampMillis), TypeFacets.NewDateTime(new int?(3), null), null);

			// Token: 0x0400665B RID: 26203
			private static readonly ParquetTypeMapper Int64TimestampMicrosUtcSql = new ParquetPrimitiveTypeMap<long, DateTime>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(false, TimeUnit.Micros), new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampMicros), new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampMicros), TypeFacets.NewDateTime(new int?(6), null), null);

			// Token: 0x0400665C RID: 26204
			private static readonly ParquetTypeMapper Int64TimestampNanosUtcSql = new ParquetPrimitiveTypeMap<long, DateTime>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(false, TimeUnit.Nanos), new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampNanos), new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampNanos), TypeFacets.NewDateTime(new int?(9), null), null);

			// Token: 0x0400665D RID: 26205
			private static readonly ParquetTypeMapper Int64TimestampMillisUtcDefault = new ParquetPrimitiveTypeMap<long, DateTimeOffset>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(true, TimeUnit.Millis), ParquetTypeMaps.Timestamps.UtcConversion(new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampMillis)), ParquetTypeMaps.Timestamps.UtcConversion(new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampMillis)), TypeFacets.NewDateTime(new int?(3), null), null);

			// Token: 0x0400665E RID: 26206
			private static readonly ParquetTypeMapper Int64TimestampMicrosUtcDefault = new ParquetPrimitiveTypeMap<long, DateTimeOffset>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(true, TimeUnit.Micros), ParquetTypeMaps.Timestamps.UtcConversion(new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampMicros)), ParquetTypeMaps.Timestamps.UtcConversion(new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampMicros)), TypeFacets.NewDateTime(new int?(6), null), null);

			// Token: 0x0400665F RID: 26207
			private static readonly ParquetTypeMapper Int64TimestampNanosUtcDefault = new ParquetPrimitiveTypeMap<long, DateTimeOffset>(LogicalTypeEnum.Timestamp, () => LogicalType.Timestamp(true, TimeUnit.Nanos), ParquetTypeMaps.Timestamps.UtcConversion(new Func<long, DateTime>(LogicalTypeConversions.Int64ToTimestampNanos)), ParquetTypeMaps.Timestamps.UtcConversion(new Func<IAllocator, DateTime, long>(LogicalTypeConversions.Int64FromTimestampNanos)), TypeFacets.NewDateTime(new int?(9), null), null);

			// Token: 0x04006660 RID: 26208
			private static readonly ParquetTypeMapper Int64TimestampMillisUtc = ParquetTypeMapper.Cased<TypeMapping>((Node node, TypeValue typeValue, SchemaConfig schemaConfig) => schemaConfig.DefaultTypeMapping, new Dictionary<TypeMapping, ParquetTypeMapper>
			{
				{
					TypeMapping.Default,
					ParquetTypeMaps.Timestamps.Int64TimestampMillisUtcDefault
				},
				{
					TypeMapping.Sql,
					ParquetTypeMaps.Timestamps.Int64TimestampMillisUtcSql
				}
			});

			// Token: 0x04006661 RID: 26209
			private static readonly ParquetTypeMapper Int64TimestampMicrosUtc = ParquetTypeMapper.Cased<TypeMapping>((Node node, TypeValue typeValue, SchemaConfig schemaConfig) => schemaConfig.DefaultTypeMapping, new Dictionary<TypeMapping, ParquetTypeMapper>
			{
				{
					TypeMapping.Default,
					ParquetTypeMaps.Timestamps.Int64TimestampMicrosUtcDefault
				},
				{
					TypeMapping.Sql,
					ParquetTypeMaps.Timestamps.Int64TimestampMicrosUtcSql
				}
			});

			// Token: 0x04006662 RID: 26210
			private static readonly ParquetTypeMapper Int64TimestampNanosUtc = ParquetTypeMapper.Cased<TypeMapping>((Node node, TypeValue typeValue, SchemaConfig schemaConfig) => schemaConfig.DefaultTypeMapping, new Dictionary<TypeMapping, ParquetTypeMapper>
			{
				{
					TypeMapping.Default,
					ParquetTypeMaps.Timestamps.Int64TimestampNanosUtcDefault
				},
				{
					TypeMapping.Sql,
					ParquetTypeMaps.Timestamps.Int64TimestampNanosUtcSql
				}
			});

			// Token: 0x04006663 RID: 26211
			public static readonly ParquetTypeMapper Mapper = ParquetTypeMapper.CheckType(ParquetTypeMapper.Cased<ValueTuple<bool, TimeUnit>>(ParquetTypeMaps.Chooser<ValueTuple<bool?, TimeUnit?>, ValueTuple<bool, TimeUnit>>(new Func<Node, SchemaConfig, ValueTuple<bool?, TimeUnit?>>(ParquetTypeMaps.Timestamps.GetKey), new Func<TypeValue, SchemaConfig, ValueTuple<bool?, TimeUnit?>>(ParquetTypeMaps.Timestamps.GetKey), delegate(Node node, TypeValue type, SchemaConfig schemaConfig, ValueTuple<bool?, TimeUnit?> choice)
			{
				bool? item = choice.Item1;
				TimeUnit? item2 = choice.Item2;
				if (node == null)
				{
					if (item == null)
					{
						throw ParquetTypeErrors.InsufficientTypeError(type, Array.Empty<string>());
					}
					return ParquetTypeMaps.Timestamps.GetKey(item.Value, item2 ?? schemaConfig.DefaultTimeUnit);
				}
				else
				{
					if (item2 == null)
					{
						throw ParquetTypeErrors.InsufficientTypeError(type, new string[] { "DateTimePrecision" });
					}
					return ParquetTypeMaps.Timestamps.GetKey(item.GetValueOrDefault(), item2.Value);
				}
			}, delegate(Node node, ValueTuple<bool?, TimeUnit?> nodeChoice, TypeValue type, ValueTuple<bool?, TimeUnit?> typeChoice)
			{
				bool? flag;
				if (nodeChoice.Item1 == null)
				{
					flag = typeChoice.Item1;
				}
				else
				{
					bool? flag2;
					if (!nodeChoice.Item1.Value)
					{
						flag2 = typeChoice.Item1;
						bool flag3 = true;
						if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
						{
							throw ParquetTypeErrors.IncompatibleTypeError(type, "Type", TypeValue.DateTimeZone, TypeValue.DateTime);
						}
					}
					flag2 = typeChoice.Item1;
					flag = ((flag2 != null) ? flag2 : nodeChoice.Item1);
				}
				TimeUnit? timeUnit3;
				if (nodeChoice.Item2 != null)
				{
					TimeUnit? timeUnit = nodeChoice.Item2;
					TimeUnit timeUnit2 = TimeUnit.Unknown;
					if (!((timeUnit.GetValueOrDefault() == timeUnit2) & (timeUnit != null)))
					{
						if (typeChoice.Item2 == null)
						{
							timeUnit3 = nodeChoice.Item2;
							goto IL_0157;
						}
						timeUnit = nodeChoice.Item2;
						TimeUnit? item3 = typeChoice.Item2;
						if ((timeUnit.GetValueOrDefault() == item3.GetValueOrDefault()) & (timeUnit != null == (item3 != null)))
						{
							timeUnit3 = nodeChoice.Item2;
							goto IL_0157;
						}
						throw ParquetTypeErrors.IncompatibleTypeError(type, "DateTimePrecision", NumberValue.New(ParquetTypeMaps.Times.GetPrecision(typeChoice.Item2.Value)), NumberValue.New(ParquetTypeMaps.Times.GetPrecision(nodeChoice.Item2.Value)));
					}
				}
				timeUnit3 = typeChoice.Item2;
				IL_0157:
				return new ValueTuple<bool?, TimeUnit?>(flag, timeUnit3);
			}), new Dictionary<ValueTuple<bool, TimeUnit>, ParquetTypeMapper>
			{
				{
					new ValueTuple<bool, TimeUnit>(false, TimeUnit.Millis),
					ParquetTypeMaps.Timestamps.Int64TimestampMillisLocal
				},
				{
					new ValueTuple<bool, TimeUnit>(false, TimeUnit.Micros),
					ParquetTypeMaps.Timestamps.Int64TimestampMicrosLocal
				},
				{
					new ValueTuple<bool, TimeUnit>(false, TimeUnit.Nanos),
					ParquetTypeMaps.Timestamps.Int64TimestampNanosLocal
				},
				{
					new ValueTuple<bool, TimeUnit>(true, TimeUnit.Millis),
					ParquetTypeMaps.Timestamps.Int64TimestampMillisUtc
				},
				{
					new ValueTuple<bool, TimeUnit>(true, TimeUnit.Micros),
					ParquetTypeMaps.Timestamps.Int64TimestampMicrosUtc
				},
				{
					new ValueTuple<bool, TimeUnit>(true, TimeUnit.Nanos),
					ParquetTypeMaps.Timestamps.Int64TimestampNanosUtc
				}
			}), new ValueKind[]
			{
				ValueKind.DateTime,
				ValueKind.DateTimeZone
			});
		}

		// Token: 0x02001FA9 RID: 8105
		private static class Decimals
		{
			// Token: 0x06010F87 RID: 69511 RVA: 0x003A881C File Offset: 0x003A6A1C
			private static PhysicalType GetKey(Node node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (typeValue == null)
				{
					return ParquetTypeMaps.Decimals.GetKey(node);
				}
				if (typeValue.TypeKind != ValueKind.Any && typeValue.TypeKind != ValueKind.Number)
				{
					throw ParquetTypeErrors.UnmappedTypeError(typeValue, "ActualTypes", ListValue.New(new Value[]
					{
						TypeValue.Decimal,
						TypeValue.Number
					}));
				}
				if (node == null)
				{
					return ParquetTypeMaps.Decimals.GetKey(typeValue, schemaConfig);
				}
				PhysicalType key = ParquetTypeMaps.Decimals.GetKey(node);
				bool flag = key == PhysicalType.ByteArray;
				if (typeValue.Facets.IsVariableLength != null)
				{
					bool? isVariableLength = typeValue.Facets.IsVariableLength;
					bool flag2 = flag;
					if (!((isVariableLength.GetValueOrDefault() == flag2) & (isVariableLength != null)))
					{
						throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "IsVariableLength", LogicalValue.New(typeValue.Facets.IsVariableLength.Value), LogicalValue.New(flag));
					}
				}
				return key;
			}

			// Token: 0x06010F88 RID: 69512 RVA: 0x003A88E3 File Offset: 0x003A6AE3
			private static PhysicalType GetKey(Node node)
			{
				return ParquetTypeMap.GetPhysicalTypeOrSentinel(node);
			}

			// Token: 0x06010F89 RID: 69513 RVA: 0x003A88EC File Offset: 0x003A6AEC
			private static PhysicalType GetKey(TypeValue typeValue, SchemaConfig schemaConfig)
			{
				TypeFacets decimalFacets = ParquetTypeMaps.Decimals.GetDecimalFacets(typeValue, schemaConfig);
				bool? isVariableLength = decimalFacets.IsVariableLength;
				bool flag = false;
				if (!((isVariableLength.GetValueOrDefault() == flag) & (isVariableLength != null)))
				{
					return PhysicalType.ByteArray;
				}
				long? num = decimalFacets.MaxLength;
				long num2 = 4L;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					return PhysicalType.Int32;
				}
				num = decimalFacets.MaxLength;
				num2 = 8L;
				if ((num.GetValueOrDefault() == num2) & (num != null))
				{
					return PhysicalType.Int64;
				}
				return PhysicalType.FixedLenByteArray;
			}

			// Token: 0x06010F8A RID: 69514 RVA: 0x003A8968 File Offset: 0x003A6B68
			private static bool CanOverflow(Node node, TypeValue typeValue)
			{
				if (node != null)
				{
					using (LogicalType logicalType = node.LogicalType)
					{
						DecimalLogicalType decimalLogicalType = logicalType as DecimalLogicalType;
						if (decimalLogicalType != null)
						{
							return decimalLogicalType.Precision > 28;
						}
					}
				}
				if (typeValue == null)
				{
					throw new InvalidOperationException();
				}
				if (typeValue.Facets.NumericPrecision != null)
				{
					return typeValue.Facets.NumericPrecision.Value > 28;
				}
				return !typeValue.NewFacets(TypeFacets.None).Equals(TypeValue.Decimal);
			}

			// Token: 0x06010F8B RID: 69515 RVA: 0x003A8A04 File Offset: 0x003A6C04
			private static ParquetTypeMapper New<TPhysical, TLogical>(ParquetTypeMaps.Decimals.ToLogicalFactoryWithLength<TPhysical, TLogical> toLogicalFactory, ParquetTypeMaps.Decimals.FromLogicalFactoryWithLength<TLogical, TPhysical> fromLogicalFactory)
			{
				return ParquetTypeMaps.Decimals.New<TPhysical, TLogical>((TypeFacets facets) => toLogicalFactory((int)facets.MaxLength.Value, facets.NumericPrecision.Value, facets.NumericScale.Value), (TypeFacets facets) => fromLogicalFactory((int)facets.MaxLength.Value, facets.NumericPrecision.Value, facets.NumericScale.Value));
			}

			// Token: 0x06010F8C RID: 69516 RVA: 0x003A8A44 File Offset: 0x003A6C44
			private static ParquetTypeMapper New<TPhysical, TLogical>(ParquetTypeMaps.Decimals.ToLogicalFactoryNoLength<TPhysical, TLogical> toLogicalFactory, ParquetTypeMaps.Decimals.FromLogicalFactoryNoLength<TLogical, TPhysical> fromLogicalFactory)
			{
				return ParquetTypeMaps.Decimals.New<TPhysical, TLogical>((TypeFacets facets) => toLogicalFactory(facets.NumericPrecision.Value, facets.NumericScale.Value), (TypeFacets facets) => fromLogicalFactory(facets.NumericPrecision.Value, facets.NumericScale.Value));
			}

			// Token: 0x06010F8D RID: 69517 RVA: 0x003A8A82 File Offset: 0x003A6C82
			private static ParquetTypeMapper New<TPhysical, TLogical>(ParquetTypeMaps.Decimals.ToLogicalFactory<TPhysical, TLogical> toLogicalFactory, ParquetTypeMaps.Decimals.FromLogicalFactory<TLogical, TPhysical> fromLogicalFactory)
			{
				return ParquetTypeMapper.MapTo<PrimitiveNode>(delegate(PrimitiveNode node, TypeValue typeValue, SchemaConfig schemaConfig)
				{
					TypeFacets decimalFacets = ParquetTypeMaps.Decimals.GetDecimalFacets(node, typeValue, schemaConfig);
					LogicalTypeEnum logicalTypeEnum = LogicalTypeEnum.Decimal;
					Func<LogicalType> func = () => LogicalType.Decimal(decimalFacets.NumericPrecision.Value, decimalFacets.NumericScale.Value);
					Func<TPhysical, TLogical> func2 = toLogicalFactory(decimalFacets);
					Func<IAllocator, TLogical, TPhysical> func3 = fromLogicalFactory(decimalFacets);
					TypeFacets decimalFacets2 = decimalFacets;
					long? maxLength = decimalFacets.MaxLength;
					return new ParquetPrimitiveTypeMap<TPhysical, TLogical>(logicalTypeEnum, func, func2, func3, decimalFacets2, (maxLength != null) ? new int?((int)maxLength.GetValueOrDefault()) : null);
				});
			}

			// Token: 0x06010F8E RID: 69518 RVA: 0x003A8AA8 File Offset: 0x003A6CA8
			private static TypeFacets GetDecimalFacets(PrimitiveNode node, TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (typeValue == null)
				{
					return ParquetTypeMaps.Decimals.GetDecimalFacets(node);
				}
				if (node != null)
				{
					TypeFacets decimalFacets = ParquetTypeMaps.Decimals.GetDecimalFacets(node);
					TypeFacets facets = typeValue.Facets;
					if (facets.NumericPrecisionBase != null)
					{
						int? numericPrecisionBase = facets.NumericPrecisionBase;
						int num = 10;
						if (!((numericPrecisionBase.GetValueOrDefault() == num) & (numericPrecisionBase != null)))
						{
							throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "NumericPrecisionBase", NumberValue.New(facets.NumericPrecisionBase.Value), NumberValue.New(10));
						}
					}
					int num2 = ParquetTypeMaps.Decimals.ChooseFacet<int>(decimalFacets.NumericPrecision, facets.NumericPrecision, "NumericPrecision", typeValue);
					int num3 = ParquetTypeMaps.Decimals.ChooseFacet<int>(decimalFacets.NumericScale, facets.NumericScale, "NumericScale", typeValue);
					bool flag = ParquetTypeMaps.Decimals.ChooseFacet<bool>(decimalFacets.IsVariableLength, facets.IsVariableLength, "IsVariableLength", typeValue);
					int? num4;
					if (flag)
					{
						if (facets.MaxLength != null)
						{
							throw ParquetTypeErrors.UnmappedTypeError(typeValue, "MaxLength", NumberValue.New(facets.MaxLength.Value));
						}
						num4 = null;
					}
					else
					{
						num4 = new int?(ParquetTypeMaps.GetTypeLength(node, typeValue));
					}
					return ParquetTypeMaps.Decimals.NewDecimalFacets(num2, num3, flag, num4);
				}
				return ParquetTypeMaps.Decimals.GetDecimalFacets(typeValue, schemaConfig);
			}

			// Token: 0x06010F8F RID: 69519 RVA: 0x003A8BD0 File Offset: 0x003A6DD0
			private static TypeFacets GetDecimalFacets(PrimitiveNode node)
			{
				int? typeLength = null;
				PhysicalType physicalType = node.PhysicalType;
				if (physicalType != PhysicalType.Int32)
				{
					if (physicalType != PhysicalType.Int64)
					{
						if (physicalType == PhysicalType.FixedLenByteArray)
						{
							typeLength = new int?(node.TypeLength);
						}
					}
					else
					{
						typeLength = new int?(8);
					}
				}
				else
				{
					typeLength = new int?(4);
				}
				return node.WithLogicalType(delegate(LogicalType logicalType)
				{
					DecimalLogicalType decimalLogicalType = logicalType as DecimalLogicalType;
					if (decimalLogicalType == null)
					{
						int? typeLength2 = typeLength;
						return TypeFacets.NewBinary((typeLength2 != null) ? new long?((long)typeLength2.GetValueOrDefault()) : null, new bool?(typeLength == null), null);
					}
					return ParquetTypeMaps.Decimals.NewDecimalFacets(decimalLogicalType.Precision, decimalLogicalType.Scale, typeLength != null, typeLength);
				});
			}

			// Token: 0x06010F90 RID: 69520 RVA: 0x003A8C48 File Offset: 0x003A6E48
			private static TypeFacets GetDecimalFacets(TypeValue typeValue, SchemaConfig schemaConfig)
			{
				if (typeValue.Facets.NumericPrecisionBase != null)
				{
					int? numericPrecisionBase = typeValue.Facets.NumericPrecisionBase;
					int num = 10;
					if (!((numericPrecisionBase.GetValueOrDefault() == num) & (numericPrecisionBase != null)))
					{
						return ParquetTypeMaps.Decimals.GetDecimalFacets(typeValue.NewFacets(TypeFacets.None), schemaConfig);
					}
				}
				int num2;
				bool flag;
				int num3;
				if (typeValue.Facets.NumericScale != null)
				{
					num2 = typeValue.Facets.NumericScale.Value;
					if (typeValue.Facets.NumericPrecision != null)
					{
						flag = false;
						num3 = typeValue.Facets.NumericPrecision.Value;
					}
					else if (typeValue.NewFacets(TypeFacets.None).Equals(TypeValue.Currency))
					{
						flag = false;
						num3 = 19;
					}
					else
					{
						flag = true;
						num3 = 29 + num2;
					}
				}
				else if (typeValue.NewFacets(TypeFacets.None).Equals(TypeValue.Currency))
				{
					flag = false;
					num2 = 4;
					num3 = typeValue.Facets.NumericPrecision.GetValueOrDefault(15) + num2;
				}
				else if (schemaConfig.DefaultTypeMapping == TypeMapping.Sql)
				{
					flag = true;
					num2 = 6;
					num3 = 34;
				}
				else
				{
					flag = true;
					num2 = 28;
					num3 = typeValue.Facets.NumericPrecision.GetValueOrDefault(29) + num2;
				}
				if (num3 < num2)
				{
					num3 = num2;
					flag = true;
				}
				if (num2 > 28)
				{
					num3 -= num2 - 28;
					num2 = 28;
				}
				if (num2 < 0)
				{
					num3 -= num2;
					num2 = 0;
				}
				if (num3 - num2 > 29)
				{
					num3 = 29 + num2;
				}
				if (num3 == 0)
				{
					num3 = 1;
				}
				int? num4 = null;
				if (!flag)
				{
					if (num3 < 9)
					{
						num4 = new int?(4);
					}
					else if (num3 < 18)
					{
						num4 = new int?(8);
					}
					else
					{
						num4 = new int?(LogicalTypeConversions.RequiredLength(num3));
					}
				}
				return ParquetTypeMaps.Decimals.NewDecimalFacets(num3, num2, flag, num4);
			}

			// Token: 0x06010F91 RID: 69521 RVA: 0x003A8E00 File Offset: 0x003A7000
			private static T ChooseFacet<T>(T? nodeFacet, T? typeFacet, string facetName, TypeValue typeValue) where T : struct
			{
				if (nodeFacet != null && typeFacet != null)
				{
					T value = nodeFacet.Value;
					if (value.Equals(typeFacet.Value))
					{
						return nodeFacet.Value;
					}
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, facetName, ValueMarshaller.MarshalFromClr(typeFacet), ValueMarshaller.MarshalFromClr(nodeFacet));
				}
				else
				{
					if (nodeFacet != null)
					{
						return nodeFacet.Value;
					}
					if (typeFacet != null)
					{
						return typeFacet.Value;
					}
					throw ParquetTypeErrors.InsufficientTypeError(typeValue, new string[] { facetName });
				}
			}

			// Token: 0x06010F92 RID: 69522 RVA: 0x003A8E9C File Offset: 0x003A709C
			private static TypeFacets NewDecimalFacets(int precision, int scale, bool isVariableLength, int? maxLength)
			{
				return TypeFacets.FromRecord(RecordValue.New(new NamedValue[]
				{
					new NamedValue("NumericPrecisionBase", NumberValue.New(10)),
					new NamedValue("NumericPrecision", NumberValue.New(precision)),
					new NamedValue("NumericScale", NumberValue.New(scale)),
					new NamedValue("IsVariableLength", LogicalValue.New(isVariableLength)),
					new NamedValue("MaxLength", (maxLength != null) ? NumberValue.New(maxLength.Value) : Value.Null)
				}));
			}

			// Token: 0x0400666B RID: 26219
			public const int DecimalPrecisionBase = 10;

			// Token: 0x0400666C RID: 26220
			private static readonly ParquetTypeMapper Int32Decimal = ParquetTypeMaps.Decimals.New<int, decimal>(new ParquetTypeMaps.Decimals.ToLogicalFactoryNoLength<int, decimal>(LogicalTypeConversions.Int32ToDecimal), new ParquetTypeMaps.Decimals.FromLogicalFactoryNoLength<decimal, int>(LogicalTypeConversions.Int32FromDecimal));

			// Token: 0x0400666D RID: 26221
			private static readonly ParquetTypeMapper Int64Decimal = ParquetTypeMaps.Decimals.New<long, decimal>(new ParquetTypeMaps.Decimals.ToLogicalFactoryNoLength<long, decimal>(LogicalTypeConversions.Int64ToDecimal), new ParquetTypeMaps.Decimals.FromLogicalFactoryNoLength<decimal, long>(LogicalTypeConversions.Int64FromDecimal));

			// Token: 0x0400666E RID: 26222
			private static readonly ParquetTypeMapper ByteArrayDecimalNoOverflow = ParquetTypeMaps.Decimals.New<ByteArray, decimal>(new ParquetTypeMaps.Decimals.ToLogicalFactoryNoLength<ByteArray, decimal>(LogicalTypeConversions.ByteArrayToDecimalNoOverflow), new ParquetTypeMaps.Decimals.FromLogicalFactoryNoLength<decimal, ByteArray>(LogicalTypeConversions.ByteArrayFromDecimalNoOverflow));

			// Token: 0x0400666F RID: 26223
			private static readonly ParquetTypeMapper ByteArrayDecimalWithOverflow = ParquetTypeMaps.Decimals.New<ByteArray, Number>(new ParquetTypeMaps.Decimals.ToLogicalFactoryNoLength<ByteArray, Number>(LogicalTypeConversions.ByteArrayToDecimalWithOverflow), new ParquetTypeMaps.Decimals.FromLogicalFactoryNoLength<Number, ByteArray>(LogicalTypeConversions.ByteArrayFromDecimalWithOverflow));

			// Token: 0x04006670 RID: 26224
			private static readonly ParquetTypeMapper FixedLenByteArrayDecimalNoOverflow = ParquetTypeMaps.Decimals.New<FixedLenByteArray, decimal>(new ParquetTypeMaps.Decimals.ToLogicalFactoryWithLength<FixedLenByteArray, decimal>(LogicalTypeConversions.FixedLenByteArrayToDecimalNoOverflow), new ParquetTypeMaps.Decimals.FromLogicalFactoryWithLength<decimal, FixedLenByteArray>(LogicalTypeConversions.FixedLenByteArrayFromDecimalNoOverflow));

			// Token: 0x04006671 RID: 26225
			private static readonly ParquetTypeMapper FixedLenByteArrayDecimalWithOverflow = ParquetTypeMaps.Decimals.New<FixedLenByteArray, Number>(new ParquetTypeMaps.Decimals.ToLogicalFactoryWithLength<FixedLenByteArray, Number>(LogicalTypeConversions.FixedLenByteArrayToDecimalWithOverflow), new ParquetTypeMaps.Decimals.FromLogicalFactoryWithLength<Number, FixedLenByteArray>(LogicalTypeConversions.FixedLenByteArrayFromDecimalWithOverflow));

			// Token: 0x04006672 RID: 26226
			public static readonly ParquetTypeMapper Mapper = ParquetTypeMapper.CheckType(ParquetTypeMapper.Matching(new ParquetTypeMapper[]
			{
				ParquetTypeMapper.Cased<PhysicalType>(new Func<Node, TypeValue, SchemaConfig, PhysicalType>(ParquetTypeMaps.Decimals.GetKey), new Dictionary<PhysicalType, ParquetTypeMapper>
				{
					{
						PhysicalType.Int32,
						ParquetTypeMaps.Decimals.Int32Decimal
					},
					{
						PhysicalType.Int64,
						ParquetTypeMaps.Decimals.Int64Decimal
					},
					{
						PhysicalType.ByteArray,
						ParquetTypeMapper.Matching(new ParquetTypeMapper[]
						{
							ParquetTypeMapper.If(ParquetTypeMaps.Decimals.ByteArrayDecimalNoOverflow, (Node node, TypeValue typeValue) => !ParquetTypeMaps.Decimals.CanOverflow(node, typeValue)),
							ParquetTypeMaps.Decimals.ByteArrayDecimalWithOverflow
						})
					},
					{
						PhysicalType.FixedLenByteArray,
						ParquetTypeMapper.Matching(new ParquetTypeMapper[]
						{
							ParquetTypeMapper.If(ParquetTypeMaps.Decimals.FixedLenByteArrayDecimalNoOverflow, (Node node, TypeValue typeValue) => !ParquetTypeMaps.Decimals.CanOverflow(node, typeValue)),
							ParquetTypeMaps.Decimals.FixedLenByteArrayDecimalWithOverflow
						})
					}
				}),
				ParquetTypeMapper.Fail((Node node, TypeValue typeValue) => ParquetTypeErrors.IncompatibleTypeError(typeValue, "NativeTypeName", TextValue.New(ParquetTypeMap.GetLogicalTypeName(LogicalTypeEnum.Decimal)), TextValue.New(ParquetTypeMap.GetPhysicalTypeName(ParquetTypeMaps.Decimals.GetKey(node)))))
			}), new ValueKind[] { ValueKind.Number });

			// Token: 0x02001FAA RID: 8106
			// (Invoke) Token: 0x06010F95 RID: 69525
			private delegate Func<TPhysical, TLogical> ToLogicalFactory<TPhysical, TLogical>(TypeFacets decimalFacets);

			// Token: 0x02001FAB RID: 8107
			// (Invoke) Token: 0x06010F99 RID: 69529
			private delegate Func<TPhysical, TLogical> ToLogicalFactoryWithLength<TPhysical, TLogical>(int length, int precision, int scale);

			// Token: 0x02001FAC RID: 8108
			// (Invoke) Token: 0x06010F9D RID: 69533
			private delegate Func<TPhysical, TLogical> ToLogicalFactoryNoLength<TPhysical, TLogical>(int precision, int scale);

			// Token: 0x02001FAD RID: 8109
			// (Invoke) Token: 0x06010FA1 RID: 69537
			private delegate Func<IAllocator, TLogical, TPhysical> FromLogicalFactory<TLogical, TPhysical>(TypeFacets decimalFacets);

			// Token: 0x02001FAE RID: 8110
			// (Invoke) Token: 0x06010FA5 RID: 69541
			private delegate Func<IAllocator, TLogical, TPhysical> FromLogicalFactoryWithLength<TLogical, TPhysical>(int length, int precision, int scale);

			// Token: 0x02001FAF RID: 8111
			// (Invoke) Token: 0x06010FA9 RID: 69545
			private delegate Func<IAllocator, TLogical, TPhysical> FromLogicalFactoryNoLength<TLogical, TPhysical>(int precision, int scale);
		}

		// Token: 0x02001FB6 RID: 8118
		private static class Intervals
		{
			// Token: 0x06010FBD RID: 69565 RVA: 0x003A9364 File Offset: 0x003A7564
			private static bool ValidateIntervalRecord(TypeValue type)
			{
				if (type.TypeKind == ValueKind.Any || type.NewFacets(TypeFacets.None).Equals(TypeValue.Record))
				{
					return true;
				}
				RecordTypeValue asRecordType = type.AsRecordType;
				if (asRecordType.Open)
				{
					throw ParquetTypeErrors.UnmappedTypeError(type, "IsOpenRecord", LogicalValue.True);
				}
				if (!asRecordType.Fields.Equals(ParquetTypeMap.IntervalType.Fields))
				{
					throw ParquetTypeErrors.UnmappedTypeError(type, "ActualType", ParquetTypeMap.IntervalType);
				}
				for (int i = 0; i < asRecordType.FieldKeys.Length; i++)
				{
					bool flag;
					TypeValue fieldType = asRecordType.GetFieldType(i, out flag);
					if (!fieldType.Facets.IsEmpty)
					{
						throw ParquetTypeErrors.UnmappedTypeError(type, "Facets", fieldType.Facets.ToRecord());
					}
				}
				return true;
			}

			// Token: 0x0400667C RID: 26236
			private static readonly ParquetTypeMapper FixedLenByteArrayRecord = ParquetTypeMapper.MapIf<PrimitiveNode>(new ParquetPrimitiveTypeMap<FixedLenByteArray, ParquetInterval>(LogicalTypeEnum.Interval, new Func<LogicalType>(LogicalType.Interval), new Func<FixedLenByteArray, ParquetInterval>(LogicalTypeConversions.FixedLenByteArrayToInterval), new Func<IAllocator, ParquetInterval, FixedLenByteArray>(LogicalTypeConversions.FixedLenByteArrayFromInterval), null, new int?(12)), null, new Func<TypeValue, bool>(ParquetTypeMaps.Intervals.ValidateIntervalRecord));

			// Token: 0x0400667D RID: 26237
			public static readonly ParquetTypeMapper Mapper = ParquetTypeMapper.CheckType(ParquetTypeMaps.Intervals.FixedLenByteArrayRecord, new ValueKind[] { ValueKind.Record });
		}

		// Token: 0x02001FB7 RID: 8119
		private static class JsonDocuments
		{
			// Token: 0x17002CD4 RID: 11476
			// (get) Token: 0x06010FBF RID: 69567 RVA: 0x003A9494 File Offset: 0x003A7694
			public static ParquetTypeMapper Mapper
			{
				get
				{
					return ParquetTypeMapper.If(ParquetTypeMaps.JsonDocuments.ByteArrayJsonText, null, (TypeValue type) => type.NewFacets(TypeFacets.None).Equals(TypeValue.Text), null, null);
				}
			}

			// Token: 0x17002CD5 RID: 11477
			// (get) Token: 0x06010FC0 RID: 69568 RVA: 0x003A94E2 File Offset: 0x003A76E2
			private static ParquetTypeMap ByteArrayJsonText
			{
				get
				{
					return ParquetTypeMap.BaseOn<ByteArray, string>(LogicalTypeEnum.Json, new Func<LogicalType>(LogicalType.Json), ParquetTypeMaps.ByteArrayString);
				}
			}
		}

		// Token: 0x02001FB9 RID: 8121
		private static class BsonDocuments
		{
			// Token: 0x17002CD6 RID: 11478
			// (get) Token: 0x06010FC4 RID: 69572 RVA: 0x003A9520 File Offset: 0x003A7720
			public static ParquetTypeMapper Mapper
			{
				get
				{
					return ParquetTypeMapper.If(ParquetTypeMaps.BsonDocuments.ByteArrayBsonBinary, null, (TypeValue typeValue) => typeValue.NewFacets(TypeFacets.None).Equals(TypeValue.Binary), null, null);
				}
			}

			// Token: 0x17002CD7 RID: 11479
			// (get) Token: 0x06010FC5 RID: 69573 RVA: 0x003A956E File Offset: 0x003A776E
			private static ParquetTypeMap ByteArrayBsonBinary
			{
				get
				{
					return ParquetTypeMap.BaseOn<ByteArray, byte[]>(LogicalTypeEnum.Bson, new Func<LogicalType>(LogicalType.Bson), ParquetTypeMaps.ByteArrayNone);
				}
			}
		}

		// Token: 0x02001FBB RID: 8123
		private static class Lists
		{
			// Token: 0x06010FC9 RID: 69577 RVA: 0x003A95AC File Offset: 0x003A77AC
			private static bool IsValid(GroupNode node, SchemaConfig config)
			{
				if (node.LogicalTypeType() != LogicalTypeEnum.List)
				{
					return false;
				}
				if (node.Repetition == Repetition.Repeated || node.FieldCount != 1)
				{
					return false;
				}
				using (Node node2 = node.Field(0))
				{
					if (node2.Repetition != Repetition.Repeated || node2.LogicalTypeType() != LogicalTypeEnum.None)
					{
						return false;
					}
					if (ParquetTypeMaps.Lists.IsStandard(node, config))
					{
						using (Node node3 = ((GroupNode)node2).Field(0))
						{
							if (node3.Repetition == Repetition.Repeated)
							{
								return false;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x06010FCA RID: 69578 RVA: 0x003A9654 File Offset: 0x003A7854
			private static bool IsStandard(GroupNode node, SchemaConfig config)
			{
				bool flag;
				using (Node node2 = node.Field(0))
				{
					GroupNode groupNode = node2 as GroupNode;
					string name = groupNode.Name;
					flag = groupNode != null && groupNode.FieldCount == 1 && name != "array" && name != node.Name + "_tuple";
				}
				return flag;
			}

			// Token: 0x06010FCB RID: 69579 RVA: 0x003A96C8 File Offset: 0x003A78C8
			private static void GetNodeNames(GroupNode node, SchemaConfig config, out string listName, out string elementName)
			{
				ParquetTypeMaps.Lists.GetNodeNames(node, true, config, out listName, out elementName);
			}

			// Token: 0x06010FCC RID: 69580 RVA: 0x003A96D4 File Offset: 0x003A78D4
			private static void GetNodeNames(GroupNode node, SchemaConfig config, out string listName)
			{
				string text;
				ParquetTypeMaps.Lists.GetNodeNames(node, false, config, out listName, out text);
			}

			// Token: 0x06010FCD RID: 69581 RVA: 0x003A96EC File Offset: 0x003A78EC
			private static void GetNodeNames(GroupNode node, bool isStandard, SchemaConfig config, out string listName, out string elementName)
			{
				if (node == null)
				{
					listName = "list";
					elementName = "element";
					return;
				}
				using (GroupNode groupNode = (GroupNode)node.Field(0))
				{
					listName = groupNode.Name;
					if (isStandard)
					{
						using (Node node2 = groupNode.Field(0))
						{
							elementName = node2.Name;
							return;
						}
					}
					elementName = null;
				}
			}

			// Token: 0x06010FCE RID: 69582 RVA: 0x003A9770 File Offset: 0x003A7970
			private static Func<TypeValue, Func<RecordValue, Value>> GetToValue(string elementName)
			{
				Func<RecordValue, Value> <>9__1;
				return delegate(TypeValue expectedTypeValue)
				{
					if (expectedTypeValue.IsTableType)
					{
						TableTypeValue tableTypeValue = expectedTypeValue.AsTableType;
						return (RecordValue record) => new ColumnReferenceListValue(record[0].AsTable, elementName).ToTable(tableTypeValue);
					}
					Func<RecordValue, Value> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (RecordValue record) => new ColumnReferenceListValue(record[0].AsTable, elementName));
					}
					return func;
				};
			}

			// Token: 0x06010FCF RID: 69583 RVA: 0x003A978C File Offset: 0x003A798C
			private static Func<TypeValue, Func<IAllocator, Value, RecordValue>> GetFromValue(string listName, string elementName = null)
			{
				ParquetTypeMaps.Lists.<>c__DisplayClass9_0 CS$<>8__locals1 = new ParquetTypeMaps.Lists.<>c__DisplayClass9_0();
				CS$<>8__locals1.listKeys = Keys.New(listName);
				if (elementName == null)
				{
					return delegate(TypeValue expectedTypeValue)
					{
						Func<IAllocator, Value, RecordValue> func;
						if ((func = CS$<>8__locals1.<>9__1) == null)
						{
							func = (CS$<>8__locals1.<>9__1 = (IAllocator allocator, Value value) => RecordValue.New(CS$<>8__locals1.listKeys, new Value[] { value }));
						}
						return func;
					};
				}
				Keys elementKeys = Keys.New(elementName);
				Func<IValueReference, IValueReference> <>9__4;
				Func<IAllocator, Value, RecordValue> <>9__3;
				return delegate(TypeValue expectedTypeValue)
				{
					Func<IAllocator, Value, RecordValue> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = delegate(IAllocator allocator, Value value)
						{
							IEnumerable<IValueReference> enumerable2;
							if (!value.IsTable)
							{
								IEnumerable<IValueReference> enumerable = value.AsList;
								enumerable2 = enumerable;
							}
							else
							{
								IEnumerable<IValueReference> enumerable = value.AsTable;
								enumerable2 = enumerable;
							}
							Func<IValueReference, IValueReference> func3;
							if ((func3 = <>9__4) == null)
							{
								func3 = (<>9__4 = (IValueReference element) => RecordValue.New(elementKeys, new IValueReference[] { element }));
							}
							IEnumerable<IValueReference> enumerable3 = enumerable2.Select(func3);
							return RecordValue.New(CS$<>8__locals1.listKeys, new Value[] { ListValue.New(enumerable3).ToTable(elementKeys) });
						});
					}
					return func2;
				};
			}

			// Token: 0x06010FD0 RID: 69584 RVA: 0x003A97DE File Offset: 0x003A79DE
			private static Func<RecordTypeValue, TypeValue> GetToTypeValue(TypeValue expectedTypeValue)
			{
				bool isList = expectedTypeValue != null && expectedTypeValue.IsListType;
				bool isTable = expectedTypeValue != null && expectedTypeValue.IsTableType;
				return delegate(RecordTypeValue recordTypeValue)
				{
					TypeValue asType = recordTypeValue.Fields[0]["Type"].AsType.AsTableType.ItemType.Fields[0]["Type"].AsType;
					if (isTable || (!isList && asType.IsRecordType && !asType.IsNullable))
					{
						return TableTypeValue.New(asType.AsRecordType);
					}
					return ListTypeValue.New(asType);
				};
			}

			// Token: 0x06010FD1 RID: 69585 RVA: 0x003A9814 File Offset: 0x003A7A14
			private static Func<TypeValue, RecordTypeValue> GetFromTypeValue(string listName, string elementName = null)
			{
				if (elementName == null)
				{
					return delegate(TypeValue typeValue)
					{
						if (typeValue.TypeKind == ValueKind.Any)
						{
							return TypeValue.Record;
						}
						TypeValue elementType = ParquetTypeMaps.Lists.GetElementType(typeValue);
						TypeValue typeValue2 = ((elementType.IsRecordType && !elementType.IsNullable) ? TableTypeValue.New(elementType.AsRecordType) : ListTypeValue.New(elementType));
						typeValue2 = ParquetGroupTypeMap.MarkIsGenerated(typeValue2);
						return RecordTypeValue.New(RecordValue.New(Keys.New(listName), new Value[] { RecordTypeValue.NewField(typeValue2, null) }));
					};
				}
				return delegate(TypeValue typeValue)
				{
					if (typeValue.TypeKind == ValueKind.Any)
					{
						return TypeValue.Record;
					}
					TypeValue elementType2 = ParquetTypeMaps.Lists.GetElementType(typeValue);
					TableTypeValue tableTypeValue = TableTypeValue.New(ParquetGroupTypeMap.MarkIsGenerated(RecordTypeValue.New(RecordValue.New(Keys.New(elementName), new Value[] { RecordTypeValue.NewField(elementType2, null) }))).AsRecordType);
					tableTypeValue = ParquetGroupTypeMap.MarkIsGenerated(tableTypeValue).AsTableType;
					return RecordTypeValue.New(RecordValue.New(Keys.New(listName), new Value[] { RecordTypeValue.NewField(tableTypeValue, null) }));
				};
			}

			// Token: 0x06010FD2 RID: 69586 RVA: 0x003A9856 File Offset: 0x003A7A56
			private static TypeValue GetElementType(TypeValue typeValue)
			{
				if (typeValue.IsTableType)
				{
					return ParquetGroupTypeMap.MarkIsGenerated(typeValue.AsTableType.ItemType);
				}
				if (typeValue.IsListType)
				{
					return typeValue.AsListType.ItemType;
				}
				throw new InvalidOperationException();
			}

			// Token: 0x06010FD3 RID: 69587 RVA: 0x003A988A File Offset: 0x003A7A8A
			private static Func<NestedColumnSelection, NestedColumnSelection> GetMapColumnSelection(string listName, string elementName)
			{
				return (NestedColumnSelection columnSelection) => new NestedColumnSelection(new ColumnSelection(Keys.New(listName)), new NestedColumnSelection[]
				{
					new NestedColumnSelection(new ColumnSelection(Keys.New(elementName)), new NestedColumnSelection[] { columnSelection })
				});
			}

			// Token: 0x06010FD4 RID: 69588 RVA: 0x003A98AC File Offset: 0x003A7AAC
			// Note: this type is marked as 'beforefieldinit'.
			static Lists()
			{
				ParquetTypeMapper[] array = new ParquetTypeMapper[2];
				int num = 0;
				ParquetTypeMapper[] array2 = new ParquetTypeMapper[2];
				int num2 = 0;
				ParquetTypeMapper listStandard = ParquetTypeMaps.Lists.ListStandard;
				Func<GroupNode, bool> func = null;
				Func<TypeValue, bool> func2 = null;
				Func<GroupNode, SchemaConfig, bool> func3 = new Func<GroupNode, SchemaConfig, bool>(ParquetTypeMaps.Lists.IsStandard);
				array2[num2] = ParquetTypeMapper.If<GroupNode>(listStandard, func, func2, null, null, func3, null);
				array2[1] = ParquetTypeMapper.If<GroupNode>(ParquetTypeMaps.Lists.ListBackCompat, null, null, new bool?(true), null, null, null);
				ParquetTypeMapper parquetTypeMapper = ParquetTypeMapper.Matching(array2);
				Func<GroupNode, bool> func4 = null;
				Func<TypeValue, bool> func5 = null;
				Func<GroupNode, SchemaConfig, bool> func6 = new Func<GroupNode, SchemaConfig, bool>(ParquetTypeMaps.Lists.IsValid);
				array[num] = ParquetTypeMapper.If<GroupNode>(parquetTypeMapper, func4, func5, null, null, func6, null);
				array[1] = ParquetTypeMapper.Fail((Node node, TypeValue typeValue) => ParquetTypeErrors.IncompatibleTypeError(typeValue, "NativeTypeName", TextValue.New(ParquetTypeMap.GetLogicalTypeName(LogicalTypeEnum.List)), Value.Null));
				ParquetTypeMaps.Lists.Mapper = ParquetTypeMapper.CheckType(ParquetTypeMapper.Matching(array), new ValueKind[]
				{
					ValueKind.List,
					ValueKind.Table
				});
			}

			// Token: 0x04006682 RID: 26242
			private static readonly ParquetTypeMapper ListStandard = ParquetTypeMapper.MapTo<GroupNode>(delegate(GroupNode node, TypeValue typeValue, SchemaConfig config)
			{
				string text;
				string text2;
				ParquetTypeMaps.Lists.GetNodeNames(node, config, out text, out text2);
				return new ParquetGroupTypeMap(new ValueKind[]
				{
					ValueKind.List,
					ValueKind.Table
				}, LogicalTypeEnum.List, new Func<LogicalType>(LogicalType.List), ParquetTypeMaps.Lists.GetToValue(text2), ParquetTypeMaps.Lists.GetFromValue(text, text2), ParquetTypeMaps.Lists.GetToTypeValue(typeValue), ParquetTypeMaps.Lists.GetFromTypeValue(text, text2), ParquetTypeMaps.Lists.GetMapColumnSelection(text, text2));
			});

			// Token: 0x04006683 RID: 26243
			private static readonly ParquetTypeMapper ListBackCompat = ParquetTypeMapper.MapTo<GroupNode>(delegate(GroupNode node, TypeValue typeValue, SchemaConfig config)
			{
				string listName;
				ParquetTypeMaps.Lists.GetNodeNames(node, config, out listName);
				return new ParquetGroupTypeMap(new ValueKind[]
				{
					ValueKind.List,
					ValueKind.Table
				}, LogicalTypeEnum.List, new Func<LogicalType>(LogicalType.List), (TypeValue expectedTypeValue) => (RecordValue record) => record[0], ParquetTypeMaps.Lists.GetFromValue(listName, null), (RecordTypeValue recordTypeValue) => recordTypeValue.Fields[0]["Type"].AsType, ParquetTypeMaps.Lists.GetFromTypeValue(listName, null), (NestedColumnSelection columnSelection) => new NestedColumnSelection(new ColumnSelection(Keys.New(listName)), new NestedColumnSelection[] { columnSelection }));
			});

			// Token: 0x04006684 RID: 26244
			public static readonly ParquetTypeMapper Mapper;
		}

		// Token: 0x02001FC5 RID: 8133
		private static class Maps
		{
			// Token: 0x06010FF2 RID: 69618 RVA: 0x003A9EB4 File Offset: 0x003A80B4
			private static bool MatchesMap(GroupNode node)
			{
				if (node.LogicalTypeType() != LogicalTypeEnum.Map)
				{
					return false;
				}
				if (node.Repetition == Repetition.Repeated || node.FieldCount != 1)
				{
					return false;
				}
				using (Node node2 = node.Field(0))
				{
					using (LogicalType logicalType = node2.LogicalType)
					{
						if (node2.Repetition != Repetition.Repeated || (logicalType.Type != LogicalTypeEnum.None && logicalType.Type != LogicalTypeEnum.Map))
						{
							return false;
						}
						GroupNode groupNode = node2 as GroupNode;
						int fieldCount;
						if (groupNode == null || (fieldCount = groupNode.FieldCount) < 1 || fieldCount > 2)
						{
							return false;
						}
						using (Node node3 = groupNode.Field(0))
						{
							if (node3.Repetition != Repetition.Required)
							{
								return false;
							}
						}
						if (fieldCount == 2)
						{
							using (Node node4 = groupNode.Field(1))
							{
								if (node4.Repetition == Repetition.Repeated)
								{
									return false;
								}
							}
						}
					}
				}
				return true;
			}

			// Token: 0x06010FF3 RID: 69619 RVA: 0x003A9FD0 File Offset: 0x003A81D0
			private static bool ValidateMap(TypeValue typeValue)
			{
				if (typeValue.TypeKind == ValueKind.Any)
				{
					return true;
				}
				RecordTypeValue itemType = typeValue.AsTableType.ItemType;
				if (itemType.NewFacets(TypeFacets.None).Equals(TypeValue.Record))
				{
					return true;
				}
				if (itemType.Open)
				{
					throw ParquetTypeErrors.UnmappedTypeError(itemType, "IsOpenRecord", LogicalValue.True);
				}
				if (!itemType.FieldKeys.Equals(ParquetTypeMaps.Maps.KvpKeys) && !itemType.FieldKeys.Equals(ParquetTypeMaps.Maps.KeyOnlyKeys))
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "Type", typeValue, ListValue.New(new Value[]
					{
						TableTypeValue.New(ParquetTypeMaps.Maps.KvpKeys, null),
						TableTypeValue.New(ParquetTypeMaps.Maps.KeyOnlyKeys, null)
					}));
				}
				return true;
			}

			// Token: 0x06010FF4 RID: 69620 RVA: 0x003AA080 File Offset: 0x003A8280
			private static bool MatchesMapKeyValue(GroupNode node)
			{
				int fieldCount;
				if (node == null || (fieldCount = node.FieldCount) < 1 || fieldCount > 2)
				{
					return false;
				}
				Node parent = node.Parent;
				if (parent != null)
				{
					using (Node node2 = parent)
					{
						using (LogicalType logicalType = node2.LogicalType)
						{
							return logicalType.Type == LogicalTypeEnum.Map;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06010FF5 RID: 69621 RVA: 0x003AA0F8 File Offset: 0x003A82F8
			private static bool ValidateMapKeyValue(TypeValue typeValue)
			{
				if (ParquetTypeMap.GetLogicalTypeFromName(typeValue.Facets.NativeTypeName) != LogicalTypeEnum.None)
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "NativeTypeName", Value.Null, TextValue.New(typeValue.Facets.NativeTypeName));
				}
				if (typeValue.TypeKind != ValueKind.Any || !typeValue.IsRecordType)
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "Type", typeValue, TypeValue.Record);
				}
				if (!typeValue.AsRecordType.FieldKeys.Equals(ParquetTypeMaps.Maps.KvpKeys) && !typeValue.AsRecordType.FieldKeys.Equals(ParquetTypeMaps.Maps.KeyOnlyKeys))
				{
					throw ParquetTypeErrors.IncompatibleTypeError(typeValue, "Type", typeValue, ListValue.New(new Value[]
					{
						RecordTypeValue.New(ParquetTypeMaps.Maps.KvpKeys),
						RecordTypeValue.New(ParquetTypeMaps.Maps.KeyOnlyKeys)
					}));
				}
				return true;
			}

			// Token: 0x06010FF6 RID: 69622 RVA: 0x003AA1C0 File Offset: 0x003A83C0
			private static void GetNodeNames(GroupNode node, SchemaConfig config, out string kvpName, out string keyName, out string valueName)
			{
				if (node == null)
				{
					kvpName = "key_value";
					keyName = "key";
					valueName = "value";
					return;
				}
				using (GroupNode groupNode = (GroupNode)node.Field(0))
				{
					kvpName = groupNode.Name;
					using (Node node2 = groupNode.Field(0))
					{
						keyName = node2.Name;
					}
					if (groupNode.FieldCount > 1)
					{
						using (Node node3 = groupNode.Field(1))
						{
							valueName = node3.Name;
						}
						if (keyName == valueName)
						{
							Keys keys = ColumnLabelGenerator.GenerateKeys(new string[] { keyName, valueName }, 2);
							keyName = keys[0];
							valueName = keys[1];
						}
					}
					else
					{
						valueName = null;
					}
				}
			}

			// Token: 0x06010FF7 RID: 69623 RVA: 0x003AA2B0 File Offset: 0x003A84B0
			private static Func<RecordValue, Value> GetToValue(TypeValue expectedTypeValue)
			{
				Keys fieldKeys = expectedTypeValue.AsTableType.ItemType.FieldKeys;
				if (fieldKeys.Length == 2)
				{
					return (RecordValue record) => ParquetTypeMaps.Maps.Dedup(record[0].AsTable.SelectColumns(ParquetTypeMaps.Maps.kvpColumnSelection));
				}
				if (fieldKeys.Length == 1 && fieldKeys[0] == "Key")
				{
					return (RecordValue record) => ParquetTypeMaps.Maps.Dedup(record[0].AsTable.SelectColumns(ParquetTypeMaps.Maps.keyColumnSelection));
				}
				if (fieldKeys.Length == 1 && fieldKeys[0] == "Value")
				{
					return (RecordValue record) => ParquetTypeMaps.Maps.Dedup(record[0].AsTable.SelectColumns(ParquetTypeMaps.Maps.kvpColumnSelection)).SelectColumns(ParquetTypeMaps.Maps.valueColumnSelection);
				}
				if (fieldKeys.Length == 0)
				{
					return (RecordValue record) => ParquetTypeMaps.Maps.Dedup(record[0].AsTable.SelectColumns(ParquetTypeMaps.Maps.keyColumnSelection)).SelectColumns(ParquetTypeMaps.Maps.emptyColumnSelection);
				}
				throw new InvalidOperationException();
			}

			// Token: 0x06010FF8 RID: 69624 RVA: 0x003AA39C File Offset: 0x003A859C
			private static Func<TypeValue, Func<IAllocator, Value, RecordValue>> GetFromValue(string kvpName, string keyName, string valueName)
			{
				Keys kvpKeys = Keys.New(kvpName);
				ListValue renames;
				if (valueName == null)
				{
					renames = ListValue.New(new string[] { "Key", keyName });
				}
				else
				{
					renames = ListValue.New(new Value[]
					{
						ListValue.New(new string[] { "Key", keyName }),
						ListValue.New(new string[] { "Value", valueName })
					});
				}
				Func<IAllocator, Value, RecordValue> <>9__1;
				return delegate(TypeValue expectedTypeValue)
				{
					Func<IAllocator, Value, RecordValue> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (IAllocator allocator, Value value) => RecordValue.New(kvpKeys, new Value[] { value.AsTable.RenameColumns(renames, MissingFieldMode.Error) }));
					}
					return func;
				};
			}

			// Token: 0x06010FF9 RID: 69625 RVA: 0x003AA430 File Offset: 0x003A8630
			private static TypeValue ToTypeValue(RecordTypeValue recordTypeValue)
			{
				RecordTypeValue itemType = recordTypeValue.Fields[0]["Type"].AsType.AsTableType.ItemType;
				bool flag;
				TypeValue fieldType = itemType.GetFieldType(0, out flag);
				if (itemType.FieldKeys.Length == 1)
				{
					return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(ParquetTypeMaps.Maps.KeyOnlyKeys, new Value[] { RecordTypeValue.NewField(fieldType, null) })), ParquetTypeMaps.Maps.tableKeys);
				}
				TypeValue fieldType2 = itemType.GetFieldType(1, out flag);
				return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(ParquetTypeMaps.Maps.KvpKeys, new Value[]
				{
					RecordTypeValue.NewField(fieldType, null),
					RecordTypeValue.NewField(fieldType2, null)
				})), ParquetTypeMaps.Maps.tableKeys);
			}

			// Token: 0x06010FFA RID: 69626 RVA: 0x003AA4E2 File Offset: 0x003A86E2
			private static Func<TypeValue, RecordTypeValue> GetFromTypeValue(string kvpName, string keyName, string valueName)
			{
				return delegate(TypeValue typeValue)
				{
					if (typeValue.TypeKind == ValueKind.Any)
					{
						return TypeValue.Record;
					}
					RecordTypeValue itemType = typeValue.AsTableType.ItemType;
					if (itemType.NewFacets(TypeFacets.None).Equals(TypeValue.Record))
					{
						return TypeValue.Record;
					}
					bool flag;
					TypeValue fieldType = itemType.GetFieldType(0, out flag);
					RecordTypeValue recordTypeValue;
					if (itemType.FieldKeys.Length > 1)
					{
						TypeValue fieldType2 = itemType.GetFieldType(1, out flag);
						recordTypeValue = RecordTypeValue.New(RecordValue.New(Keys.New(keyName, valueName), new Value[]
						{
							RecordTypeValue.NewField(fieldType, null),
							RecordTypeValue.NewField(fieldType2, null)
						}));
					}
					else
					{
						recordTypeValue = RecordTypeValue.New(RecordValue.New(Keys.New(keyName), new Value[] { RecordTypeValue.NewField(fieldType, null) }));
					}
					recordTypeValue = ParquetGroupTypeMap.MarkIsGenerated(recordTypeValue).AsRecordType;
					TableTypeValue tableTypeValue = TableTypeValue.New(recordTypeValue);
					tableTypeValue = ParquetGroupTypeMap.MarkIsGenerated(tableTypeValue).AsTableType;
					return RecordTypeValue.New(RecordValue.New(Keys.New(kvpName), new Value[] { RecordTypeValue.NewField(tableTypeValue, null) }));
				};
			}

			// Token: 0x06010FFB RID: 69627 RVA: 0x003AA509 File Offset: 0x003A8709
			private static Func<NestedColumnSelection, NestedColumnSelection> GetMapColumnSelection(string kvpName, string keyName, string valueName)
			{
				return delegate(NestedColumnSelection columnSelection)
				{
					NestedColumnSelection nestedColumnSelection = columnSelection;
					if (!nestedColumnSelection.IsAll)
					{
						if (nestedColumnSelection.ColumnSelection.Keys.Length == 0)
						{
							return new NestedColumnSelection(new ColumnSelection(Keys.New(keyName)), null);
						}
						if (nestedColumnSelection.ColumnSelection.Keys.Length > 2)
						{
							throw new NotSupportedException();
						}
						int num;
						if (nestedColumnSelection.ColumnSelection.Keys.TryGetKeyIndex("Key", out num))
						{
							nestedColumnSelection = nestedColumnSelection.Rename(num, keyName);
						}
						if (nestedColumnSelection.ColumnSelection.Keys.TryGetKeyIndex("Value", out num))
						{
							nestedColumnSelection = nestedColumnSelection.Rename(num, valueName);
						}
					}
					if (nestedColumnSelection.GetColumn(0) != 0)
					{
						nestedColumnSelection = new NestedColumnSelection(new ColumnSelection(Keys.New(keyName, valueName)), new NestedColumnSelection[]
						{
							NestedColumnSelection.All,
							nestedColumnSelection.GetNestedColumnSelection(0)
						});
					}
					if (!nestedColumnSelection.GetNestedColumnSelection(0).IsAll)
					{
						ColumnSelection columnSelection2 = nestedColumnSelection.ColumnSelection;
						NestedColumnSelection[] array2;
						if (nestedColumnSelection.ColumnSelection.Keys.Length != 1)
						{
							NestedColumnSelection[] array = new NestedColumnSelection[2];
							array[0] = NestedColumnSelection.All;
							array2 = array;
							array[1] = nestedColumnSelection.GetNestedColumnSelection(0);
						}
						else
						{
							array2 = null;
						}
						nestedColumnSelection = new NestedColumnSelection(columnSelection2, array2);
					}
					return new NestedColumnSelection(new ColumnSelection(Keys.New(kvpName)), new NestedColumnSelection[] { nestedColumnSelection });
				};
			}

			// Token: 0x06010FFC RID: 69628 RVA: 0x003AA530 File Offset: 0x003A8730
			private static TableValue Dedup(TableValue table)
			{
				return ListValue.New(ParquetTypeMaps.Maps.Dedup(table, ParquetTypeMaps.Maps.keyComparer)).ToTable(table.Type.AsTableType.ReplaceTableKeys(ParquetTypeMaps.Maps.tableKeys));
			}

			// Token: 0x06010FFD RID: 69629 RVA: 0x003AA55C File Offset: 0x003A875C
			private static IEnumerable<IValueReference> Dedup(IEnumerable<IValueReference> values, IEqualityComparer<Value> comparer)
			{
				Dictionary<Value, Value> dictionary = new Dictionary<Value, Value>(comparer);
				foreach (IValueReference valueReference in values)
				{
					dictionary[valueReference.Value] = valueReference.Value;
				}
				foreach (Value value in dictionary.Values)
				{
					yield return value;
				}
				Dictionary<Value, Value>.ValueCollection.Enumerator enumerator2 = default(Dictionary<Value, Value>.ValueCollection.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06010FFE RID: 69630 RVA: 0x003AA574 File Offset: 0x003A8774
			// Note: this type is marked as 'beforefieldinit'.
			static Maps()
			{
				ParquetTypeMapper parquetTypeMapper = ParquetTypeMaps.GroupNone;
				Func<GroupNode, bool> func = null;
				Func<TypeValue, bool> func2 = new Func<TypeValue, bool>(ParquetTypeMaps.Maps.ValidateMapKeyValue);
				bool? flag = null;
				bool? flag2 = null;
				ParquetTypeMaps.Maps.MapKeyValue = ParquetTypeMapper.If<GroupNode>(parquetTypeMapper, func, func2, flag, flag2, null, null);
				ParquetTypeMapper[] array = new ParquetTypeMapper[3];
				int num = 0;
				ParquetTypeMapper mapKeyValue = ParquetTypeMaps.Maps.MapKeyValue;
				flag2 = new bool?(true);
				array[num] = ParquetTypeMapper.If<GroupNode>(mapKeyValue, new Func<GroupNode, bool>(ParquetTypeMaps.Maps.MatchesMapKeyValue), null, flag2, null, null, null);
				array[1] = ParquetTypeMapper.If<GroupNode>(ParquetTypeMaps.Maps.Map, new Func<GroupNode, bool>(ParquetTypeMaps.Maps.MatchesMap), null, null, null, null, null);
				array[2] = ParquetTypeMapper.Fail((Node node, TypeValue typeValue) => ParquetTypeErrors.IncompatibleTypeError(typeValue, "NativeTypeName", TextValue.New(ParquetTypeMap.GetLogicalTypeName(LogicalTypeEnum.Map)), Value.Null));
				ParquetTypeMaps.Maps.Mapper = ParquetTypeMapper.Matching(array);
				ParquetTypeMaps.Maps.kvpColumnSelection = new ColumnSelection(ParquetTypeMaps.Maps.KvpKeys);
				ParquetTypeMaps.Maps.keyColumnSelection = new ColumnSelection(ParquetTypeMaps.Maps.KeyOnlyKeys);
				ParquetTypeMaps.Maps.valueColumnSelection = new ColumnSelection(Keys.New("Value"), new int[] { 1 });
				ParquetTypeMaps.Maps.emptyColumnSelection = new ColumnSelection(Keys.Empty);
				ParquetTypeMaps.Maps.tableKeys = new TableKey[]
				{
					new TableKey(new int[1], true)
				};
				ParquetTypeMaps.Maps.keyComparer = new TableDistinct(new Distinct[]
				{
					new Distinct(new TableValue.ColumnSelectorFunctionValue("Key", 0), null)
				}).ToComparer();
			}

			// Token: 0x0400669A RID: 26266
			private const string keyKey = "Key";

			// Token: 0x0400669B RID: 26267
			private const string valueKey = "Value";

			// Token: 0x0400669C RID: 26268
			public static readonly Keys KvpKeys = Keys.New("Key", "Value");

			// Token: 0x0400669D RID: 26269
			public static readonly Keys KeyOnlyKeys = Keys.New("Key");

			// Token: 0x0400669E RID: 26270
			private static readonly ParquetTypeMapper Map = ParquetTypeMapper.CheckType(ParquetTypeMapper.MapIf<GroupNode>(delegate(GroupNode node, TypeValue typeValue, SchemaConfig config)
			{
				string text;
				string text2;
				string text3;
				ParquetTypeMaps.Maps.GetNodeNames(node, config, out text, out text2, out text3);
				if (text2 == text3)
				{
					Keys keys = ParquetGroupTypeMap.GenerateKeys(new string[] { text2, text3 });
					text2 = keys[0];
					text3 = keys[1];
				}
				return new ParquetGroupTypeMap(new ValueKind[] { ValueKind.Table }, LogicalTypeEnum.Map, new Func<LogicalType>(LogicalType.Map), new Func<TypeValue, Func<RecordValue, Value>>(ParquetTypeMaps.Maps.GetToValue), ParquetTypeMaps.Maps.GetFromValue(text, text2, text3), new Func<RecordTypeValue, TypeValue>(ParquetTypeMaps.Maps.ToTypeValue), ParquetTypeMaps.Maps.GetFromTypeValue(text, text2, text3), ParquetTypeMaps.Maps.GetMapColumnSelection(text, text2, text3));
			}, null, new Func<TypeValue, bool>(ParquetTypeMaps.Maps.ValidateMap), null, null), new ValueKind[] { ValueKind.Table });

			// Token: 0x0400669F RID: 26271
			private static readonly ParquetTypeMapper MapKeyValue;

			// Token: 0x040066A0 RID: 26272
			public static readonly ParquetTypeMapper Mapper;

			// Token: 0x040066A1 RID: 26273
			private static readonly ColumnSelection kvpColumnSelection;

			// Token: 0x040066A2 RID: 26274
			private static readonly ColumnSelection keyColumnSelection;

			// Token: 0x040066A3 RID: 26275
			private static readonly ColumnSelection valueColumnSelection;

			// Token: 0x040066A4 RID: 26276
			private static readonly ColumnSelection emptyColumnSelection;

			// Token: 0x040066A5 RID: 26277
			private static readonly TableKey[] tableKeys;

			// Token: 0x040066A6 RID: 26278
			private static readonly IEqualityComparer<Value> keyComparer;
		}
	}
}

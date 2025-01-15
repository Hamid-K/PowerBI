using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.DeltaLake.Types;
using Microsoft.Mashup.DeltaLake;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EFF RID: 7935
	internal static class DeltaTypeConversion
	{
		// Token: 0x06010B56 RID: 68438 RVA: 0x00398ECB File Offset: 0x003970CB
		public static TypeValue Convert(DataType type)
		{
			return type.Accept<TypeValue>(new DeltaTypeConversion.DeltaTypeConverter());
		}

		// Token: 0x06010B57 RID: 68439 RVA: 0x00398ED8 File Offset: 0x003970D8
		public static StructType Convert(string name, TypeValue typeValue, bool columnMapping, out TypeValue parquetType)
		{
			StructType structType = (StructType)DeltaTypeConversion.MTypeConverter.ConvertType(name, typeValue.AsTableType.ItemType, columnMapping, out parquetType);
			parquetType = TableTypeValue.New(parquetType.AsRecordType);
			return structType;
		}

		// Token: 0x06010B58 RID: 68440 RVA: 0x00398F00 File Offset: 0x00397100
		public static bool RequiresColumnMapping(TypeValue typeValue)
		{
			return DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(typeValue);
		}

		// Token: 0x0400641B RID: 25627
		private static readonly Keys mapKeys = Keys.New("Key", "Value");

		// Token: 0x0400641C RID: 25628
		private static readonly TableKey[] mapTableKeys = new TableKey[]
		{
			new TableKey(new int[1], true)
		};

		// Token: 0x02001F00 RID: 7936
		private sealed class DeltaTypeConverter : ITypeVisitor<TypeValue>
		{
			// Token: 0x06010B5A RID: 68442 RVA: 0x00398F38 File Offset: 0x00397138
			public TypeValue VisitArray(ArrayType array)
			{
				TypeValue typeValue = array.ElementType.Accept<TypeValue>(this);
				if (!array.ContainsNull && typeValue.IsRecordType)
				{
					return TableTypeValue.New(typeValue.AsRecordType);
				}
				if (array.ContainsNull)
				{
					typeValue = typeValue.Nullable;
				}
				return ListTypeValue.New(typeValue);
			}

			// Token: 0x06010B5B RID: 68443 RVA: 0x00398F84 File Offset: 0x00397184
			public TypeValue VisitMap(MapType map)
			{
				TypeValue typeValue = map.KeyType.Accept<TypeValue>(this);
				TypeValue typeValue2 = map.ValueType.Accept<TypeValue>(this);
				if (map.ValueContainsNull)
				{
					typeValue2 = typeValue2.Nullable;
				}
				return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(DeltaTypeConversion.mapKeys, new Value[]
				{
					RecordTypeValue.NewField(typeValue, null),
					RecordTypeValue.NewField(typeValue2, null)
				})), DeltaTypeConversion.mapTableKeys);
			}

			// Token: 0x06010B5C RID: 68444 RVA: 0x00398FF0 File Offset: 0x003971F0
			public TypeValue VisitScalar(ScalarType scalar)
			{
				TypeValue typeValue;
				if (!DeltaTypeConversion.DeltaTypeConverter.scalarTypeMap.TryGetValue(scalar, out typeValue))
				{
					DecimalType decimalType = scalar as DecimalType;
					if (decimalType == null)
					{
						throw new InvalidOperationException();
					}
					typeValue = TypeValue.Decimal.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(decimalType.Precision), new int?(decimalType.Scale), null));
				}
				return typeValue;
			}

			// Token: 0x06010B5D RID: 68445 RVA: 0x0039904C File Offset: 0x0039724C
			public TypeValue VisitStruct(StructType @struct)
			{
				RecordBuilder recordBuilder = new RecordBuilder(@struct.Fields.Length);
				foreach (StructField structField in @struct.Fields)
				{
					TypeValue typeValue = structField.DataType.Accept<TypeValue>(this);
					if (structField.Nullable)
					{
						typeValue = typeValue.Nullable;
					}
					string text = null;
					object obj;
					if (structField.Metadata.TryGetValue("delta.generationExpression", out obj) && obj is string)
					{
						text = (string)obj;
					}
					string text2 = structField.DataType.ToString();
					int num = text2.IndexOf('(');
					if (num > 0)
					{
						text2 = text2.Substring(0, num);
					}
					typeValue = typeValue.NewFacets(typeValue.Facets.AddNative(text2, null, text));
					RecordValue recordValue = RecordTypeValue.NewField(typeValue, null);
					recordBuilder.Add(structField.Name, recordValue, recordValue.Type);
				}
				return RecordTypeValue.New(recordBuilder.ToRecord());
			}

			// Token: 0x0400641D RID: 25629
			private static readonly Dictionary<DataType, TypeValue> scalarTypeMap = new Dictionary<DataType, TypeValue>
			{
				{
					DataTypes.Binary,
					TypeValue.Binary
				},
				{
					DataTypes.Boolean,
					TypeValue.Logical
				},
				{
					DataTypes.Byte,
					TypeValue.Byte
				},
				{
					DataTypes.Date,
					TypeValue.Date
				},
				{
					DataTypes.Double,
					TypeValue.Double
				},
				{
					DataTypes.Float,
					TypeValue.Single
				},
				{
					DataTypes.Integer,
					TypeValue.Int32
				},
				{
					DataTypes.Long,
					TypeValue.Int64
				},
				{
					DataTypes.Null,
					TypeValue.Null
				},
				{
					DataTypes.Short,
					TypeValue.Int16
				},
				{
					DataTypes.String,
					TypeValue.Text
				},
				{
					DataTypes.Timestamp,
					TypeValue.DateTime
				},
				{
					DataTypes.TimestampNtz,
					TypeValue.DateTime
				}
			};
		}

		// Token: 0x02001F01 RID: 7937
		private static class MTypeConverter
		{
			// Token: 0x06010B60 RID: 68448 RVA: 0x00399227 File Offset: 0x00397427
			public static DataType ConvertType(string name, TypeValue typeValue, bool columnMapping, out TypeValue parquetType)
			{
				DataType dataType = DeltaTypeConversion.MTypeConverter.ConvertNonNullableType(name, typeValue, columnMapping, out parquetType);
				if (typeValue.IsNullable)
				{
					parquetType = parquetType.Nullable;
				}
				return dataType;
			}

			// Token: 0x06010B61 RID: 68449 RVA: 0x00399244 File Offset: 0x00397444
			public static bool RequiresColumnMapping(TypeValue typeValue)
			{
				switch (typeValue.TypeKind)
				{
				case ValueKind.List:
					return DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(typeValue.AsListType.ItemType);
				case ValueKind.Record:
					return DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(typeValue.AsRecordType);
				case ValueKind.Table:
					return DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(typeValue.AsTableType);
				default:
					return false;
				}
			}

			// Token: 0x06010B62 RID: 68450 RVA: 0x0039929C File Offset: 0x0039749C
			private static bool RequiresColumnMapping(RecordTypeValue recordType)
			{
				Keys fieldKeys = recordType.FieldKeys;
				for (int i = 0; i < fieldKeys.Length; i++)
				{
					string text = recordType.FieldKeys[i];
					TypeValue asType = recordType.Fields[i]["Type"].AsType;
					if (!TypeBuilder.IsValidFieldName(text) || DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(asType))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06010B63 RID: 68451 RVA: 0x003992FC File Offset: 0x003974FC
			private static bool RequiresColumnMapping(TableTypeValue tableType)
			{
				RecordTypeValue itemType = tableType.ItemType;
				if (itemType.FieldKeys.Equals(DeltaTypeConversion.mapKeys) && tableType.TableKeys.SequenceEqual(DeltaTypeConversion.mapTableKeys))
				{
					TypeValue asType = itemType.Fields[0]["Type"].AsType;
					TypeValue asType2 = itemType.Fields[1]["Type"].AsType;
					if (!asType.IsNullable && (!TypeBuilder.IsValidFieldName(DeltaTypeConversion.mapKeys[0]) || DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(asType) || !TypeBuilder.IsValidFieldName(DeltaTypeConversion.mapKeys[1]) || DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(asType2)))
					{
						return true;
					}
				}
				return DeltaTypeConversion.MTypeConverter.RequiresColumnMapping(itemType);
			}

			// Token: 0x06010B64 RID: 68452 RVA: 0x003993B4 File Offset: 0x003975B4
			private static DataType ConvertNonNullableType(string name, TypeValue typeValue, bool columnMapping, out TypeValue parquetType)
			{
				parquetType = typeValue.SubtractMetaValue.AsType.NewFacets(TypeFacets.None);
				switch (typeValue.TypeKind)
				{
				case ValueKind.Null:
					return DataTypes.Null;
				case ValueKind.Date:
					return DataTypes.Date;
				case ValueKind.DateTime:
					return DataTypes.Timestamp;
				case ValueKind.Number:
					return DeltaTypeConversion.MTypeConverter.ConvertNumber(typeValue, ref parquetType);
				case ValueKind.Logical:
					return DataTypes.Boolean;
				case ValueKind.Text:
					parquetType = TypeValue.Text;
					return DataTypes.String;
				case ValueKind.Binary:
					return DataTypes.Binary;
				case ValueKind.List:
					return DeltaTypeConversion.MTypeConverter.ConvertList(name, typeValue.AsListType, columnMapping, out parquetType);
				case ValueKind.Record:
					return DeltaTypeConversion.MTypeConverter.ConvertRecord(name, typeValue.AsRecordType, columnMapping, out parquetType);
				case ValueKind.Table:
					return DeltaTypeConversion.MTypeConverter.ConvertTable(name, typeValue.AsTableType, columnMapping, out parquetType);
				}
				throw ValueException.NewExpressionError<Message2>(Resources.CantConvertType(typeValue.TypeKind, name), typeValue, null);
			}

			// Token: 0x06010B65 RID: 68453 RVA: 0x00399493 File Offset: 0x00397693
			private static DataType ConvertList(string name, ListTypeValue listType, bool columnMapping, out TypeValue parquetType)
			{
				DataType dataType = DeltaTypeConversion.MTypeConverter.ConvertType(name, listType.ItemType, columnMapping, out parquetType);
				parquetType = ListTypeValue.New(parquetType);
				return new ArrayType(dataType, listType.ItemType.IsNullable);
			}

			// Token: 0x06010B66 RID: 68454 RVA: 0x003994BC File Offset: 0x003976BC
			private static DataType ConvertNumber(TypeValue numberType, ref TypeValue parquetType)
			{
				numberType = numberType.NonNullable;
				if (numberType.Equals(TypeValue.Byte))
				{
					return DataTypes.Byte;
				}
				if (numberType.Equals(TypeValue.Int16) || numberType.Equals(TypeValue.Int8))
				{
					return DataTypes.Short;
				}
				if (numberType.Equals(TypeValue.Int32))
				{
					return DataTypes.Integer;
				}
				if (numberType.Equals(TypeValue.Int64))
				{
					return DataTypes.Long;
				}
				if (numberType.Equals(TypeValue.Single))
				{
					return DataTypes.Float;
				}
				if (numberType.Equals(TypeValue.Double))
				{
					return DataTypes.Double;
				}
				if (numberType.Equals(TypeValue.Decimal) || numberType.Equals(TypeValue.Currency) || numberType.Equals(TypeValue.Percentage))
				{
					int num2;
					int num3;
					if (numberType.Facets.NumericPrecision != null && numberType.Facets.NumericScale != null)
					{
						if (numberType.Facets.NumericPrecisionBase != null)
						{
							int? numericPrecisionBase = numberType.Facets.NumericPrecisionBase;
							int num = 10;
							if (!((numericPrecisionBase.GetValueOrDefault() == num) & (numericPrecisionBase != null)))
							{
								goto IL_013D;
							}
						}
						num2 = numberType.Facets.NumericPrecision.Value;
						num3 = numberType.Facets.NumericScale.Value;
						goto IL_0156;
					}
					IL_013D:
					if (numberType.Equals(TypeValue.Currency))
					{
						num2 = 19;
						num3 = 4;
					}
					else
					{
						num2 = 34;
						num3 = 6;
					}
					IL_0156:
					parquetType = parquetType.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(num2), new int?(num3), null));
					return new DecimalType(num2, num3);
				}
				return DataTypes.Double;
			}

			// Token: 0x06010B67 RID: 68455 RVA: 0x00399650 File Offset: 0x00397850
			private static DataType ConvertRecord(string name, RecordTypeValue recordType, bool columnMapping, out TypeValue parquetType)
			{
				if (recordType.Open)
				{
					throw ValueException.NewExpressionError<Message2>(Resources.CantConvertType(PiiFree.New("open record"), name), recordType, null);
				}
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				StructField[] array = new StructField[recordType.Fields.Count];
				RecordBuilder recordBuilder = new RecordBuilder(recordType.Fields.Count);
				for (int i = 0; i < array.Length; i++)
				{
					string text = recordType.FieldKeys[i];
					if (!hashSet.Add(text))
					{
						throw ValueException.NewDataFormatError<Message1>(Resources.CaseConflict(text), recordType, null);
					}
					TypeValue asType = recordType.Fields[i]["Type"].AsType;
					TypeValue typeValue;
					DataType dataType = DeltaTypeConversion.MTypeConverter.ConvertType(text, asType, columnMapping, out typeValue);
					Value value = RecordTypeAlgebra.NewField(typeValue, recordType.Fields[i]["Optional"].AsBoolean);
					if (columnMapping)
					{
						string text2 = Guid.NewGuid().ToString("D");
						array[i] = new StructField(recordType.FieldKeys[i], text2, dataType, asType.IsNullable, null);
						recordBuilder.Add(text2, value, value.Type);
					}
					else
					{
						array[i] = new StructField(recordType.FieldKeys[i], dataType, asType.IsNullable, null);
						recordBuilder.Add(recordType.FieldKeys[i], value, value.Type);
					}
				}
				parquetType = RecordTypeValue.New(recordBuilder.ToRecord());
				return new StructType(array);
			}

			// Token: 0x06010B68 RID: 68456 RVA: 0x003997D0 File Offset: 0x003979D0
			private static DataType ConvertTable(string name, TableTypeValue tableType, bool columnMapping, out TypeValue parquetType)
			{
				RecordTypeValue itemType = tableType.ItemType;
				if (itemType.FieldKeys.Equals(DeltaTypeConversion.mapKeys) && tableType.TableKeys.SequenceEqual(DeltaTypeConversion.mapTableKeys))
				{
					TypeValue asType = itemType.Fields[0]["Type"].AsType;
					TypeValue asType2 = itemType.Fields[1]["Type"].AsType;
					if (!asType.IsNullable)
					{
						DataType dataType = DeltaTypeConversion.MTypeConverter.ConvertType(DeltaTypeConversion.mapKeys[0], asType, columnMapping, out asType);
						DataType dataType2 = DeltaTypeConversion.MTypeConverter.ConvertType(DeltaTypeConversion.mapKeys[1], asType2, columnMapping, out asType2);
						RecordBuilder recordBuilder = new RecordBuilder(2);
						recordBuilder.Add(DeltaTypeConversion.mapKeys[0], asType, asType.Type);
						recordBuilder.Add(DeltaTypeConversion.mapKeys[1], asType2, asType2.Type);
						parquetType = TableTypeValue.New(RecordTypeValue.New(recordBuilder.ToRecord()), tableType.TableKeys);
						return new MapType(dataType, dataType2, asType2.IsNullable);
					}
				}
				TypeValue typeValue;
				DataType dataType3 = DeltaTypeConversion.MTypeConverter.ConvertRecord(name, itemType, columnMapping, out typeValue);
				parquetType = TableTypeValue.New(typeValue.AsRecordType);
				return new ArrayType(dataType3, false);
			}
		}
	}
}

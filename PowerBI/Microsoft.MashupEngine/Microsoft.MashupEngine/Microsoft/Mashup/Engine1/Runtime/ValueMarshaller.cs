using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020016A8 RID: 5800
	public static class ValueMarshaller
	{
		// Token: 0x06009363 RID: 37731 RVA: 0x001E7268 File Offset: 0x001E5468
		public static Value MarshalFromClr(object instance)
		{
			Value value;
			if (ValueMarshaller.TryMarshalFromClr(instance, out value))
			{
				return value;
			}
			string text;
			if (instance is UnsupportedType)
			{
				text = ((UnsupportedType)instance).Value;
			}
			else
			{
				text = instance.GetType().ToString();
			}
			throw new NotSupportedException(Strings.UnsupportedClrType(text));
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x001E72B8 File Offset: 0x001E54B8
		public static bool TryMarshalFromClr(object instance, out Value value)
		{
			value = null;
			if (instance == null)
			{
				value = Value.Null;
			}
			else
			{
				switch (Type.GetTypeCode(instance.GetType()))
				{
				case TypeCode.Empty:
					value = Value.Null;
					break;
				case TypeCode.DBNull:
					value = Value.Null;
					break;
				case TypeCode.Boolean:
					value = LogicalValue.New((bool)instance);
					break;
				case TypeCode.Char:
					value = TextValue.New((char)instance);
					break;
				case TypeCode.SByte:
					value = NumberValue.New((int)((sbyte)instance));
					break;
				case TypeCode.Byte:
					value = NumberValue.New((int)((byte)instance));
					break;
				case TypeCode.Int16:
					value = NumberValue.New((int)((short)instance));
					break;
				case TypeCode.UInt16:
					value = NumberValue.New((int)((ushort)instance));
					break;
				case TypeCode.Int32:
					value = NumberValue.New((int)instance);
					break;
				case TypeCode.UInt32:
					value = NumberValue.New((long)((ulong)((uint)instance)));
					break;
				case TypeCode.Int64:
					value = NumberValue.New((long)instance);
					break;
				case TypeCode.UInt64:
					value = NumberValue.New((ulong)instance);
					break;
				case TypeCode.Single:
					value = NumberValue.New((double)((float)instance));
					break;
				case TypeCode.Double:
					value = NumberValue.New((double)instance);
					break;
				case TypeCode.Decimal:
					value = NumberValue.New((decimal)instance);
					break;
				case TypeCode.DateTime:
					value = DateTimeValue.New((DateTime)instance);
					break;
				case TypeCode.String:
					value = TextValue.New((string)instance);
					break;
				}
			}
			if (value == null)
			{
				if (instance is Date)
				{
					value = DateValue.New(((Date)instance).DateTime);
				}
				else if (instance is Time)
				{
					value = TimeValue.New(((Time)instance).TimeSpan);
				}
				else if (instance is Guid)
				{
					value = TextValue.New(instance.ToString());
				}
				else if (instance is TimeSpan)
				{
					value = TimeValue.New((TimeSpan)instance);
				}
				else if (instance is DateTimeOffset)
				{
					value = DateTimeZoneValue.New((DateTimeOffset)instance);
				}
				else if (instance is byte[])
				{
					value = BinaryValue.New((byte[])instance);
				}
				else if (instance is Number)
				{
					Number number = (Number)instance;
					switch (number.Kind)
					{
					case NumberKind.Decimal:
					case NumberKind.Numeric:
						value = NumberValue.New(number.ToDecimal());
						break;
					case NumberKind.Double:
						value = NumberValue.New(number.ToDouble());
						break;
					}
				}
				else if (instance is Currency)
				{
					value = NumberValue.New(((Currency)instance).Value);
				}
			}
			return value != null;
		}

		// Token: 0x06009365 RID: 37733 RVA: 0x001E7568 File Offset: 0x001E5768
		public static Value MarshalFromClrDictionary(IDictionary<string, object> instance, int maxDepth = 3)
		{
			if (instance == null)
			{
				return ValueMarshaller.MarshalFromClr(instance);
			}
			if (maxDepth <= 0)
			{
				throw new DepthLimitExceededException("Depth limit exceeded");
			}
			RecordBuilder recordBuilder = new RecordBuilder(instance.Count);
			foreach (KeyValuePair<string, object> keyValuePair in instance)
			{
				IDictionary<string, object> dictionary = keyValuePair.Value as IDictionary<string, object>;
				Value value = ((dictionary != null) ? ValueMarshaller.MarshalFromClrDictionary(dictionary, maxDepth - 1) : ValueMarshaller.MarshalFromClr(keyValuePair.Value));
				recordBuilder.Add(keyValuePair.Key, value, value.Type);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x06009366 RID: 37734 RVA: 0x001E7618 File Offset: 0x001E5818
		public static Value MarshalFromClrDictionary(IDictionary<string, string> instance)
		{
			if (instance == null)
			{
				return ValueMarshaller.MarshalFromClr(instance);
			}
			RecordBuilder recordBuilder = new RecordBuilder(instance.Count);
			foreach (KeyValuePair<string, string> keyValuePair in instance)
			{
				recordBuilder.Add(keyValuePair.Key, TextValue.NewOrNull(keyValuePair.Value), (keyValuePair.Value == null) ? TypeValue.Null : TypeValue.Text);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x06009367 RID: 37735 RVA: 0x001E76A8 File Offset: 0x001E58A8
		public static object MarshalToClr(Value value)
		{
			return ValueMarshaller.MarshalToClr(value, TypeValue.Any);
		}

		// Token: 0x06009368 RID: 37736 RVA: 0x001E76B8 File Offset: 0x001E58B8
		public static object MarshalToClr(Value value, TypeValue intendedType)
		{
			if (value == null)
			{
				return null;
			}
			switch (value.Kind)
			{
			case ValueKind.Null:
				return null;
			case ValueKind.Time:
				return value.AsTime.AsClrTimeSpan;
			case ValueKind.Date:
				return value.AsDate.AsClrDateTime;
			case ValueKind.DateTime:
				return value.AsDateTime.AsClrDateTime;
			case ValueKind.DateTimeZone:
				return value.AsDateTimeZone.AsClrDateTimeOffset;
			case ValueKind.Duration:
				return value.AsDuration.AsTimeSpan;
			case ValueKind.Number:
			{
				NumberValue asNumber = value.AsNumber;
				if (intendedType != null)
				{
					if (intendedType.Equals(TypeValue.Byte))
					{
						return (byte)asNumber.AsInteger32;
					}
					if (intendedType.Equals(TypeValue.Int8))
					{
						return (sbyte)asNumber.AsInteger32;
					}
					if (intendedType.Equals(TypeValue.Int16))
					{
						return (short)asNumber.AsInteger32;
					}
					if (intendedType.Equals(TypeValue.Int32))
					{
						return asNumber.AsInteger32;
					}
					if (intendedType.Equals(TypeValue.Int64))
					{
						return asNumber.AsInteger64;
					}
					if (intendedType.Equals(TypeValue.Single))
					{
						return (float)asNumber.AsDouble;
					}
					if (intendedType.Equals(TypeValue.Decimal))
					{
						return asNumber.AsDecimal;
					}
					if (intendedType.Equals(TypeValue.Currency))
					{
						return asNumber.AsDecimal;
					}
					if (intendedType.Equals(TypeValue.Percentage))
					{
						return asNumber.AsDecimal;
					}
				}
				return asNumber.AsDouble;
			}
			case ValueKind.Logical:
				return value.AsBoolean;
			case ValueKind.Text:
				return value.AsString;
			case ValueKind.Binary:
				return value.AsBinary.AsBytes;
			default:
				throw ValueException.NewExpressionError<Message0>(Strings.ValueMarshaller_CannotMarshalToClr, value, null);
			}
		}

		// Token: 0x06009369 RID: 37737 RVA: 0x001E7884 File Offset: 0x001E5A84
		public static Dictionary<string, object> MarshalToClrDictionary(RecordValue record)
		{
			return (Dictionary<string, object>)ValueMarshaller.ToClrVisitor.ToClr(record);
		}

		// Token: 0x0600936A RID: 37738 RVA: 0x001E7891 File Offset: 0x001E5A91
		public static string ToOleDbString(string s, Value value, Type type)
		{
			if (type == typeof(string) || type == typeof(object))
			{
				return s;
			}
			throw ValueMarshaller.CreateTypeError(value, type);
		}

		// Token: 0x0600936B RID: 37739 RVA: 0x001E78C0 File Offset: 0x001E5AC0
		public static ValueException CreateTypeError(Value value, Type intendedType)
		{
			return ValueException.CastTypeMismatch(value, ValueMarshaller.GetMType(intendedType));
		}

		// Token: 0x0600936C RID: 37740 RVA: 0x001E78D0 File Offset: 0x001E5AD0
		public static Func<IDataReader, int, Value> GetRetrievalFunction(Type type, TypeValue intendedType)
		{
			string fullName = type.FullName;
			if (fullName != null)
			{
				switch (fullName.Length)
				{
				case 11:
				{
					char c = fullName[7];
					if (c != 'B')
					{
						if (c == 'G')
						{
							if (fullName == "System.Guid")
							{
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveGuidAsText);
							}
						}
					}
					else if (fullName == "System.Byte")
					{
						return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveByteAsNumber);
					}
					break;
				}
				case 12:
				{
					char c = fullName[10];
					if (c <= '3')
					{
						if (c != '1')
						{
							if (c == '3')
							{
								if (fullName == "System.Int32")
								{
									if (intendedType.TypeKind != ValueKind.Logical)
									{
										return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveInt32AsNumber);
									}
									return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveInt32AsLogical);
								}
							}
						}
						else if (fullName == "System.Int16")
						{
							return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveInt16AsNumber);
						}
					}
					else if (c != '6')
					{
						if (c == 't')
						{
							if (fullName == "System.SByte")
							{
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveSByteAsNumber);
							}
						}
					}
					else if (fullName == "System.Int64")
					{
						if (intendedType.TypeKind == ValueKind.Duration)
						{
							return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveTimeSpanTicksAsDuration);
						}
						return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveInt64AsNumber);
					}
					break;
				}
				case 13:
				{
					char c = fullName[11];
					if (c <= '6')
					{
						if (c != '1')
						{
							if (c != '3')
							{
								if (c == '6')
								{
									if (fullName == "System.UInt64")
									{
										return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveUInt64AsNumber);
									}
								}
							}
							else if (fullName == "System.UInt32")
							{
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveUInt32AsNumber);
							}
						}
						else if (fullName == "System.UInt16")
						{
							return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveUInt16AsNumber);
						}
					}
					else if (c != '[')
					{
						if (c != 'l')
						{
							if (c == 'n')
							{
								if (fullName == "System.String")
								{
									return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveStringAsText);
								}
							}
						}
						else
						{
							if (fullName == "System.Single")
							{
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveSingleAsNumber);
							}
							if (fullName == "System.Double")
							{
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveDoubleAsNumber);
							}
						}
					}
					else if (fullName == "System.Byte[]")
					{
						return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveByteArrayAsBinary);
					}
					break;
				}
				case 14:
				{
					char c = fullName[7];
					if (c != 'B')
					{
						if (c == 'D')
						{
							if (fullName == "System.Decimal")
							{
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveDecimalAsNumber);
							}
						}
					}
					else if (fullName == "System.Boolean")
					{
						return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveBooleanAsLogical);
					}
					break;
				}
				case 15:
				{
					char c = fullName[7];
					if (c != 'D')
					{
						if (c == 'T')
						{
							if (fullName == "System.TimeSpan")
							{
								if (intendedType.TypeKind != ValueKind.Duration)
								{
									return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveTimeSpanAsTime);
								}
								return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveTimeSpanAsDuration);
							}
						}
					}
					else if (fullName == "System.DateTime")
					{
						ValueKind typeKind = intendedType.TypeKind;
						if (typeKind == ValueKind.Time)
						{
							return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveDateTimeAsTime);
						}
						if (typeKind == ValueKind.Date)
						{
							return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveDateTimeAsDate);
						}
						return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveDateTimeAsDateTime);
					}
					break;
				}
				case 21:
					if (fullName == "System.DateTimeOffset")
					{
						return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveDateTimeOffsetAsDateTimeZone);
					}
					break;
				}
			}
			return new Func<IDataReader, int, Value>(ValueMarshaller.RetrieveObjectAsValue);
		}

		// Token: 0x0600936D RID: 37741 RVA: 0x001E7CD8 File Offset: 0x001E5ED8
		public static TypeValue GetMType(Type clrType)
		{
			string fullName = clrType.FullName;
			if (fullName != null)
			{
				switch (fullName.Length)
				{
				case 11:
				{
					char c = fullName[7];
					if (c != 'B')
					{
						if (c != 'G')
						{
							goto IL_0381;
						}
						if (!(fullName == "System.Guid"))
						{
							goto IL_0381;
						}
						goto IL_0363;
					}
					else
					{
						if (!(fullName == "System.Byte"))
						{
							goto IL_0381;
						}
						return TypeValue.Byte;
					}
					break;
				}
				case 12:
				{
					char c = fullName[10];
					if (c <= '3')
					{
						if (c != '1')
						{
							if (c != '3')
							{
								goto IL_0381;
							}
							if (!(fullName == "System.Int32"))
							{
								goto IL_0381;
							}
						}
						else
						{
							if (!(fullName == "System.Int16"))
							{
								goto IL_0381;
							}
							return TypeValue.Int16;
						}
					}
					else if (c != '6')
					{
						if (c != 't')
						{
							goto IL_0381;
						}
						if (!(fullName == "System.SByte"))
						{
							goto IL_0381;
						}
						return TypeValue.Int8;
					}
					else
					{
						if (!(fullName == "System.Int64"))
						{
							goto IL_0381;
						}
						goto IL_0339;
					}
					break;
				}
				case 13:
				{
					char c = fullName[11];
					if (c <= '6')
					{
						if (c != '1')
						{
							if (c != '3')
							{
								if (c != '6')
								{
									goto IL_0381;
								}
								if (!(fullName == "System.UInt64"))
								{
									goto IL_0381;
								}
								return TypeValue.Decimal;
							}
							else
							{
								if (!(fullName == "System.UInt32"))
								{
									goto IL_0381;
								}
								goto IL_0339;
							}
						}
						else if (!(fullName == "System.UInt16"))
						{
							goto IL_0381;
						}
					}
					else if (c != '[')
					{
						if (c != 'l')
						{
							if (c != 'n')
							{
								goto IL_0381;
							}
							if (!(fullName == "System.String"))
							{
								goto IL_0381;
							}
							goto IL_0363;
						}
						else
						{
							if (fullName == "System.Single")
							{
								return TypeValue.Single;
							}
							if (!(fullName == "System.Double"))
							{
								goto IL_0381;
							}
							return TypeValue.Double;
						}
					}
					else
					{
						if (!(fullName == "System.Byte[]"))
						{
							goto IL_0381;
						}
						return TypeValue.Binary;
					}
					break;
				}
				case 14:
				{
					char c = fullName[7];
					if (c != 'B')
					{
						if (c != 'D')
						{
							goto IL_0381;
						}
						if (!(fullName == "System.Decimal"))
						{
							goto IL_0381;
						}
						return TypeValue.Decimal;
					}
					else
					{
						if (!(fullName == "System.Boolean"))
						{
							goto IL_0381;
						}
						return TypeValue.Logical;
					}
					break;
				}
				case 15:
				{
					char c = fullName[7];
					if (c != 'D')
					{
						if (c != 'T')
						{
							goto IL_0381;
						}
						if (!(fullName == "System.TimeSpan"))
						{
							goto IL_0381;
						}
						return TypeValue.Time;
					}
					else
					{
						if (!(fullName == "System.DateTime"))
						{
							goto IL_0381;
						}
						return TypeValue.DateTime;
					}
					break;
				}
				case 16:
				case 17:
				case 18:
				case 19:
				case 23:
					goto IL_0381;
				case 20:
				{
					char c = fullName[16];
					if (c != 'D')
					{
						if (c != 'T')
						{
							goto IL_0381;
						}
						if (!(fullName == "Microsoft.OleDb.Time"))
						{
							goto IL_0381;
						}
						return TypeValue.Time;
					}
					else
					{
						if (!(fullName == "Microsoft.OleDb.Date"))
						{
							goto IL_0381;
						}
						return TypeValue.Date;
					}
					break;
				}
				case 21:
					if (!(fullName == "System.DateTimeOffset"))
					{
						goto IL_0381;
					}
					return TypeValue.DateTimeZone;
				case 22:
					if (!(fullName == "Microsoft.OleDb.Number"))
					{
						goto IL_0381;
					}
					return TypeValue.Decimal;
				case 24:
					if (!(fullName == "Microsoft.OleDb.Currency"))
					{
						goto IL_0381;
					}
					return TypeValue.Currency;
				default:
					goto IL_0381;
				}
				return TypeValue.Int32;
				IL_0339:
				return TypeValue.Int64;
				IL_0363:
				return TypeValue.Text;
			}
			IL_0381:
			return TypeValue.Any;
		}

		// Token: 0x0600936E RID: 37742 RVA: 0x001E806B File Offset: 0x001E626B
		public static Value RetrieveObjectAsValue(IDataReader reader, int index)
		{
			return ValueMarshaller.MarshalFromClr(reader[index]);
		}

		// Token: 0x0600936F RID: 37743 RVA: 0x001E8079 File Offset: 0x001E6279
		private static Value RetrieveSingleAsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((double)reader.GetFloat(index));
		}

		// Token: 0x06009370 RID: 37744 RVA: 0x001E8088 File Offset: 0x001E6288
		private static Value RetrieveDoubleAsNumber(IDataReader reader, int index)
		{
			return NumberValue.New(reader.GetDouble(index));
		}

		// Token: 0x06009371 RID: 37745 RVA: 0x001E8098 File Offset: 0x001E6298
		private static Value RetrieveDecimalAsNumber(IDataReader reader, int index)
		{
			object obj = reader[index];
			if (obj is decimal)
			{
				return NumberValue.New((decimal)obj);
			}
			return NumberValue.New((double)obj);
		}

		// Token: 0x06009372 RID: 37746 RVA: 0x001E80CC File Offset: 0x001E62CC
		private static Value RetrieveSByteAsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((int)((sbyte)reader[index]));
		}

		// Token: 0x06009373 RID: 37747 RVA: 0x001E80DF File Offset: 0x001E62DF
		private static Value RetrieveByteAsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((int)reader.GetByte(index));
		}

		// Token: 0x06009374 RID: 37748 RVA: 0x001E80ED File Offset: 0x001E62ED
		private static Value RetrieveInt16AsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((int)reader.GetInt16(index));
		}

		// Token: 0x06009375 RID: 37749 RVA: 0x001E80FB File Offset: 0x001E62FB
		private static Value RetrieveUInt16AsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((int)((ushort)reader[index]));
		}

		// Token: 0x06009376 RID: 37750 RVA: 0x001E810E File Offset: 0x001E630E
		private static Value RetrieveInt32AsNumber(IDataReader reader, int index)
		{
			return NumberValue.New(reader.GetInt32(index));
		}

		// Token: 0x06009377 RID: 37751 RVA: 0x001E811C File Offset: 0x001E631C
		private static Value RetrieveUInt32AsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((long)((ulong)((uint)reader[index])));
		}

		// Token: 0x06009378 RID: 37752 RVA: 0x001E8130 File Offset: 0x001E6330
		private static Value RetrieveInt64AsNumber(IDataReader reader, int index)
		{
			return NumberValue.New(reader.GetInt64(index));
		}

		// Token: 0x06009379 RID: 37753 RVA: 0x001E813E File Offset: 0x001E633E
		private static Value RetrieveUInt64AsNumber(IDataReader reader, int index)
		{
			return NumberValue.New((ulong)reader[index]);
		}

		// Token: 0x0600937A RID: 37754 RVA: 0x001E8151 File Offset: 0x001E6351
		private static Value RetrieveBooleanAsLogical(IDataReader reader, int index)
		{
			return LogicalValue.New(reader.GetBoolean(index));
		}

		// Token: 0x0600937B RID: 37755 RVA: 0x001E815F File Offset: 0x001E635F
		private static Value RetrieveInt32AsLogical(IDataReader reader, int index)
		{
			return LogicalValue.New(reader.GetInt32(index) != 0);
		}

		// Token: 0x0600937C RID: 37756 RVA: 0x001E8170 File Offset: 0x001E6370
		private static Value RetrieveTimeSpanAsTime(IDataReader reader, int index)
		{
			return TimeValue.New((TimeSpan)reader[index]);
		}

		// Token: 0x0600937D RID: 37757 RVA: 0x001E8183 File Offset: 0x001E6383
		private static Value RetrieveTimeSpanTicksAsDuration(IDataReader reader, int index)
		{
			return DurationValue.New((long)reader[index]);
		}

		// Token: 0x0600937E RID: 37758 RVA: 0x001E8196 File Offset: 0x001E6396
		private static Value RetrieveTimeSpanAsDuration(IDataReader reader, int index)
		{
			return DurationValue.New((TimeSpan)reader[index]);
		}

		// Token: 0x0600937F RID: 37759 RVA: 0x001E81AC File Offset: 0x001E63AC
		private static Value RetrieveDateTimeAsTime(IDataReader reader, int index)
		{
			return TimeValue.New(reader.GetDateTime(index).TimeOfDay);
		}

		// Token: 0x06009380 RID: 37760 RVA: 0x001E81CD File Offset: 0x001E63CD
		private static Value RetrieveDateTimeAsDate(IDataReader reader, int index)
		{
			return DateValue.New(reader.GetDateTime(index));
		}

		// Token: 0x06009381 RID: 37761 RVA: 0x001E81DB File Offset: 0x001E63DB
		private static Value RetrieveDateTimeAsDateTime(IDataReader reader, int index)
		{
			return DateTimeValue.New(reader.GetDateTime(index));
		}

		// Token: 0x06009382 RID: 37762 RVA: 0x001E81E9 File Offset: 0x001E63E9
		private static Value RetrieveDateTimeOffsetAsDateTimeZone(IDataReader reader, int index)
		{
			return DateTimeZoneValue.New((DateTimeOffset)reader[index]);
		}

		// Token: 0x06009383 RID: 37763 RVA: 0x001E81FC File Offset: 0x001E63FC
		private static Value RetrieveGuidAsText(IDataReader reader, int index)
		{
			return TextValue.New(reader.GetGuid(index).ToString());
		}

		// Token: 0x06009384 RID: 37764 RVA: 0x001E8223 File Offset: 0x001E6423
		private static Value RetrieveStringAsText(IDataReader reader, int index)
		{
			return TextValue.New(reader.GetString(index));
		}

		// Token: 0x06009385 RID: 37765 RVA: 0x001E8231 File Offset: 0x001E6431
		private static Value RetrieveByteArrayAsBinary(IDataReader reader, int index)
		{
			return BinaryValue.New((byte[])reader[index]);
		}

		// Token: 0x020016A9 RID: 5801
		private sealed class ToClrVisitor : ValueVisitor
		{
			// Token: 0x06009386 RID: 37766 RVA: 0x001E8244 File Offset: 0x001E6444
			public static object ToClr(Value value)
			{
				ValueMarshaller.ToClrVisitor toClrVisitor = new ValueMarshaller.ToClrVisitor();
				toClrVisitor.VisitValue(value);
				return toClrVisitor.value;
			}

			// Token: 0x06009387 RID: 37767 RVA: 0x001E8257 File Offset: 0x001E6457
			protected override void VisitPrimitiveValue(Value value)
			{
				this.value = ValueMarshaller.MarshalToClr(value);
			}

			// Token: 0x06009388 RID: 37768 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override void VisitAction(ActionValue action)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06009389 RID: 37769 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override void VisitFunction(FunctionValue function)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600938A RID: 37770 RVA: 0x001E8265 File Offset: 0x001E6465
			protected override void VisitList(ListValue list)
			{
				this.value = new List<object>();
				base.VisitList(list);
			}

			// Token: 0x0600938B RID: 37771 RVA: 0x001E827C File Offset: 0x001E647C
			protected override void VisitListItem(int index, Value value)
			{
				List<object> list = (List<object>)this.value;
				this.VisitValue(value);
				list.Add(this.value);
				this.value = list;
			}

			// Token: 0x0600938C RID: 37772 RVA: 0x0000336E File Offset: 0x0000156E
			protected override void VisitMetaValue(RecordValue metaValue)
			{
			}

			// Token: 0x0600938D RID: 37773 RVA: 0x001E82AF File Offset: 0x001E64AF
			protected override void VisitRecord(RecordValue record)
			{
				this.value = new Dictionary<string, object>();
				base.VisitRecord(record);
			}

			// Token: 0x0600938E RID: 37774 RVA: 0x001E82C4 File Offset: 0x001E64C4
			protected override void VisitRecordField(string name, Value value)
			{
				Dictionary<string, object> dictionary = (Dictionary<string, object>)this.value;
				this.VisitValue(value);
				dictionary[name] = this.value;
				this.value = dictionary;
			}

			// Token: 0x0600938F RID: 37775 RVA: 0x001E82F8 File Offset: 0x001E64F8
			protected override void VisitTable(TableValue table)
			{
				this.value = new List<object>();
				base.VisitTable(table);
			}

			// Token: 0x06009390 RID: 37776 RVA: 0x001E830C File Offset: 0x001E650C
			protected override void VisitTableRow(int rowNumber, RecordValue row)
			{
				List<object> list = (List<object>)this.value;
				this.VisitRecord(row);
				list.Add(this.value);
				this.value = list;
			}

			// Token: 0x06009391 RID: 37777 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override void VisitType(TypeValue type)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04004EC6 RID: 20166
			private object value;
		}
	}
}

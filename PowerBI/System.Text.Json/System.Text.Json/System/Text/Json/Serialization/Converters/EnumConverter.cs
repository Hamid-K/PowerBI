using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F6 RID: 246
	internal sealed class EnumConverter<T> : JsonPrimitiveConverter<T> where T : struct, Enum
	{
		// Token: 0x06000CA5 RID: 3237 RVA: 0x00032144 File Offset: 0x00030344
		public override bool CanConvert(Type type)
		{
			return type.IsEnum;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0003214C File Offset: 0x0003034C
		public EnumConverter(EnumConverterOptions converterOptions, JsonSerializerOptions serializerOptions)
			: this(converterOptions, null, serializerOptions)
		{
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00032158 File Offset: 0x00030358
		public EnumConverter(EnumConverterOptions converterOptions, JsonNamingPolicy namingPolicy, JsonSerializerOptions serializerOptions)
		{
			this._converterOptions = converterOptions;
			this._namingPolicy = namingPolicy;
			this._nameCacheForWriting = new ConcurrentDictionary<ulong, JsonEncodedText>();
			if (namingPolicy != null)
			{
				this._nameCacheForReading = new ConcurrentDictionary<string, T>();
			}
			string[] names = Enum.GetNames(this.Type);
			Array values = Enum.GetValues(this.Type);
			JavaScriptEncoder encoder = serializerOptions.Encoder;
			for (int i = 0; i < names.Length; i++)
			{
				T t = (T)((object)values.GetValue(i));
				ulong num = EnumConverter<T>.ConvertToUInt64(t);
				string text = names[i];
				string text2 = EnumConverter<T>.FormatJsonName(text, namingPolicy);
				this._nameCacheForWriting.TryAdd(num, JsonEncodedText.Encode(text2, encoder));
				ConcurrentDictionary<string, T> nameCacheForReading = this._nameCacheForReading;
				if (nameCacheForReading != null)
				{
					nameCacheForReading.TryAdd(text2, t);
				}
				if (text.AsSpan().IndexOfAny(',', ' ') >= 0)
				{
					ThrowHelper.ThrowInvalidOperationException_InvalidEnumTypeWithSpecialChar(typeof(T), text);
				}
			}
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00032240 File Offset: 0x00030440
		public unsafe override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			JsonTokenType tokenType = reader.TokenType;
			if (tokenType == JsonTokenType.String)
			{
				if ((this._converterOptions & EnumConverterOptions.AllowStrings) == (EnumConverterOptions)0)
				{
					ThrowHelper.ThrowJsonException(null);
					return default(T);
				}
				string @string = reader.GetString();
				T t;
				if (EnumConverter<T>.TryParseEnumCore(@string, options, out t))
				{
					return t;
				}
				return this.ReadEnumUsingNamingPolicy(@string);
			}
			else
			{
				if (tokenType != JsonTokenType.Number || (this._converterOptions & EnumConverterOptions.AllowNumbers) == (EnumConverterOptions)0)
				{
					ThrowHelper.ThrowJsonException(null);
					return default(T);
				}
				switch (EnumConverter<T>.s_enumTypeCode)
				{
				case TypeCode.SByte:
				{
					sbyte b;
					if (reader.TryGetSByte(out b))
					{
						return *Unsafe.As<sbyte, T>(ref b);
					}
					break;
				}
				case TypeCode.Byte:
				{
					byte b2;
					if (reader.TryGetByte(out b2))
					{
						return *Unsafe.As<byte, T>(ref b2);
					}
					break;
				}
				case TypeCode.Int16:
				{
					short num;
					if (reader.TryGetInt16(out num))
					{
						return *Unsafe.As<short, T>(ref num);
					}
					break;
				}
				case TypeCode.UInt16:
				{
					ushort num2;
					if (reader.TryGetUInt16(out num2))
					{
						return *Unsafe.As<ushort, T>(ref num2);
					}
					break;
				}
				case TypeCode.Int32:
				{
					int num3;
					if (reader.TryGetInt32(out num3))
					{
						return *Unsafe.As<int, T>(ref num3);
					}
					break;
				}
				case TypeCode.UInt32:
				{
					uint num4;
					if (reader.TryGetUInt32(out num4))
					{
						return *Unsafe.As<uint, T>(ref num4);
					}
					break;
				}
				case TypeCode.Int64:
				{
					long num5;
					if (reader.TryGetInt64(out num5))
					{
						return *Unsafe.As<long, T>(ref num5);
					}
					break;
				}
				case TypeCode.UInt64:
				{
					ulong num6;
					if (reader.TryGetUInt64(out num6))
					{
						return *Unsafe.As<ulong, T>(ref num6);
					}
					break;
				}
				}
				ThrowHelper.ThrowJsonException(null);
				return default(T);
			}
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000323B4 File Offset: 0x000305B4
		public unsafe override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			if ((this._converterOptions & EnumConverterOptions.AllowStrings) != (EnumConverterOptions)0)
			{
				ulong num = EnumConverter<T>.ConvertToUInt64(value);
				JsonEncodedText jsonEncodedText;
				if (this._nameCacheForWriting.TryGetValue(num, out jsonEncodedText))
				{
					writer.WriteStringValue(jsonEncodedText);
					return;
				}
				string text = value.ToString();
				if (EnumConverter<T>.IsValidIdentifier(text))
				{
					text = EnumConverter<T>.FormatJsonName(text, this._namingPolicy);
					if (this._nameCacheForWriting.Count < 64)
					{
						jsonEncodedText = JsonEncodedText.Encode(text, options.Encoder);
						writer.WriteStringValue(jsonEncodedText);
						this._nameCacheForWriting.TryAdd(num, jsonEncodedText);
						return;
					}
					writer.WriteStringValue(text);
					return;
				}
			}
			if ((this._converterOptions & EnumConverterOptions.AllowNumbers) == (EnumConverterOptions)0)
			{
				ThrowHelper.ThrowJsonException(null);
			}
			switch (EnumConverter<T>.s_enumTypeCode)
			{
			case TypeCode.SByte:
				writer.WriteNumberValue((int)(*Unsafe.As<T, sbyte>(ref value)));
				return;
			case TypeCode.Byte:
				writer.WriteNumberValue((int)(*Unsafe.As<T, byte>(ref value)));
				return;
			case TypeCode.Int16:
				writer.WriteNumberValue((int)(*Unsafe.As<T, short>(ref value)));
				return;
			case TypeCode.UInt16:
				writer.WriteNumberValue((int)(*Unsafe.As<T, ushort>(ref value)));
				return;
			case TypeCode.Int32:
				writer.WriteNumberValue(*Unsafe.As<T, int>(ref value));
				return;
			case TypeCode.UInt32:
				writer.WriteNumberValue(*Unsafe.As<T, uint>(ref value));
				return;
			case TypeCode.Int64:
				writer.WriteNumberValue(*Unsafe.As<T, long>(ref value));
				return;
			case TypeCode.UInt64:
				writer.WriteNumberValue(*Unsafe.As<T, ulong>(ref value));
				return;
			default:
				ThrowHelper.ThrowJsonException(null);
				return;
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00032510 File Offset: 0x00030710
		internal override T ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			T t;
			if (!EnumConverter<T>.TryParseEnumCore(reader.GetString(), options, out t))
			{
				ThrowHelper.ThrowJsonException(null);
			}
			return t;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00032538 File Offset: 0x00030738
		internal unsafe override void WriteAsPropertyNameCore(Utf8JsonWriter writer, T value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			ulong num = EnumConverter<T>.ConvertToUInt64(value);
			JsonEncodedText jsonEncodedText;
			if (options.DictionaryKeyPolicy == null && this._nameCacheForWriting.TryGetValue(num, out jsonEncodedText))
			{
				writer.WritePropertyName(jsonEncodedText);
				return;
			}
			string text = value.ToString();
			if (EnumConverter<T>.IsValidIdentifier(text))
			{
				if (options.DictionaryKeyPolicy != null)
				{
					text = EnumConverter<T>.FormatJsonName(text, options.DictionaryKeyPolicy);
					writer.WritePropertyName(text);
					return;
				}
				text = EnumConverter<T>.FormatJsonName(text, this._namingPolicy);
				if (this._nameCacheForWriting.Count < 64)
				{
					jsonEncodedText = JsonEncodedText.Encode(text, options.Encoder);
					writer.WritePropertyName(jsonEncodedText);
					this._nameCacheForWriting.TryAdd(num, jsonEncodedText);
					return;
				}
				writer.WritePropertyName(text);
				return;
			}
			else
			{
				switch (EnumConverter<T>.s_enumTypeCode)
				{
				case TypeCode.SByte:
					writer.WritePropertyName((int)(*(sbyte*)(&value)));
					return;
				case TypeCode.Byte:
					writer.WritePropertyName((int)(*(byte*)(&value)));
					return;
				case TypeCode.Int16:
					writer.WritePropertyName((int)(*(short*)(&value)));
					return;
				case TypeCode.UInt16:
					writer.WritePropertyName((int)(*(ushort*)(&value)));
					return;
				case TypeCode.Int32:
					writer.WritePropertyName(*(int*)(&value));
					return;
				case TypeCode.UInt32:
					writer.WritePropertyName(*(uint*)(&value));
					return;
				case TypeCode.Int64:
					writer.WritePropertyName(*(long*)(&value));
					return;
				case TypeCode.UInt64:
					writer.WritePropertyName((ulong)(*(long*)(&value)));
					return;
				default:
					ThrowHelper.ThrowJsonException(null);
					return;
				}
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0003267C File Offset: 0x0003087C
		private static bool TryParseEnumCore(string enumString, JsonSerializerOptions _, out T value)
		{
			T t;
			bool flag = Enum.TryParse<T>(enumString, out t) || Enum.TryParse<T>(enumString, true, out t);
			value = t;
			return flag;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000326A8 File Offset: 0x000308A8
		private T ReadEnumUsingNamingPolicy(string enumString)
		{
			if (this._namingPolicy == null)
			{
				ThrowHelper.ThrowJsonException(null);
			}
			if (enumString == null)
			{
				ThrowHelper.ThrowJsonException(null);
			}
			T t;
			bool flag;
			if (!(flag = this._nameCacheForReading.TryGetValue(enumString, out t)) && enumString.Contains(", "))
			{
				string[] array = EnumConverter<T>.SplitFlagsEnum(enumString);
				ulong num = 0UL;
				for (int i = 0; i < array.Length; i++)
				{
					flag = this._nameCacheForReading.TryGetValue(array[i], out t);
					if (!flag)
					{
						break;
					}
					num |= EnumConverter<T>.ConvertToUInt64(t);
				}
				t = (T)((object)Enum.ToObject(typeof(T), num));
				if (flag && this._nameCacheForReading.Count < 64)
				{
					this._nameCacheForReading[enumString] = t;
				}
			}
			if (!flag)
			{
				ThrowHelper.ThrowJsonException(null);
			}
			return t;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0003276C File Offset: 0x0003096C
		private static ulong ConvertToUInt64(object value)
		{
			ulong num;
			switch (EnumConverter<T>.s_enumTypeCode)
			{
			case TypeCode.SByte:
				num = (ulong)((long)((sbyte)value));
				break;
			case TypeCode.Byte:
				num = (ulong)((byte)value);
				break;
			case TypeCode.Int16:
				num = (ulong)((long)((short)value));
				break;
			case TypeCode.UInt16:
				num = (ulong)((ushort)value);
				break;
			case TypeCode.Int32:
				num = (ulong)((long)((int)value));
				break;
			case TypeCode.UInt32:
				num = (ulong)((uint)value);
				break;
			case TypeCode.Int64:
				num = (ulong)((long)value);
				break;
			case TypeCode.UInt64:
				num = (ulong)value;
				break;
			default:
				throw new InvalidOperationException();
			}
			return num;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00032800 File Offset: 0x00030A00
		private static bool IsValidIdentifier(string value)
		{
			return value[0] >= 'A' && (!EnumConverter<T>.s_isSignedEnum || !value.StartsWith(NumberFormatInfo.CurrentInfo.NegativeSign));
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003282C File Offset: 0x00030A2C
		private static string FormatJsonName(string value, JsonNamingPolicy namingPolicy)
		{
			if (namingPolicy == null)
			{
				return value;
			}
			string text;
			if (!value.Contains(", "))
			{
				text = namingPolicy.ConvertName(value);
				if (text == null)
				{
					ThrowHelper.ThrowInvalidOperationException_NamingPolicyReturnNull(namingPolicy);
				}
			}
			else
			{
				string[] array = EnumConverter<T>.SplitFlagsEnum(value);
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = namingPolicy.ConvertName(array[i]);
					if (text2 == null)
					{
						ThrowHelper.ThrowInvalidOperationException_NamingPolicyReturnNull(namingPolicy);
					}
					array[i] = text2;
				}
				text = string.Join(", ", array);
			}
			return text;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00032897 File Offset: 0x00030A97
		private static string[] SplitFlagsEnum(string value)
		{
			return value.Split(new string[] { ", " }, StringSplitOptions.None);
		}

		// Token: 0x0400040B RID: 1035
		private static readonly TypeCode s_enumTypeCode = Type.GetTypeCode(typeof(T));

		// Token: 0x0400040C RID: 1036
		private static readonly bool s_isSignedEnum = EnumConverter<T>.s_enumTypeCode % TypeCode.DBNull == TypeCode.Object;

		// Token: 0x0400040D RID: 1037
		private const string ValueSeparator = ", ";

		// Token: 0x0400040E RID: 1038
		private readonly EnumConverterOptions _converterOptions;

		// Token: 0x0400040F RID: 1039
		private readonly JsonNamingPolicy _namingPolicy;

		// Token: 0x04000410 RID: 1040
		private readonly ConcurrentDictionary<ulong, JsonEncodedText> _nameCacheForWriting;

		// Token: 0x04000411 RID: 1041
		private readonly ConcurrentDictionary<string, T> _nameCacheForReading;

		// Token: 0x04000412 RID: 1042
		private const int NameCacheSizeSoftLimit = 64;
	}
}

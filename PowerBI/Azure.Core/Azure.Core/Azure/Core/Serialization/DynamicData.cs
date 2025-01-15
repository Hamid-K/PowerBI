using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core.Json;

namespace Azure.Core.Serialization
{
	// Token: 0x020000C1 RID: 193
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	[RequiresUnreferencedCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[RequiresDynamicCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[JsonConverter(typeof(DynamicData.DynamicDataJsonConverter))]
	public sealed class DynamicData : IDisposable, IDynamicMetaObjectProvider
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x00014B9C File Offset: 0x00012D9C
		internal DynamicData(MutableJsonElement element, DynamicDataOptions options)
		{
			this._element = element;
			this._options = options;
			this._serializerOptions = DynamicDataOptions.ToSerializerOptions(options);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00014BC0 File Offset: 0x00012DC0
		internal void WriteTo(Stream stream)
		{
			using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(stream, default(JsonWriterOptions)))
			{
				this._element.WriteTo(utf8JsonWriter);
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00014C08 File Offset: 0x00012E08
		[return: Nullable(2)]
		private object GetProperty(string name)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			JsonValueKind? jsonValueKind = this._element.ValueKind;
			JsonValueKind jsonValueKind2 = 2;
			if (((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && name == "Length")
			{
				return this._element.GetArrayLength();
			}
			MutableJsonElement mutableJsonElement;
			if (this._element.TryGetProperty(name, out mutableJsonElement))
			{
				jsonValueKind = mutableJsonElement.ValueKind;
				jsonValueKind2 = 7;
				if ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null))
				{
					return null;
				}
				return new DynamicData(mutableJsonElement, this._options);
			}
			else
			{
				if (this._options.PropertyNameFormat == JsonPropertyNames.UseExact || !this._element.TryGetProperty(this.FormatPropertyName(name), out mutableJsonElement))
				{
					return null;
				}
				jsonValueKind = mutableJsonElement.ValueKind;
				jsonValueKind2 = 7;
				if ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null))
				{
					return null;
				}
				return new DynamicData(mutableJsonElement, this._options);
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00014CF0 File Offset: 0x00012EF0
		private string FormatPropertyName(string value)
		{
			JsonPropertyNames propertyNameFormat = this._options.PropertyNameFormat;
			string text;
			if (propertyNameFormat != JsonPropertyNames.UseExact)
			{
				if (propertyNameFormat != JsonPropertyNames.CamelCase)
				{
					throw new NotSupportedException(string.Format("Unknown value for DynamicDataOptions.PropertyNamingConvention: '{0}'.", this._options.PropertyNameFormat));
				}
				text = JsonNamingPolicy.CamelCase.ConvertName(value);
			}
			else
			{
				text = value;
			}
			return text;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00014D48 File Offset: 0x00012F48
		[return: Nullable(2)]
		private object GetViaIndexer(object index)
		{
			string text = index as string;
			if (text == null)
			{
				if (!(index is int))
				{
					throw new InvalidOperationException(string.Format("Tried to access indexer with an unsupported index type: {0}", index));
				}
				int num = (int)index;
				MutableJsonElement indexElement = this._element.GetIndexElement(num);
				JsonValueKind? jsonValueKind = indexElement.ValueKind;
				JsonValueKind jsonValueKind2 = 7;
				if ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null))
				{
					return null;
				}
				return new DynamicData(indexElement, this._options);
			}
			else
			{
				MutableJsonElement mutableJsonElement;
				if (!this._element.TryGetProperty(text, out mutableJsonElement))
				{
					throw new KeyNotFoundException("Could not find JSON member with name '" + text + "'.");
				}
				JsonValueKind? jsonValueKind = mutableJsonElement.ValueKind;
				JsonValueKind jsonValueKind2 = 7;
				if ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null))
				{
					return null;
				}
				return new DynamicData(mutableJsonElement, this._options);
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00014E18 File Offset: 0x00013018
		private IEnumerable GetEnumerable()
		{
			JsonValueKind? valueKind = this._element.ValueKind;
			if (valueKind != null)
			{
				JsonValueKind valueOrDefault = valueKind.GetValueOrDefault();
				IEnumerable enumerable;
				if (valueOrDefault != 1)
				{
					if (valueOrDefault != 2)
					{
						goto IL_0061;
					}
					enumerable = new DynamicData.ArrayEnumerator(this._element.EnumerateArray(), this._options);
				}
				else
				{
					enumerable = new DynamicData.ObjectEnumerator(this._element.EnumerateObject(), this._options);
				}
				return enumerable;
			}
			IL_0061:
			throw new InvalidCastException(string.Format("Unable to enumerate JSON element of kind '{0}'.  Cannot cast value to IEnumerable.", this._element.ValueKind));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00014EA8 File Offset: 0x000130A8
		[return: Nullable(2)]
		private object SetProperty(string name, object value)
		{
			Argument.AssertNotNullOrEmpty(name, "name");
			DynamicData.AllowList.AssertAllowedValue<object>(value);
			if (DynamicData.HasTypeConverter(value))
			{
				value = this.ConvertType(value);
			}
			MutableJsonElement mutableJsonElement;
			if (this._options.PropertyNameFormat == JsonPropertyNames.UseExact || this._element.TryGetProperty(name, out mutableJsonElement))
			{
				this.SetPropertyInternal(name, value);
				return null;
			}
			this.SetPropertyInternal(this.FormatPropertyName(name), value);
			return null;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00014F14 File Offset: 0x00013114
		private static bool HasTypeConverter(object value)
		{
			return value is DateTime || value is DateTimeOffset || value is TimeSpan;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00014F4A File Offset: 0x0001314A
		private JsonElement ConvertType(object value)
		{
			return MutableJsonElement.SerializeToJsonElement(value, this._serializerOptions);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00014F58 File Offset: 0x00013158
		[return: Nullable(2)]
		private object SetViaIndexer(object index, object value)
		{
			DynamicData.AllowList.AssertAllowedValue<object>(value);
			string text = index as string;
			if (text != null)
			{
				this.SetPropertyInternal(text, value);
				return null;
			}
			if (index is int)
			{
				int num = (int)index;
				MutableJsonElement indexElement = this._element.GetIndexElement(num);
				this.SetInternal(ref indexElement, value);
				return new DynamicData(indexElement, this._options);
			}
			throw new InvalidOperationException(string.Format("Tried to access indexer with an unsupported index type: {0}", index));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00014FC4 File Offset: 0x000131C4
		private void SetPropertyInternal(string name, object value)
		{
			if (value is bool)
			{
				bool flag = (bool)value;
				this._element = this._element.SetProperty(name, flag);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				this._element = this._element.SetProperty(name, text);
				return;
			}
			if (value is byte)
			{
				byte b = (byte)value;
				this._element = this._element.SetProperty(name, b);
				return;
			}
			if (value is sbyte)
			{
				sbyte b2 = (sbyte)value;
				this._element = this._element.SetProperty(name, b2);
				return;
			}
			if (value is short)
			{
				short num = (short)value;
				this._element = this._element.SetProperty(name, num);
				return;
			}
			if (value is ushort)
			{
				ushort num2 = (ushort)value;
				this._element = this._element.SetProperty(name, num2);
				return;
			}
			if (value is int)
			{
				int num3 = (int)value;
				this._element = this._element.SetProperty(name, num3);
				return;
			}
			if (value is uint)
			{
				uint num4 = (uint)value;
				this._element = this._element.SetProperty(name, num4);
				return;
			}
			if (value is long)
			{
				long num5 = (long)value;
				this._element = this._element.SetProperty(name, num5);
				return;
			}
			if (value is ulong)
			{
				ulong num6 = (ulong)value;
				this._element = this._element.SetProperty(name, num6);
				return;
			}
			if (value is float)
			{
				float num7 = (float)value;
				this._element = this._element.SetProperty(name, num7);
				return;
			}
			if (value is double)
			{
				double num8 = (double)value;
				this._element = this._element.SetProperty(name, num8);
				return;
			}
			if (value is decimal)
			{
				decimal num9 = (decimal)value;
				this._element = this._element.SetProperty(name, num9);
				return;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				this._element = this._element.SetProperty(name, dateTime);
				return;
			}
			if (value is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				this._element = this._element.SetProperty(name, dateTimeOffset);
				return;
			}
			if (value is Guid)
			{
				Guid guid = (Guid)value;
				this._element = this._element.SetProperty(name, guid);
				return;
			}
			if (value == null)
			{
				this._element = this._element.SetPropertyNull(name);
				return;
			}
			if (value is JsonElement)
			{
				JsonElement jsonElement = (JsonElement)value;
				this._element = this._element.SetProperty(name, jsonElement);
				return;
			}
			JsonElement jsonElement2 = this.ConvertType(value);
			this._element = this._element.SetProperty(name, jsonElement2);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000152C8 File Offset: 0x000134C8
		private void SetInternal(ref MutableJsonElement element, object value)
		{
			if (value is bool)
			{
				bool flag = (bool)value;
				element.Set(flag);
				return;
			}
			string text = value as string;
			if (text != null)
			{
				element.Set(text);
				return;
			}
			if (value is byte)
			{
				byte b = (byte)value;
				element.Set(b);
				return;
			}
			if (value is sbyte)
			{
				sbyte b2 = (sbyte)value;
				element.Set(b2);
				return;
			}
			if (value is short)
			{
				short num = (short)value;
				element.Set(num);
				return;
			}
			if (value is ushort)
			{
				ushort num2 = (ushort)value;
				element.Set(num2);
				return;
			}
			if (value is int)
			{
				int num3 = (int)value;
				element.Set(num3);
				return;
			}
			if (value is uint)
			{
				uint num4 = (uint)value;
				element.Set(num4);
				return;
			}
			if (value is long)
			{
				long num5 = (long)value;
				element.Set(num5);
				return;
			}
			if (value is ulong)
			{
				ulong num6 = (ulong)value;
				element.Set(num6);
				return;
			}
			if (value is float)
			{
				float num7 = (float)value;
				element.Set(num7);
				return;
			}
			if (value is double)
			{
				double num8 = (double)value;
				element.Set(num8);
				return;
			}
			if (value is decimal)
			{
				decimal num9 = (decimal)value;
				element.Set(num9);
				return;
			}
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				element.Set(dateTime);
				return;
			}
			if (value is DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				element.Set(dateTimeOffset);
				return;
			}
			if (value is Guid)
			{
				Guid guid = (Guid)value;
				element.Set(guid);
				return;
			}
			if (value == null)
			{
				element.SetNull();
				return;
			}
			if (value is JsonElement)
			{
				JsonElement jsonElement = (JsonElement)value;
				element.Set(jsonElement);
				return;
			}
			JsonElement jsonElement2 = this.ConvertType(value);
			element.Set(jsonElement2);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000154E8 File Offset: 0x000136E8
		[NullableContext(2)]
		private T ConvertTo<T>()
		{
			JsonElement jsonElement = this._element.GetJsonElement();
			T t;
			try
			{
				Utf8JsonReader readerForElement = MutableJsonElement.GetReaderForElement(jsonElement);
				t = JsonSerializer.Deserialize<T>(ref readerForElement, this._serializerOptions);
			}
			catch (JsonException ex)
			{
				throw new InvalidCastException(string.Format("Unable to convert value of kind '{0}' to type '{1}'.", jsonElement.ValueKind, typeof(T)), ex);
			}
			return t;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00015554 File Offset: 0x00013754
		public override string ToString()
		{
			return this._element.ToString();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00015567 File Offset: 0x00013767
		public void Dispose()
		{
			this._element.DisposeRoot();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00015574 File Offset: 0x00013774
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			JsonValueKind? jsonValueKind;
			JsonValueKind jsonValueKind2;
			if (obj == null)
			{
				jsonValueKind = this._element.ValueKind;
				jsonValueKind2 = 7;
				return (jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null);
			}
			jsonValueKind = this._element.ValueKind;
			jsonValueKind2 = 7;
			if ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null))
			{
				DynamicData dynamicData = obj as DynamicData;
				return dynamicData != null && this.Equals(dynamicData);
			}
			string text = obj as string;
			bool flag3;
			if (text == null)
			{
				if (obj is bool)
				{
					bool flag = (bool)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 5;
					bool flag2;
					if (!((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)))
					{
						jsonValueKind = this._element.ValueKind;
						jsonValueKind2 = 6;
						if (!((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)))
						{
							flag2 = false;
							goto IL_020A;
						}
					}
					flag2 = this._element.GetBoolean() == flag;
					IL_020A:
					flag3 = flag2;
				}
				else if (obj is byte)
				{
					byte b = (byte)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					byte b2;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetByte(out b2) && b == b2;
				}
				else if (obj is sbyte)
				{
					sbyte b3 = (sbyte)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					sbyte b4;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetSByte(out b4) && b3 == b4;
				}
				else if (obj is short)
				{
					short num = (short)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					short num2;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetInt16(out num2) && num == num2;
				}
				else if (obj is ushort)
				{
					ushort num3 = (ushort)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					ushort num4;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetUInt16(out num4) && num3 == num4;
				}
				else if (obj is int)
				{
					int num5 = (int)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					int num6;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetInt32(out num6) && num5 == num6;
				}
				else if (obj is uint)
				{
					uint num7 = (uint)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					uint num8;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetUInt32(out num8) && num7 == num8;
				}
				else if (obj is long)
				{
					long num9 = (long)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					long num10;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetInt64(out num10) && num9 == num10;
				}
				else if (obj is ulong)
				{
					ulong num11 = (ulong)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					ulong num12;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetUInt64(out num12) && num11 == num12;
				}
				else if (obj is float)
				{
					float num13 = (float)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					float num14;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetSingle(out num14) && num13 == num14;
				}
				else if (obj is double)
				{
					double num15 = (double)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					double num16;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetDouble(out num16) && num15 == num16;
				}
				else if (obj is decimal)
				{
					decimal num17 = (decimal)obj;
					jsonValueKind = this._element.ValueKind;
					jsonValueKind2 = 4;
					decimal num18;
					flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.TryGetDecimal(out num18) && num17 == num18;
				}
				else
				{
					DynamicData dynamicData2 = obj as DynamicData;
					if (dynamicData2 == null)
					{
						flag3 = base.Equals(obj);
					}
					else
					{
						flag3 = this.Equals(dynamicData2);
					}
				}
			}
			else
			{
				jsonValueKind = this._element.ValueKind;
				jsonValueKind2 = 3;
				flag3 = ((jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null)) && this._element.GetString() == text;
			}
			return flag3;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00015A74 File Offset: 0x00013C74
		internal bool Equals(DynamicData other)
		{
			JsonValueKind? jsonValueKind;
			if (other == null)
			{
				jsonValueKind = this._element.ValueKind;
				JsonValueKind jsonValueKind2 = 7;
				return (jsonValueKind.GetValueOrDefault() == jsonValueKind2) & (jsonValueKind != null);
			}
			jsonValueKind = this._element.ValueKind;
			JsonValueKind? jsonValueKind3 = other._element.ValueKind;
			if (!((jsonValueKind.GetValueOrDefault() == jsonValueKind3.GetValueOrDefault()) & (jsonValueKind != null == (jsonValueKind3 != null))))
			{
				return false;
			}
			jsonValueKind3 = this._element.ValueKind;
			if (jsonValueKind3 != null)
			{
				switch (jsonValueKind3.GetValueOrDefault())
				{
				case 3:
					return this._element.GetString() == other._element.GetString();
				case 4:
					return this.NumberEqual(other);
				case 5:
					return true;
				case 6:
					return true;
				case 7:
					return true;
				}
			}
			return base.Equals(other);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00015B5C File Offset: 0x00013D5C
		private bool NumberEqual(DynamicData other)
		{
			double num;
			if (this._element.TryGetDouble(out num))
			{
				double num2;
				return other._element.TryGetDouble(out num2) && num == num2;
			}
			long num3;
			long num4;
			return this._element.TryGetInt64(out num3) && other._element.TryGetInt64(out num4) && num3 == num4;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00015BB4 File Offset: 0x00013DB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this._element.GetHashCode();
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00015BC7 File Offset: 0x00013DC7
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay
		{
			get
			{
				return this._element.DebuggerDisplay;
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00015BD4 File Offset: 0x00013DD4
		DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
		{
			return new DynamicData.MetaObject(parameter, this);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00015BE0 File Offset: 0x00013DE0
		public static implicit operator bool(DynamicData value)
		{
			bool boolean;
			try
			{
				boolean = value._element.GetBoolean();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(bool), value._element), ex);
			}
			return boolean;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00015C2C File Offset: 0x00013E2C
		[return: Nullable(2)]
		public static implicit operator string(DynamicData value)
		{
			string @string;
			try
			{
				@string = value._element.GetString();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(string), value._element), ex);
			}
			return @string;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00015C78 File Offset: 0x00013E78
		public static implicit operator byte(DynamicData value)
		{
			byte @byte;
			try
			{
				@byte = value._element.GetByte();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(byte), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(byte), value._element), ex2);
			}
			return @byte;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00015CEC File Offset: 0x00013EEC
		public static implicit operator sbyte(DynamicData value)
		{
			sbyte @sbyte;
			try
			{
				@sbyte = value._element.GetSByte();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(sbyte), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(sbyte), value._element), ex2);
			}
			return @sbyte;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00015D60 File Offset: 0x00013F60
		public static implicit operator short(DynamicData value)
		{
			short @int;
			try
			{
				@int = value._element.GetInt16();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(short), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(short), value._element), ex2);
			}
			return @int;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00015DD4 File Offset: 0x00013FD4
		public static implicit operator ushort(DynamicData value)
		{
			ushort @uint;
			try
			{
				@uint = value._element.GetUInt16();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(ushort), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(ushort), value._element), ex2);
			}
			return @uint;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00015E48 File Offset: 0x00014048
		public static implicit operator int(DynamicData value)
		{
			int @int;
			try
			{
				@int = value._element.GetInt32();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(int), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(int), value._element), ex2);
			}
			return @int;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00015EBC File Offset: 0x000140BC
		public static implicit operator uint(DynamicData value)
		{
			uint @uint;
			try
			{
				@uint = value._element.GetUInt32();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(uint), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(uint), value._element), ex2);
			}
			return @uint;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00015F30 File Offset: 0x00014130
		public static implicit operator long(DynamicData value)
		{
			long @int;
			try
			{
				@int = value._element.GetInt64();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(long), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(long), value._element), ex2);
			}
			return @int;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00015FA4 File Offset: 0x000141A4
		public static implicit operator ulong(DynamicData value)
		{
			ulong @uint;
			try
			{
				@uint = value._element.GetUInt64();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(ulong), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(ulong), value._element), ex2);
			}
			return @uint;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00016018 File Offset: 0x00014218
		public static implicit operator float(DynamicData value)
		{
			float single;
			try
			{
				single = value._element.GetSingle();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(float), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(float), value._element), ex2);
			}
			return single;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001608C File Offset: 0x0001428C
		public static implicit operator double(DynamicData value)
		{
			double @double;
			try
			{
				@double = value._element.GetDouble();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(double), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(double), value._element), ex2);
			}
			return @double;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00016100 File Offset: 0x00014300
		public static implicit operator decimal(DynamicData value)
		{
			decimal @decimal;
			try
			{
				@decimal = value._element.GetDecimal();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(decimal), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(decimal), value._element), ex2);
			}
			return @decimal;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00016174 File Offset: 0x00014374
		public static explicit operator DateTime(DynamicData value)
		{
			DateTime dateTime;
			try
			{
				if (value._options.DateTimeFormat.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					dateTime = value.ConvertTo<DateTime>();
				}
				else
				{
					dateTime = value._element.GetDateTime();
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(DateTime), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(DateTime), value._element), ex2);
			}
			return dateTime;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00016208 File Offset: 0x00014408
		public static explicit operator DateTimeOffset(DynamicData value)
		{
			DateTimeOffset dateTimeOffset;
			try
			{
				if (value._options.DateTimeFormat.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					dateTimeOffset = value.ConvertTo<DateTimeOffset>();
				}
				else
				{
					dateTimeOffset = value._element.GetDateTimeOffset();
				}
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(DateTimeOffset), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(DateTimeOffset), value._element), ex2);
			}
			return dateTimeOffset;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001629C File Offset: 0x0001449C
		public static explicit operator Guid(DynamicData value)
		{
			Guid guid;
			try
			{
				guid = value._element.GetGuid();
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidCastException(DynamicData.GetInvalidKindExceptionText(typeof(Guid), value._element), ex);
			}
			catch (FormatException ex2)
			{
				throw new InvalidCastException(DynamicData.GetInvalidFormatExceptionText(typeof(Guid), value._element), ex2);
			}
			return guid;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00016310 File Offset: 0x00014510
		[NullableContext(2)]
		public static bool operator ==(DynamicData left, object right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00016321 File Offset: 0x00014521
		[NullableContext(2)]
		public static bool operator !=(DynamicData left, object right)
		{
			return !(left == right);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001632D File Offset: 0x0001452D
		private static string GetInvalidKindExceptionText(Type target, MutableJsonElement element)
		{
			return string.Format("Unable to cast element to '{0}'.  Element has kind '{1}'.", target, element.ValueKind);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00016346 File Offset: 0x00014546
		private static string GetInvalidFormatExceptionText(Type target, MutableJsonElement element)
		{
			return string.Format("Unable to cast element to '{0}'.  Element has value '{1}'.", target, element);
		}

		// Token: 0x04000273 RID: 627
		internal const string RoundTripFormat = "o";

		// Token: 0x04000274 RID: 628
		internal const string UnixFormat = "x";

		// Token: 0x04000275 RID: 629
		private static readonly MethodInfo GetPropertyMethod = typeof(DynamicData).GetMethod("GetProperty", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000276 RID: 630
		private static readonly MethodInfo SetPropertyMethod = typeof(DynamicData).GetMethod("SetProperty", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000277 RID: 631
		private static readonly MethodInfo GetEnumerableMethod = typeof(DynamicData).GetMethod("GetEnumerable", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000278 RID: 632
		private static readonly MethodInfo GetViaIndexerMethod = typeof(DynamicData).GetMethod("GetViaIndexer", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000279 RID: 633
		private static readonly MethodInfo SetViaIndexerMethod = typeof(DynamicData).GetMethod("SetViaIndexer", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0400027A RID: 634
		private MutableJsonElement _element;

		// Token: 0x0400027B RID: 635
		private readonly DynamicDataOptions _options;

		// Token: 0x0400027C RID: 636
		private readonly JsonSerializerOptions _serializerOptions;

		// Token: 0x0400027D RID: 637
		internal const string SerializationRequiresUnreferencedCodeClass = "This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.";

		// Token: 0x0400027E RID: 638
		private static readonly Dictionary<Type, MethodInfo> CastFromOperators = (from method in typeof(DynamicData).GetMethods(BindingFlags.Static | BindingFlags.Public)
			where method.Name == "op_Explicit" || method.Name == "op_Implicit"
			select method).ToDictionary((MethodInfo method) => method.ReturnType);

		// Token: 0x02000148 RID: 328
		[Nullable(0)]
		internal class AllowList
		{
			// Token: 0x060008A8 RID: 2216 RVA: 0x00021162 File Offset: 0x0001F362
			public static void AssertAllowedValue<[Nullable(2)] T>(T value)
			{
				if (value == null)
				{
					return;
				}
				if (!DynamicData.AllowList.IsAllowedValue<T>(value))
				{
					throw new NotSupportedException(string.Format("Assigning this value is not supported, either because its type '{0}' is not allowed, or because it contains unallowed types.", value.GetType()));
				}
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x00021194 File Offset: 0x0001F394
			private static bool IsAllowedValue<[Nullable(2)] T>(T value)
			{
				if (value == null)
				{
					return true;
				}
				Type type = value.GetType();
				return DynamicData.AllowList.IsAllowedType(type) || DynamicData.AllowList.IsAllowedCollectionValue<T>(type, value) || DynamicData.AllowList.IsAllowedAnonymousValue<T>(type, value);
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x000211D8 File Offset: 0x0001F3D8
			private static bool IsAllowedType(Type type)
			{
				return type.IsPrimitive || type == typeof(decimal) || type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan) || type == typeof(Uri) || type == typeof(Guid) || type == typeof(ETag) || type == typeof(JsonElement) || type == typeof(JsonDocument) || type == typeof(DynamicData);
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x000212BF File Offset: 0x0001F4BF
			private static bool IsAllowedCollectionValue<[Nullable(2)] T>(Type type, T value)
			{
				return DynamicData.AllowList.IsAllowedArrayValue<T>(type, value) || DynamicData.AllowList.IsAllowedListValue<T>(type, value) || DynamicData.AllowList.IsAllowedDictionaryValue<T>(type, value);
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x000212DC File Offset: 0x0001F4DC
			private static bool IsAllowedArrayValue<[Nullable(2)] T>(Type type, T value)
			{
				Array array = value as Array;
				if (array == null)
				{
					return false;
				}
				Type elementType = type.GetElementType();
				return !(elementType == null) && (elementType.IsPrimitive || elementType == typeof(string) || DynamicData.AllowList.IsAllowedEnumerableValue(elementType, array));
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x00021330 File Offset: 0x0001F530
			private static bool IsAllowedListValue<[Nullable(2)] T>(Type type, T value)
			{
				if (value == null)
				{
					return true;
				}
				if (!type.IsGenericType)
				{
					return false;
				}
				if (type.GetGenericTypeDefinition() != typeof(List<>))
				{
					return false;
				}
				Type type2 = type.GetGenericArguments()[0];
				return type2.IsPrimitive || type2 == typeof(string) || DynamicData.AllowList.IsAllowedEnumerableValue(type2, (IEnumerable)((object)value));
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x000213A0 File Offset: 0x0001F5A0
			private static bool IsAllowedDictionaryValue<[Nullable(2)] T>(Type type, T value)
			{
				if (value == null)
				{
					return true;
				}
				if (!type.IsGenericType)
				{
					return false;
				}
				if (type.GetGenericTypeDefinition() != typeof(Dictionary<, >))
				{
					return false;
				}
				Type[] genericArguments = type.GetGenericArguments();
				return !(genericArguments[0] != typeof(string)) && (genericArguments[1].IsPrimitive || genericArguments[1] == typeof(string) || DynamicData.AllowList.IsAllowedEnumerableValue(genericArguments[1], ((IDictionary)((object)value)).Values));
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00021430 File Offset: 0x0001F630
			private static bool IsAllowedEnumerableValue(Type elementType, IEnumerable enumerable)
			{
				foreach (object obj in enumerable)
				{
					if (obj != null)
					{
						if (obj.GetType() != elementType && !DynamicData.AllowList.IsAllowedType(obj.GetType()))
						{
							return false;
						}
						if (!DynamicData.AllowList.IsAllowedValue<object>(obj))
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x000214AC File Offset: 0x0001F6AC
			private static bool IsAllowedAnonymousValue<[Nullable(2)] T>(Type type, T value)
			{
				if (!DynamicData.AllowList.IsAnonymousType(type))
				{
					return false;
				}
				PropertyInfo[] properties = type.GetProperties();
				for (int i = 0; i < properties.Length; i++)
				{
					if (!DynamicData.AllowList.IsAllowedValue<object>(properties[i].GetValue(value)))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x000214F0 File Offset: 0x0001F6F0
			private static bool IsAnonymousType(Type type)
			{
				return type.Name.StartsWith("<>f__AnonymousType");
			}
		}

		// Token: 0x02000149 RID: 329
		[Nullable(0)]
		[DebuggerDisplay("{Current,nq}")]
		internal struct ArrayEnumerator : IEnumerable<DynamicData>, IEnumerable, IEnumerator<DynamicData>, IDisposable, IEnumerator
		{
			// Token: 0x060008B3 RID: 2227 RVA: 0x0002150A File Offset: 0x0001F70A
			internal ArrayEnumerator(MutableJsonElement.ArrayEnumerator enumerator, DynamicDataOptions options)
			{
				this._enumerator = enumerator;
				this._options = options;
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x0002151A File Offset: 0x0001F71A
			public DynamicData.ArrayEnumerator GetEnumerator()
			{
				return new DynamicData.ArrayEnumerator(this._enumerator.GetEnumerator(), this._options);
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00021532 File Offset: 0x0001F732
			public DynamicData Current
			{
				get
				{
					return new DynamicData(this._enumerator.Current, this._options);
				}
			}

			// Token: 0x060008B6 RID: 2230 RVA: 0x0002154A File Offset: 0x0001F74A
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x00021557 File Offset: 0x0001F757
			IEnumerator<DynamicData> IEnumerable<DynamicData>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x00021564 File Offset: 0x0001F764
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00021571 File Offset: 0x0001F771
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060008BA RID: 2234 RVA: 0x00021579 File Offset: 0x0001F779
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x060008BB RID: 2235 RVA: 0x00021586 File Offset: 0x0001F786
			public void Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x040004F4 RID: 1268
			private MutableJsonElement.ArrayEnumerator _enumerator;

			// Token: 0x040004F5 RID: 1269
			private readonly DynamicDataOptions _options;
		}

		// Token: 0x0200014A RID: 330
		[Nullable(0)]
		internal class DynamicDateTimeConverter : JsonConverter<DateTime>
		{
			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x060008BC RID: 2236 RVA: 0x00021593 File Offset: 0x0001F793
			public string Format { get; }

			// Token: 0x060008BD RID: 2237 RVA: 0x0002159B File Offset: 0x0001F79B
			public DynamicDateTimeConverter(string format)
			{
				this.Format = format;
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x000215AC File Offset: 0x0001F7AC
			public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (this.Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;
				}
				string @string = reader.GetString();
				if (@string == null)
				{
					throw new JsonException(string.Format("Failed to read 'string' value at JSON position {0}.", reader.Position));
				}
				return DateTime.Parse(@string, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime();
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x0002161C File Offset: 0x0001F81C
			public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
			{
				if (this.Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					if (dateTimeValue.Kind == DateTimeKind.Utc)
					{
						long num = dateTimeValue.ToUnixTimeSeconds();
						long num2 = num;
						writer.WriteNumberValue(num2);
						return;
					}
					throw new NotSupportedException(string.Format("DateTime {0} has a Kind of {1}. Azure SDK requires it to be UTC. You can call DateTime.SpecifyKind to change Kind property value to DateTimeKind.Utc.", dateTimeValue, dateTimeValue.Kind));
				}
				else
				{
					if (dateTimeValue.Kind == DateTimeKind.Utc)
					{
						string text = dateTimeValue.ToUniversalTime().ToString(this.Format, CultureInfo.InvariantCulture);
						string text2 = text;
						writer.WriteStringValue(text2);
						return;
					}
					throw new NotSupportedException(string.Format("DateTime {0} has a Kind of {1}. Azure SDK requires it to be UTC. You can call DateTime.SpecifyKind to change Kind property value to DateTimeKind.Utc.", dateTimeValue, dateTimeValue.Kind));
				}
			}
		}

		// Token: 0x0200014B RID: 331
		[Nullable(0)]
		internal class DynamicDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
		{
			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x060008C0 RID: 2240 RVA: 0x000216D8 File Offset: 0x0001F8D8
			public string Format { get; }

			// Token: 0x060008C1 RID: 2241 RVA: 0x000216E0 File Offset: 0x0001F8E0
			public DynamicDateTimeOffsetConverter(string format)
			{
				this.Format = format;
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x000216F0 File Offset: 0x0001F8F0
			public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				if (this.Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).ToUniversalTime();
				}
				string @string = reader.GetString();
				if (@string == null)
				{
					throw new JsonException(string.Format("Failed to read 'string' value at JSON position {0}.", reader.Position));
				}
				return DateTimeOffset.Parse(@string, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x00021758 File Offset: 0x0001F958
			public override void Write(Utf8JsonWriter writer, DateTimeOffset dateTimeOffsetValue, JsonSerializerOptions options)
			{
				if (this.Format.Equals("x", StringComparison.InvariantCultureIgnoreCase))
				{
					long num = dateTimeOffsetValue.ToUniversalTime().ToUnixTimeSeconds();
					writer.WriteNumberValue(num);
					return;
				}
				string text = dateTimeOffsetValue.ToUniversalTime().UtcDateTime.ToString(this.Format, CultureInfo.InvariantCulture);
				writer.WriteStringValue(text);
			}
		}

		// Token: 0x0200014C RID: 332
		[Nullable(0)]
		internal class DynamicTimeSpanConverter : JsonConverter<TimeSpan>
		{
			// Token: 0x060008C4 RID: 2244 RVA: 0x000217BA File Offset: 0x0001F9BA
			public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				string @string = reader.GetString();
				if (@string == null)
				{
					throw new JsonException(string.Format("Failed to read 'string' value at JSON position {0}.", reader.Position));
				}
				return TimeSpan.ParseExact(@string, "c", CultureInfo.InvariantCulture);
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x000217F0 File Offset: 0x0001F9F0
			public override void Write(Utf8JsonWriter writer, TimeSpan timeValue, JsonSerializerOptions options)
			{
				string text = timeValue.ToString("c", CultureInfo.InvariantCulture);
				writer.WriteStringValue(text);
			}
		}

		// Token: 0x0200014D RID: 333
		[Nullable(new byte[] { 0, 1 })]
		[RequiresUnreferencedCode("Using DynamicData or DynamicDataConverter is not compatible with trimming due to reflection-based serialization.")]
		[RequiresDynamicCode("Using DynamicData or DynamicDataConverter is not compatible with trimming due to reflection-based serialization.")]
		private class DynamicDataJsonConverter : JsonConverter<DynamicData>
		{
			// Token: 0x060008C7 RID: 2247 RVA: 0x0002181E File Offset: 0x0001FA1E
			public override DynamicData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return new DynamicData(MutableJsonDocument.Parse(ref reader, null).RootElement, DynamicDataOptions.FromSerializerOptions(options));
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x00021837 File Offset: 0x0001FA37
			public override void Write(Utf8JsonWriter writer, DynamicData value, JsonSerializerOptions options)
			{
				value._element.WriteTo(writer);
			}

			// Token: 0x040004F8 RID: 1272
			public const string ClassIsIncompatibleWithTrimming = "Using DynamicData or DynamicDataConverter is not compatible with trimming due to reflection-based serialization.";
		}

		// Token: 0x0200014E RID: 334
		[Nullable(0)]
		private class MetaObject : DynamicMetaObject
		{
			// Token: 0x060008CA RID: 2250 RVA: 0x0002184D File Offset: 0x0001FA4D
			internal MetaObject(Expression parameter, IDynamicMetaObjectProvider value)
				: base(parameter, BindingRestrictions.Empty, value)
			{
				this._value = (DynamicData)value;
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x00021868 File Offset: 0x0001FA68
			public override IEnumerable<string> GetDynamicMemberNames()
			{
				JsonValueKind? valueKind = this._value._element.ValueKind;
				JsonValueKind jsonValueKind = 1;
				if ((valueKind.GetValueOrDefault() == jsonValueKind) & (valueKind != null))
				{
					return from p in this._value._element.EnumerateObject()
						select p.Item1;
				}
				return Array.Empty<string>();
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x000218DC File Offset: 0x0001FADC
			public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
			{
				UnaryExpression unaryExpression = Expression.Convert(base.Expression, base.LimitType);
				Expression[] array = new Expression[] { Expression.Constant(binder.Name) };
				Expression expression = Expression.Call(unaryExpression, DynamicData.GetPropertyMethod, array);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x00021934 File Offset: 0x0001FB34
			public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
			{
				UnaryExpression unaryExpression = Expression.Convert(base.Expression, base.LimitType);
				Expression[] array = new Expression[] { Expression.Convert(indexes[0].Expression, typeof(object)) };
				Expression expression = Expression.Call(unaryExpression, DynamicData.GetViaIndexerMethod, array);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x00021998 File Offset: 0x0001FB98
			public override DynamicMetaObject BindConvert(ConvertBinder binder)
			{
				Expression expression = Expression.Convert(base.Expression, base.LimitType);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				if (binder.Type == typeof(IEnumerable))
				{
					return new DynamicMetaObject(Expression.Call(expression, DynamicData.GetEnumerableMethod), typeRestriction);
				}
				if (binder.Type == typeof(IDisposable))
				{
					return new DynamicMetaObject(Expression.Convert(expression, binder.Type), typeRestriction);
				}
				MethodInfo methodInfo;
				if (DynamicData.CastFromOperators.TryGetValue(binder.Type, out methodInfo))
				{
					return new DynamicMetaObject(Expression.Call(methodInfo, expression), typeRestriction);
				}
				return new DynamicMetaObject(Expression.Call(expression, "ConvertTo", new Type[] { binder.Type }, Array.Empty<Expression>()), typeRestriction);
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x00021A64 File Offset: 0x0001FC64
			public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
			{
				UnaryExpression unaryExpression = Expression.Convert(base.Expression, base.LimitType);
				Expression[] array = new Expression[]
				{
					Expression.Constant(binder.Name),
					Expression.Convert(value.Expression, typeof(object))
				};
				Expression expression = Expression.Call(unaryExpression, DynamicData.SetPropertyMethod, array);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x00021AD4 File Offset: 0x0001FCD4
			public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
			{
				UnaryExpression unaryExpression = Expression.Convert(base.Expression, base.LimitType);
				Expression[] array = new Expression[]
				{
					Expression.Convert(indexes[0].Expression, typeof(object)),
					Expression.Convert(value.Expression, typeof(object))
				};
				Expression expression = Expression.Call(unaryExpression, DynamicData.SetViaIndexerMethod, array);
				BindingRestrictions typeRestriction = BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
				return new DynamicMetaObject(expression, typeRestriction);
			}

			// Token: 0x040004F9 RID: 1273
			private DynamicData _value;
		}

		// Token: 0x0200014F RID: 335
		[NullableContext(0)]
		[DebuggerDisplay("{Current,nq}")]
		internal struct ObjectEnumerator : IEnumerable<DynamicDataProperty>, IEnumerable, IEnumerator<DynamicDataProperty>, IDisposable, IEnumerator
		{
			// Token: 0x060008D1 RID: 2257 RVA: 0x00021B50 File Offset: 0x0001FD50
			[NullableContext(1)]
			internal ObjectEnumerator(MutableJsonElement.ObjectEnumerator enumerator, DynamicDataOptions options)
			{
				this._enumerator = enumerator;
				this._options = options;
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x00021B60 File Offset: 0x0001FD60
			public DynamicData.ObjectEnumerator GetEnumerator()
			{
				return new DynamicData.ObjectEnumerator(this._enumerator.GetEnumerator(), this._options);
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00021B78 File Offset: 0x0001FD78
			public DynamicDataProperty Current
			{
				get
				{
					return new DynamicDataProperty(this._enumerator.Current.Item1, new DynamicData(this._enumerator.Current.Item2, this._options));
				}
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x00021BAA File Offset: 0x0001FDAA
			[NullableContext(1)]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x00021BB7 File Offset: 0x0001FDB7
			[NullableContext(1)]
			IEnumerator<DynamicDataProperty> IEnumerable<DynamicDataProperty>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060008D6 RID: 2262 RVA: 0x00021BC4 File Offset: 0x0001FDC4
			public void Reset()
			{
				this._enumerator.Reset();
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00021BD1 File Offset: 0x0001FDD1
			[Nullable(1)]
			object IEnumerator.Current
			{
				[NullableContext(1)]
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060008D8 RID: 2264 RVA: 0x00021BDE File Offset: 0x0001FDDE
			public bool MoveNext()
			{
				return this._enumerator.MoveNext();
			}

			// Token: 0x060008D9 RID: 2265 RVA: 0x00021BEB File Offset: 0x0001FDEB
			public void Dispose()
			{
				this._enumerator.Dispose();
			}

			// Token: 0x040004FA RID: 1274
			private MutableJsonElement.ObjectEnumerator _enumerator;

			// Token: 0x040004FB RID: 1275
			[Nullable(1)]
			private readonly DynamicDataOptions _options;
		}
	}
}

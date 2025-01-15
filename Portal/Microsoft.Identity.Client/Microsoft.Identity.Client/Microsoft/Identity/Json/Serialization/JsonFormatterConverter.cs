using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Identity.Json.Linq;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Serialization
{
	// Token: 0x0200008E RID: 142
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001CDB1 File Offset: 0x0001AFB1
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001CDE4 File Offset: 0x0001AFE4
		private T GetTokenValue<T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)global::System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001CE18 File Offset: 0x0001B018
		[return: Nullable(2)]
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._reader.CreateISerializableItem(jtoken, type, this._contract, this._member);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001CE64 File Offset: 0x0001B064
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = value as JValue;
			return global::System.Convert.ChangeType((jvalue != null) ? jvalue.Value : value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001CE9A File Offset: 0x0001B09A
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001CEA3 File Offset: 0x0001B0A3
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001CEAC File Offset: 0x0001B0AC
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001CEB5 File Offset: 0x0001B0B5
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001CEBE File Offset: 0x0001B0BE
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001CEC7 File Offset: 0x0001B0C7
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001CED0 File Offset: 0x0001B0D0
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001CED9 File Offset: 0x0001B0D9
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001CEE2 File Offset: 0x0001B0E2
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001CEEB File Offset: 0x0001B0EB
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001CEFD File Offset: 0x0001B0FD
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001CF06 File Offset: 0x0001B106
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001CF0F File Offset: 0x0001B10F
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001CF18 File Offset: 0x0001B118
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04000275 RID: 629
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x04000276 RID: 630
		private readonly JsonISerializableContract _contract;

		// Token: 0x04000277 RID: 631
		[Nullable(2)]
		private readonly JsonProperty _member;
	}
}

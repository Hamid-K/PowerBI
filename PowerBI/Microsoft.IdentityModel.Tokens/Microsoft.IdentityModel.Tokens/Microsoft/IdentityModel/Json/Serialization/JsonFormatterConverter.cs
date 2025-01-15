using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Json.Utilities;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200008F RID: 143
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0001D385 File Offset: 0x0001B585
		public JsonFormatterConverter(JsonSerializerInternalReader reader, JsonISerializableContract contract, [Nullable(2)] JsonProperty member)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(contract, "contract");
			this._reader = reader;
			this._contract = contract;
			this._member = member;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		private T GetTokenValue<[Nullable(2)] T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			return (T)((object)global::System.Convert.ChangeType(((JValue)value).Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001D3EC File Offset: 0x0001B5EC
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

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001D438 File Offset: 0x0001B638
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = value as JValue;
			return global::System.Convert.ChangeType((jvalue != null) ? jvalue.Value : value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001D46E File Offset: 0x0001B66E
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001D477 File Offset: 0x0001B677
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001D480 File Offset: 0x0001B680
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001D489 File Offset: 0x0001B689
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001D492 File Offset: 0x0001B692
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001D49B File Offset: 0x0001B69B
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001D4AD File Offset: 0x0001B6AD
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001D4B6 File Offset: 0x0001B6B6
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001D4BF File Offset: 0x0001B6BF
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001D4C8 File Offset: 0x0001B6C8
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001D4D1 File Offset: 0x0001B6D1
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001D4DA File Offset: 0x0001B6DA
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001D4E3 File Offset: 0x0001B6E3
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001D4EC File Offset: 0x0001B6EC
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04000290 RID: 656
		private readonly JsonSerializerInternalReader _reader;

		// Token: 0x04000291 RID: 657
		private readonly JsonISerializableContract _contract;

		// Token: 0x04000292 RID: 658
		[Nullable(2)]
		private readonly JsonProperty _member;
	}
}

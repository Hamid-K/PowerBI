using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000103 RID: 259
	[DataContract]
	public sealed class Calculation
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000EBF3 File Offset: 0x0000CDF3
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0000EBFB File Offset: 0x0000CDFB
		[DataMember(Name = "Id", IsRequired = true, Order = 0)]
		public string Id { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000EC04 File Offset: 0x0000CE04
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0000EC26 File Offset: 0x0000CE26
		[DataMember(Name = "Value", IsRequired = true, Order = 10)]
		public object JsonValue
		{
			get
			{
				if (this.m_jsonValue == null)
				{
					this.m_jsonValue = this.TypeEncodeValueForJson(this.m_rawValue);
				}
				return this.m_jsonValue;
			}
			set
			{
				this.m_jsonValue = value;
				this.m_rawValue = null;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000EC36 File Offset: 0x0000CE36
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public object RawValue
		{
			get
			{
				if (this.m_rawValue == null)
				{
					this.m_rawValue = this.ParseTypeEncodedString(this.m_jsonValue);
				}
				return this.m_rawValue;
			}
			set
			{
				this.m_rawValue = value;
				this.m_jsonValue = null;
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000EC68 File Offset: 0x0000CE68
		private object TypeEncodeValueForJson(object rawValue)
		{
			if (rawValue == null || rawValue is bool)
			{
				return rawValue;
			}
			return PrimitiveValueEncoding.ToTypeEncodedString(rawValue);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000EC80 File Offset: 0x0000CE80
		private object ParseTypeEncodedString(object jsonValue)
		{
			if (jsonValue == null || jsonValue is bool || jsonValue is long || jsonValue is double)
			{
				return jsonValue;
			}
			string text = jsonValue as string;
			PrimitiveValue primitiveValue;
			if (text != null && PrimitiveValueEncoding.TryParseTypeEncodedString(text, out primitiveValue))
			{
				return primitiveValue.GetValueAsObject();
			}
			throw new InvalidDataContractException(StringUtil.FormatInvariant("Unexpected value encoding for calculation {0}: {1}", this.Id, jsonValue));
		}

		// Token: 0x040002F4 RID: 756
		private object m_rawValue;

		// Token: 0x040002F5 RID: 757
		private object m_jsonValue;
	}
}

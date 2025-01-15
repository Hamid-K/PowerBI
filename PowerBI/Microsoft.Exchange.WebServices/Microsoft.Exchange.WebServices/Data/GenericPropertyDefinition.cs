using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002C9 RID: 713
	internal class GenericPropertyDefinition<TPropertyValue> : TypedPropertyDefinition where TPropertyValue : struct
	{
		// Token: 0x06001963 RID: 6499 RVA: 0x00044EF0 File Offset: 0x00043EF0
		internal GenericPropertyDefinition(string xmlElementName, string uri, ExchangeVersion version)
			: base(xmlElementName, uri, version)
		{
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x00044EFB File Offset: 0x00043EFB
		internal GenericPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00044F08 File Offset: 0x00043F08
		internal GenericPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, bool isNullable)
			: base(xmlElementName, uri, flags, version, isNullable)
		{
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00044F17 File Offset: 0x00043F17
		internal override object Parse(string value)
		{
			return EwsUtilities.Parse<TPropertyValue>(value);
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00044F24 File Offset: 0x00043F24
		public override Type Type
		{
			get
			{
				if (!this.IsNullable)
				{
					return typeof(TPropertyValue);
				}
				return typeof(TPropertyValue?);
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00044F43 File Offset: 0x00043F43
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			jsonObject.Add(base.XmlElementName, propertyBag[this]);
		}
	}
}

using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002CB RID: 715
	internal sealed class ByteArrayPropertyDefinition : TypedPropertyDefinition
	{
		// Token: 0x0600196D RID: 6509 RVA: 0x00044F8C File Offset: 0x00043F8C
		internal ByteArrayPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00044F99 File Offset: 0x00043F99
		internal override object Parse(string value)
		{
			return Convert.FromBase64String(value);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00044FA1 File Offset: 0x00043FA1
		internal override string ToString(object value)
		{
			return Convert.ToBase64String((byte[])value);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00044FAE File Offset: 0x00043FAE
		internal override bool IsNullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001971 RID: 6513 RVA: 0x00044FB1 File Offset: 0x00043FB1
		public override Type Type
		{
			get
			{
				return typeof(byte[]);
			}
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00044FBD File Offset: 0x00043FBD
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			if (propertyBag[this] != null)
			{
				jsonObject.Add(base.XmlElementName, this.ToString(propertyBag[this]));
			}
		}
	}
}

using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002DE RID: 734
	internal class StringPropertyDefinition : TypedPropertyDefinition
	{
		// Token: 0x060019EF RID: 6639 RVA: 0x00046871 File Offset: 0x00045871
		internal StringPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0004687E File Offset: 0x0004587E
		internal override object Parse(string value)
		{
			return value;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00046881 File Offset: 0x00045881
		internal override bool IsNullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x00046884 File Offset: 0x00045884
		public override Type Type
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00046890 File Offset: 0x00045890
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			jsonObject.Add(base.XmlElementName, propertyBag[this]);
		}
	}
}

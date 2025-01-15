using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002E0 RID: 736
	internal class TimeSpanPropertyDefinition : GenericPropertyDefinition<TimeSpan>
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x00046987 File Offset: 0x00045987
		internal TimeSpanPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00046994 File Offset: 0x00045994
		internal override object Parse(string value)
		{
			return EwsUtilities.XSDurationToTimeSpan(value);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000469A1 File Offset: 0x000459A1
		internal override string ToString(object value)
		{
			return EwsUtilities.TimeSpanToXSDuration((TimeSpan)value);
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000469AE File Offset: 0x000459AE
		internal override void WriteJsonValue(JsonObject jsonObject, PropertyBag propertyBag, ExchangeService service, bool isUpdateOperation)
		{
			jsonObject.Add(base.XmlElementName, propertyBag[this]);
		}
	}
}

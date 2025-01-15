using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D5 RID: 725
	internal class ListValuePropertyDefinition<TPropertyValue> : GenericPropertyDefinition<TPropertyValue> where TPropertyValue : struct
	{
		// Token: 0x060019C2 RID: 6594 RVA: 0x00045ECB File Offset: 0x00044ECB
		internal ListValuePropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00045ED8 File Offset: 0x00044ED8
		internal override object Parse(string value)
		{
			string text = (string.IsNullOrEmpty(value) ? value : value.Replace(' ', ','));
			return EwsUtilities.Parse<TPropertyValue>(text);
		}
	}
}

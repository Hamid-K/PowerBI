using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002D4 RID: 724
	internal class IntPropertyDefinition : GenericPropertyDefinition<int>
	{
		// Token: 0x060019BF RID: 6591 RVA: 0x00045EA4 File Offset: 0x00044EA4
		internal IntPropertyDefinition(string xmlElementName, string uri, ExchangeVersion version)
			: base(xmlElementName, uri, version)
		{
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00045EAF File Offset: 0x00044EAF
		internal IntPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00045EBC File Offset: 0x00044EBC
		internal IntPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, bool isNullable)
			: base(xmlElementName, uri, flags, version, isNullable)
		{
		}
	}
}

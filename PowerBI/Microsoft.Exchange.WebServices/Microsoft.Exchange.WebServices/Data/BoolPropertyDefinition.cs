using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002CA RID: 714
	internal sealed class BoolPropertyDefinition : GenericPropertyDefinition<bool>
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x00044F58 File Offset: 0x00043F58
		internal BoolPropertyDefinition(string xmlElementName, string uri, ExchangeVersion version)
			: base(xmlElementName, uri, version)
		{
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00044F63 File Offset: 0x00043F63
		internal BoolPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version)
			: base(xmlElementName, uri, flags, version)
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x00044F70 File Offset: 0x00043F70
		internal BoolPropertyDefinition(string xmlElementName, string uri, PropertyDefinitionFlags flags, ExchangeVersion version, bool isNullable)
			: base(xmlElementName, uri, flags, version, isNullable)
		{
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x00044F7F File Offset: 0x00043F7F
		internal override string ToString(object value)
		{
			return EwsUtilities.BoolToXSBool((bool)value);
		}
	}
}

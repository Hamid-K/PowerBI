using System;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000153 RID: 339
	[EditorBrowsable(1)]
	internal sealed class CreateResponseObjectResponse : CreateItemResponseBase
	{
		// Token: 0x0600104F RID: 4175 RVA: 0x0002FB73 File Offset: 0x0002EB73
		internal override Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, xmlElementName);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002FB7C File Offset: 0x0002EB7C
		internal CreateResponseObjectResponse()
		{
		}
	}
}

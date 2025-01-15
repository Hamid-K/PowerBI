using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200002C RID: 44
	internal interface ICustomUpdateSerializer
	{
		// Token: 0x06000205 RID: 517
		bool WriteSetUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject, PropertyDefinition propertyDefinition);

		// Token: 0x06000206 RID: 518
		bool WriteDeleteUpdateToXml(EwsServiceXmlWriter writer, ServiceObject ewsObject);

		// Token: 0x06000207 RID: 519
		bool WriteSetUpdateToJson(ExchangeService service, ServiceObject ewsObject, PropertyDefinition propertyDefinition, List<JsonObject> updates);

		// Token: 0x06000208 RID: 520
		bool WriteDeleteUpdateToJson(ExchangeService service, ServiceObject ewsObject, List<JsonObject> updates);
	}
}

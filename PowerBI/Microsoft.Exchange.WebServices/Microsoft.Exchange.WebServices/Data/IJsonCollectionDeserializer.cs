using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200002D RID: 45
	internal interface IJsonCollectionDeserializer
	{
		// Token: 0x06000209 RID: 521
		void CreateFromJsonCollection(object[] jsonCollection, ExchangeService service);

		// Token: 0x0600020A RID: 522
		void UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service);
	}
}

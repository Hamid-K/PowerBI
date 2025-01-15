using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000292 RID: 658
	// (Invoke) Token: 0x06001740 RID: 5952
	internal delegate T GetObjectInstanceDelegate<T>(ExchangeService service, string xmlElementName) where T : ServiceObject;
}

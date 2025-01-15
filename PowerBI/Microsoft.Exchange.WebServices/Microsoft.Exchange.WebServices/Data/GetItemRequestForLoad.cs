using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200011A RID: 282
	internal sealed class GetItemRequestForLoad : GetItemRequestBase<ServiceResponse>
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002B532 File Offset: 0x0002A532
		internal GetItemRequestForLoad(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002B53C File Offset: 0x0002A53C
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetItemResponse(base.ItemIds[responseIndex], base.PropertySet);
		}
	}
}

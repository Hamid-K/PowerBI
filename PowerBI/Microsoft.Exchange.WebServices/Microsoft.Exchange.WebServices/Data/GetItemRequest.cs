using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000119 RID: 281
	internal sealed class GetItemRequest : GetItemRequestBase<GetItemResponse>
	{
		// Token: 0x06000DDE RID: 3550 RVA: 0x0002B50F File Offset: 0x0002A50F
		internal GetItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0002B519 File Offset: 0x0002A519
		internal override GetItemResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetItemResponse(base.ItemIds[responseIndex], base.PropertySet);
		}
	}
}

using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F6 RID: 246
	internal sealed class CreateItemRequest : CreateItemRequestBase<Item, ServiceResponse>
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00028FAC File Offset: 0x00027FAC
		internal CreateItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00028FB6 File Offset: 0x00027FB6
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new CreateItemResponse((Item)EwsUtilities.GetEnumeratedObjectAt(base.Items, responseIndex));
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00028FD0 File Offset: 0x00027FD0
		internal override void Validate()
		{
			base.Validate();
			foreach (Item item in base.Items)
			{
				item.Validate();
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00029024 File Offset: 0x00028024
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}

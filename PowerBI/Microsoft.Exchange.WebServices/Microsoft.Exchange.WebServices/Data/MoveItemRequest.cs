using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000131 RID: 305
	internal class MoveItemRequest : MoveCopyItemRequest<MoveCopyItemResponse>
	{
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002C975 File Offset: 0x0002B975
		internal MoveItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002C97F File Offset: 0x0002B97F
		internal override MoveCopyItemResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new MoveCopyItemResponse();
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002C986 File Offset: 0x0002B986
		internal override string GetXmlElementName()
		{
			return "MoveItem";
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0002C98D File Offset: 0x0002B98D
		internal override string GetResponseXmlElementName()
		{
			return "MoveItemResponse";
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002C994 File Offset: 0x0002B994
		internal override string GetResponseMessageXmlElementName()
		{
			return "MoveItemResponseMessage";
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002C99B File Offset: 0x0002B99B
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}

using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F1 RID: 241
	internal class CopyItemRequest : MoveCopyItemRequest<MoveCopyItemResponse>
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x00028953 File Offset: 0x00027953
		internal CopyItemRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002895D File Offset: 0x0002795D
		internal override MoveCopyItemResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new MoveCopyItemResponse();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00028964 File Offset: 0x00027964
		internal override string GetXmlElementName()
		{
			return "CopyItem";
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002896B File Offset: 0x0002796B
		internal override string GetResponseXmlElementName()
		{
			return "CopyItemResponse";
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00028972 File Offset: 0x00027972
		internal override string GetResponseMessageXmlElementName()
		{
			return "CopyItemResponseMessage";
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00028979 File Offset: 0x00027979
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}

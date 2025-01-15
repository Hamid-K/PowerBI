using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000F7 RID: 247
	internal sealed class CreateResponseObjectRequest : CreateItemRequestBase<ServiceObject, CreateResponseObjectResponse>
	{
		// Token: 0x06000C6E RID: 3182 RVA: 0x00029027 File Offset: 0x00028027
		internal CreateResponseObjectRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00029031 File Offset: 0x00028031
		internal override CreateResponseObjectResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new CreateResponseObjectResponse();
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00029038 File Offset: 0x00028038
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}

using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000115 RID: 277
	internal sealed class GetFolderRequestForLoad : GetFolderRequestBase<ServiceResponse>
	{
		// Token: 0x06000DBC RID: 3516 RVA: 0x0002B2FE File Offset: 0x0002A2FE
		internal GetFolderRequestForLoad(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002B308 File Offset: 0x0002A308
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetFolderResponse(base.FolderIds[responseIndex].GetFolder(), base.PropertySet);
		}
	}
}

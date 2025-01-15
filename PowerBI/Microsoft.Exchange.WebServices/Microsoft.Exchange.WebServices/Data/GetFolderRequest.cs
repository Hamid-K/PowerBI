using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000114 RID: 276
	internal sealed class GetFolderRequest : GetFolderRequestBase<GetFolderResponse>
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x0002B2D6 File Offset: 0x0002A2D6
		internal GetFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002B2E0 File Offset: 0x0002A2E0
		internal override GetFolderResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetFolderResponse(base.FolderIds[responseIndex].GetFolder(), base.PropertySet);
		}
	}
}

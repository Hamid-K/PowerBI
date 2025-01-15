using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000130 RID: 304
	internal class MoveFolderRequest : MoveCopyFolderRequest<MoveCopyFolderResponse>
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x0002C94C File Offset: 0x0002B94C
		internal MoveFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002C956 File Offset: 0x0002B956
		internal override MoveCopyFolderResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new MoveCopyFolderResponse();
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002C95D File Offset: 0x0002B95D
		internal override string GetXmlElementName()
		{
			return "MoveFolder";
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002C964 File Offset: 0x0002B964
		internal override string GetResponseXmlElementName()
		{
			return "MoveFolderResponse";
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002C96B File Offset: 0x0002B96B
		internal override string GetResponseMessageXmlElementName()
		{
			return "MoveFolderResponseMessage";
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002C972 File Offset: 0x0002B972
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}

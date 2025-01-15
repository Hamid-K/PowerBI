using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000EF RID: 239
	internal class CopyFolderRequest : MoveCopyFolderRequest<MoveCopyFolderResponse>
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x00028838 File Offset: 0x00027838
		internal CopyFolderRequest(ExchangeService service, ServiceErrorHandling errorHandlingMode)
			: base(service, errorHandlingMode)
		{
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00028842 File Offset: 0x00027842
		internal override MoveCopyFolderResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new MoveCopyFolderResponse();
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00028849 File Offset: 0x00027849
		internal override string GetXmlElementName()
		{
			return "CopyFolder";
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00028850 File Offset: 0x00027850
		internal override string GetResponseXmlElementName()
		{
			return "CopyFolderResponse";
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00028857 File Offset: 0x00027857
		internal override string GetResponseMessageXmlElementName()
		{
			return "CopyFolderResponseMessage";
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0002885E File Offset: 0x0002785E
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}
	}
}

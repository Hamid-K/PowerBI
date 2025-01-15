using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200025A RID: 602
	[Serializable]
	public sealed class CreateAttachmentException : BatchServiceResponseException<CreateAttachmentResponse>
	{
		// Token: 0x060015A8 RID: 5544 RVA: 0x0003CD20 File Offset: 0x0003BD20
		internal CreateAttachmentException(ServiceResponseCollection<CreateAttachmentResponse> serviceResponses, string message)
			: base(serviceResponses, message)
		{
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0003CD2A File Offset: 0x0003BD2A
		internal CreateAttachmentException(ServiceResponseCollection<CreateAttachmentResponse> serviceResponses, string message, Exception innerException)
			: base(serviceResponses, message, innerException)
		{
		}
	}
}

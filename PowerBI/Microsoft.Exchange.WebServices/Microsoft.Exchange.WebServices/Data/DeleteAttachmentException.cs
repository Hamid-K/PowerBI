using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200025B RID: 603
	[Serializable]
	public sealed class DeleteAttachmentException : BatchServiceResponseException<DeleteAttachmentResponse>
	{
		// Token: 0x060015AA RID: 5546 RVA: 0x0003CD35 File Offset: 0x0003BD35
		internal DeleteAttachmentException(ServiceResponseCollection<DeleteAttachmentResponse> serviceResponses, string message)
			: base(serviceResponses, message)
		{
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0003CD3F File Offset: 0x0003BD3F
		internal DeleteAttachmentException(ServiceResponseCollection<DeleteAttachmentResponse> serviceResponses, string message, Exception innerException)
			: base(serviceResponses, message, innerException)
		{
		}
	}
}

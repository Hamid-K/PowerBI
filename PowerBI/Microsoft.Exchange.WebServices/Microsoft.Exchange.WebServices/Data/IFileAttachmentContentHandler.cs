using System;
using System.IO;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200026B RID: 619
	public interface IFileAttachmentContentHandler
	{
		// Token: 0x060015D8 RID: 5592
		Stream GetOutputStream(string attachmentId);
	}
}

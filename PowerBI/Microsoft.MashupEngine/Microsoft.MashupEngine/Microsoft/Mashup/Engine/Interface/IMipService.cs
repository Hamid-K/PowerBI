using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000080 RID: 128
	public interface IMipService
	{
		// Token: 0x060001E4 RID: 484
		FileProtectionInformation GetInfo(IResource resource, Stream stream, string fileExtension);

		// Token: 0x060001E5 RID: 485
		Stream GetDecryptedStream(IResource resource, Stream stream, string fileExtension);

		// Token: 0x060001E6 RID: 486
		ProtectionInformation[] GetClassifications(IResource resource);
	}
}

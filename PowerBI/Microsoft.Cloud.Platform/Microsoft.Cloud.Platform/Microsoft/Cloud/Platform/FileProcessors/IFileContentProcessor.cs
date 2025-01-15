using System;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000ED RID: 237
	public interface IFileContentProcessor
	{
		// Token: 0x060006B6 RID: 1718
		bool CanProcessFile(FileProcessorInfo fileProcessorInfo);

		// Token: 0x060006B7 RID: 1719
		IFileContentInfo Process(FileProcessorInfo fileProcessorInfo, IFileContentInfo fileInfo);
	}
}

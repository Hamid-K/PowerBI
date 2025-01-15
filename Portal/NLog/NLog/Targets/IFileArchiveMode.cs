using System;
using System.Collections.Generic;

namespace NLog.Targets
{
	// Token: 0x0200003E RID: 62
	internal interface IFileArchiveMode
	{
		// Token: 0x06000690 RID: 1680
		bool AttemptCleanupOnInitializeFile(string archiveFilePath, int maxArchiveFiles);

		// Token: 0x06000691 RID: 1681
		string GenerateFileNameMask(string archiveFilePath);

		// Token: 0x06000692 RID: 1682
		List<DateAndSequenceArchive> GetExistingArchiveFiles(string archiveFilePath);

		// Token: 0x06000693 RID: 1683
		DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles);

		// Token: 0x06000694 RID: 1684
		IEnumerable<DateAndSequenceArchive> CheckArchiveCleanup(string archiveFilePath, List<DateAndSequenceArchive> existingArchiveFiles, int maxArchiveFiles);
	}
}

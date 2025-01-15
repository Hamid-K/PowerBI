using System;
using System.Collections.Generic;
using System.IO;

namespace NLog.Targets.FileArchiveModes
{
	// Token: 0x0200007D RID: 125
	internal sealed class FileArchiveModeDynamicTemplate : IFileArchiveMode
	{
		// Token: 0x0600094E RID: 2382 RVA: 0x00018564 File Offset: 0x00016764
		private static string CreateDynamicTemplate(string archiveFilePath)
		{
			string extension = Path.GetExtension(archiveFilePath);
			return Path.ChangeExtension(archiveFilePath, ".{#}" + extension);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00018589 File Offset: 0x00016789
		public FileArchiveModeDynamicTemplate(IFileArchiveMode archiveHelper)
		{
			this._archiveHelper = archiveHelper;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00018598 File Offset: 0x00016798
		public bool AttemptCleanupOnInitializeFile(string archiveFilePath, int maxArchiveFiles)
		{
			return this._archiveHelper.AttemptCleanupOnInitializeFile(archiveFilePath, maxArchiveFiles);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000185A7 File Offset: 0x000167A7
		public IEnumerable<DateAndSequenceArchive> CheckArchiveCleanup(string archiveFilePath, List<DateAndSequenceArchive> existingArchiveFiles, int maxArchiveFiles)
		{
			return this._archiveHelper.CheckArchiveCleanup(FileArchiveModeDynamicTemplate.CreateDynamicTemplate(archiveFilePath), existingArchiveFiles, maxArchiveFiles);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000185BC File Offset: 0x000167BC
		public DateAndSequenceArchive GenerateArchiveFileName(string archiveFilePath, DateTime archiveDate, List<DateAndSequenceArchive> existingArchiveFiles)
		{
			return this._archiveHelper.GenerateArchiveFileName(FileArchiveModeDynamicTemplate.CreateDynamicTemplate(archiveFilePath), archiveDate, existingArchiveFiles);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000185D1 File Offset: 0x000167D1
		public string GenerateFileNameMask(string archiveFilePath)
		{
			return this._archiveHelper.GenerateFileNameMask(FileArchiveModeDynamicTemplate.CreateDynamicTemplate(archiveFilePath));
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000185E4 File Offset: 0x000167E4
		public List<DateAndSequenceArchive> GetExistingArchiveFiles(string archiveFilePath)
		{
			return this._archiveHelper.GetExistingArchiveFiles(FileArchiveModeDynamicTemplate.CreateDynamicTemplate(archiveFilePath));
		}

		// Token: 0x04000223 RID: 547
		private readonly IFileArchiveMode _archiveHelper;
	}
}

using System;
using System.IO;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000EB RID: 235
	public interface IFileContentInfoFactory
	{
		// Token: 0x060006AE RID: 1710
		IFileContentInfo CreateFileInfo(string fullPath);

		// Token: 0x060006AF RID: 1711
		IFileContentInfo CreateFileInfo(byte[] fileContents, DateTime lastFileModifiedTime);

		// Token: 0x060006B0 RID: 1712
		string[] GetFilesInDirectory(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Owin.FileSystems
{
	// Token: 0x02000004 RID: 4
	public interface IFileSystem
	{
		// Token: 0x0600000D RID: 13
		bool TryGetFileInfo(string subpath, out IFileInfo fileInfo);

		// Token: 0x0600000E RID: 14
		bool TryGetDirectoryContents(string subpath, out IEnumerable<IFileInfo> contents);
	}
}

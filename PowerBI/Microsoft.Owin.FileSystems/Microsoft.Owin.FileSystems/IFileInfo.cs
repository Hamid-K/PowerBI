using System;
using System.IO;

namespace Microsoft.Owin.FileSystems
{
	// Token: 0x02000003 RID: 3
	public interface IFileInfo
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7
		long Length { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8
		string PhysicalPath { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9
		string Name { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10
		DateTime LastModified { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		bool IsDirectory { get; }

		// Token: 0x0600000C RID: 12
		Stream CreateReadStream();
	}
}

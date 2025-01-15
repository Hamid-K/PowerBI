using System;

namespace Azure.Identity
{
	// Token: 0x02000062 RID: 98
	internal interface IFileSystemService
	{
		// Token: 0x0600037C RID: 892
		bool FileExists(string path);

		// Token: 0x0600037D RID: 893
		string ReadAllText(string path);
	}
}

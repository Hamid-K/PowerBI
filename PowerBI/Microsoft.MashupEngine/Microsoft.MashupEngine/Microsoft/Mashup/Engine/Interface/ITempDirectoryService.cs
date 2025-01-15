using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200004E RID: 78
	public interface ITempDirectoryService
	{
		// Token: 0x0600014C RID: 332
		Stream CreateFile();

		// Token: 0x0600014D RID: 333
		Stream CreateFile(string extension, FileAccess fileAccess, out string path);
	}
}

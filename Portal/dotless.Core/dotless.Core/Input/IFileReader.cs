using System;

namespace dotless.Core.Input
{
	// Token: 0x020000B5 RID: 181
	public interface IFileReader
	{
		// Token: 0x0600052D RID: 1325
		byte[] GetBinaryFileContents(string fileName);

		// Token: 0x0600052E RID: 1326
		string GetFileContents(string fileName);

		// Token: 0x0600052F RID: 1327
		bool DoesFileExist(string fileName);

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000530 RID: 1328
		bool UseCacheDependencies { get; }
	}
}

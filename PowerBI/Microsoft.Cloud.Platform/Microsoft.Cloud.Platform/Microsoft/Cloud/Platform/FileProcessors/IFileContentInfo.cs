using System;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000E8 RID: 232
	public interface IFileContentInfo
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600069B RID: 1691
		DateTime LastModified { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600069C RID: 1692
		byte[] FileContents { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600069D RID: 1693
		string HashString { get; }
	}
}

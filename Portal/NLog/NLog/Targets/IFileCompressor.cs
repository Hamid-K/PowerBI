using System;

namespace NLog.Targets
{
	// Token: 0x0200003F RID: 63
	public interface IFileCompressor
	{
		// Token: 0x06000695 RID: 1685
		void CompressFile(string fileName, string archiveFileName);
	}
}

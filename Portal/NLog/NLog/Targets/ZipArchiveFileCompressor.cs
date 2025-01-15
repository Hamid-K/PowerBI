using System;
using System.IO;
using System.IO.Compression;

namespace NLog.Targets
{
	// Token: 0x0200005E RID: 94
	internal class ZipArchiveFileCompressor : IFileCompressor
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x000153F0 File Offset: 0x000135F0
		public void CompressFile(string fileName, string archiveFileName)
		{
			using (FileStream fileStream = new FileStream(archiveFileName, FileMode.Create))
			{
				using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
				{
					using (FileStream fileStream2 = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						using (Stream stream = zipArchive.CreateEntry(Path.GetFileName(fileName)).Open())
						{
							fileStream2.CopyTo(stream);
						}
					}
				}
			}
		}
	}
}

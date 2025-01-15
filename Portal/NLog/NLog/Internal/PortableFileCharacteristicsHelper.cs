using System;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x02000132 RID: 306
	internal class PortableFileCharacteristicsHelper : FileCharacteristicsHelper
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x000264F0 File Offset: 0x000246F0
		public override FileCharacteristics GetFileCharacteristics(string fileName, FileStream fileStream)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				FileInfo fileInfo = new FileInfo(fileName);
				if (fileInfo.Exists)
				{
					return new FileCharacteristics(fileInfo.GetCreationTimeUtc(), fileInfo.GetLastWriteTimeUtc(), fileInfo.Length);
				}
			}
			return null;
		}
	}
}

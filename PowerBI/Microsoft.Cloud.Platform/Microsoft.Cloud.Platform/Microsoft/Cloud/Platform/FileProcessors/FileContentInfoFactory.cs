using System;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.FileProcessors
{
	// Token: 0x020000EC RID: 236
	[BlockServiceProvider(typeof(IFileContentInfoFactory))]
	public class FileContentInfoFactory : Block, IFileContentInfoFactory
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x00010777 File Offset: 0x0000E977
		protected FileContentInfoFactory(string name)
			: base(name)
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017DAC File Offset: 0x00015FAC
		public FileContentInfoFactory()
			: this(typeof(FileContentInfoFactory).Name)
		{
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017DC3 File Offset: 0x00015FC3
		public IFileContentInfo CreateFileInfo(string fullPath)
		{
			return new FileContentInfo(fullPath);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00017DCB File Offset: 0x00015FCB
		public IFileContentInfo CreateFileInfo([NotNull] byte[] fileContents, DateTime lastFileModifiedTime)
		{
			return new FileContentInfo(fileContents, lastFileModifiedTime);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017DD4 File Offset: 0x00015FD4
		public string[] GetFilesInDirectory(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
		{
			if (!Directory.Exists(path))
			{
				return new string[0];
			}
			if (searchOption == SearchOption.AllDirectories)
			{
				return Directory.GetFiles(path, searchPattern, searchOption);
			}
			return Directory.GetFiles(path, searchPattern);
		}
	}
}

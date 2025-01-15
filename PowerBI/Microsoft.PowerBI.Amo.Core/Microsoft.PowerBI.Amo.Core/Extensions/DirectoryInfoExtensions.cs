using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.AnalysisServices.Extensions
{
	// Token: 0x0200014B RID: 331
	internal static class DirectoryInfoExtensions
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x0003CF0F File Offset: 0x0003B10F
		public static DirectoryInfo EnsureSubdirectory(this DirectoryInfo dir, string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(dir.FullName, path));
			if (!directoryInfo.Exists)
			{
				throw new ArgumentException(RuntimeSR.Exception_InvalidRelativePath(path, dir.FullName), "path");
			}
			return directoryInfo;
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0003CF41 File Offset: 0x0003B141
		public static bool IsEmpty(this DirectoryInfo dir, bool ignoreEmptySubDirectories = true)
		{
			dir.Refresh();
			return dir.EnumerateFiles("*", SearchOption.AllDirectories).FirstOrDefault<FileInfo>() == null && (ignoreEmptySubDirectories || dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).FirstOrDefault<DirectoryInfo>() == null);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0003CF78 File Offset: 0x0003B178
		public static void CleanupEmptySubDirectories(this DirectoryInfo dir)
		{
			dir.Refresh();
			Queue<DirectoryInfo> queue = new Queue<DirectoryInfo>();
			using (IEnumerator<DirectoryInfo> enumerator = dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DirectoryInfo directoryInfo = enumerator.Current;
					queue.Enqueue(directoryInfo);
				}
				goto IL_00A1;
			}
			IL_0042:
			DirectoryInfo directoryInfo2 = queue.Dequeue();
			if (directoryInfo2.EnumerateFiles("*", SearchOption.AllDirectories).FirstOrDefault<FileInfo>() != null)
			{
				using (IEnumerator<DirectoryInfo> enumerator = directoryInfo2.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DirectoryInfo directoryInfo3 = enumerator.Current;
						queue.Enqueue(directoryInfo3);
					}
					goto IL_00A1;
				}
			}
			try
			{
				directoryInfo2.Delete(true);
			}
			catch (IOException)
			{
			}
			IL_00A1:
			if (queue.Count <= 0)
			{
				return;
			}
			goto IL_0042;
		}
	}
}

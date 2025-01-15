using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.AnalysisServices.AdomdClient.Extensions
{
	// Token: 0x02000156 RID: 342
	internal static class DirectoryInfoExtensions
	{
		// Token: 0x060010D4 RID: 4308 RVA: 0x0003A60B File Offset: 0x0003880B
		public static DirectoryInfo EnsureSubdirectory(this DirectoryInfo dir, string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(dir.FullName, path));
			if (!directoryInfo.Exists)
			{
				throw new ArgumentException(RuntimeSR.Exception_InvalidRelativePath(path, dir.FullName), "path");
			}
			return directoryInfo;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003A63D File Offset: 0x0003883D
		public static bool IsEmpty(this DirectoryInfo dir, bool ignoreEmptySubDirectories = true)
		{
			dir.Refresh();
			return dir.EnumerateFiles("*", SearchOption.AllDirectories).FirstOrDefault<FileInfo>() == null && (ignoreEmptySubDirectories || dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).FirstOrDefault<DirectoryInfo>() == null);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003A674 File Offset: 0x00038874
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

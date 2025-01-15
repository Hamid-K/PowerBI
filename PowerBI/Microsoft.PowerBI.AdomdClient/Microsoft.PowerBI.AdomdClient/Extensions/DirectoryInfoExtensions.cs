using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.AnalysisServices.AdomdClient.Extensions
{
	// Token: 0x02000156 RID: 342
	internal static class DirectoryInfoExtensions
	{
		// Token: 0x060010C7 RID: 4295 RVA: 0x0003A2DB File Offset: 0x000384DB
		public static DirectoryInfo EnsureSubdirectory(this DirectoryInfo dir, string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(dir.FullName, path));
			if (!directoryInfo.Exists)
			{
				throw new ArgumentException(RuntimeSR.Exception_InvalidRelativePath(path, dir.FullName), "path");
			}
			return directoryInfo;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003A30D File Offset: 0x0003850D
		public static bool IsEmpty(this DirectoryInfo dir, bool ignoreEmptySubDirectories = true)
		{
			dir.Refresh();
			return dir.EnumerateFiles("*", SearchOption.AllDirectories).FirstOrDefault<FileInfo>() == null && (ignoreEmptySubDirectories || dir.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).FirstOrDefault<DirectoryInfo>() == null);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003A344 File Offset: 0x00038544
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

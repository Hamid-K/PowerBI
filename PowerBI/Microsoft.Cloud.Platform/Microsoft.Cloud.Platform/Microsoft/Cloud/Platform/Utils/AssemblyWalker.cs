using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200017E RID: 382
	public static class AssemblyWalker
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x0002248A File Offset: 0x0002068A
		public static IEnumerable<string> GetAssemblyFileNames([NotNull] string path, bool recursive, [NotNull] Predicate<string> assemblyLoadPredicate)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(path, "path");
			ExtendedDiagnostics.EnsureArgumentNotNull<Predicate<string>>(assemblyLoadPredicate, "assemblyLoadPredicate");
			SearchOption searchOptions = (recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			if (File.Exists(path))
			{
				if (assemblyLoadPredicate(path))
				{
					yield return path;
				}
			}
			else if (Directory.Exists(path))
			{
				string[] array = new string[] { "*.exe", "*.dll" };
				foreach (string text in array)
				{
					foreach (string text2 in Directory.GetFiles(path, text, searchOptions))
					{
						if (assemblyLoadPredicate(text2))
						{
							yield return text2;
						}
					}
					string[] array3 = null;
				}
				string[] array2 = null;
			}
			yield break;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000224A8 File Offset: 0x000206A8
		public static bool AssemblyHasResourceName(string filename, string resourceName)
		{
			string text;
			return ExtendedFileVersionInfo.TryGetVersionString(filename, resourceName, out text) && text.Equals("1", StringComparison.OrdinalIgnoreCase);
		}
	}
}

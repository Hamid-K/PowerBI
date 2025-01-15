using System;
using System.IO;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000207 RID: 519
	public static class ExtendedPath
	{
		// Token: 0x06000DC1 RID: 3521 RVA: 0x00030700 File Offset: 0x0002E900
		static ExtendedPath()
		{
			string text = "{0}{1}".FormatWithInvariantCulture(new object[]
			{
				string.Join<char>(string.Empty, Path.GetInvalidFileNameChars()),
				string.Join<char>(string.Empty, Path.GetInvalidPathChars())
			});
			ExtendedPath.s_invalidFilenameCharactersRegex = new Regex("[{0}]".FormatWithInvariantCulture(new object[] { Regex.Escape(text) }), RegexOptions.Compiled | RegexOptions.Singleline);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00030767 File Offset: 0x0002E967
		public static string NormalizeFilename([NotNull] string filename, [NotNull] string invalidCharacterReplacer)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(filename, "filename");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(invalidCharacterReplacer, "invalidCharacterReplacer");
			return ExtendedPath.s_invalidFilenameCharactersRegex.Replace(filename, invalidCharacterReplacer);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003078C File Offset: 0x0002E98C
		public static bool TryGetFileNameWithoutExtension([NotNull] string filePath, out string fileName)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(filePath, "filePath");
			try
			{
				fileName = Path.GetFileNameWithoutExtension(filePath);
			}
			catch (ArgumentException)
			{
				fileName = null;
			}
			return fileName != null;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000307CC File Offset: 0x0002E9CC
		public static bool TryGetFileExtension([NotNull] string filePath, out string fileExtension)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(filePath, "filePath");
			try
			{
				fileExtension = Path.GetExtension(filePath);
			}
			catch (ArgumentException)
			{
				fileExtension = null;
			}
			return fileExtension != null;
		}

		// Token: 0x0400056B RID: 1387
		private static readonly Regex s_invalidFilenameCharactersRegex;
	}
}

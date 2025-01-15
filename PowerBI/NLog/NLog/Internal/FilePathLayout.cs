using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NLog.Layouts;
using NLog.Targets;

namespace NLog.Internal
{
	// Token: 0x0200011B RID: 283
	internal class FilePathLayout : IRenderable
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x00024760 File Offset: 0x00022960
		public FilePathLayout(Layout layout, bool cleanupInvalidChars, FilePathKind filePathKind)
		{
			this._layout = layout;
			this._filePathKind = filePathKind;
			this._cleanupInvalidChars = cleanupInvalidChars;
			if (this._layout == null)
			{
				this._filePathKind = FilePathKind.Unknown;
				return;
			}
			if (cleanupInvalidChars || this._filePathKind == FilePathKind.Unknown)
			{
				this._cleanedFixedResult = FilePathLayout.CreateCleanedFixedResult(cleanupInvalidChars, layout);
				this._filePathKind = FilePathLayout.DetectKind(layout, this._filePathKind);
			}
			if (this._filePathKind == FilePathKind.Relative)
			{
				this._baseDir = LogFactory.CurrentAppDomain.BaseDirectory;
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000247DC File Offset: 0x000229DC
		private static FilePathKind DetectKind(Layout layout, FilePathKind currentFilePathKind)
		{
			SimpleLayout simpleLayout;
			if ((simpleLayout = layout as SimpleLayout) == null)
			{
				return FilePathKind.Unknown;
			}
			if (currentFilePathKind == FilePathKind.Unknown)
			{
				return FilePathLayout.DetectFilePathKind(simpleLayout);
			}
			return currentFilePathKind;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00024800 File Offset: 0x00022A00
		private static string CreateCleanedFixedResult(bool cleanupInvalidChars, Layout layout)
		{
			SimpleLayout simpleLayout;
			if ((simpleLayout = layout as SimpleLayout) != null && simpleLayout.IsFixedText)
			{
				string text = simpleLayout.FixedText;
				if (cleanupInvalidChars)
				{
					text = FilePathLayout.CleanupInvalidFilePath(text);
				}
				return text;
			}
			return null;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00024833 File Offset: 0x00022A33
		public Layout GetLayout()
		{
			return this._layout;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002483C File Offset: 0x00022A3C
		private string GetRenderedFileName(LogEventInfo logEvent, StringBuilder reusableBuilder = null)
		{
			if (this._cleanedFixedResult != null)
			{
				return this._cleanedFixedResult;
			}
			if (this._layout == null)
			{
				return null;
			}
			if (reusableBuilder == null)
			{
				return this._layout.Render(logEvent);
			}
			object obj;
			if ((!this._layout.ThreadAgnostic || this._layout.MutableUnsafe) && logEvent.TryGetCachedLayoutValue(this._layout, out obj))
			{
				return ((obj != null) ? obj.ToString() : null) ?? string.Empty;
			}
			this._layout.RenderAppendBuilder(logEvent, reusableBuilder, false);
			if (this._cachedPrevRawFileName != null && reusableBuilder.EqualTo(this._cachedPrevRawFileName))
			{
				return this._cachedPrevRawFileName;
			}
			this._cachedPrevRawFileName = reusableBuilder.ToString();
			this._cachedPrevCleanFileName = null;
			return this._cachedPrevRawFileName;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000248FC File Offset: 0x00022AFC
		private string GetCleanFileName(string rawFileName)
		{
			string text = rawFileName;
			if (this._cleanupInvalidChars && this._cleanedFixedResult == null)
			{
				text = FilePathLayout.CleanupInvalidFilePath(rawFileName);
			}
			if (this._filePathKind == FilePathKind.Absolute)
			{
				return text;
			}
			if (this._filePathKind == FilePathKind.Relative && this._baseDir != null)
			{
				return Path.Combine(this._baseDir, text);
			}
			return Path.GetFullPath(text);
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00024955 File Offset: 0x00022B55
		public string Render(LogEventInfo logEvent)
		{
			return this.RenderWithBuilder(logEvent, null);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00024960 File Offset: 0x00022B60
		internal string RenderWithBuilder(LogEventInfo logEvent, StringBuilder reusableBuilder = null)
		{
			string renderedFileName = this.GetRenderedFileName(logEvent, reusableBuilder);
			if (string.IsNullOrEmpty(renderedFileName))
			{
				return renderedFileName;
			}
			if ((!this._cleanupInvalidChars || this._cleanedFixedResult != null) && this._filePathKind == FilePathKind.Absolute)
			{
				return renderedFileName;
			}
			if (string.Equals(this._cachedPrevRawFileName, renderedFileName, StringComparison.Ordinal) && this._cachedPrevCleanFileName != null)
			{
				return this._cachedPrevCleanFileName;
			}
			string cleanFileName = this.GetCleanFileName(renderedFileName);
			this._cachedPrevCleanFileName = cleanFileName;
			this._cachedPrevRawFileName = renderedFileName;
			return cleanFileName;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000249D0 File Offset: 0x00022BD0
		internal static FilePathKind DetectFilePathKind(Layout pathLayout)
		{
			SimpleLayout simpleLayout;
			if ((simpleLayout = pathLayout as SimpleLayout) != null)
			{
				return FilePathLayout.DetectFilePathKind(simpleLayout);
			}
			return FilePathKind.Unknown;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x000249F0 File Offset: 0x00022BF0
		private static FilePathKind DetectFilePathKind(SimpleLayout pathLayout)
		{
			bool isFixedText = pathLayout.IsFixedText;
			return FilePathLayout.DetectFilePathKind(isFixedText ? pathLayout.FixedText : pathLayout.Text, isFixedText);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00024A1C File Offset: 0x00022C1C
		internal static FilePathKind DetectFilePathKind(string path, bool isFixedText = true)
		{
			if (!string.IsNullOrEmpty(path))
			{
				path = path.TrimStart(new char[0]);
				int length = path.Length;
				if (length >= 1)
				{
					char c = path[0];
					if (FilePathLayout.IsAbsoluteStartChar(c))
					{
						return FilePathKind.Absolute;
					}
					if (c == '.')
					{
						return FilePathKind.Relative;
					}
					if (length >= 2)
					{
						char c2 = path[1];
						if (Path.VolumeSeparatorChar != Path.DirectorySeparatorChar && c2 == Path.VolumeSeparatorChar)
						{
							return FilePathKind.Absolute;
						}
					}
					if (FilePathLayout.IsLayoutRenderer(path, isFixedText))
					{
						return FilePathKind.Unknown;
					}
					return FilePathKind.Relative;
				}
			}
			return FilePathKind.Unknown;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00024A93 File Offset: 0x00022C93
		private static bool IsLayoutRenderer(string path, bool isFixedText)
		{
			return !isFixedText && path.StartsWith("${", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00024AA6 File Offset: 0x00022CA6
		private static bool IsAbsoluteStartChar(char firstChar)
		{
			return firstChar == Path.DirectorySeparatorChar || firstChar == Path.AltDirectorySeparatorChar;
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00024ABC File Offset: 0x00022CBC
		private static string CleanupInvalidFilePath(string filePath)
		{
			if (StringHelpers.IsNullOrWhiteSpace(filePath))
			{
				return filePath;
			}
			int num = filePath.LastIndexOfAny(FilePathLayout.DirectorySeparatorChars);
			char[] array = null;
			for (int i = num + 1; i < filePath.Length; i++)
			{
				if (FilePathLayout.InvalidFileNameChars.Contains(filePath[i]))
				{
					if (array == null)
					{
						array = filePath.Substring(num + 1).ToCharArray();
					}
					array[i - (num + 1)] = '_';
				}
			}
			if (array != null)
			{
				string text = ((num > 0) ? filePath.Substring(0, num + 1) : string.Empty);
				string text2 = new string(array);
				return Path.Combine(text, text2);
			}
			return filePath;
		}

		// Token: 0x040003F0 RID: 1008
		private static readonly char[] DirectorySeparatorChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x040003F1 RID: 1009
		private static readonly HashSet<char> InvalidFileNameChars = new HashSet<char>(Path.GetInvalidFileNameChars());

		// Token: 0x040003F2 RID: 1010
		private readonly Layout _layout;

		// Token: 0x040003F3 RID: 1011
		private readonly FilePathKind _filePathKind;

		// Token: 0x040003F4 RID: 1012
		private readonly string _baseDir;

		// Token: 0x040003F5 RID: 1013
		private readonly string _cleanedFixedResult;

		// Token: 0x040003F6 RID: 1014
		private readonly bool _cleanupInvalidChars;

		// Token: 0x040003F7 RID: 1015
		private string _cachedPrevRawFileName;

		// Token: 0x040003F8 RID: 1016
		private string _cachedPrevCleanFileName;
	}
}

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000057 RID: 87
	internal static class CatalogItemNameUtility
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000094E8 File Offset: 0x000076E8
		public static void SplitPath(string itemPath, out string itemName, out string parentPath)
		{
			if (itemPath == null)
			{
				parentPath = null;
				itemName = null;
				return;
			}
			int num = itemPath.LastIndexOf('/');
			int num2 = 0;
			if (itemPath.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
			{
				num2 = "http://".Length;
			}
			else if (itemPath.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				num2 = "https://".Length;
			}
			if (num < num2)
			{
				parentPath = null;
				itemName = string.Empty;
				return;
			}
			parentPath = itemPath.Substring(0, num);
			itemName = itemPath.Substring(num + 1);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009562 File Offset: 0x00007762
		public static int MaxItemPathLength
		{
			get
			{
				return 260;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009569 File Offset: 0x00007769
		public static bool IsWebUrl(string path)
		{
			return path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009587 File Offset: 0x00007787
		public static string ConvertPathToInternal(string externalPath)
		{
			if (Localization.CatalogCultureCompare(externalPath, CatalogItemNameUtility.PathSeparatorString) == 0)
			{
				return string.Empty;
			}
			return externalPath;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000959D File Offset: 0x0000779D
		public static string ConvertPathToExternal(string internalPath)
		{
			if (internalPath == null || internalPath.Length == 0)
			{
				return CatalogItemNameUtility.PathSeparatorString;
			}
			return internalPath;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000095B4 File Offset: 0x000077B4
		public static bool ValidateItemPath(string path, bool isInternal)
		{
			if (path == null)
			{
				return false;
			}
			path = path.Trim(CatalogItemNameUtility.NameTrimCharacters);
			if (path.Length == 0)
			{
				return isInternal;
			}
			if (Localization.CatalogCultureCompare(path, CatalogItemNameUtility.PathSeparatorString) == 0)
			{
				return !isInternal;
			}
			return path.Length <= CatalogItemNameUtility.MaxItemPathLength && CatalogItemNameUtility.m_ValidItemPathRegex.Match(path).Success;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009610 File Offset: 0x00007810
		public static string ValidateAndTrimItemName(string name, string parameterName)
		{
			if (name == null)
			{
				throw new MissingParameterException(parameterName);
			}
			string text = name.Trim(CatalogItemNameUtility.NameTrimCharacters);
			if (text.Length == 0 || text.Length > CatalogItemNameUtility.MaxItemNameLength)
			{
				throw CatalogItemNameUtility.InvalidItemNameException(name);
			}
			if (CatalogItemNameUtility.m_ValidItemNameRegex.IsMatch(text))
			{
				return text;
			}
			throw CatalogItemNameUtility.InvalidItemNameException(name);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009664 File Offset: 0x00007864
		public static void ValidateItemName(string name, string parameterName)
		{
			string text = CatalogItemNameUtility.ValidateAndTrimItemName(name, parameterName);
			if (string.CompareOrdinal(name, text) != 0)
			{
				throw CatalogItemNameUtility.InvalidItemNameException(name);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000968C File Offset: 0x0000788C
		public static string BuildChildPath(string parentPath, string childName)
		{
			if (Localization.CatalogCultureCompare(parentPath, CatalogItemNameUtility.PathSeparatorString) == 0)
			{
				throw new InternalCatalogException("parentPath = '/' on BuildChildPath");
			}
			string text = parentPath + "/" + childName;
			if (text.Length > CatalogItemNameUtility.MaxItemPathLength)
			{
				throw new ItemPathLengthExceededException(text);
			}
			return text;
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000096D3 File Offset: 0x000078D3
		public static int MaxItemNameLength
		{
			get
			{
				return 260;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000096DC File Offset: 0x000078DC
		public static string GetNormalizedUrlPath(string urlPath)
		{
			string text2;
			try
			{
				string text = new Uri(urlPath).GetComponents(UriComponents.AbsoluteUri, UriFormat.Unescaped);
				if (text.EndsWith(CatalogItemNameUtility.PathSeparatorString, StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(0, text.Length - 1);
				}
				text2 = text;
			}
			catch (UriFormatException ex)
			{
				throw new InvalidItemPathException(urlPath, null, ex);
			}
			return text2;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009738 File Offset: 0x00007938
		private static InvalidItemNameException InvalidItemNameException(string name)
		{
			return new InvalidItemNameException(name, CatalogItemNameUtility.MaxItemNameLength);
		}

		// Token: 0x04000140 RID: 320
		public const char PathSeparatorChar = '/';

		// Token: 0x04000141 RID: 321
		public static readonly string PathSeparatorString = new string('/', 1);

		// Token: 0x04000142 RID: 322
		private static char[] NameTrimCharacters = new char[] { ' ' };

		// Token: 0x04000143 RID: 323
		public const string ExternalRootPath = "/";

		// Token: 0x04000144 RID: 324
		public const string InternalRootPath = "";

		// Token: 0x04000145 RID: 325
		private static readonly Regex m_ValidItemPathRegex = new Regex("\\A(/(\\.[^/]*)?[^/\\. ]([^/]*[^/ ])?)*\\z(?<=\\A[^;\\?:@&=\\+\\$,\\\\\\*<>\\|\"]{0," + CatalogItemNameUtility.MaxItemPathLength.ToString(CultureInfo.InvariantCulture) + "}\\z)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000146 RID: 326
		private static readonly Regex m_ValidItemNameRegex = new Regex("\\A(\\..*)?[^\\. ](.*[^ ])?\\z(?<=\\A[^/;\\?:@&=\\+\\$,\\\\\\*<>\\|\"]{0," + CatalogItemNameUtility.MaxItemPathLength.ToString(CultureInfo.InvariantCulture) + "}\\z)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x04000147 RID: 327
		private static readonly Regex m_ValidSharePointItemNameRegex = new Regex("\\A[^\\.]+(\\.[^\\.]+)*\\z(?<=\\A[^~\"#%&\\*:<>\\?\\{\\}\\|/]{1," + CatalogItemNameUtility.MaxItemPathLength.ToString(CultureInfo.InvariantCulture) + "}\\z)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}

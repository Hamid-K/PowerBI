using System;
using System.Linq;
using System.Text;

namespace Microsoft.ReportingServices.Portal.Services.Util
{
	// Token: 0x02000024 RID: 36
	internal static class CatalogItemPathUtils
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000CE4C File Offset: 0x0000B04C
		public static string EnsureSeparators(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return path.Trim(new char[] { '/' }).Insert(0, "/");
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000CE78 File Offset: 0x0000B078
		public static string ConcatPathSegments(params string[] segments)
		{
			if (!segments.Any<string>())
			{
				return "/";
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in segments)
			{
				if (text == null)
				{
					throw new ArgumentNullException("segment");
				}
				if (!(text == "/"))
				{
					stringBuilder.Append(CatalogItemPathUtils.EnsureSeparators(text));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000CEDC File Offset: 0x0000B0DC
		public static bool IsParentOf(string path, string basePath)
		{
			string[] array = CatalogItemPathUtils.EnsureSeparators(path).Split(new char[] { '/' });
			string[] array2 = CatalogItemPathUtils.EnsureSeparators(basePath).Split(new char[] { '/' });
			if (array.Length != array2.Length + 1)
			{
				return false;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (string.Compare(array2[i], array[i], true) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400008D RID: 141
		private const char PathSeparatorChar = '/';

		// Token: 0x0400008E RID: 142
		private const string PathSeparator = "/";
	}
}

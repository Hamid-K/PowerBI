using System;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C10 RID: 7184
	public static class PathMethods
	{
		// Token: 0x0600B340 RID: 45888 RVA: 0x00247618 File Offset: 0x00245818
		public static string ConvertToFilename(string text)
		{
			int num = text.IndexOfAny(PathMethods.specialFilenameCharacters);
			if (num < 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			for (int i = num; i < stringBuilder.Length; i++)
			{
				int num2 = PathMethods.specialFilenameString.IndexOf(stringBuilder[i]);
				if (num2 >= 0)
				{
					stringBuilder.Insert(i++, '@');
					stringBuilder[i] = (char)(65 + num2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600B341 RID: 45889 RVA: 0x00247688 File Offset: 0x00245888
		public static string CombineUrl(this string path, string subpath)
		{
			if (string.IsNullOrEmpty(subpath))
			{
				return path;
			}
			if (string.IsNullOrEmpty(path))
			{
				return subpath;
			}
			path = path.TrimEnd(new char[] { '/' });
			subpath = subpath.TrimStart(new char[] { '/' });
			return path + "/" + subpath;
		}

		// Token: 0x04005B72 RID: 23410
		private const char filenameEscapeCharacter = '@';

		// Token: 0x04005B73 RID: 23411
		private static readonly char[] specialFilenameCharacters = Path.GetInvalidFileNameChars().Add('@');

		// Token: 0x04005B74 RID: 23412
		private static readonly string specialFilenameString = new string(PathMethods.specialFilenameCharacters);
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000369 RID: 873
	public static class NameUtils
	{
		// Token: 0x060019FE RID: 6654 RVA: 0x000602F4 File Offset: 0x0005E4F4
		public static string CapitalizeNameFirstLetter([NotNull] string name)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			string text = name.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture);
			string text2 = string.Empty;
			if (name.Length > 1)
			{
				text2 = name.Substring(1, name.Length - 1);
			}
			return text + text2;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00060343 File Offset: 0x0005E543
		public static string FixNameWithUnderscores(string name)
		{
			return NameUtils.Replace(name, new string[] { ".", "(", ")" }, "_");
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00060370 File Offset: 0x0005E570
		private static string Replace(string name, IEnumerable<string> oldStrings, string newString)
		{
			StringBuilder stringBuilder = new StringBuilder(name);
			foreach (string text in oldStrings)
			{
				stringBuilder.Replace(text, newString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000603C8 File Offset: 0x0005E5C8
		public static string MemberName(string name)
		{
			return string.Format(CultureInfo.InvariantCulture, "m_{0}", new object[] { name });
		}
	}
}

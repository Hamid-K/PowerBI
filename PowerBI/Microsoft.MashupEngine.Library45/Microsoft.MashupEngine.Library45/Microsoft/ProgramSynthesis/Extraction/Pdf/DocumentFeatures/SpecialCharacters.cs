using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures
{
	// Token: 0x02000D74 RID: 3444
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SpecialCharacters
	{
		// Token: 0x060057CF RID: 22479 RVA: 0x001170CF File Offset: 0x001152CF
		private static bool IsHyphen(char ch)
		{
			return SpecialCharacters.Hyphens.Contains(ch);
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x001170DC File Offset: 0x001152DC
		internal static bool StartsWithHyphen(string str)
		{
			return str.Length > 0 && SpecialCharacters.IsHyphen(str[0]);
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x001170F5 File Offset: 0x001152F5
		internal static bool EndsWithHyphen(string str)
		{
			return str.Length > 0 && SpecialCharacters.IsHyphen(str[str.Length - 1]);
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x00117115 File Offset: 0x00115315
		internal static bool EndsWithColon(string str)
		{
			return str.Length > 0 && str[str.Length - 1] == ':';
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00117134 File Offset: 0x00115334
		internal static bool IsBulletPoint(string str)
		{
			return str.Length == 1 && SpecialCharacters.BulletPoints.Contains(str[0]);
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00117152 File Offset: 0x00115352
		internal static bool IsBulletPointOrHyphen(string str)
		{
			return str.Length == 1 && (SpecialCharacters.BulletPoints.Contains(str[0]) || SpecialCharacters.Hyphens.Contains(str[0]));
		}

		// Token: 0x060057D5 RID: 22485 RVA: 0x00117185 File Offset: 0x00115385
		internal static bool StartsWithCurrency(string str)
		{
			return SpecialCharacters.CurrencyAtStartRegex.IsMatch(str);
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x00117192 File Offset: 0x00115392
		internal static bool EndsWithCurrency(string str)
		{
			return SpecialCharacters.CurrencyAtEndRegex.IsMatch(str);
		}

		// Token: 0x0400288B RID: 10379
		private const char Colon = ':';

		// Token: 0x0400288C RID: 10380
		private static readonly char[] Hyphens = new char[] { '-', '\u00ad' };

		// Token: 0x0400288D RID: 10381
		private static readonly char[] BulletPoints = new char[] { '•', '‣', '⁃', '○', '●', '◘', '◙', '◦', '⦾', '⦿' };

		// Token: 0x0400288E RID: 10382
		private static readonly Regex CurrencyAtStartRegex = new Regex("^\\p{Sc}", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

		// Token: 0x0400288F RID: 10383
		private static readonly Regex CurrencyAtEndRegex = new Regex("\\p{Sc}$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
	}
}

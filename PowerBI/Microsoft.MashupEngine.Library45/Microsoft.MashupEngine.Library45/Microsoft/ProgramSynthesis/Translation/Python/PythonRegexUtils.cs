using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000315 RID: 789
	public static class PythonRegexUtils
	{
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0003312E File Offset: 0x0003132E
		private static Regex BackReferenceRegex
		{
			get
			{
				return PythonRegexUtils._backReferenceRegexLazy.Value;
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003313C File Offset: 0x0003133C
		public static IEnumerable<string> ConvertToPythonRegEx(RegularExpression regEx)
		{
			return from AbstractRegexToken t in regEx.Tokens
				select t.Regex into r
				select PythonRegexUtils.ConvertToPythonRegEx(r);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0003319C File Offset: 0x0003139C
		public static string ConvertToPythonRegEx(Regex regEx)
		{
			string text = regEx.ToString();
			return PythonRegexUtils.BackReferenceRegex.Replace(text, (Match m) => FormattableString.Invariant(FormattableStringFactory.Create("(?P={0})", new object[] { m.Groups[1].Value })));
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000331DC File Offset: 0x000313DC
		public static string ConvertToPythonRegExStr(RegularExpression regEx, out string name)
		{
			IEnumerable<AbstractRegexToken> enumerable = regEx.Tokens.Cast<AbstractRegexToken>();
			name = string.Join("_", enumerable.Select((AbstractRegexToken x) => x.Name));
			return string.Concat(enumerable.Select((AbstractRegexToken x) => PythonRegexUtils.ConvertToPythonRegEx(x.Regex)));
		}

		// Token: 0x04000876 RID: 2166
		private static readonly Lazy<Regex> _backReferenceRegexLazy = new Lazy<Regex>(() => new Regex("\\\\k<(.+)>", RegexOptions.Compiled));
	}
}

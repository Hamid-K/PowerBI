using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001FB RID: 507
	public class PhoneNumberToken : ValueBasedEntityToken
	{
		// Token: 0x06000AF8 RID: 2808 RVA: 0x0001C631 File Offset: 0x0001A831
		public PhoneNumberToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x000203A7 File Offset: 0x0001E5A7
		public override double ScoreMultiplier
		{
			get
			{
				return 5.0;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x00021883 File Offset: 0x0001FA83
		public override string EntityName
		{
			get
			{
				return "PhoneNumber";
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002188C File Offset: 0x0001FA8C
		private string CanonicalizePhoneNumber(string s)
		{
			bool flag = s.StartsWith("+", StringComparison.Ordinal);
			if (flag)
			{
				s = s.Substring(1);
			}
			string text = PhoneNumberToken.SeparatorRegex.Replace(s, "-");
			if (!flag)
			{
				return text;
			}
			return "+" + text;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000218D4 File Offset: 0x0001FAD4
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			string text = base.Value.Trim();
			string text2 = this.CanonicalizePhoneNumber(text);
			tree.Add(text, new CompletionInfo(text, this, 1.0, null));
			tree.Add(text2, new CompletionInfo(text2, this, 1.0, null));
			string text3 = text2.Replace("-", " ");
			tree.Add(text3, new CompletionInfo(text3, this, 1.0, null));
		}

		// Token: 0x04000595 RID: 1429
		private static readonly Regex DigitsRegex = new Regex("\\d+", RegexOptions.Compiled);

		// Token: 0x04000596 RID: 1430
		private static readonly Regex SeparatorRegex = new Regex("[\\-\\.\\s]", RegexOptions.Compiled);
	}
}

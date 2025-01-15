using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000203 RID: 515
	internal class PunctuationSeparatedAlphaNumericToken : ValueBasedEntityToken
	{
		// Token: 0x06000B16 RID: 2838 RVA: 0x0001C631 File Offset: 0x0001A831
		public PunctuationSeparatedAlphaNumericToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		public override double ScoreMultiplier
		{
			get
			{
				return 2.0;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00021E99 File Offset: 0x00020099
		public override string EntityName
		{
			get
			{
				return "PunctuationSeparatedAlphaNumeric";
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00021EA0 File Offset: 0x000200A0
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
			foreach (Match match in PunctuationSeparatedAlphaNumericToken.SplitRegex.NonCachingMatches(base.Value))
			{
				string text = base.Value.Substring(match.Index + 1);
				tree.Add(text, new CompletionInfo(text, this, (double)text.Length / (double)base.Value.Length, null));
			}
		}

		// Token: 0x040005B5 RID: 1461
		private static readonly Regex SplitRegex = new Regex("\\p{P}", RegexOptions.Compiled);
	}
}

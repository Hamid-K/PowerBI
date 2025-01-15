using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000211 RID: 529
	internal class TitleCaseToken : ValueBasedEntityToken
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0001C631 File Offset: 0x0001A831
		public TitleCaseToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0001C63C File Offset: 0x0001A83C
		public override double ScoreMultiplier
		{
			get
			{
				return 1.5;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00022D6F File Offset: 0x00020F6F
		public override string EntityName
		{
			get
			{
				return "TitleCaseWord";
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00022D78 File Offset: 0x00020F78
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
			foreach (Match match in TitleCaseToken.SplitRegex.NonCachingMatches(base.Value))
			{
				string text = base.Value.Substring(match.Index, base.Value.Length - match.Index);
				tree.Add(text, new CompletionInfo(text, this, 1.0 - (double)text.Length / (double)base.Value.Length, null));
			}
		}

		// Token: 0x040005EC RID: 1516
		private static readonly Regex SplitRegex = new Regex("\\p{Lu}", RegexOptions.Compiled);
	}
}

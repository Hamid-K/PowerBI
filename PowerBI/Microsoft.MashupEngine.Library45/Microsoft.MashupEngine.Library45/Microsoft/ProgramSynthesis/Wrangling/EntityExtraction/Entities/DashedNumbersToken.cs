using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001C3 RID: 451
	public class DashedNumbersToken : ValueBasedEntityToken
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x0001C631 File Offset: 0x0001A831
		public DashedNumbersToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0001CD2B File Offset: 0x0001AF2B
		public override double ScoreMultiplier
		{
			get
			{
				return 3.0;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001CD36 File Offset: 0x0001AF36
		public override string EntityName
		{
			get
			{
				return "DashedNumbers";
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001CD40 File Offset: 0x0001AF40
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
			foreach (int num in from m in DashedNumbersToken.SplitRegex.NonCachingMatches(base.Value)
				select m.Index + 1)
			{
				string text = base.Value.Substring(num, base.Value.Length - num);
				tree.Add(text, new CompletionInfo(text, this, (double)(base.Value.Length - num) / (double)base.Value.Length, null));
			}
		}

		// Token: 0x040004BA RID: 1210
		private static readonly Regex SplitRegex = new Regex("\\p{Pd}", RegexOptions.Compiled);
	}
}

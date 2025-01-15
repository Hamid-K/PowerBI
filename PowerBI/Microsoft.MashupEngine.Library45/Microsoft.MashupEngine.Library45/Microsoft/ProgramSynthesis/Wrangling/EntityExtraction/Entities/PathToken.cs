using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001F7 RID: 503
	public class PathToken : ValueBasedEntityToken
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001C631 File Offset: 0x0001A831
		public PathToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00021063 File Offset: 0x0001F263
		public override double ScoreMultiplier
		{
			get
			{
				return 16.0;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002106E File Offset: 0x0001F26E
		public override string EntityName
		{
			get
			{
				return "Path";
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00021078 File Offset: 0x0001F278
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
			foreach (Match match in PathToken.PathSeparator.NonCachingMatches(base.Value))
			{
				string text = base.Value.Substring(match.Index, base.Value.Length - match.Index);
				string text2 = base.Value.Substring(match.Index + 1, base.Value.Length - match.Index - 1);
				tree.Add(text, new CompletionInfo(text, this, (double)text.Length / (double)base.Value.Length, null));
				tree.Add(text2, new CompletionInfo(text2, this, (double)text2.Length / (double)base.Value.Length, null));
			}
		}

		// Token: 0x04000574 RID: 1396
		private static readonly Regex PathSeparator = new Regex("\\\\|/", RegexOptions.Compiled);
	}
}

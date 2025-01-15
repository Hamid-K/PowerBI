using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000214 RID: 532
	public class UrlToken : ValueBasedEntityToken
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x0001C631 File Offset: 0x0001A831
		public UrlToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00022EE1 File Offset: 0x000210E1
		public override double ScoreMultiplier
		{
			get
			{
				return 1.2;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00022EEC File Offset: 0x000210EC
		public override string EntityName
		{
			get
			{
				return "URL";
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

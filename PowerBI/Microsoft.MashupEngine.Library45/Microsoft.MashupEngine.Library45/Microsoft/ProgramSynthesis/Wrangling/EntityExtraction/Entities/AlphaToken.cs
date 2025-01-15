using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001B7 RID: 439
	internal class AlphaToken : ValueBasedEntityToken
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0001C631 File Offset: 0x0001A831
		public AlphaToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		public override double ScoreMultiplier
		{
			get
			{
				return 2.0;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001C704 File Offset: 0x0001A904
		public override string EntityName
		{
			get
			{
				return "Alpha";
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

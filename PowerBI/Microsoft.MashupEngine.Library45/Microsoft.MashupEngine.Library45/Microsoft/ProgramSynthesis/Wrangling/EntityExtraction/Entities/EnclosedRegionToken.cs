using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001DA RID: 474
	internal class EnclosedRegionToken : ValueBasedEntityToken
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0001C631 File Offset: 0x0001A831
		public EnclosedRegionToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001C63C File Offset: 0x0001A83C
		public override double ScoreMultiplier
		{
			get
			{
				return 1.5;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x0001FA87 File Offset: 0x0001DC87
		public override string EntityName
		{
			get
			{
				return "Enclosed Region";
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

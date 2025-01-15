using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001BA RID: 442
	internal class CharCategoryBasedToken : ValueBasedEntityToken
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0001C791 File Offset: 0x0001A991
		public CharCategoryBasedToken(string sourceAsString, int start, int end, UnicodeCategory charCategory)
			: base(sourceAsString, start, end)
		{
			this.CharCategory = charCategory;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		public UnicodeCategory CharCategory { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00012DE5 File Offset: 0x00010FE5
		public override double ScoreMultiplier
		{
			get
			{
				return 1.0;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0001C7B5 File Offset: 0x0001A9B5
		public override string EntityName
		{
			get
			{
				return "CharCategoryBasedToken";
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001B4 RID: 436
	internal class AlphaNumericToken : ValueBasedEntityToken
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0001C631 File Offset: 0x0001A831
		public AlphaNumericToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001C63C File Offset: 0x0001A83C
		public override double ScoreMultiplier
		{
			get
			{
				return 1.5;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001C647 File Offset: 0x0001A847
		public override string EntityName
		{
			get
			{
				return "AlphaNumeric";
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

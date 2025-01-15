using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x02000200 RID: 512
	internal class PhraseToken : ValueBasedEntityToken
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x0001C631 File Offset: 0x0001A831
		public PhraseToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x0001C6F9 File Offset: 0x0001A8F9
		public override double ScoreMultiplier
		{
			get
			{
				return 2.0;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00021DFF File Offset: 0x0001FFFF
		public override string EntityName
		{
			get
			{
				return "Phrase";
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

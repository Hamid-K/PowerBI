using System;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001ED RID: 493
	public class MacAddressToken : ValueBasedEntityToken
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x0001C631 File Offset: 0x0001A831
		public MacAddressToken(string source, int start, int end)
			: base(source, start, end)
		{
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0001EA46 File Offset: 0x0001CC46
		public override double ScoreMultiplier
		{
			get
			{
				return 10.0;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00020523 File Offset: 0x0001E723
		public override string EntityName
		{
			get
			{
				return "MAC Address";
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0001C64E File Offset: 0x0001A84E
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
		}
	}
}

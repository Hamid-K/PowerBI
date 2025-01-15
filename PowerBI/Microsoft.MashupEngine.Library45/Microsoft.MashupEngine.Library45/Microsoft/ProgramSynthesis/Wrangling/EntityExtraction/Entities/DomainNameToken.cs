using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001D3 RID: 467
	public class DomainNameToken : ValueBasedEntityToken
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0001EA17 File Offset: 0x0001CC17
		public DomainNameToken(string source, int start, int end)
			: base(source, start, end)
		{
			this.Components = base.Value.Split(new char[] { '.' });
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0001EA3E File Offset: 0x0001CC3E
		public IReadOnlyList<string> Components { get; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0001EA46 File Offset: 0x0001CC46
		public override double ScoreMultiplier
		{
			get
			{
				return 10.0;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001EA51 File Offset: 0x0001CC51
		public override string EntityName
		{
			get
			{
				return "Domain Name";
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0001EA58 File Offset: 0x0001CC58
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			tree.Add(base.Value, new CompletionInfo(base.Value, this, 1.0, null));
			foreach (string text in this.Components)
			{
				tree.Add(text, new CompletionInfo(text, this, 0.8, null));
			}
		}
	}
}

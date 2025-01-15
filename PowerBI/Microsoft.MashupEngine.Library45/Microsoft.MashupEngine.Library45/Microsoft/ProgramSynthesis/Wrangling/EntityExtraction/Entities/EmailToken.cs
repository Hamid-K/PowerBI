using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001D7 RID: 471
	public class EmailToken : ValueBasedEntityToken
	{
		// Token: 0x06000A4B RID: 2635 RVA: 0x0001F8D3 File Offset: 0x0001DAD3
		public EmailToken(string source, int start, int end, string userName, string domainName)
			: base(source, start, end)
		{
			this._userName = userName;
			this._domainName = domainName;
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001EA46 File Offset: 0x0001CC46
		public override double ScoreMultiplier
		{
			get
			{
				return 10.0;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001F8EE File Offset: 0x0001DAEE
		public override string EntityName
		{
			get
			{
				return "Email";
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001F8F8 File Offset: 0x0001DAF8
		public override void MakeSearchTreeEntries(IAutoCompleteSearchTree tree, bool includeNonExtensionCompletions = false)
		{
			string text = FormattableString.Invariant(FormattableStringFactory.Create("{0}@{1}", new object[] { this._userName, this._domainName }));
			tree.Add(text, new CompletionInfo(text, this, 1.0, null));
			tree.Add(this._userName, new CompletionInfo(this._userName, this, 0.8, null));
			tree.Add(this._domainName, new CompletionInfo(this._domainName, this, 0.8, null));
		}

		// Token: 0x04000522 RID: 1314
		private readonly string _domainName;

		// Token: 0x04000523 RID: 1315
		private readonly string _userName;
	}
}

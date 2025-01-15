using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F4 RID: 756
	public sealed class FindConversationResults
	{
		// Token: 0x06001AB5 RID: 6837 RVA: 0x0004829C File Offset: 0x0004729C
		internal FindConversationResults()
		{
			this.Conversations = new Collection<Conversation>();
			this.HighlightTerms = new Collection<HighlightTerm>();
			this.TotalCount = default(int?);
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x000482D4 File Offset: 0x000472D4
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x000482DC File Offset: 0x000472DC
		public Collection<Conversation> Conversations { get; internal set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000482E5 File Offset: 0x000472E5
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x000482ED File Offset: 0x000472ED
		public Collection<HighlightTerm> HighlightTerms { get; internal set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x000482F6 File Offset: 0x000472F6
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x000482FE File Offset: 0x000472FE
		public int? TotalCount { get; internal set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x00048307 File Offset: 0x00047307
		// (set) Token: 0x06001ABD RID: 6845 RVA: 0x0004830F File Offset: 0x0004730F
		public int? IndexedOffset { get; internal set; }
	}
}

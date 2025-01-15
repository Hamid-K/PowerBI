using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000279 RID: 633
	public sealed class AtomFeedMetadata : ODataAnnotatable
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00049C5F File Offset: 0x00047E5F
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00049C67 File Offset: 0x00047E67
		public IEnumerable<AtomPersonMetadata> Authors { get; set; }

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00049C70 File Offset: 0x00047E70
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x00049C78 File Offset: 0x00047E78
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00049C81 File Offset: 0x00047E81
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x00049C89 File Offset: 0x00047E89
		public IEnumerable<AtomPersonMetadata> Contributors { get; set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x00049C92 File Offset: 0x00047E92
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00049C9A File Offset: 0x00047E9A
		public AtomGeneratorMetadata Generator { get; set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x00049CA3 File Offset: 0x00047EA3
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00049CAB File Offset: 0x00047EAB
		public Uri Icon { get; set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x00049CB4 File Offset: 0x00047EB4
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x00049CBC File Offset: 0x00047EBC
		public IEnumerable<AtomLinkMetadata> Links { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x00049CC5 File Offset: 0x00047EC5
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x00049CCD File Offset: 0x00047ECD
		public Uri Logo { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x00049CD6 File Offset: 0x00047ED6
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x00049CDE File Offset: 0x00047EDE
		public AtomTextConstruct Rights { get; set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x00049CE7 File Offset: 0x00047EE7
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x00049CEF File Offset: 0x00047EEF
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x00049CF8 File Offset: 0x00047EF8
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x00049D00 File Offset: 0x00047F00
		public AtomLinkMetadata NextPageLink { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00049D09 File Offset: 0x00047F09
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x00049D11 File Offset: 0x00047F11
		public string SourceId { get; set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00049D1A File Offset: 0x00047F1A
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x00049D22 File Offset: 0x00047F22
		public AtomTextConstruct Subtitle { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x00049D2B File Offset: 0x00047F2B
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x00049D33 File Offset: 0x00047F33
		public AtomTextConstruct Title { get; set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00049D3C File Offset: 0x00047F3C
		// (set) Token: 0x060013D4 RID: 5076 RVA: 0x00049D44 File Offset: 0x00047F44
		public DateTimeOffset? Updated { get; set; }
	}
}

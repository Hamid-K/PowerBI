using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200027A RID: 634
	public sealed class AtomEntryMetadata : ODataAnnotatable
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00049D55 File Offset: 0x00047F55
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00049D5D File Offset: 0x00047F5D
		public IEnumerable<AtomPersonMetadata> Authors { get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00049D66 File Offset: 0x00047F66
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x00049D6E File Offset: 0x00047F6E
		public AtomCategoryMetadata CategoryWithTypeName { get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00049D77 File Offset: 0x00047F77
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00049D7F File Offset: 0x00047F7F
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00049D88 File Offset: 0x00047F88
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x00049D90 File Offset: 0x00047F90
		public IEnumerable<AtomPersonMetadata> Contributors { get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00049D99 File Offset: 0x00047F99
		// (set) Token: 0x060013DF RID: 5087 RVA: 0x00049DA1 File Offset: 0x00047FA1
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00049DAA File Offset: 0x00047FAA
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x00049DB2 File Offset: 0x00047FB2
		public AtomLinkMetadata EditLink { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00049DBB File Offset: 0x00047FBB
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00049DC3 File Offset: 0x00047FC3
		public IEnumerable<AtomLinkMetadata> Links { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00049DCC File Offset: 0x00047FCC
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00049DD4 File Offset: 0x00047FD4
		public DateTimeOffset? Published { get; set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00049DDD File Offset: 0x00047FDD
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00049DE5 File Offset: 0x00047FE5
		public AtomTextConstruct Rights { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00049DEE File Offset: 0x00047FEE
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00049DF6 File Offset: 0x00047FF6
		public AtomFeedMetadata Source { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00049DFF File Offset: 0x00047FFF
		// (set) Token: 0x060013EB RID: 5099 RVA: 0x00049E07 File Offset: 0x00048007
		public AtomTextConstruct Summary { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x00049E10 File Offset: 0x00048010
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x00049E18 File Offset: 0x00048018
		public AtomTextConstruct Title { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00049E21 File Offset: 0x00048021
		// (set) Token: 0x060013EF RID: 5103 RVA: 0x00049E29 File Offset: 0x00048029
		public DateTimeOffset? Updated { get; set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00049E32 File Offset: 0x00048032
		// (set) Token: 0x060013F1 RID: 5105 RVA: 0x00049E3A File Offset: 0x0004803A
		internal string PublishedString
		{
			get
			{
				return this.publishedString;
			}
			set
			{
				this.publishedString = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00049E43 File Offset: 0x00048043
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00049E4B File Offset: 0x0004804B
		internal string UpdatedString
		{
			get
			{
				return this.updatedString;
			}
			set
			{
				this.updatedString = value;
			}
		}

		// Token: 0x0400079D RID: 1949
		private string publishedString;

		// Token: 0x0400079E RID: 1950
		private string updatedString;
	}
}

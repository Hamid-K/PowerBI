using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000011 RID: 17
	public sealed class AtomEntryMetadata : ODataAnnotatable
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C38 File Offset: 0x00000E38
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002C40 File Offset: 0x00000E40
		public IEnumerable<AtomPersonMetadata> Authors { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002C49 File Offset: 0x00000E49
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002C51 File Offset: 0x00000E51
		public AtomCategoryMetadata CategoryWithTypeName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002C5A File Offset: 0x00000E5A
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002C62 File Offset: 0x00000E62
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C6B File Offset: 0x00000E6B
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002C73 File Offset: 0x00000E73
		public IEnumerable<AtomPersonMetadata> Contributors { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002C7C File Offset: 0x00000E7C
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002C84 File Offset: 0x00000E84
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002C8D File Offset: 0x00000E8D
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002C95 File Offset: 0x00000E95
		public AtomLinkMetadata EditLink { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C9E File Offset: 0x00000E9E
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002CA6 File Offset: 0x00000EA6
		public IEnumerable<AtomLinkMetadata> Links { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002CAF File Offset: 0x00000EAF
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public DateTimeOffset? Published { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002CC0 File Offset: 0x00000EC0
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public AtomTextConstruct Rights { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002CD1 File Offset: 0x00000ED1
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002CD9 File Offset: 0x00000ED9
		public AtomFeedMetadata Source { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002CE2 File Offset: 0x00000EE2
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002CEA File Offset: 0x00000EEA
		public AtomTextConstruct Summary { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002CF3 File Offset: 0x00000EF3
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002CFB File Offset: 0x00000EFB
		public AtomTextConstruct Title { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002D04 File Offset: 0x00000F04
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002D0C File Offset: 0x00000F0C
		public DateTimeOffset? Updated { get; set; }
	}
}

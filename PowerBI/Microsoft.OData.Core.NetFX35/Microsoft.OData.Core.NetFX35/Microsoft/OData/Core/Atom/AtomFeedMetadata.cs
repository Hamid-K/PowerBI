using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000012 RID: 18
	public sealed class AtomFeedMetadata : ODataAnnotatable
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002D1D File Offset: 0x00000F1D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002D25 File Offset: 0x00000F25
		public IEnumerable<AtomPersonMetadata> Authors { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002D2E File Offset: 0x00000F2E
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002D36 File Offset: 0x00000F36
		public IEnumerable<AtomCategoryMetadata> Categories { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002D3F File Offset: 0x00000F3F
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002D47 File Offset: 0x00000F47
		public IEnumerable<AtomPersonMetadata> Contributors { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002D50 File Offset: 0x00000F50
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002D58 File Offset: 0x00000F58
		public AtomGeneratorMetadata Generator { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002D61 File Offset: 0x00000F61
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002D69 File Offset: 0x00000F69
		public Uri Icon { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002D72 File Offset: 0x00000F72
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002D7A File Offset: 0x00000F7A
		public IEnumerable<AtomLinkMetadata> Links { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002D83 File Offset: 0x00000F83
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002D8B File Offset: 0x00000F8B
		public Uri Logo { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002D94 File Offset: 0x00000F94
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002D9C File Offset: 0x00000F9C
		public AtomTextConstruct Rights { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002DA5 File Offset: 0x00000FA5
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002DAD File Offset: 0x00000FAD
		public AtomLinkMetadata SelfLink { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002DB6 File Offset: 0x00000FB6
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002DBE File Offset: 0x00000FBE
		public AtomLinkMetadata NextPageLink { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002DC7 File Offset: 0x00000FC7
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002DCF File Offset: 0x00000FCF
		public Uri SourceId { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002DD8 File Offset: 0x00000FD8
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public AtomTextConstruct Subtitle { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002DE9 File Offset: 0x00000FE9
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public AtomTextConstruct Title { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002DFA File Offset: 0x00000FFA
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002E02 File Offset: 0x00001002
		public DateTimeOffset? Updated { get; set; }
	}
}

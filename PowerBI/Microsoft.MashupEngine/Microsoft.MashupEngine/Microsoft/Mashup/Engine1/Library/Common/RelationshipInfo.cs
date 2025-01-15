using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200107C RID: 4220
	public sealed class RelationshipInfo
	{
		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x06006E88 RID: 28296 RVA: 0x0017DC37 File Offset: 0x0017BE37
		// (set) Token: 0x06006E89 RID: 28297 RVA: 0x0017DC3F File Offset: 0x0017BE3F
		public SchemaItem Foreign { get; set; }

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x06006E8A RID: 28298 RVA: 0x0017DC48 File Offset: 0x0017BE48
		// (set) Token: 0x06006E8B RID: 28299 RVA: 0x0017DC50 File Offset: 0x0017BE50
		public SchemaItem Primary { get; set; }

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x06006E8C RID: 28300 RVA: 0x0017DC59 File Offset: 0x0017BE59
		public IDictionary<long, string> ReferringColumns
		{
			get
			{
				return this.referringColumns;
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x06006E8D RID: 28301 RVA: 0x0017DC61 File Offset: 0x0017BE61
		public IDictionary<long, string> TargetColumns
		{
			get
			{
				return this.targetColumns;
			}
		}

		// Token: 0x04003D58 RID: 15704
		private readonly IDictionary<long, string> referringColumns = new SortedDictionary<long, string>();

		// Token: 0x04003D59 RID: 15705
		private readonly IDictionary<long, string> targetColumns = new SortedDictionary<long, string>();
	}
}

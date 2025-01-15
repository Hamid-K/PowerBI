using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200002D RID: 45
	public sealed class CustomQueryOptionQueryNode : QueryNode
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005D71 File Offset: 0x00003F71
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005D79 File Offset: 0x00003F79
		public string Name { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005D82 File Offset: 0x00003F82
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005D8A File Offset: 0x00003F8A
		public string Value { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005D93 File Offset: 0x00003F93
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.CustomQueryOption;
			}
		}
	}
}

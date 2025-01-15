using System;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x0200005A RID: 90
	public class NamedArgument
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00014A8D File Offset: 0x00012C8D
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00014A95 File Offset: 0x00012C95
		public string Name { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00014A9E File Offset: 0x00012C9E
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00014AA6 File Offset: 0x00012CA6
		public Expression Value { get; set; }
	}
}

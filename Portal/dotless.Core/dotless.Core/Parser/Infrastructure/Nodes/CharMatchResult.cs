using System;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x0200005D RID: 93
	public class CharMatchResult : TextNode
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00014F68 File Offset: 0x00013168
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x00014F70 File Offset: 0x00013170
		public char Char { get; set; }

		// Token: 0x0600040A RID: 1034 RVA: 0x00014F79 File Offset: 0x00013179
		public CharMatchResult(char c)
			: base(c.ToString())
		{
			this.Char = c;
		}
	}
}

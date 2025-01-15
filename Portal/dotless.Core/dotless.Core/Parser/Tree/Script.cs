using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000048 RID: 72
	public class Script : Node
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000DC53 File Offset: 0x0000BE53
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000DC5B File Offset: 0x0000BE5B
		public string Expression { get; set; }

		// Token: 0x060002EE RID: 750 RVA: 0x0000DC64 File Offset: 0x0000BE64
		public Script(string script)
		{
			this.Expression = script;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000DC73 File Offset: 0x0000BE73
		protected override Node CloneCore()
		{
			return new Script(this.Expression);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000DC80 File Offset: 0x0000BE80
		public override Node Evaluate(Env env)
		{
			return new TextNode("[script unsupported]");
		}
	}
}

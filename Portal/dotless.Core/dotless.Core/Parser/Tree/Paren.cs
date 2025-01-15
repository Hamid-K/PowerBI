using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000043 RID: 67
	public class Paren : Node
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public Node Value { get; set; }

		// Token: 0x0600029A RID: 666 RVA: 0x0000C6E1 File Offset: 0x0000A8E1
		public Paren(Node value)
		{
			this.Value = value;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		protected override Node CloneCore()
		{
			return new Paren(this.Value.Clone());
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C702 File Offset: 0x0000A902
		public override void AppendCSS(Env env)
		{
			env.Output.Append(new char?('(')).Append(this.Value).Append(new char?(')'));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C72E File Offset: 0x0000A92E
		public override Node Evaluate(Env env)
		{
			return new Paren(this.Value.Evaluate(env));
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C741 File Offset: 0x0000A941
		public override void Accept(IVisitor visitor)
		{
			this.Value = base.VisitAndReplace<Node>(this.Value, visitor);
		}
	}
}

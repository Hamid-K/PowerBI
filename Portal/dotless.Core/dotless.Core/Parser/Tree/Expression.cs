using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000036 RID: 54
	public class Expression : Node
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000A529 File Offset: 0x00008729
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000A531 File Offset: 0x00008731
		public NodeList Value { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000A53A File Offset: 0x0000873A
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000A542 File Offset: 0x00008742
		private bool IsExpressionList { get; set; }

		// Token: 0x0600020F RID: 527 RVA: 0x0000A54B File Offset: 0x0000874B
		public Expression(IEnumerable<Node> value)
			: this(value, false)
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A555 File Offset: 0x00008755
		public Expression(IEnumerable<Node> value, bool isExpressionList)
		{
			this.IsExpressionList = isExpressionList;
			if (value is NodeList)
			{
				this.Value = value as NodeList;
				return;
			}
			this.Value = new NodeList(value);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A588 File Offset: 0x00008788
		public override Node Evaluate(Env env)
		{
			if (this.Value.Count > 1)
			{
				return new Expression(new NodeList(this.Value.Select((Node e) => e.Evaluate(env))), this.IsExpressionList).ReducedFrom<Node>(new Node[] { this });
			}
			if (this.Value.Count == 1)
			{
				return this.Value[0].Evaluate(env).ReducedFrom<Node>(new Node[] { this });
			}
			return this;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A61D File Offset: 0x0000881D
		protected override Node CloneCore()
		{
			return new Expression((NodeList)this.Value.Clone(), this.IsExpressionList);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A63A File Offset: 0x0000883A
		public override void AppendCSS(Env env)
		{
			env.Output.AppendMany<Node>(this.Value, this.IsExpressionList ? ", " : " ");
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A662 File Offset: 0x00008862
		public override void Accept(IVisitor visitor)
		{
			this.Value = base.VisitAndReplace<NodeList>(this.Value, visitor);
		}
	}
}

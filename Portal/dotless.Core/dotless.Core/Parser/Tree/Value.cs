using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200004C RID: 76
	public class Value : Node
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public NodeList Values { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000E1CD File Offset: 0x0000C3CD
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000E1D5 File Offset: 0x0000C3D5
		public NodeList PreImportantComments { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000E1E6 File Offset: 0x0000C3E6
		public string Important { get; set; }

		// Token: 0x0600031A RID: 794 RVA: 0x0000E1EF File Offset: 0x0000C3EF
		public Value(IEnumerable<Node> values, string important)
		{
			this.Values = new NodeList(values);
			this.Important = important;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000E20A File Offset: 0x0000C40A
		protected override Node CloneCore()
		{
			return new Value((NodeList)this.Values.Clone(), this.Important);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000E228 File Offset: 0x0000C428
		public override void AppendCSS(Env env)
		{
			env.Output.AppendMany<Node>(this.Values, env.Compress ? "," : ", ");
			if (!string.IsNullOrEmpty(this.Important))
			{
				if (this.PreImportantComments)
				{
					env.Output.Append(this.PreImportantComments);
				}
				env.Output.Append(" ").Append(this.Important);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
		public override string ToString()
		{
			return this.ToCSS(new Env(null));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public override Node Evaluate(Env env)
		{
			Node node;
			if (this.Values.Count == 1 && string.IsNullOrEmpty(this.Important))
			{
				node = this.Values[0].Evaluate(env);
			}
			else
			{
				(node = new Value(this.Values.Select((Node n) => n.Evaluate(env)), this.Important)).PreImportantComments = this.PreImportantComments;
			}
			return node.ReducedFrom<Node>(new Node[] { this });
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000E344 File Offset: 0x0000C544
		public override void Accept(IVisitor visitor)
		{
			this.Values = base.VisitAndReplace<NodeList>(this.Values, visitor);
		}
	}
}

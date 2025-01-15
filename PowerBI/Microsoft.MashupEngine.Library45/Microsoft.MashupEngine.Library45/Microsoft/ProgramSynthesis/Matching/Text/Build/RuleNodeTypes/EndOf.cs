using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E4 RID: 4580
	public struct EndOf : IProgramNodeBuilder, IEquatable<EndOf>
	{
		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06008995 RID: 35221 RVA: 0x001CF5C2 File Offset: 0x001CD7C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x001CF5CA File Offset: 0x001CD7CA
		private EndOf(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x001CF5D3 File Offset: 0x001CD7D3
		public static EndOf CreateUnsafe(ProgramNode node)
		{
			return new EndOf(node);
		}

		// Token: 0x06008998 RID: 35224 RVA: 0x001CF5DC File Offset: 0x001CD7DC
		public static EndOf? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EndOf)
			{
				return null;
			}
			return new EndOf?(EndOf.CreateUnsafe(node));
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x001CF611 File Offset: 0x001CD811
		public EndOf(GrammarBuilders g, sRegion value0)
		{
			this._node = g.Rule.EndOf.BuildASTNode(value0.Node);
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x001CF630 File Offset: 0x001CD830
		public static implicit operator match(EndOf arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x0600899B RID: 35227 RVA: 0x001CF63E File Offset: 0x001CD83E
		public sRegion sRegion
		{
			get
			{
				return sRegion.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600899C RID: 35228 RVA: 0x001CF652 File Offset: 0x001CD852
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600899D RID: 35229 RVA: 0x001CF668 File Offset: 0x001CD868
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600899E RID: 35230 RVA: 0x001CF692 File Offset: 0x001CD892
		public bool Equals(EndOf other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003898 RID: 14488
		private ProgramNode _node;
	}
}

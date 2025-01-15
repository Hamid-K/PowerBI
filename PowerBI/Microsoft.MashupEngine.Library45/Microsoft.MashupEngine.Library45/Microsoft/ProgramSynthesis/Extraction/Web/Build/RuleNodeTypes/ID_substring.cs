using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001025 RID: 4133
	public struct ID_substring : IProgramNodeBuilder, IEquatable<ID_substring>
	{
		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06007A09 RID: 31241 RVA: 0x001A1426 File Offset: 0x0019F626
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x001A142E File Offset: 0x0019F62E
		private ID_substring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x001A1437 File Offset: 0x0019F637
		public static ID_substring CreateUnsafe(ProgramNode node)
		{
			return new ID_substring(node);
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x001A1440 File Offset: 0x0019F640
		public static ID_substring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ID_substring)
			{
				return null;
			}
			return new ID_substring?(ID_substring.CreateUnsafe(node));
		}

		// Token: 0x06007A0D RID: 31245 RVA: 0x001A1475 File Offset: 0x0019F675
		public ID_substring(GrammarBuilders g, name value0, node value1)
		{
			this._node = g.Rule.ID_substring.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007A0E RID: 31246 RVA: 0x001A149B File Offset: 0x0019F69B
		public static implicit operator atomExpr(ID_substring arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06007A0F RID: 31247 RVA: 0x001A14A9 File Offset: 0x0019F6A9
		public name name
		{
			get
			{
				return name.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06007A10 RID: 31248 RVA: 0x001A14BD File Offset: 0x0019F6BD
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007A11 RID: 31249 RVA: 0x001A14D1 File Offset: 0x0019F6D1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A12 RID: 31250 RVA: 0x001A14E4 File Offset: 0x0019F6E4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x001A150E File Offset: 0x0019F70E
		public bool Equals(ID_substring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400333E RID: 13118
		private ProgramNode _node;
	}
}

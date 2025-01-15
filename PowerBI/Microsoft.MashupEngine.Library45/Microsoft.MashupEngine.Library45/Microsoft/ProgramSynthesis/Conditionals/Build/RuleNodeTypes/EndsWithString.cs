using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A40 RID: 2624
	public struct EndsWithString : IProgramNodeBuilder, IEquatable<EndsWithString>
	{
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000CAB76 File Offset: 0x000C8D76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x000CAB7E File Offset: 0x000C8D7E
		private EndsWithString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000CAB87 File Offset: 0x000C8D87
		public static EndsWithString CreateUnsafe(ProgramNode node)
		{
			return new EndsWithString(node);
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x000CAB90 File Offset: 0x000C8D90
		public static EndsWithString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EndsWithString)
			{
				return null;
			}
			return new EndsWithString?(EndsWithString.CreateUnsafe(node));
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000CABC5 File Offset: 0x000C8DC5
		public EndsWithString(GrammarBuilders g, s value0, str value1)
		{
			this._node = g.Rule.EndsWithString.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x000CABEB File Offset: 0x000C8DEB
		public static implicit operator match(EndsWithString arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000CABF9 File Offset: 0x000C8DF9
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000CAC0D File Offset: 0x000C8E0D
		public str str
		{
			get
			{
				return str.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000CAC21 File Offset: 0x000C8E21
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x000CAC34 File Offset: 0x000C8E34
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000CAC5E File Offset: 0x000C8E5E
		public bool Equals(EndsWithString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D7B RID: 7547
		private ProgramNode _node;
	}
}

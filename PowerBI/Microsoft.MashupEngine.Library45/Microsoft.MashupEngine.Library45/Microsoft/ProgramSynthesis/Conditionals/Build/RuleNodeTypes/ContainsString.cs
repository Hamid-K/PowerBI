using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A43 RID: 2627
	public struct ContainsString : IProgramNodeBuilder, IEquatable<ContainsString>
	{
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x000CAE3A File Offset: 0x000C903A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x000CAE42 File Offset: 0x000C9042
		private ContainsString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x000CAE4B File Offset: 0x000C904B
		public static ContainsString CreateUnsafe(ProgramNode node)
		{
			return new ContainsString(node);
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x000CAE54 File Offset: 0x000C9054
		public static ContainsString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ContainsString)
			{
				return null;
			}
			return new ContainsString?(ContainsString.CreateUnsafe(node));
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x000CAE89 File Offset: 0x000C9089
		public ContainsString(GrammarBuilders g, s value0, str value1, k value2)
		{
			this._node = g.Rule.ContainsString.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x000CAEB6 File Offset: 0x000C90B6
		public static implicit operator match(ContainsString arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06004090 RID: 16528 RVA: 0x000CAEC4 File Offset: 0x000C90C4
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x000CAED8 File Offset: 0x000C90D8
		public str str
		{
			get
			{
				return str.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000CAEEC File Offset: 0x000C90EC
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x000CAF00 File Offset: 0x000C9100
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x000CAF14 File Offset: 0x000C9114
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x000CAF3E File Offset: 0x000C913E
		public bool Equals(ContainsString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D7E RID: 7550
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A3C RID: 2620
	public struct True : IProgramNodeBuilder, IEquatable<True>
	{
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06004043 RID: 16451 RVA: 0x000CA7E6 File Offset: 0x000C89E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004044 RID: 16452 RVA: 0x000CA7EE File Offset: 0x000C89EE
		private True(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004045 RID: 16453 RVA: 0x000CA7F7 File Offset: 0x000C89F7
		public static True CreateUnsafe(ProgramNode node)
		{
			return new True(node);
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x000CA800 File Offset: 0x000C8A00
		public static True? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.True)
			{
				return null;
			}
			return new True?(True.CreateUnsafe(node));
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000CA835 File Offset: 0x000C8A35
		public True(GrammarBuilders g)
		{
			this._node = g.Rule.True.BuildASTNode(Array.Empty<ProgramNode>());
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x000CA852 File Offset: 0x000C8A52
		public static implicit operator match(True arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x000CA860 File Offset: 0x000C8A60
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x000CA874 File Offset: 0x000C8A74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600404B RID: 16459 RVA: 0x000CA89E File Offset: 0x000C8A9E
		public bool Equals(True other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D77 RID: 7543
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A3E RID: 2622
	public struct StartsWithDigit : IProgramNodeBuilder, IEquatable<StartsWithDigit>
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06004057 RID: 16471 RVA: 0x000CA9AE File Offset: 0x000C8BAE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000CA9B6 File Offset: 0x000C8BB6
		private StartsWithDigit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000CA9BF File Offset: 0x000C8BBF
		public static StartsWithDigit CreateUnsafe(ProgramNode node)
		{
			return new StartsWithDigit(node);
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x000CA9C8 File Offset: 0x000C8BC8
		public static StartsWithDigit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartsWithDigit)
			{
				return null;
			}
			return new StartsWithDigit?(StartsWithDigit.CreateUnsafe(node));
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000CA9FD File Offset: 0x000C8BFD
		public StartsWithDigit(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.StartsWithDigit.BuildASTNode(value0.Node);
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x000CAA1C File Offset: 0x000C8C1C
		public static implicit operator match(StartsWithDigit arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000CAA2A File Offset: 0x000C8C2A
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600405E RID: 16478 RVA: 0x000CAA3E File Offset: 0x000C8C3E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x000CAA54 File Offset: 0x000C8C54
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x000CAA7E File Offset: 0x000C8C7E
		public bool Equals(StartsWithDigit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D79 RID: 7545
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200109B RID: 4251
	public struct direction : IProgramNodeBuilder, IEquatable<direction>
	{
		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x06008015 RID: 32789 RVA: 0x001ACF3A File Offset: 0x001AB13A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008016 RID: 32790 RVA: 0x001ACF42 File Offset: 0x001AB142
		private direction(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x001ACF4B File Offset: 0x001AB14B
		public static direction CreateUnsafe(ProgramNode node)
		{
			return new direction(node);
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x001ACF54 File Offset: 0x001AB154
		public static direction? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.direction)
			{
				return null;
			}
			return new direction?(direction.CreateUnsafe(node));
		}

		// Token: 0x06008019 RID: 32793 RVA: 0x001ACF8E File Offset: 0x001AB18E
		public static direction CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new direction(new Hole(g.Symbol.direction, holeId));
		}

		// Token: 0x0600801A RID: 32794 RVA: 0x001ACFA6 File Offset: 0x001AB1A6
		public direction(GrammarBuilders g, KeyDirections value)
		{
			this = new direction(new LiteralNode(g.Symbol.direction, value));
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x0600801B RID: 32795 RVA: 0x001ACFC4 File Offset: 0x001AB1C4
		public KeyDirections Value
		{
			get
			{
				return (KeyDirections)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600801C RID: 32796 RVA: 0x001ACFDB File Offset: 0x001AB1DB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600801D RID: 32797 RVA: 0x001ACFF0 File Offset: 0x001AB1F0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x001AD01A File Offset: 0x001AB21A
		public bool Equals(direction other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B4 RID: 13236
		private ProgramNode _node;
	}
}

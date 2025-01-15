using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C63 RID: 7267
	public struct roundingSpec : IProgramNodeBuilder, IEquatable<roundingSpec>
	{
		// Token: 0x170028FE RID: 10494
		// (get) Token: 0x0600F616 RID: 62998 RVA: 0x00348B76 File Offset: 0x00346D76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F617 RID: 62999 RVA: 0x00348B7E File Offset: 0x00346D7E
		private roundingSpec(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F618 RID: 63000 RVA: 0x00348B87 File Offset: 0x00346D87
		public static roundingSpec CreateUnsafe(ProgramNode node)
		{
			return new roundingSpec(node);
		}

		// Token: 0x0600F619 RID: 63001 RVA: 0x00348B90 File Offset: 0x00346D90
		public static roundingSpec? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.roundingSpec)
			{
				return null;
			}
			return new roundingSpec?(roundingSpec.CreateUnsafe(node));
		}

		// Token: 0x0600F61A RID: 63002 RVA: 0x00348BCA File Offset: 0x00346DCA
		public static roundingSpec CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new roundingSpec(new Hole(g.Symbol.roundingSpec, holeId));
		}

		// Token: 0x0600F61B RID: 63003 RVA: 0x00348BE2 File Offset: 0x00346DE2
		public roundingSpec(GrammarBuilders g, RoundingSpec value)
		{
			this = new roundingSpec(new LiteralNode(g.Symbol.roundingSpec, value));
		}

		// Token: 0x170028FF RID: 10495
		// (get) Token: 0x0600F61C RID: 63004 RVA: 0x00348BFB File Offset: 0x00346DFB
		public RoundingSpec Value
		{
			get
			{
				return (RoundingSpec)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F61D RID: 63005 RVA: 0x00348C12 File Offset: 0x00346E12
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F61E RID: 63006 RVA: 0x00348C28 File Offset: 0x00346E28
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F61F RID: 63007 RVA: 0x00348C52 File Offset: 0x00346E52
		public bool Equals(roundingSpec other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B52 RID: 23378
		private ProgramNode _node;
	}
}

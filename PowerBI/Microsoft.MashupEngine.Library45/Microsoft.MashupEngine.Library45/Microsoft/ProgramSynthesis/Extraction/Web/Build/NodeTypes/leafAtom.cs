using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001077 RID: 4215
	public struct leafAtom : IProgramNodeBuilder, IEquatable<leafAtom>
	{
		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06007E07 RID: 32263 RVA: 0x001A870A File Offset: 0x001A690A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E08 RID: 32264 RVA: 0x001A8712 File Offset: 0x001A6912
		private leafAtom(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E09 RID: 32265 RVA: 0x001A871B File Offset: 0x001A691B
		public static leafAtom CreateUnsafe(ProgramNode node)
		{
			return new leafAtom(node);
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x001A8724 File Offset: 0x001A6924
		public static leafAtom? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.leafAtom)
			{
				return null;
			}
			return new leafAtom?(leafAtom.CreateUnsafe(node));
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x001A875E File Offset: 0x001A695E
		public static leafAtom CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new leafAtom(new Hole(g.Symbol.leafAtom, holeId));
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x001A8776 File Offset: 0x001A6976
		public leafAtom_literalExpr Cast_leafAtom_literalExpr()
		{
			return leafAtom_literalExpr.CreateUnsafe(this.Node);
		}

		// Token: 0x06007E0D RID: 32269 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_leafAtom_literalExpr(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06007E0E RID: 32270 RVA: 0x001A8783 File Offset: 0x001A6983
		public bool Is_leafAtom_literalExpr(GrammarBuilders g, out leafAtom_literalExpr value)
		{
			value = leafAtom_literalExpr.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06007E0F RID: 32271 RVA: 0x001A8797 File Offset: 0x001A6997
		public leafAtom_literalExpr? As_leafAtom_literalExpr(GrammarBuilders g)
		{
			return new leafAtom_literalExpr?(leafAtom_literalExpr.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E10 RID: 32272 RVA: 0x001A87A9 File Offset: 0x001A69A9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E11 RID: 32273 RVA: 0x001A87BC File Offset: 0x001A69BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E12 RID: 32274 RVA: 0x001A87E6 File Offset: 0x001A69E6
		public bool Equals(leafAtom other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003390 RID: 13200
		private ProgramNode _node;
	}
}

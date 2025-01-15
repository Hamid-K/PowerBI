using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C07 RID: 3079
	public struct dir : IProgramNodeBuilder, IEquatable<dir>
	{
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06004F89 RID: 20361 RVA: 0x000FADCE File Offset: 0x000F8FCE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x000FADD6 File Offset: 0x000F8FD6
		private dir(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x000FADDF File Offset: 0x000F8FDF
		public static dir CreateUnsafe(ProgramNode node)
		{
			return new dir(node);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x000FADE8 File Offset: 0x000F8FE8
		public static dir? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dir)
			{
				return null;
			}
			return new dir?(dir.CreateUnsafe(node));
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x000FAE22 File Offset: 0x000F9022
		public static dir CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dir(new Hole(g.Symbol.dir, holeId));
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x000FAE3A File Offset: 0x000F903A
		public dir(GrammarBuilders g, Direction value)
		{
			this = new dir(new LiteralNode(g.Symbol.dir, value));
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x000FAE58 File Offset: 0x000F9058
		public Direction Value
		{
			get
			{
				return (Direction)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x000FAE6F File Offset: 0x000F906F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x000FAE84 File Offset: 0x000F9084
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x000FAEAE File Offset: 0x000F90AE
		public bool Equals(dir other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400232F RID: 9007
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C06 RID: 3078
	public struct axis : IProgramNodeBuilder, IEquatable<axis>
	{
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06004F7F RID: 20351 RVA: 0x000FACDA File Offset: 0x000F8EDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x000FACE2 File Offset: 0x000F8EE2
		private axis(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x000FACEB File Offset: 0x000F8EEB
		public static axis CreateUnsafe(ProgramNode node)
		{
			return new axis(node);
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x000FACF4 File Offset: 0x000F8EF4
		public static axis? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.axis)
			{
				return null;
			}
			return new axis?(axis.CreateUnsafe(node));
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x000FAD2E File Offset: 0x000F8F2E
		public static axis CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new axis(new Hole(g.Symbol.axis, holeId));
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x000FAD46 File Offset: 0x000F8F46
		public axis(GrammarBuilders g, Axis value)
		{
			this = new axis(new LiteralNode(g.Symbol.axis, value));
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x000FAD64 File Offset: 0x000F8F64
		public Axis Value
		{
			get
			{
				return (Axis)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x000FAD7B File Offset: 0x000F8F7B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x000FAD90 File Offset: 0x000F8F90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x000FADBA File Offset: 0x000F8FBA
		public bool Equals(axis other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400232E RID: 9006
		private ProgramNode _node;
	}
}

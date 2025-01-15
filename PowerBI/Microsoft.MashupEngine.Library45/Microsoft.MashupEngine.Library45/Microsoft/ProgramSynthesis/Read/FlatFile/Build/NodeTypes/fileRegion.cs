using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x0200128E RID: 4750
	public struct fileRegion : IProgramNodeBuilder, IEquatable<fileRegion>
	{
		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06008FC2 RID: 36802 RVA: 0x001E31DA File Offset: 0x001E13DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008FC3 RID: 36803 RVA: 0x001E31E2 File Offset: 0x001E13E2
		private fileRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008FC4 RID: 36804 RVA: 0x001E31EB File Offset: 0x001E13EB
		public static fileRegion CreateUnsafe(ProgramNode node)
		{
			return new fileRegion(node);
		}

		// Token: 0x06008FC5 RID: 36805 RVA: 0x001E31F4 File Offset: 0x001E13F4
		public static fileRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fileRegion)
			{
				return null;
			}
			return new fileRegion?(fileRegion.CreateUnsafe(node));
		}

		// Token: 0x06008FC6 RID: 36806 RVA: 0x001E322E File Offset: 0x001E142E
		public static fileRegion CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fileRegion(new Hole(g.Symbol.fileRegion, holeId));
		}

		// Token: 0x06008FC7 RID: 36807 RVA: 0x001E3246 File Offset: 0x001E1446
		public fileRegion(GrammarBuilders g)
		{
			this = new fileRegion(new VariableNode(g.Symbol.fileRegion));
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06008FC8 RID: 36808 RVA: 0x001E325E File Offset: 0x001E145E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008FC9 RID: 36809 RVA: 0x001E326B File Offset: 0x001E146B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008FCA RID: 36810 RVA: 0x001E3280 File Offset: 0x001E1480
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008FCB RID: 36811 RVA: 0x001E32AA File Offset: 0x001E14AA
		public bool Equals(fileRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A7F RID: 14975
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200097F RID: 2431
	public struct file : IProgramNodeBuilder, IEquatable<file>
	{
		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000B30D6 File Offset: 0x000B12D6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000B30DE File Offset: 0x000B12DE
		private file(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000B30E7 File Offset: 0x000B12E7
		public static file CreateUnsafe(ProgramNode node)
		{
			return new file(node);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000B30F0 File Offset: 0x000B12F0
		public static file? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.file)
			{
				return null;
			}
			return new file?(file.CreateUnsafe(node));
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000B312A File Offset: 0x000B132A
		public static file CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new file(new Hole(g.Symbol.file, holeId));
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000B3142 File Offset: 0x000B1342
		public file(GrammarBuilders g)
		{
			this = new file(new VariableNode(g.Symbol.file));
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06003A22 RID: 14882 RVA: 0x000B315A File Offset: 0x000B135A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000B3167 File Offset: 0x000B1367
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000B317C File Offset: 0x000B137C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000B31A6 File Offset: 0x000B13A6
		public bool Equals(file other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A9F RID: 6815
		private ProgramNode _node;
	}
}

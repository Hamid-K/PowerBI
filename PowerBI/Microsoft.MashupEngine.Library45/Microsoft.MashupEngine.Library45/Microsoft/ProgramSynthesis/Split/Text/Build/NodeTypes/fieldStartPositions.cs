using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200137E RID: 4990
	public struct fieldStartPositions : IProgramNodeBuilder, IEquatable<fieldStartPositions>
	{
		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06009ADF RID: 39647 RVA: 0x0020B94A File Offset: 0x00209B4A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009AE0 RID: 39648 RVA: 0x0020B952 File Offset: 0x00209B52
		private fieldStartPositions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009AE1 RID: 39649 RVA: 0x0020B95B File Offset: 0x00209B5B
		public static fieldStartPositions CreateUnsafe(ProgramNode node)
		{
			return new fieldStartPositions(node);
		}

		// Token: 0x06009AE2 RID: 39650 RVA: 0x0020B964 File Offset: 0x00209B64
		public static fieldStartPositions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fieldStartPositions)
			{
				return null;
			}
			return new fieldStartPositions?(fieldStartPositions.CreateUnsafe(node));
		}

		// Token: 0x06009AE3 RID: 39651 RVA: 0x0020B99E File Offset: 0x00209B9E
		public static fieldStartPositions CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fieldStartPositions(new Hole(g.Symbol.fieldStartPositions, holeId));
		}

		// Token: 0x06009AE4 RID: 39652 RVA: 0x0020B9B6 File Offset: 0x00209BB6
		public fieldStartPositions(GrammarBuilders g, int[] value)
		{
			this = new fieldStartPositions(new LiteralNode(g.Symbol.fieldStartPositions, value));
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06009AE5 RID: 39653 RVA: 0x0020B9CF File Offset: 0x00209BCF
		public int[] Value
		{
			get
			{
				return (int[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06009AE6 RID: 39654 RVA: 0x0020B9E6 File Offset: 0x00209BE6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009AE7 RID: 39655 RVA: 0x0020B9FC File Offset: 0x00209BFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009AE8 RID: 39656 RVA: 0x0020BA26 File Offset: 0x00209C26
		public bool Equals(fieldStartPositions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DF5 RID: 15861
		private ProgramNode _node;
	}
}

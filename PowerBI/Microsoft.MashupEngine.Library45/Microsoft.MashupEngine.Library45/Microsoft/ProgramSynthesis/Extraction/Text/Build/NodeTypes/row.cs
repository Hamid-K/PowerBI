using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F4A RID: 3914
	public struct row : IProgramNodeBuilder, IEquatable<row>
	{
		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06006D0D RID: 27917 RVA: 0x001641CA File Offset: 0x001623CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006D0E RID: 27918 RVA: 0x001641D2 File Offset: 0x001623D2
		private row(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006D0F RID: 27919 RVA: 0x001641DB File Offset: 0x001623DB
		public static row CreateUnsafe(ProgramNode node)
		{
			return new row(node);
		}

		// Token: 0x06006D10 RID: 27920 RVA: 0x001641E4 File Offset: 0x001623E4
		public static row? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.row)
			{
				return null;
			}
			return new row?(row.CreateUnsafe(node));
		}

		// Token: 0x06006D11 RID: 27921 RVA: 0x0016421E File Offset: 0x0016241E
		public static row CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new row(new Hole(g.Symbol.row, holeId));
		}

		// Token: 0x06006D12 RID: 27922 RVA: 0x00164236 File Offset: 0x00162436
		public row(GrammarBuilders g)
		{
			this = new row(new VariableNode(g.Symbol.row));
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06006D13 RID: 27923 RVA: 0x0016424E File Offset: 0x0016244E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06006D14 RID: 27924 RVA: 0x0016425B File Offset: 0x0016245B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006D15 RID: 27925 RVA: 0x00164270 File Offset: 0x00162470
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x0016429A File Offset: 0x0016249A
		public bool Equals(row other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F35 RID: 12085
		private ProgramNode _node;
	}
}

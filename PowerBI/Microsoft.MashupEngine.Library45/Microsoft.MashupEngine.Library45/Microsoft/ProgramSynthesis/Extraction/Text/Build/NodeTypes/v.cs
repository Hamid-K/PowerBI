using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F49 RID: 3913
	public struct v : IProgramNodeBuilder, IEquatable<v>
	{
		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06006D03 RID: 27907 RVA: 0x001640E6 File Offset: 0x001622E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x001640EE File Offset: 0x001622EE
		private v(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006D05 RID: 27909 RVA: 0x001640F7 File Offset: 0x001622F7
		public static v CreateUnsafe(ProgramNode node)
		{
			return new v(node);
		}

		// Token: 0x06006D06 RID: 27910 RVA: 0x00164100 File Offset: 0x00162300
		public static v? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.v)
			{
				return null;
			}
			return new v?(v.CreateUnsafe(node));
		}

		// Token: 0x06006D07 RID: 27911 RVA: 0x0016413A File Offset: 0x0016233A
		public static v CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new v(new Hole(g.Symbol.v, holeId));
		}

		// Token: 0x06006D08 RID: 27912 RVA: 0x00164152 File Offset: 0x00162352
		public v(GrammarBuilders g)
		{
			this = new v(new VariableNode(g.Symbol.v));
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06006D09 RID: 27913 RVA: 0x0016416A File Offset: 0x0016236A
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06006D0A RID: 27914 RVA: 0x00164177 File Offset: 0x00162377
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006D0B RID: 27915 RVA: 0x0016418C File Offset: 0x0016238C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006D0C RID: 27916 RVA: 0x001641B6 File Offset: 0x001623B6
		public bool Equals(v other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F34 RID: 12084
		private ProgramNode _node;
	}
}

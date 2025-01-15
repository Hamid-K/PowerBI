using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200136D RID: 4973
	public struct gen_Concat : IProgramNodeBuilder, IEquatable<gen_Concat>
	{
		// Token: 0x17001A76 RID: 6774
		// (get) Token: 0x06009A1D RID: 39453 RVA: 0x0020A6F6 File Offset: 0x002088F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A1E RID: 39454 RVA: 0x0020A6FE File Offset: 0x002088FE
		private gen_Concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A1F RID: 39455 RVA: 0x0020A707 File Offset: 0x00208907
		public static gen_Concat CreateUnsafe(ProgramNode node)
		{
			return new gen_Concat(node);
		}

		// Token: 0x06009A20 RID: 39456 RVA: 0x0020A710 File Offset: 0x00208910
		public static gen_Concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_Concat)
			{
				return null;
			}
			return new gen_Concat?(gen_Concat.CreateUnsafe(node));
		}

		// Token: 0x06009A21 RID: 39457 RVA: 0x0020A74A File Offset: 0x0020894A
		public static gen_Concat CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_Concat(new Hole(g.Symbol.gen_Concat, holeId));
		}

		// Token: 0x06009A22 RID: 39458 RVA: 0x0020A762 File Offset: 0x00208962
		public GEN_Concat Cast_GEN_Concat()
		{
			return GEN_Concat.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A23 RID: 39459 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_Concat(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A24 RID: 39460 RVA: 0x0020A76F File Offset: 0x0020896F
		public bool Is_GEN_Concat(GrammarBuilders g, out GEN_Concat value)
		{
			value = GEN_Concat.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A25 RID: 39461 RVA: 0x0020A783 File Offset: 0x00208983
		public GEN_Concat? As_GEN_Concat(GrammarBuilders g)
		{
			return new GEN_Concat?(GEN_Concat.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A26 RID: 39462 RVA: 0x0020A795 File Offset: 0x00208995
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A27 RID: 39463 RVA: 0x0020A7A8 File Offset: 0x002089A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A28 RID: 39464 RVA: 0x0020A7D2 File Offset: 0x002089D2
		public bool Equals(gen_Concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE4 RID: 15844
		private ProgramNode _node;
	}
}

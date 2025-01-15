using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200136E RID: 4974
	public struct gen_LookAround : IProgramNodeBuilder, IEquatable<gen_LookAround>
	{
		// Token: 0x17001A77 RID: 6775
		// (get) Token: 0x06009A29 RID: 39465 RVA: 0x0020A7E6 File Offset: 0x002089E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A2A RID: 39466 RVA: 0x0020A7EE File Offset: 0x002089EE
		private gen_LookAround(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A2B RID: 39467 RVA: 0x0020A7F7 File Offset: 0x002089F7
		public static gen_LookAround CreateUnsafe(ProgramNode node)
		{
			return new gen_LookAround(node);
		}

		// Token: 0x06009A2C RID: 39468 RVA: 0x0020A800 File Offset: 0x00208A00
		public static gen_LookAround? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_LookAround)
			{
				return null;
			}
			return new gen_LookAround?(gen_LookAround.CreateUnsafe(node));
		}

		// Token: 0x06009A2D RID: 39469 RVA: 0x0020A83A File Offset: 0x00208A3A
		public static gen_LookAround CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_LookAround(new Hole(g.Symbol.gen_LookAround, holeId));
		}

		// Token: 0x06009A2E RID: 39470 RVA: 0x0020A852 File Offset: 0x00208A52
		public GEN_LookAround Cast_GEN_LookAround()
		{
			return GEN_LookAround.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A2F RID: 39471 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_LookAround(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A30 RID: 39472 RVA: 0x0020A85F File Offset: 0x00208A5F
		public bool Is_GEN_LookAround(GrammarBuilders g, out GEN_LookAround value)
		{
			value = GEN_LookAround.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A31 RID: 39473 RVA: 0x0020A873 File Offset: 0x00208A73
		public GEN_LookAround? As_GEN_LookAround(GrammarBuilders g)
		{
			return new GEN_LookAround?(GEN_LookAround.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A32 RID: 39474 RVA: 0x0020A885 File Offset: 0x00208A85
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A33 RID: 39475 RVA: 0x0020A898 File Offset: 0x00208A98
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A34 RID: 39476 RVA: 0x0020A8C2 File Offset: 0x00208AC2
		public bool Equals(gen_LookAround other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE5 RID: 15845
		private ProgramNode _node;
	}
}

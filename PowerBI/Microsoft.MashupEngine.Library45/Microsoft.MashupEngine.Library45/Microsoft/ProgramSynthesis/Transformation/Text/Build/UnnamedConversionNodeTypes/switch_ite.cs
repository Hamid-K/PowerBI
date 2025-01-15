using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BEF RID: 7151
	public struct switch_ite : IProgramNodeBuilder, IEquatable<switch_ite>
	{
		// Token: 0x17002800 RID: 10240
		// (get) Token: 0x0600F057 RID: 61527 RVA: 0x0033DFF9 File Offset: 0x0033C1F9
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F058 RID: 61528 RVA: 0x0033E001 File Offset: 0x0033C201
		private switch_ite(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F059 RID: 61529 RVA: 0x0033E00A File Offset: 0x0033C20A
		public static switch_ite CreateUnsafe(ProgramNode node)
		{
			return new switch_ite(node);
		}

		// Token: 0x0600F05A RID: 61530 RVA: 0x0033E014 File Offset: 0x0033C214
		public static switch_ite? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.switch_ite)
			{
				return null;
			}
			return new switch_ite?(switch_ite.CreateUnsafe(node));
		}

		// Token: 0x0600F05B RID: 61531 RVA: 0x0033E049 File Offset: 0x0033C249
		public switch_ite(GrammarBuilders g, ite value0)
		{
			this._node = g.UnnamedConversion.switch_ite.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F05C RID: 61532 RVA: 0x0033E068 File Offset: 0x0033C268
		public static implicit operator @switch(switch_ite arg)
		{
			return @switch.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002801 RID: 10241
		// (get) Token: 0x0600F05D RID: 61533 RVA: 0x0033E076 File Offset: 0x0033C276
		public ite ite
		{
			get
			{
				return ite.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F05E RID: 61534 RVA: 0x0033E08A File Offset: 0x0033C28A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F05F RID: 61535 RVA: 0x0033E0A0 File Offset: 0x0033C2A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F060 RID: 61536 RVA: 0x0033E0CA File Offset: 0x0033C2CA
		public bool Equals(switch_ite other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005ADE RID: 23262
		private ProgramNode _node;
	}
}

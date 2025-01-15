using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E1E RID: 3614
	public struct TrimAboveBottomBorder : IProgramNodeBuilder, IEquatable<TrimAboveBottomBorder>
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600606D RID: 24685 RVA: 0x0013DAC2 File Offset: 0x0013BCC2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x0013DACA File Offset: 0x0013BCCA
		private TrimAboveBottomBorder(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x0013DAD3 File Offset: 0x0013BCD3
		public static TrimAboveBottomBorder CreateUnsafe(ProgramNode node)
		{
			return new TrimAboveBottomBorder(node);
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x0013DADC File Offset: 0x0013BCDC
		public static TrimAboveBottomBorder? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimAboveBottomBorder)
			{
				return null;
			}
			return new TrimAboveBottomBorder?(TrimAboveBottomBorder.CreateUnsafe(node));
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x0013DB11 File Offset: 0x0013BD11
		public TrimAboveBottomBorder(GrammarBuilders g, trimTop value0)
		{
			this._node = g.Rule.TrimAboveBottomBorder.BuildASTNode(value0.Node);
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x0013DB30 File Offset: 0x0013BD30
		public static implicit operator trimBottom(TrimAboveBottomBorder arg)
		{
			return trimBottom.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x0013DB3E File Offset: 0x0013BD3E
		public trimTop trimTop
		{
			get
			{
				return trimTop.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x0013DB52 File Offset: 0x0013BD52
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x0013DB68 File Offset: 0x0013BD68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x0013DB92 File Offset: 0x0013BD92
		public bool Equals(TrimAboveBottomBorder other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC8 RID: 11208
		private ProgramNode _node;
	}
}

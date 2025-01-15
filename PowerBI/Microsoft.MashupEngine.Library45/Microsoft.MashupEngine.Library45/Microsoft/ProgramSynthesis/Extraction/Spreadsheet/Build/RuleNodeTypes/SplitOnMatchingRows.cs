using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E2E RID: 3630
	public struct SplitOnMatchingRows : IProgramNodeBuilder, IEquatable<SplitOnMatchingRows>
	{
		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x0013E98E File Offset: 0x0013CB8E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x0013E996 File Offset: 0x0013CB96
		private SplitOnMatchingRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x0013E99F File Offset: 0x0013CB9F
		public static SplitOnMatchingRows CreateUnsafe(ProgramNode node)
		{
			return new SplitOnMatchingRows(node);
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x0013E9A8 File Offset: 0x0013CBA8
		public static SplitOnMatchingRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SplitOnMatchingRows)
			{
				return null;
			}
			return new SplitOnMatchingRows?(SplitOnMatchingRows.CreateUnsafe(node));
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x0013E9DD File Offset: 0x0013CBDD
		public SplitOnMatchingRows(GrammarBuilders g, verticalSheetSection value0, styleFilter value1, splitMode value2)
		{
			this._node = g.Rule.SplitOnMatchingRows.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x0013EA0A File Offset: 0x0013CC0A
		public static implicit operator horizontalSheetSplits(SplitOnMatchingRows arg)
		{
			return horizontalSheetSplits.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06006117 RID: 24855 RVA: 0x0013EA18 File Offset: 0x0013CC18
		public verticalSheetSection verticalSheetSection
		{
			get
			{
				return verticalSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06006118 RID: 24856 RVA: 0x0013EA2C File Offset: 0x0013CC2C
		public styleFilter styleFilter
		{
			get
			{
				return styleFilter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06006119 RID: 24857 RVA: 0x0013EA40 File Offset: 0x0013CC40
		public splitMode splitMode
		{
			get
			{
				return splitMode.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x0013EA54 File Offset: 0x0013CC54
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x0013EA68 File Offset: 0x0013CC68
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x0013EA92 File Offset: 0x0013CC92
		public bool Equals(SplitOnMatchingRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD8 RID: 11224
		private ProgramNode _node;
	}
}

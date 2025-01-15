using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200133E RID: 4926
	public struct ExtractionSplit : IProgramNodeBuilder, IEquatable<ExtractionSplit>
	{
		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x060097B4 RID: 38836 RVA: 0x00205B2A File Offset: 0x00203D2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060097B5 RID: 38837 RVA: 0x00205B32 File Offset: 0x00203D32
		private ExtractionSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x00205B3B File Offset: 0x00203D3B
		public static ExtractionSplit CreateUnsafe(ProgramNode node)
		{
			return new ExtractionSplit(node);
		}

		// Token: 0x060097B7 RID: 38839 RVA: 0x00205B44 File Offset: 0x00203D44
		public static ExtractionSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ExtractionSplit)
			{
				return null;
			}
			return new ExtractionSplit?(ExtractionSplit.CreateUnsafe(node));
		}

		// Token: 0x060097B8 RID: 38840 RVA: 0x00205B79 File Offset: 0x00203D79
		public ExtractionSplit(GrammarBuilders g, v value0, delimiterList value1, extractionPoints value2)
		{
			this._node = g.Rule.ExtractionSplit.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060097B9 RID: 38841 RVA: 0x00205BA6 File Offset: 0x00203DA6
		public static implicit operator regionSplit(ExtractionSplit arg)
		{
			return regionSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x060097BA RID: 38842 RVA: 0x00205BB4 File Offset: 0x00203DB4
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x060097BB RID: 38843 RVA: 0x00205BC8 File Offset: 0x00203DC8
		public delimiterList delimiterList
		{
			get
			{
				return delimiterList.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x060097BC RID: 38844 RVA: 0x00205BDC File Offset: 0x00203DDC
		public extractionPoints extractionPoints
		{
			get
			{
				return extractionPoints.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060097BD RID: 38845 RVA: 0x00205BF0 File Offset: 0x00203DF0
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060097BE RID: 38846 RVA: 0x00205C04 File Offset: 0x00203E04
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060097BF RID: 38847 RVA: 0x00205C2E File Offset: 0x00203E2E
		public bool Equals(ExtractionSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB5 RID: 15797
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x02000947 RID: 2375
	public struct BreakLine : IProgramNodeBuilder, IEquatable<BreakLine>
	{
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000ADB16 File Offset: 0x000ABD16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000ADB1E File Offset: 0x000ABD1E
		private BreakLine(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000ADB27 File Offset: 0x000ABD27
		public static BreakLine CreateUnsafe(ProgramNode node)
		{
			return new BreakLine(node);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000ADB30 File Offset: 0x000ABD30
		public static BreakLine? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.BreakLine)
			{
				return null;
			}
			return new BreakLine?(BreakLine.CreateUnsafe(node));
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000ADB65 File Offset: 0x000ABD65
		public BreakLine(GrammarBuilders g, records value0)
		{
			this._node = g.Rule.BreakLine.BuildASTNode(value0.Node);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000ADB84 File Offset: 0x000ABD84
		public static implicit operator primarySelector(BreakLine arg)
		{
			return primarySelector.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000ADB92 File Offset: 0x000ABD92
		public records records
		{
			get
			{
				return records.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000ADBA6 File Offset: 0x000ABDA6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000ADBBC File Offset: 0x000ABDBC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000ADBE6 File Offset: 0x000ABDE6
		public bool Equals(BreakLine other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A67 RID: 6759
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C0C RID: 7180
	public struct DtRangeConcat : IProgramNodeBuilder, IEquatable<DtRangeConcat>
	{
		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x0600F18D RID: 61837 RVA: 0x0033FC0A File Offset: 0x0033DE0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F18E RID: 61838 RVA: 0x0033FC12 File Offset: 0x0033DE12
		private DtRangeConcat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F18F RID: 61839 RVA: 0x0033FC1B File Offset: 0x0033DE1B
		public static DtRangeConcat CreateUnsafe(ProgramNode node)
		{
			return new DtRangeConcat(node);
		}

		// Token: 0x0600F190 RID: 61840 RVA: 0x0033FC24 File Offset: 0x0033DE24
		public static DtRangeConcat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.DtRangeConcat)
			{
				return null;
			}
			return new DtRangeConcat?(DtRangeConcat.CreateUnsafe(node));
		}

		// Token: 0x0600F191 RID: 61841 RVA: 0x0033FC59 File Offset: 0x0033DE59
		public DtRangeConcat(GrammarBuilders g, dtRangeSubstring value0, dtRangeString value1)
		{
			this._node = g.Rule.DtRangeConcat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F192 RID: 61842 RVA: 0x0033FC7F File Offset: 0x0033DE7F
		public static implicit operator dtRangeString(DtRangeConcat arg)
		{
			return dtRangeString.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x0600F193 RID: 61843 RVA: 0x0033FC8D File Offset: 0x0033DE8D
		public dtRangeSubstring dtRangeSubstring
		{
			get
			{
				return dtRangeSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x0600F194 RID: 61844 RVA: 0x0033FCA1 File Offset: 0x0033DEA1
		public dtRangeString dtRangeString
		{
			get
			{
				return dtRangeString.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F195 RID: 61845 RVA: 0x0033FCB5 File Offset: 0x0033DEB5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F196 RID: 61846 RVA: 0x0033FCC8 File Offset: 0x0033DEC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F197 RID: 61847 RVA: 0x0033FCF2 File Offset: 0x0033DEF2
		public bool Equals(DtRangeConcat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AFB RID: 23291
		private ProgramNode _node;
	}
}

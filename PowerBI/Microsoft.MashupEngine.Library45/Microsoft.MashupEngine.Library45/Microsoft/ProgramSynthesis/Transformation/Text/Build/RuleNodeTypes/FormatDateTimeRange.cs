using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C07 RID: 7175
	public struct FormatDateTimeRange : IProgramNodeBuilder, IEquatable<FormatDateTimeRange>
	{
		// Token: 0x1700283D RID: 10301
		// (get) Token: 0x0600F154 RID: 61780 RVA: 0x0033F6C2 File Offset: 0x0033D8C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F155 RID: 61781 RVA: 0x0033F6CA File Offset: 0x0033D8CA
		private FormatDateTimeRange(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F156 RID: 61782 RVA: 0x0033F6D3 File Offset: 0x0033D8D3
		public static FormatDateTimeRange CreateUnsafe(ProgramNode node)
		{
			return new FormatDateTimeRange(node);
		}

		// Token: 0x0600F157 RID: 61783 RVA: 0x0033F6DC File Offset: 0x0033D8DC
		public static FormatDateTimeRange? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FormatDateTimeRange)
			{
				return null;
			}
			return new FormatDateTimeRange?(FormatDateTimeRange.CreateUnsafe(node));
		}

		// Token: 0x0600F158 RID: 61784 RVA: 0x0033F714 File Offset: 0x0033D914
		public FormatDateTimeRange(GrammarBuilders g, inputDateTime value0, outputDtFormat value1, s value2, dtRoundingSpec value3, dtRoundingSpec value4)
		{
			this._node = g.Rule.FormatDateTimeRange.BuildASTNode(new ProgramNode[] { value0.Node, value1.Node, value2.Node, value3.Node, value4.Node });
		}

		// Token: 0x0600F159 RID: 61785 RVA: 0x0033F76F File Offset: 0x0033D96F
		public static implicit operator conv(FormatDateTimeRange arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700283E RID: 10302
		// (get) Token: 0x0600F15A RID: 61786 RVA: 0x0033F77D File Offset: 0x0033D97D
		public inputDateTime inputDateTime
		{
			get
			{
				return inputDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x1700283F RID: 10303
		// (get) Token: 0x0600F15B RID: 61787 RVA: 0x0033F791 File Offset: 0x0033D991
		public outputDtFormat outputDtFormat
		{
			get
			{
				return outputDtFormat.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17002840 RID: 10304
		// (get) Token: 0x0600F15C RID: 61788 RVA: 0x0033F7A5 File Offset: 0x0033D9A5
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x17002841 RID: 10305
		// (get) Token: 0x0600F15D RID: 61789 RVA: 0x0033F7B9 File Offset: 0x0033D9B9
		public dtRoundingSpec dtRoundingSpec1
		{
			get
			{
				return dtRoundingSpec.CreateUnsafe(this.Node.Children[3]);
			}
		}

		// Token: 0x17002842 RID: 10306
		// (get) Token: 0x0600F15E RID: 61790 RVA: 0x0033F7CD File Offset: 0x0033D9CD
		public dtRoundingSpec dtRoundingSpec2
		{
			get
			{
				return dtRoundingSpec.CreateUnsafe(this.Node.Children[4]);
			}
		}

		// Token: 0x0600F15F RID: 61791 RVA: 0x0033F7E1 File Offset: 0x0033D9E1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F160 RID: 61792 RVA: 0x0033F7F4 File Offset: 0x0033D9F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F161 RID: 61793 RVA: 0x0033F81E File Offset: 0x0033DA1E
		public bool Equals(FormatDateTimeRange other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF6 RID: 23286
		private ProgramNode _node;
	}
}

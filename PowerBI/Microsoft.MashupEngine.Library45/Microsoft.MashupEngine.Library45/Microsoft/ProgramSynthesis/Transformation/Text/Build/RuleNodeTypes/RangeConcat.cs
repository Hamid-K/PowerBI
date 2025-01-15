using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C08 RID: 7176
	public struct RangeConcat : IProgramNodeBuilder, IEquatable<RangeConcat>
	{
		// Token: 0x17002843 RID: 10307
		// (get) Token: 0x0600F162 RID: 61794 RVA: 0x0033F832 File Offset: 0x0033DA32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F163 RID: 61795 RVA: 0x0033F83A File Offset: 0x0033DA3A
		private RangeConcat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F164 RID: 61796 RVA: 0x0033F843 File Offset: 0x0033DA43
		public static RangeConcat CreateUnsafe(ProgramNode node)
		{
			return new RangeConcat(node);
		}

		// Token: 0x0600F165 RID: 61797 RVA: 0x0033F84C File Offset: 0x0033DA4C
		public static RangeConcat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RangeConcat)
			{
				return null;
			}
			return new RangeConcat?(RangeConcat.CreateUnsafe(node));
		}

		// Token: 0x0600F166 RID: 61798 RVA: 0x0033F881 File Offset: 0x0033DA81
		public RangeConcat(GrammarBuilders g, rangeSubstring value0, rangeString value1)
		{
			this._node = g.Rule.RangeConcat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F167 RID: 61799 RVA: 0x0033F8A7 File Offset: 0x0033DAA7
		public static implicit operator rangeString(RangeConcat arg)
		{
			return rangeString.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002844 RID: 10308
		// (get) Token: 0x0600F168 RID: 61800 RVA: 0x0033F8B5 File Offset: 0x0033DAB5
		public rangeSubstring rangeSubstring
		{
			get
			{
				return rangeSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002845 RID: 10309
		// (get) Token: 0x0600F169 RID: 61801 RVA: 0x0033F8C9 File Offset: 0x0033DAC9
		public rangeString rangeString
		{
			get
			{
				return rangeString.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F16A RID: 61802 RVA: 0x0033F8DD File Offset: 0x0033DADD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F16B RID: 61803 RVA: 0x0033F8F0 File Offset: 0x0033DAF0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F16C RID: 61804 RVA: 0x0033F91A File Offset: 0x0033DB1A
		public bool Equals(RangeConcat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF7 RID: 23287
		private ProgramNode _node;
	}
}

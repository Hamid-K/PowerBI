using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001359 RID: 4953
	public struct Append : IProgramNodeBuilder, IEquatable<Append>
	{
		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x060098E3 RID: 39139 RVA: 0x002076AA File Offset: 0x002058AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098E4 RID: 39140 RVA: 0x002076B2 File Offset: 0x002058B2
		private Append(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098E5 RID: 39141 RVA: 0x002076BB File Offset: 0x002058BB
		public static Append CreateUnsafe(ProgramNode node)
		{
			return new Append(node);
		}

		// Token: 0x060098E6 RID: 39142 RVA: 0x002076C4 File Offset: 0x002058C4
		public static Append? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Append)
			{
				return null;
			}
			return new Append?(Append.CreateUnsafe(node));
		}

		// Token: 0x060098E7 RID: 39143 RVA: 0x002076F9 File Offset: 0x002058F9
		public Append(GrammarBuilders g, item1 value0, output value1)
		{
			this._node = g.Rule.Append.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060098E8 RID: 39144 RVA: 0x0020771F File Offset: 0x0020591F
		public static implicit operator _LetB1(Append arg)
		{
			return _LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x060098E9 RID: 39145 RVA: 0x0020772D File Offset: 0x0020592D
		public item1 item1
		{
			get
			{
				return item1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x060098EA RID: 39146 RVA: 0x00207741 File Offset: 0x00205941
		public output output
		{
			get
			{
				return output.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060098EB RID: 39147 RVA: 0x00207755 File Offset: 0x00205955
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098EC RID: 39148 RVA: 0x00207768 File Offset: 0x00205968
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098ED RID: 39149 RVA: 0x00207792 File Offset: 0x00205992
		public bool Equals(Append other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD0 RID: 15824
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200135A RID: 4954
	public struct Split : IProgramNodeBuilder, IEquatable<Split>
	{
		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x060098EE RID: 39150 RVA: 0x002077A6 File Offset: 0x002059A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098EF RID: 39151 RVA: 0x002077AE File Offset: 0x002059AE
		private Split(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098F0 RID: 39152 RVA: 0x002077B7 File Offset: 0x002059B7
		public static Split CreateUnsafe(ProgramNode node)
		{
			return new Split(node);
		}

		// Token: 0x060098F1 RID: 39153 RVA: 0x002077C0 File Offset: 0x002059C0
		public static Split? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Split)
			{
				return null;
			}
			return new Split?(Split.CreateUnsafe(node));
		}

		// Token: 0x060098F2 RID: 39154 RVA: 0x002077F5 File Offset: 0x002059F5
		public Split(GrammarBuilders g, v value0, delimiter value1)
		{
			this._node = g.Rule.Split.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060098F3 RID: 39155 RVA: 0x0020781B File Offset: 0x00205A1B
		public static implicit operator _LetB2(Split arg)
		{
			return _LetB2.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x060098F4 RID: 39156 RVA: 0x00207829 File Offset: 0x00205A29
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x060098F5 RID: 39157 RVA: 0x0020783D File Offset: 0x00205A3D
		public delimiter delimiter
		{
			get
			{
				return delimiter.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060098F6 RID: 39158 RVA: 0x00207851 File Offset: 0x00205A51
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098F7 RID: 39159 RVA: 0x00207864 File Offset: 0x00205A64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098F8 RID: 39160 RVA: 0x0020788E File Offset: 0x00205A8E
		public bool Equals(Split other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD1 RID: 15825
		private ProgramNode _node;
	}
}

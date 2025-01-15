using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F34 RID: 3892
	public struct LetSplit : IProgramNodeBuilder, IEquatable<LetSplit>
	{
		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x06006BDF RID: 27615 RVA: 0x00161B1E File Offset: 0x0015FD1E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BE0 RID: 27616 RVA: 0x00161B26 File Offset: 0x0015FD26
		private LetSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BE1 RID: 27617 RVA: 0x00161B2F File Offset: 0x0015FD2F
		public static LetSplit CreateUnsafe(ProgramNode node)
		{
			return new LetSplit(node);
		}

		// Token: 0x06006BE2 RID: 27618 RVA: 0x00161B38 File Offset: 0x0015FD38
		public static LetSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSplit)
			{
				return null;
			}
			return new LetSplit?(LetSplit.CreateUnsafe(node));
		}

		// Token: 0x06006BE3 RID: 27619 RVA: 0x00161B6D File Offset: 0x0015FD6D
		public LetSplit(GrammarBuilders g, split value0, _LetB2 value1)
		{
			this._node = new LetNode(g.Rule.LetSplit, value0.Node, value1.Node);
		}

		// Token: 0x06006BE4 RID: 27620 RVA: 0x00161B93 File Offset: 0x0015FD93
		public static implicit operator colSplit(LetSplit arg)
		{
			return colSplit.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001352 RID: 4946
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x00161BA1 File Offset: 0x0015FDA1
		public split split
		{
			get
			{
				return split.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06006BE6 RID: 27622 RVA: 0x00161BB5 File Offset: 0x0015FDB5
		public _LetB2 _LetB2
		{
			get
			{
				return _LetB2.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BE7 RID: 27623 RVA: 0x00161BC9 File Offset: 0x0015FDC9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BE8 RID: 27624 RVA: 0x00161BDC File Offset: 0x0015FDDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BE9 RID: 27625 RVA: 0x00161C06 File Offset: 0x0015FE06
		public bool Equals(LetSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F1F RID: 12063
		private ProgramNode _node;
	}
}

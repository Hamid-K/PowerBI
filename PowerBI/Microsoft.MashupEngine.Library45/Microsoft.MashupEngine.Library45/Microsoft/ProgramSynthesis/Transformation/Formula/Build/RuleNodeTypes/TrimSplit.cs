using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001580 RID: 5504
	public struct TrimSplit : IProgramNodeBuilder, IEquatable<TrimSplit>
	{
		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x0600B428 RID: 46120 RVA: 0x0027467A File Offset: 0x0027287A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B429 RID: 46121 RVA: 0x00274682 File Offset: 0x00272882
		private TrimSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B42A RID: 46122 RVA: 0x0027468B File Offset: 0x0027288B
		public static TrimSplit CreateUnsafe(ProgramNode node)
		{
			return new TrimSplit(node);
		}

		// Token: 0x0600B42B RID: 46123 RVA: 0x00274694 File Offset: 0x00272894
		public static TrimSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimSplit)
			{
				return null;
			}
			return new TrimSplit?(TrimSplit.CreateUnsafe(node));
		}

		// Token: 0x0600B42C RID: 46124 RVA: 0x002746C9 File Offset: 0x002728C9
		public TrimSplit(GrammarBuilders g, split value0)
		{
			this._node = g.Rule.TrimSplit.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B42D RID: 46125 RVA: 0x002746E8 File Offset: 0x002728E8
		public static implicit operator splitTrim(TrimSplit arg)
		{
			return splitTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x0600B42E RID: 46126 RVA: 0x002746F6 File Offset: 0x002728F6
		public split split
		{
			get
			{
				return split.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B42F RID: 46127 RVA: 0x0027470A File Offset: 0x0027290A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B430 RID: 46128 RVA: 0x00274720 File Offset: 0x00272920
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B431 RID: 46129 RVA: 0x0027474A File Offset: 0x0027294A
		public bool Equals(TrimSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400462E RID: 17966
		private ProgramNode _node;
	}
}

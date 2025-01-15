using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001581 RID: 5505
	public struct TrimFullSplit : IProgramNodeBuilder, IEquatable<TrimFullSplit>
	{
		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x0600B432 RID: 46130 RVA: 0x0027475E File Offset: 0x0027295E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B433 RID: 46131 RVA: 0x00274766 File Offset: 0x00272966
		private TrimFullSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B434 RID: 46132 RVA: 0x0027476F File Offset: 0x0027296F
		public static TrimFullSplit CreateUnsafe(ProgramNode node)
		{
			return new TrimFullSplit(node);
		}

		// Token: 0x0600B435 RID: 46133 RVA: 0x00274778 File Offset: 0x00272978
		public static TrimFullSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimFullSplit)
			{
				return null;
			}
			return new TrimFullSplit?(TrimFullSplit.CreateUnsafe(node));
		}

		// Token: 0x0600B436 RID: 46134 RVA: 0x002747AD File Offset: 0x002729AD
		public TrimFullSplit(GrammarBuilders g, split value0)
		{
			this._node = g.Rule.TrimFullSplit.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B437 RID: 46135 RVA: 0x002747CC File Offset: 0x002729CC
		public static implicit operator splitTrim(TrimFullSplit arg)
		{
			return splitTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x0600B438 RID: 46136 RVA: 0x002747DA File Offset: 0x002729DA
		public split split
		{
			get
			{
				return split.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B439 RID: 46137 RVA: 0x002747EE File Offset: 0x002729EE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B43A RID: 46138 RVA: 0x00274804 File Offset: 0x00272A04
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B43B RID: 46139 RVA: 0x0027482E File Offset: 0x00272A2E
		public bool Equals(TrimFullSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400462F RID: 17967
		private ProgramNode _node;
	}
}

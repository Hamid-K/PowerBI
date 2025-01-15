using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001583 RID: 5507
	public struct TrimSlice : IProgramNodeBuilder, IEquatable<TrimSlice>
	{
		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x0600B448 RID: 46152 RVA: 0x0027495A File Offset: 0x00272B5A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B449 RID: 46153 RVA: 0x00274962 File Offset: 0x00272B62
		private TrimSlice(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B44A RID: 46154 RVA: 0x0027496B File Offset: 0x00272B6B
		public static TrimSlice CreateUnsafe(ProgramNode node)
		{
			return new TrimSlice(node);
		}

		// Token: 0x0600B44B RID: 46155 RVA: 0x00274974 File Offset: 0x00272B74
		public static TrimSlice? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimSlice)
			{
				return null;
			}
			return new TrimSlice?(TrimSlice.CreateUnsafe(node));
		}

		// Token: 0x0600B44C RID: 46156 RVA: 0x002749A9 File Offset: 0x00272BA9
		public TrimSlice(GrammarBuilders g, slice value0)
		{
			this._node = g.Rule.TrimSlice.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B44D RID: 46157 RVA: 0x002749C8 File Offset: 0x00272BC8
		public static implicit operator sliceTrim(TrimSlice arg)
		{
			return sliceTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x0600B44E RID: 46158 RVA: 0x002749D6 File Offset: 0x00272BD6
		public slice slice
		{
			get
			{
				return slice.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B44F RID: 46159 RVA: 0x002749EA File Offset: 0x00272BEA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B450 RID: 46160 RVA: 0x00274A00 File Offset: 0x00272C00
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B451 RID: 46161 RVA: 0x00274A2A File Offset: 0x00272C2A
		public bool Equals(TrimSlice other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004631 RID: 17969
		private ProgramNode _node;
	}
}

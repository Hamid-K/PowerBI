using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200157B RID: 5499
	public struct SlicePrefixAbs : IProgramNodeBuilder, IEquatable<SlicePrefixAbs>
	{
		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x0600B3EF RID: 46063 RVA: 0x00274156 File Offset: 0x00272356
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B3F0 RID: 46064 RVA: 0x0027415E File Offset: 0x0027235E
		private SlicePrefixAbs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B3F1 RID: 46065 RVA: 0x00274167 File Offset: 0x00272367
		public static SlicePrefixAbs CreateUnsafe(ProgramNode node)
		{
			return new SlicePrefixAbs(node);
		}

		// Token: 0x0600B3F2 RID: 46066 RVA: 0x00274170 File Offset: 0x00272370
		public static SlicePrefixAbs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SlicePrefixAbs)
			{
				return null;
			}
			return new SlicePrefixAbs?(SlicePrefixAbs.CreateUnsafe(node));
		}

		// Token: 0x0600B3F3 RID: 46067 RVA: 0x002741A5 File Offset: 0x002723A5
		public SlicePrefixAbs(GrammarBuilders g, x value0, slicePrefixAbsPos value1)
		{
			this._node = g.Rule.SlicePrefixAbs.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B3F4 RID: 46068 RVA: 0x002741CB File Offset: 0x002723CB
		public static implicit operator substring(SlicePrefixAbs arg)
		{
			return substring.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x0600B3F5 RID: 46069 RVA: 0x002741D9 File Offset: 0x002723D9
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x0600B3F6 RID: 46070 RVA: 0x002741ED File Offset: 0x002723ED
		public slicePrefixAbsPos slicePrefixAbsPos
		{
			get
			{
				return slicePrefixAbsPos.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B3F7 RID: 46071 RVA: 0x00274201 File Offset: 0x00272401
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B3F8 RID: 46072 RVA: 0x00274214 File Offset: 0x00272414
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B3F9 RID: 46073 RVA: 0x0027423E File Offset: 0x0027243E
		public bool Equals(SlicePrefixAbs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004629 RID: 17961
		private ProgramNode _node;
	}
}

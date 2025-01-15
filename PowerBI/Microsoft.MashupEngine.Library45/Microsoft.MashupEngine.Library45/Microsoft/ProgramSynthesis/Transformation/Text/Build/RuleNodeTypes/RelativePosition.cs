using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C18 RID: 7192
	public struct RelativePosition : IProgramNodeBuilder, IEquatable<RelativePosition>
	{
		// Token: 0x17002872 RID: 10354
		// (get) Token: 0x0600F211 RID: 61969 RVA: 0x003407E2 File Offset: 0x0033E9E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F212 RID: 61970 RVA: 0x003407EA File Offset: 0x0033E9EA
		private RelativePosition(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F213 RID: 61971 RVA: 0x003407F3 File Offset: 0x0033E9F3
		public static RelativePosition CreateUnsafe(ProgramNode node)
		{
			return new RelativePosition(node);
		}

		// Token: 0x0600F214 RID: 61972 RVA: 0x003407FC File Offset: 0x0033E9FC
		public static RelativePosition? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RelativePosition)
			{
				return null;
			}
			return new RelativePosition?(RelativePosition.CreateUnsafe(node));
		}

		// Token: 0x0600F215 RID: 61973 RVA: 0x00340831 File Offset: 0x0033EA31
		public RelativePosition(GrammarBuilders g, x value0, k value1)
		{
			this._node = g.Rule.RelativePosition.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F216 RID: 61974 RVA: 0x00340857 File Offset: 0x0033EA57
		public static implicit operator pos(RelativePosition arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002873 RID: 10355
		// (get) Token: 0x0600F217 RID: 61975 RVA: 0x00340865 File Offset: 0x0033EA65
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002874 RID: 10356
		// (get) Token: 0x0600F218 RID: 61976 RVA: 0x00340879 File Offset: 0x0033EA79
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F219 RID: 61977 RVA: 0x0034088D File Offset: 0x0033EA8D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F21A RID: 61978 RVA: 0x003408A0 File Offset: 0x0033EAA0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F21B RID: 61979 RVA: 0x003408CA File Offset: 0x0033EACA
		public bool Equals(RelativePosition other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B07 RID: 23303
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E6E RID: 7790
	public struct Children : IProgramNodeBuilder, IEquatable<Children>
	{
		// Token: 0x17002BB5 RID: 11189
		// (get) Token: 0x060106B2 RID: 67250 RVA: 0x0038A152 File Offset: 0x00388352
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106B3 RID: 67251 RVA: 0x0038A15A File Offset: 0x0038835A
		private Children(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106B4 RID: 67252 RVA: 0x0038A163 File Offset: 0x00388363
		public static Children CreateUnsafe(ProgramNode node)
		{
			return new Children(node);
		}

		// Token: 0x060106B5 RID: 67253 RVA: 0x0038A16C File Offset: 0x0038836C
		public static Children? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Children)
			{
				return null;
			}
			return new Children?(Children.CreateUnsafe(node));
		}

		// Token: 0x060106B6 RID: 67254 RVA: 0x0038A1A1 File Offset: 0x003883A1
		public Children(GrammarBuilders g, parent value0)
		{
			this._node = g.Rule.Children.BuildASTNode(value0.Node);
		}

		// Token: 0x060106B7 RID: 67255 RVA: 0x0038A1C0 File Offset: 0x003883C0
		public static implicit operator parentChildren(Children arg)
		{
			return parentChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BB6 RID: 11190
		// (get) Token: 0x060106B8 RID: 67256 RVA: 0x0038A1CE File Offset: 0x003883CE
		public parent parent
		{
			get
			{
				return parent.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060106B9 RID: 67257 RVA: 0x0038A1E2 File Offset: 0x003883E2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106BA RID: 67258 RVA: 0x0038A1F8 File Offset: 0x003883F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106BB RID: 67259 RVA: 0x0038A222 File Offset: 0x00388422
		public bool Equals(Children other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062AD RID: 25261
		private ProgramNode _node;
	}
}

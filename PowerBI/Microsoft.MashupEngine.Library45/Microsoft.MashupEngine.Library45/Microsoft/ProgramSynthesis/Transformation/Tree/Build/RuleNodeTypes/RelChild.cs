using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E71 RID: 7793
	public struct RelChild : IProgramNodeBuilder, IEquatable<RelChild>
	{
		// Token: 0x17002BBC RID: 11196
		// (get) Token: 0x060106D1 RID: 67281 RVA: 0x0038A416 File Offset: 0x00388616
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106D2 RID: 67282 RVA: 0x0038A41E File Offset: 0x0038861E
		private RelChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106D3 RID: 67283 RVA: 0x0038A427 File Offset: 0x00388627
		public static RelChild CreateUnsafe(ProgramNode node)
		{
			return new RelChild(node);
		}

		// Token: 0x060106D4 RID: 67284 RVA: 0x0038A430 File Offset: 0x00388630
		public static RelChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RelChild)
			{
				return null;
			}
			return new RelChild?(RelChild.CreateUnsafe(node));
		}

		// Token: 0x060106D5 RID: 67285 RVA: 0x0038A465 File Offset: 0x00388665
		public RelChild(GrammarBuilders g, select value0)
		{
			this._node = g.Rule.RelChild.BuildASTNode(value0.Node);
		}

		// Token: 0x060106D6 RID: 67286 RVA: 0x0038A484 File Offset: 0x00388684
		public static implicit operator relChild(RelChild arg)
		{
			return relChild.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BBD RID: 11197
		// (get) Token: 0x060106D7 RID: 67287 RVA: 0x0038A492 File Offset: 0x00388692
		public select select
		{
			get
			{
				return select.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060106D8 RID: 67288 RVA: 0x0038A4A6 File Offset: 0x003886A6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106D9 RID: 67289 RVA: 0x0038A4BC File Offset: 0x003886BC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106DA RID: 67290 RVA: 0x0038A4E6 File Offset: 0x003886E6
		public bool Equals(RelChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B0 RID: 25264
		private ProgramNode _node;
	}
}

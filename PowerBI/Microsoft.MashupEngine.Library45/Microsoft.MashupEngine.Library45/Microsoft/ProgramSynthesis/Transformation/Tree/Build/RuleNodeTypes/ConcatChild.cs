using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E6F RID: 7791
	public struct ConcatChild : IProgramNodeBuilder, IEquatable<ConcatChild>
	{
		// Token: 0x17002BB7 RID: 11191
		// (get) Token: 0x060106BC RID: 67260 RVA: 0x0038A236 File Offset: 0x00388436
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106BD RID: 67261 RVA: 0x0038A23E File Offset: 0x0038843E
		private ConcatChild(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106BE RID: 67262 RVA: 0x0038A247 File Offset: 0x00388447
		public static ConcatChild CreateUnsafe(ProgramNode node)
		{
			return new ConcatChild(node);
		}

		// Token: 0x060106BF RID: 67263 RVA: 0x0038A250 File Offset: 0x00388450
		public static ConcatChild? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConcatChild)
			{
				return null;
			}
			return new ConcatChild?(ConcatChild.CreateUnsafe(node));
		}

		// Token: 0x060106C0 RID: 67264 RVA: 0x0038A285 File Offset: 0x00388485
		public ConcatChild(GrammarBuilders g, singleRelChildList value0, relChildList value1)
		{
			this._node = g.Rule.ConcatChild.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060106C1 RID: 67265 RVA: 0x0038A2AB File Offset: 0x003884AB
		public static implicit operator relChildList(ConcatChild arg)
		{
			return relChildList.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BB8 RID: 11192
		// (get) Token: 0x060106C2 RID: 67266 RVA: 0x0038A2B9 File Offset: 0x003884B9
		public singleRelChildList singleRelChildList
		{
			get
			{
				return singleRelChildList.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002BB9 RID: 11193
		// (get) Token: 0x060106C3 RID: 67267 RVA: 0x0038A2CD File Offset: 0x003884CD
		public relChildList relChildList
		{
			get
			{
				return relChildList.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060106C4 RID: 67268 RVA: 0x0038A2E1 File Offset: 0x003884E1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106C5 RID: 67269 RVA: 0x0038A2F4 File Offset: 0x003884F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106C6 RID: 67270 RVA: 0x0038A31E File Offset: 0x0038851E
		public bool Equals(ConcatChild other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062AE RID: 25262
		private ProgramNode _node;
	}
}

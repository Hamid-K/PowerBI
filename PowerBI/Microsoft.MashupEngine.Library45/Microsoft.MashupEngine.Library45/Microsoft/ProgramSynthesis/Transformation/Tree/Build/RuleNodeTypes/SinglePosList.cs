using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E70 RID: 7792
	public struct SinglePosList : IProgramNodeBuilder, IEquatable<SinglePosList>
	{
		// Token: 0x17002BBA RID: 11194
		// (get) Token: 0x060106C7 RID: 67271 RVA: 0x0038A332 File Offset: 0x00388532
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106C8 RID: 67272 RVA: 0x0038A33A File Offset: 0x0038853A
		private SinglePosList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106C9 RID: 67273 RVA: 0x0038A343 File Offset: 0x00388543
		public static SinglePosList CreateUnsafe(ProgramNode node)
		{
			return new SinglePosList(node);
		}

		// Token: 0x060106CA RID: 67274 RVA: 0x0038A34C File Offset: 0x0038854C
		public static SinglePosList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SinglePosList)
			{
				return null;
			}
			return new SinglePosList?(SinglePosList.CreateUnsafe(node));
		}

		// Token: 0x060106CB RID: 67275 RVA: 0x0038A381 File Offset: 0x00388581
		public SinglePosList(GrammarBuilders g, relChild value0)
		{
			this._node = g.Rule.SinglePosList.BuildASTNode(value0.Node);
		}

		// Token: 0x060106CC RID: 67276 RVA: 0x0038A3A0 File Offset: 0x003885A0
		public static implicit operator singleRelChildList(SinglePosList arg)
		{
			return singleRelChildList.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BBB RID: 11195
		// (get) Token: 0x060106CD RID: 67277 RVA: 0x0038A3AE File Offset: 0x003885AE
		public relChild relChild
		{
			get
			{
				return relChild.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060106CE RID: 67278 RVA: 0x0038A3C2 File Offset: 0x003885C2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106CF RID: 67279 RVA: 0x0038A3D8 File Offset: 0x003885D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106D0 RID: 67280 RVA: 0x0038A402 File Offset: 0x00388602
		public bool Equals(SinglePosList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062AF RID: 25263
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E72 RID: 7794
	public struct AbsPos : IProgramNodeBuilder, IEquatable<AbsPos>
	{
		// Token: 0x17002BBE RID: 11198
		// (get) Token: 0x060106DB RID: 67291 RVA: 0x0038A4FA File Offset: 0x003886FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060106DC RID: 67292 RVA: 0x0038A502 File Offset: 0x00388702
		private AbsPos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060106DD RID: 67293 RVA: 0x0038A50B File Offset: 0x0038870B
		public static AbsPos CreateUnsafe(ProgramNode node)
		{
			return new AbsPos(node);
		}

		// Token: 0x060106DE RID: 67294 RVA: 0x0038A514 File Offset: 0x00388714
		public static AbsPos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AbsPos)
			{
				return null;
			}
			return new AbsPos?(AbsPos.CreateUnsafe(node));
		}

		// Token: 0x060106DF RID: 67295 RVA: 0x0038A549 File Offset: 0x00388749
		public AbsPos(GrammarBuilders g, p value0)
		{
			this._node = g.Rule.AbsPos.BuildASTNode(value0.Node);
		}

		// Token: 0x060106E0 RID: 67296 RVA: 0x0038A568 File Offset: 0x00388768
		public static implicit operator pos(AbsPos arg)
		{
			return pos.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002BBF RID: 11199
		// (get) Token: 0x060106E1 RID: 67297 RVA: 0x0038A576 File Offset: 0x00388776
		public p p
		{
			get
			{
				return p.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060106E2 RID: 67298 RVA: 0x0038A58A File Offset: 0x0038878A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060106E3 RID: 67299 RVA: 0x0038A5A0 File Offset: 0x003887A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060106E4 RID: 67300 RVA: 0x0038A5CA File Offset: 0x003887CA
		public bool Equals(AbsPos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062B1 RID: 25265
		private ProgramNode _node;
	}
}

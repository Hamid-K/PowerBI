using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes
{
	// Token: 0x02001E65 RID: 7781
	public struct Not : IProgramNodeBuilder, IEquatable<Not>
	{
		// Token: 0x17002B94 RID: 11156
		// (get) Token: 0x06010649 RID: 67145 RVA: 0x003897AA File Offset: 0x003879AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601064A RID: 67146 RVA: 0x003897B2 File Offset: 0x003879B2
		private Not(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601064B RID: 67147 RVA: 0x003897BB File Offset: 0x003879BB
		public static Not CreateUnsafe(ProgramNode node)
		{
			return new Not(node);
		}

		// Token: 0x0601064C RID: 67148 RVA: 0x003897C4 File Offset: 0x003879C4
		public static Not? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Not)
			{
				return null;
			}
			return new Not?(Not.CreateUnsafe(node));
		}

		// Token: 0x0601064D RID: 67149 RVA: 0x003897F9 File Offset: 0x003879F9
		public Not(GrammarBuilders g, pred value0)
		{
			this._node = g.Rule.Not.BuildASTNode(value0.Node);
		}

		// Token: 0x0601064E RID: 67150 RVA: 0x00389818 File Offset: 0x00387A18
		public static implicit operator pred(Not arg)
		{
			return pred.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B95 RID: 11157
		// (get) Token: 0x0601064F RID: 67151 RVA: 0x00389826 File Offset: 0x00387A26
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06010650 RID: 67152 RVA: 0x0038983A File Offset: 0x00387A3A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010651 RID: 67153 RVA: 0x00389850 File Offset: 0x00387A50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010652 RID: 67154 RVA: 0x0038987A File Offset: 0x00387A7A
		public bool Equals(Not other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062A4 RID: 25252
		private ProgramNode _node;
	}
}

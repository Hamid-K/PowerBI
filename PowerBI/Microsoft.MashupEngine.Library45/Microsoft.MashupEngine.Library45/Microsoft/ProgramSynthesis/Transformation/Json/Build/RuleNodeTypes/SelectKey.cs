using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A2C RID: 6700
	public struct SelectKey : IProgramNodeBuilder, IEquatable<SelectKey>
	{
		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x0600DC27 RID: 56359 RVA: 0x002EE83A File Offset: 0x002ECA3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC28 RID: 56360 RVA: 0x002EE842 File Offset: 0x002ECA42
		private SelectKey(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC29 RID: 56361 RVA: 0x002EE84B File Offset: 0x002ECA4B
		public static SelectKey CreateUnsafe(ProgramNode node)
		{
			return new SelectKey(node);
		}

		// Token: 0x0600DC2A RID: 56362 RVA: 0x002EE854 File Offset: 0x002ECA54
		public static SelectKey? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectKey)
			{
				return null;
			}
			return new SelectKey?(SelectKey.CreateUnsafe(node));
		}

		// Token: 0x0600DC2B RID: 56363 RVA: 0x002EE889 File Offset: 0x002ECA89
		public SelectKey(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.SelectKey.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC2C RID: 56364 RVA: 0x002EE8AF File Offset: 0x002ECAAF
		public static implicit operator selectKey(SelectKey arg)
		{
			return selectKey.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x0600DC2D RID: 56365 RVA: 0x002EE8BD File Offset: 0x002ECABD
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x0600DC2E RID: 56366 RVA: 0x002EE8D1 File Offset: 0x002ECAD1
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC2F RID: 56367 RVA: 0x002EE8E5 File Offset: 0x002ECAE5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC30 RID: 56368 RVA: 0x002EE8F8 File Offset: 0x002ECAF8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC31 RID: 56369 RVA: 0x002EE922 File Offset: 0x002ECB22
		public bool Equals(SelectKey other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400541D RID: 21533
		private ProgramNode _node;
	}
}

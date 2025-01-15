using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A2D RID: 6701
	public struct SelectValue : IProgramNodeBuilder, IEquatable<SelectValue>
	{
		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x0600DC32 RID: 56370 RVA: 0x002EE936 File Offset: 0x002ECB36
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC33 RID: 56371 RVA: 0x002EE93E File Offset: 0x002ECB3E
		private SelectValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC34 RID: 56372 RVA: 0x002EE947 File Offset: 0x002ECB47
		public static SelectValue CreateUnsafe(ProgramNode node)
		{
			return new SelectValue(node);
		}

		// Token: 0x0600DC35 RID: 56373 RVA: 0x002EE950 File Offset: 0x002ECB50
		public static SelectValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectValue)
			{
				return null;
			}
			return new SelectValue?(SelectValue.CreateUnsafe(node));
		}

		// Token: 0x0600DC36 RID: 56374 RVA: 0x002EE985 File Offset: 0x002ECB85
		public SelectValue(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.SelectValue.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC37 RID: 56375 RVA: 0x002EE9AB File Offset: 0x002ECBAB
		public static implicit operator selectValue(SelectValue arg)
		{
			return selectValue.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x0600DC38 RID: 56376 RVA: 0x002EE9B9 File Offset: 0x002ECBB9
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x0600DC39 RID: 56377 RVA: 0x002EE9CD File Offset: 0x002ECBCD
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC3A RID: 56378 RVA: 0x002EE9E1 File Offset: 0x002ECBE1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC3B RID: 56379 RVA: 0x002EE9F4 File Offset: 0x002ECBF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC3C RID: 56380 RVA: 0x002EEA1E File Offset: 0x002ECC1E
		public bool Equals(SelectValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400541E RID: 21534
		private ProgramNode _node;
	}
}

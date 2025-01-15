using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A2A RID: 6698
	public struct Key : IProgramNodeBuilder, IEquatable<Key>
	{
		// Token: 0x170024DE RID: 9438
		// (get) Token: 0x0600DC13 RID: 56339 RVA: 0x002EE672 File Offset: 0x002EC872
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC14 RID: 56340 RVA: 0x002EE67A File Offset: 0x002EC87A
		private Key(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC15 RID: 56341 RVA: 0x002EE683 File Offset: 0x002EC883
		public static Key CreateUnsafe(ProgramNode node)
		{
			return new Key(node);
		}

		// Token: 0x0600DC16 RID: 56342 RVA: 0x002EE68C File Offset: 0x002EC88C
		public static Key? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Key)
			{
				return null;
			}
			return new Key?(Key.CreateUnsafe(node));
		}

		// Token: 0x0600DC17 RID: 56343 RVA: 0x002EE6C1 File Offset: 0x002EC8C1
		public Key(GrammarBuilders g, selectValue value0)
		{
			this._node = g.Rule.Key.BuildASTNode(value0.Node);
		}

		// Token: 0x0600DC18 RID: 56344 RVA: 0x002EE6E0 File Offset: 0x002EC8E0
		public static implicit operator key(Key arg)
		{
			return key.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024DF RID: 9439
		// (get) Token: 0x0600DC19 RID: 56345 RVA: 0x002EE6EE File Offset: 0x002EC8EE
		public selectValue selectValue
		{
			get
			{
				return selectValue.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600DC1A RID: 56346 RVA: 0x002EE702 File Offset: 0x002EC902
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC1B RID: 56347 RVA: 0x002EE718 File Offset: 0x002EC918
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC1C RID: 56348 RVA: 0x002EE742 File Offset: 0x002EC942
		public bool Equals(Key other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400541B RID: 21531
		private ProgramNode _node;
	}
}

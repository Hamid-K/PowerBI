using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A29 RID: 6697
	public struct SelectProperty : IProgramNodeBuilder, IEquatable<SelectProperty>
	{
		// Token: 0x170024DB RID: 9435
		// (get) Token: 0x0600DC08 RID: 56328 RVA: 0x002EE576 File Offset: 0x002EC776
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC09 RID: 56329 RVA: 0x002EE57E File Offset: 0x002EC77E
		private SelectProperty(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC0A RID: 56330 RVA: 0x002EE587 File Offset: 0x002EC787
		public static SelectProperty CreateUnsafe(ProgramNode node)
		{
			return new SelectProperty(node);
		}

		// Token: 0x0600DC0B RID: 56331 RVA: 0x002EE590 File Offset: 0x002EC790
		public static SelectProperty? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SelectProperty)
			{
				return null;
			}
			return new SelectProperty?(SelectProperty.CreateUnsafe(node));
		}

		// Token: 0x0600DC0C RID: 56332 RVA: 0x002EE5C5 File Offset: 0x002EC7C5
		public SelectProperty(GrammarBuilders g, x value0, path value1)
		{
			this._node = g.Rule.SelectProperty.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600DC0D RID: 56333 RVA: 0x002EE5EB File Offset: 0x002EC7EB
		public static implicit operator property(SelectProperty arg)
		{
			return property.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024DC RID: 9436
		// (get) Token: 0x0600DC0E RID: 56334 RVA: 0x002EE5F9 File Offset: 0x002EC7F9
		public x x
		{
			get
			{
				return x.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170024DD RID: 9437
		// (get) Token: 0x0600DC0F RID: 56335 RVA: 0x002EE60D File Offset: 0x002EC80D
		public path path
		{
			get
			{
				return path.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC10 RID: 56336 RVA: 0x002EE621 File Offset: 0x002EC821
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC11 RID: 56337 RVA: 0x002EE634 File Offset: 0x002EC834
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC12 RID: 56338 RVA: 0x002EE65E File Offset: 0x002EC85E
		public bool Equals(SelectProperty other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400541A RID: 21530
		private ProgramNode _node;
	}
}

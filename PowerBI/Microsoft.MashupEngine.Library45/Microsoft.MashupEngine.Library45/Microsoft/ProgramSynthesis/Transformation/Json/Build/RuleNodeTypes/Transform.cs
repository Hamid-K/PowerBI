using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A34 RID: 6708
	public struct Transform : IProgramNodeBuilder, IEquatable<Transform>
	{
		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x0600DC7E RID: 56446 RVA: 0x002EF006 File Offset: 0x002ED206
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DC7F RID: 56447 RVA: 0x002EF00E File Offset: 0x002ED20E
		private Transform(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DC80 RID: 56448 RVA: 0x002EF017 File Offset: 0x002ED217
		public static Transform CreateUnsafe(ProgramNode node)
		{
			return new Transform(node);
		}

		// Token: 0x0600DC81 RID: 56449 RVA: 0x002EF020 File Offset: 0x002ED220
		public static Transform? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Transform)
			{
				return null;
			}
			return new Transform?(Transform.CreateUnsafe(node));
		}

		// Token: 0x0600DC82 RID: 56450 RVA: 0x002EF055 File Offset: 0x002ED255
		public Transform(GrammarBuilders g, value value0, selectArray value1)
		{
			this._node = g.Rule.Transform.BuildConceptASTFromDslAST(new ProgramNode[] { value0.Node, value1.Node });
		}

		// Token: 0x0600DC83 RID: 56451 RVA: 0x002EF087 File Offset: 0x002ED287
		public static implicit operator elements(Transform arg)
		{
			return elements.CreateUnsafe(arg.Node);
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x0600DC84 RID: 56452 RVA: 0x002EF095 File Offset: 0x002ED295
		public value value
		{
			get
			{
				return value.CreateUnsafe(this.Node.Children[0].Children[0]);
			}
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x0600DC85 RID: 56453 RVA: 0x002EF0B0 File Offset: 0x002ED2B0
		public selectArray selectArray
		{
			get
			{
				return selectArray.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DC86 RID: 56454 RVA: 0x002EF0C4 File Offset: 0x002ED2C4
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DC87 RID: 56455 RVA: 0x002EF0D8 File Offset: 0x002ED2D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DC88 RID: 56456 RVA: 0x002EF102 File Offset: 0x002ED302
		public bool Equals(Transform other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005425 RID: 21541
		private ProgramNode _node;
	}
}

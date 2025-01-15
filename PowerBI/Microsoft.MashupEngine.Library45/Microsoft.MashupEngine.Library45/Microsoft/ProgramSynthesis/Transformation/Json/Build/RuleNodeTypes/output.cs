using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes
{
	// Token: 0x02001A38 RID: 6712
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x0600DCA8 RID: 56488 RVA: 0x002EF3EE File Offset: 0x002ED5EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DCA9 RID: 56489 RVA: 0x002EF3F6 File Offset: 0x002ED5F6
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DCAA RID: 56490 RVA: 0x002EF3FF File Offset: 0x002ED5FF
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x0600DCAB RID: 56491 RVA: 0x002EF408 File Offset: 0x002ED608
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x0600DCAC RID: 56492 RVA: 0x002EF43D File Offset: 0x002ED63D
		public output(GrammarBuilders g, v value0, value value1)
		{
			this._node = new LetNode(g.Rule.output, value0.Node, value1.Node);
		}

		// Token: 0x0600DCAD RID: 56493 RVA: 0x002EF463 File Offset: 0x002ED663
		public static implicit operator output(output arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x0600DCAE RID: 56494 RVA: 0x002EF471 File Offset: 0x002ED671
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x0600DCAF RID: 56495 RVA: 0x002EF485 File Offset: 0x002ED685
		public value value
		{
			get
			{
				return value.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600DCB0 RID: 56496 RVA: 0x002EF499 File Offset: 0x002ED699
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DCB1 RID: 56497 RVA: 0x002EF4AC File Offset: 0x002ED6AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DCB2 RID: 56498 RVA: 0x002EF4D6 File Offset: 0x002ED6D6
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005429 RID: 21545
		private ProgramNode _node;
	}
}

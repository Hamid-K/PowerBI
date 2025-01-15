using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C1C RID: 7196
	public struct RoundNumber : IProgramNodeBuilder, IEquatable<RoundNumber>
	{
		// Token: 0x17002880 RID: 10368
		// (get) Token: 0x0600F23F RID: 62015 RVA: 0x00340C0A File Offset: 0x0033EE0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F240 RID: 62016 RVA: 0x00340C12 File Offset: 0x0033EE12
		private RoundNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F241 RID: 62017 RVA: 0x00340C1B File Offset: 0x0033EE1B
		public static RoundNumber CreateUnsafe(ProgramNode node)
		{
			return new RoundNumber(node);
		}

		// Token: 0x0600F242 RID: 62018 RVA: 0x00340C24 File Offset: 0x0033EE24
		public static RoundNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.RoundNumber)
			{
				return null;
			}
			return new RoundNumber?(RoundNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F243 RID: 62019 RVA: 0x00340C59 File Offset: 0x0033EE59
		public RoundNumber(GrammarBuilders g, inputNumber value0, roundingSpec value1)
		{
			this._node = g.Rule.RoundNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F244 RID: 62020 RVA: 0x00340C7F File Offset: 0x0033EE7F
		public static implicit operator number(RoundNumber arg)
		{
			return number.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002881 RID: 10369
		// (get) Token: 0x0600F245 RID: 62021 RVA: 0x00340C8D File Offset: 0x0033EE8D
		public inputNumber inputNumber
		{
			get
			{
				return inputNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002882 RID: 10370
		// (get) Token: 0x0600F246 RID: 62022 RVA: 0x00340CA1 File Offset: 0x0033EEA1
		public roundingSpec roundingSpec
		{
			get
			{
				return roundingSpec.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F247 RID: 62023 RVA: 0x00340CB5 File Offset: 0x0033EEB5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F248 RID: 62024 RVA: 0x00340CC8 File Offset: 0x0033EEC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F249 RID: 62025 RVA: 0x00340CF2 File Offset: 0x0033EEF2
		public bool Equals(RoundNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B0B RID: 23307
		private ProgramNode _node;
	}
}

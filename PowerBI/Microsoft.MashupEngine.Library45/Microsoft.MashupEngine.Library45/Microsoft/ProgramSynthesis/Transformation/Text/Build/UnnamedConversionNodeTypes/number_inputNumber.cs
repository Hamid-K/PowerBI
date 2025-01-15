using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF6 RID: 7158
	public struct number_inputNumber : IProgramNodeBuilder, IEquatable<number_inputNumber>
	{
		// Token: 0x1700280E RID: 10254
		// (get) Token: 0x0600F09D RID: 61597 RVA: 0x0033E636 File Offset: 0x0033C836
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F09E RID: 61598 RVA: 0x0033E63E File Offset: 0x0033C83E
		private number_inputNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F09F RID: 61599 RVA: 0x0033E647 File Offset: 0x0033C847
		public static number_inputNumber CreateUnsafe(ProgramNode node)
		{
			return new number_inputNumber(node);
		}

		// Token: 0x0600F0A0 RID: 61600 RVA: 0x0033E650 File Offset: 0x0033C850
		public static number_inputNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.number_inputNumber)
			{
				return null;
			}
			return new number_inputNumber?(number_inputNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F0A1 RID: 61601 RVA: 0x0033E685 File Offset: 0x0033C885
		public number_inputNumber(GrammarBuilders g, inputNumber value0)
		{
			this._node = g.UnnamedConversion.number_inputNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F0A2 RID: 61602 RVA: 0x0033E6A4 File Offset: 0x0033C8A4
		public static implicit operator number(number_inputNumber arg)
		{
			return number.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700280F RID: 10255
		// (get) Token: 0x0600F0A3 RID: 61603 RVA: 0x0033E6B2 File Offset: 0x0033C8B2
		public inputNumber inputNumber
		{
			get
			{
				return inputNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F0A4 RID: 61604 RVA: 0x0033E6C6 File Offset: 0x0033C8C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0A5 RID: 61605 RVA: 0x0033E6DC File Offset: 0x0033C8DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0A6 RID: 61606 RVA: 0x0033E706 File Offset: 0x0033C906
		public bool Equals(number_inputNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE5 RID: 23269
		private ProgramNode _node;
	}
}

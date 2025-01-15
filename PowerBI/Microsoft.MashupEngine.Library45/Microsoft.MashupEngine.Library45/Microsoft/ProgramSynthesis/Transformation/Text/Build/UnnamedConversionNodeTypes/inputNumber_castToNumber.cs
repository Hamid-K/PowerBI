using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001BF7 RID: 7159
	public struct inputNumber_castToNumber : IProgramNodeBuilder, IEquatable<inputNumber_castToNumber>
	{
		// Token: 0x17002810 RID: 10256
		// (get) Token: 0x0600F0A7 RID: 61607 RVA: 0x0033E71A File Offset: 0x0033C91A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F0A8 RID: 61608 RVA: 0x0033E722 File Offset: 0x0033C922
		private inputNumber_castToNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F0A9 RID: 61609 RVA: 0x0033E72B File Offset: 0x0033C92B
		public static inputNumber_castToNumber CreateUnsafe(ProgramNode node)
		{
			return new inputNumber_castToNumber(node);
		}

		// Token: 0x0600F0AA RID: 61610 RVA: 0x0033E734 File Offset: 0x0033C934
		public static inputNumber_castToNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.inputNumber_castToNumber)
			{
				return null;
			}
			return new inputNumber_castToNumber?(inputNumber_castToNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F0AB RID: 61611 RVA: 0x0033E769 File Offset: 0x0033C969
		public inputNumber_castToNumber(GrammarBuilders g, castToNumber value0)
		{
			this._node = g.UnnamedConversion.inputNumber_castToNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F0AC RID: 61612 RVA: 0x0033E788 File Offset: 0x0033C988
		public static implicit operator inputNumber(inputNumber_castToNumber arg)
		{
			return inputNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x0600F0AD RID: 61613 RVA: 0x0033E796 File Offset: 0x0033C996
		public castToNumber castToNumber
		{
			get
			{
				return castToNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F0AE RID: 61614 RVA: 0x0033E7AA File Offset: 0x0033C9AA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F0AF RID: 61615 RVA: 0x0033E7C0 File Offset: 0x0033C9C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F0B0 RID: 61616 RVA: 0x0033E7EA File Offset: 0x0033C9EA
		public bool Equals(inputNumber_castToNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AE6 RID: 23270
		private ProgramNode _node;
	}
}

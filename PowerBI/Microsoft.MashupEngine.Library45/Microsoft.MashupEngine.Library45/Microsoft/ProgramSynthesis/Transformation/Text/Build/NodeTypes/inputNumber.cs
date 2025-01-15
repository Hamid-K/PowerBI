using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C51 RID: 7249
	public struct inputNumber : IProgramNodeBuilder, IEquatable<inputNumber>
	{
		// Token: 0x170028E7 RID: 10471
		// (get) Token: 0x0600F536 RID: 62774 RVA: 0x003473AE File Offset: 0x003455AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F537 RID: 62775 RVA: 0x003473B6 File Offset: 0x003455B6
		private inputNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F538 RID: 62776 RVA: 0x003473BF File Offset: 0x003455BF
		public static inputNumber CreateUnsafe(ProgramNode node)
		{
			return new inputNumber(node);
		}

		// Token: 0x0600F539 RID: 62777 RVA: 0x003473C8 File Offset: 0x003455C8
		public static inputNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inputNumber)
			{
				return null;
			}
			return new inputNumber?(inputNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F53A RID: 62778 RVA: 0x00347402 File Offset: 0x00345602
		public static inputNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inputNumber(new Hole(g.Symbol.inputNumber, holeId));
		}

		// Token: 0x0600F53B RID: 62779 RVA: 0x0034741A File Offset: 0x0034561A
		public bool Is_inputNumber_castToNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.inputNumber_castToNumber;
		}

		// Token: 0x0600F53C RID: 62780 RVA: 0x00347434 File Offset: 0x00345634
		public bool Is_inputNumber_castToNumber(GrammarBuilders g, out inputNumber_castToNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inputNumber_castToNumber)
			{
				value = inputNumber_castToNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(inputNumber_castToNumber);
			return false;
		}

		// Token: 0x0600F53D RID: 62781 RVA: 0x0034746C File Offset: 0x0034566C
		public inputNumber_castToNumber? As_inputNumber_castToNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.inputNumber_castToNumber)
			{
				return null;
			}
			return new inputNumber_castToNumber?(inputNumber_castToNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F53E RID: 62782 RVA: 0x003474AC File Offset: 0x003456AC
		public inputNumber_castToNumber Cast_inputNumber_castToNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inputNumber_castToNumber)
			{
				return inputNumber_castToNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_inputNumber_castToNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F53F RID: 62783 RVA: 0x00347501 File Offset: 0x00345701
		public bool Is_inputNumber_parsedNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.inputNumber_parsedNumber;
		}

		// Token: 0x0600F540 RID: 62784 RVA: 0x0034751B File Offset: 0x0034571B
		public bool Is_inputNumber_parsedNumber(GrammarBuilders g, out inputNumber_parsedNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inputNumber_parsedNumber)
			{
				value = inputNumber_parsedNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(inputNumber_parsedNumber);
			return false;
		}

		// Token: 0x0600F541 RID: 62785 RVA: 0x00347550 File Offset: 0x00345750
		public inputNumber_parsedNumber? As_inputNumber_parsedNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.inputNumber_parsedNumber)
			{
				return null;
			}
			return new inputNumber_parsedNumber?(inputNumber_parsedNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F542 RID: 62786 RVA: 0x00347590 File Offset: 0x00345790
		public inputNumber_parsedNumber Cast_inputNumber_parsedNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.inputNumber_parsedNumber)
			{
				return inputNumber_parsedNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_inputNumber_parsedNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F543 RID: 62787 RVA: 0x003475E8 File Offset: 0x003457E8
		public T Switch<T>(GrammarBuilders g, Func<inputNumber_castToNumber, T> func0, Func<inputNumber_parsedNumber, T> func1)
		{
			inputNumber_castToNumber inputNumber_castToNumber;
			if (this.Is_inputNumber_castToNumber(g, out inputNumber_castToNumber))
			{
				return func0(inputNumber_castToNumber);
			}
			inputNumber_parsedNumber inputNumber_parsedNumber;
			if (this.Is_inputNumber_parsedNumber(g, out inputNumber_parsedNumber))
			{
				return func1(inputNumber_parsedNumber);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol inputNumber");
		}

		// Token: 0x0600F544 RID: 62788 RVA: 0x00347640 File Offset: 0x00345840
		public void Switch(GrammarBuilders g, Action<inputNumber_castToNumber> func0, Action<inputNumber_parsedNumber> func1)
		{
			inputNumber_castToNumber inputNumber_castToNumber;
			if (this.Is_inputNumber_castToNumber(g, out inputNumber_castToNumber))
			{
				func0(inputNumber_castToNumber);
				return;
			}
			inputNumber_parsedNumber inputNumber_parsedNumber;
			if (this.Is_inputNumber_parsedNumber(g, out inputNumber_parsedNumber))
			{
				func1(inputNumber_parsedNumber);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol inputNumber");
		}

		// Token: 0x0600F545 RID: 62789 RVA: 0x00347697 File Offset: 0x00345897
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F546 RID: 62790 RVA: 0x003476AC File Offset: 0x003458AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F547 RID: 62791 RVA: 0x003476D6 File Offset: 0x003458D6
		public bool Equals(inputNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B40 RID: 23360
		private ProgramNode _node;
	}
}

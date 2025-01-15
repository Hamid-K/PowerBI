using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A3A RID: 6714
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x0600DCBE RID: 56510 RVA: 0x002EF5E6 File Offset: 0x002ED7E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DCBF RID: 56511 RVA: 0x002EF5EE File Offset: 0x002ED7EE
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DCC0 RID: 56512 RVA: 0x002EF5F7 File Offset: 0x002ED7F7
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x0600DCC1 RID: 56513 RVA: 0x002EF600 File Offset: 0x002ED800
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x0600DCC2 RID: 56514 RVA: 0x002EF63A File Offset: 0x002ED83A
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x0600DCC3 RID: 56515 RVA: 0x002EF652 File Offset: 0x002ED852
		public bool Is_output(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.output;
		}

		// Token: 0x0600DCC4 RID: 56516 RVA: 0x002EF66C File Offset: 0x002ED86C
		public bool Is_output(GrammarBuilders g, out output value)
		{
			if (this.Node.GrammarRule == g.Rule.output)
			{
				value = output.CreateUnsafe(this.Node);
				return true;
			}
			value = default(output);
			return false;
		}

		// Token: 0x0600DCC5 RID: 56517 RVA: 0x002EF6A4 File Offset: 0x002ED8A4
		public output? As_output(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCC6 RID: 56518 RVA: 0x002EF6E4 File Offset: 0x002ED8E4
		public output Cast_output(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.output)
			{
				return output.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_output is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCC7 RID: 56519 RVA: 0x002EF739 File Offset: 0x002ED939
		public bool Is_output_v(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.output_v;
		}

		// Token: 0x0600DCC8 RID: 56520 RVA: 0x002EF753 File Offset: 0x002ED953
		public bool Is_output_v(GrammarBuilders g, out output_v value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.output_v)
			{
				value = output_v.CreateUnsafe(this.Node);
				return true;
			}
			value = default(output_v);
			return false;
		}

		// Token: 0x0600DCC9 RID: 56521 RVA: 0x002EF788 File Offset: 0x002ED988
		public output_v? As_output_v(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.output_v)
			{
				return null;
			}
			return new output_v?(output_v.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCCA RID: 56522 RVA: 0x002EF7C8 File Offset: 0x002ED9C8
		public output_v Cast_output_v(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.output_v)
			{
				return output_v.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_output_v is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCCB RID: 56523 RVA: 0x002EF820 File Offset: 0x002EDA20
		public T Switch<T>(GrammarBuilders g, Func<output, T> func0, Func<output_v, T> func1)
		{
			output output;
			if (this.Is_output(g, out output))
			{
				return func0(output);
			}
			output_v output_v;
			if (this.Is_output_v(g, out output_v))
			{
				return func1(output_v);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x0600DCCC RID: 56524 RVA: 0x002EF878 File Offset: 0x002EDA78
		public void Switch(GrammarBuilders g, Action<output> func0, Action<output_v> func1)
		{
			output output;
			if (this.Is_output(g, out output))
			{
				func0(output);
				return;
			}
			output_v output_v;
			if (this.Is_output_v(g, out output_v))
			{
				func1(output_v);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x0600DCCD RID: 56525 RVA: 0x002EF8CF File Offset: 0x002EDACF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DCCE RID: 56526 RVA: 0x002EF8E4 File Offset: 0x002EDAE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DCCF RID: 56527 RVA: 0x002EF90E File Offset: 0x002EDB0E
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400542B RID: 21547
		private ProgramNode _node;
	}
}

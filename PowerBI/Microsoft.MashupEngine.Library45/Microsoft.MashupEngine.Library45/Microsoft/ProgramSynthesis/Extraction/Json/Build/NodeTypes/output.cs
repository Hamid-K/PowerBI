using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B65 RID: 2917
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x000E84C2 File Offset: 0x000E66C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x000E84CA File Offset: 0x000E66CA
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x000E84D3 File Offset: 0x000E66D3
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x000E84DC File Offset: 0x000E66DC
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x000E8516 File Offset: 0x000E6716
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x000E852E File Offset: 0x000E672E
		public bool Is_output_struct(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.output_struct;
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x000E8548 File Offset: 0x000E6748
		public bool Is_output_struct(GrammarBuilders g, out output_struct value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.output_struct)
			{
				value = output_struct.CreateUnsafe(this.Node);
				return true;
			}
			value = default(output_struct);
			return false;
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x000E8580 File Offset: 0x000E6780
		public output_struct? As_output_struct(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.output_struct)
			{
				return null;
			}
			return new output_struct?(output_struct.CreateUnsafe(this.Node));
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x000E85C0 File Offset: 0x000E67C0
		public output_struct Cast_output_struct(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.output_struct)
			{
				return output_struct.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_output_struct is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049C4 RID: 18884 RVA: 0x000E8615 File Offset: 0x000E6815
		public bool Is_output_sequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.output_sequence;
		}

		// Token: 0x060049C5 RID: 18885 RVA: 0x000E862F File Offset: 0x000E682F
		public bool Is_output_sequence(GrammarBuilders g, out output_sequence value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.output_sequence)
			{
				value = output_sequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(output_sequence);
			return false;
		}

		// Token: 0x060049C6 RID: 18886 RVA: 0x000E8664 File Offset: 0x000E6864
		public output_sequence? As_output_sequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.output_sequence)
			{
				return null;
			}
			return new output_sequence?(output_sequence.CreateUnsafe(this.Node));
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x000E86A4 File Offset: 0x000E68A4
		public output_sequence Cast_output_sequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.output_sequence)
			{
				return output_sequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_output_sequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x000E86FC File Offset: 0x000E68FC
		public T Switch<T>(GrammarBuilders g, Func<output_struct, T> func0, Func<output_sequence, T> func1)
		{
			output_struct output_struct;
			if (this.Is_output_struct(g, out output_struct))
			{
				return func0(output_struct);
			}
			output_sequence output_sequence;
			if (this.Is_output_sequence(g, out output_sequence))
			{
				return func1(output_sequence);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x000E8754 File Offset: 0x000E6954
		public void Switch(GrammarBuilders g, Action<output_struct> func0, Action<output_sequence> func1)
		{
			output_struct output_struct;
			if (this.Is_output_struct(g, out output_struct))
			{
				func0(output_struct);
				return;
			}
			output_sequence output_sequence;
			if (this.Is_output_sequence(g, out output_sequence))
			{
				func1(output_sequence);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x000E87AB File Offset: 0x000E69AB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x000E87C0 File Offset: 0x000E69C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x000E87EA File Offset: 0x000E69EA
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002160 RID: 8544
		private ProgramNode _node;
	}
}

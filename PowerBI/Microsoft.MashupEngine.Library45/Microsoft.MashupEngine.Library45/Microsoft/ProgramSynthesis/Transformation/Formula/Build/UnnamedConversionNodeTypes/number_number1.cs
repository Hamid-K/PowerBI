using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001530 RID: 5424
	public struct number_number1 : IProgramNodeBuilder, IEquatable<number_number1>
	{
		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x0600B0D7 RID: 45271 RVA: 0x0026FA2A File Offset: 0x0026DC2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0D8 RID: 45272 RVA: 0x0026FA32 File Offset: 0x0026DC32
		private number_number1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0D9 RID: 45273 RVA: 0x0026FA3B File Offset: 0x0026DC3B
		public static number_number1 CreateUnsafe(ProgramNode node)
		{
			return new number_number1(node);
		}

		// Token: 0x0600B0DA RID: 45274 RVA: 0x0026FA44 File Offset: 0x0026DC44
		public static number_number1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.number_number1)
			{
				return null;
			}
			return new number_number1?(number_number1.CreateUnsafe(node));
		}

		// Token: 0x0600B0DB RID: 45275 RVA: 0x0026FA79 File Offset: 0x0026DC79
		public number_number1(GrammarBuilders g, number1 value0)
		{
			this._node = g.UnnamedConversion.number_number1.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0DC RID: 45276 RVA: 0x0026FA98 File Offset: 0x0026DC98
		public static implicit operator number(number_number1 arg)
		{
			return number.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x0600B0DD RID: 45277 RVA: 0x0026FAA6 File Offset: 0x0026DCA6
		public number1 number1
		{
			get
			{
				return number1.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0DE RID: 45278 RVA: 0x0026FABA File Offset: 0x0026DCBA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0DF RID: 45279 RVA: 0x0026FAD0 File Offset: 0x0026DCD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0E0 RID: 45280 RVA: 0x0026FAFA File Offset: 0x0026DCFA
		public bool Equals(number_number1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045DE RID: 17886
		private ProgramNode _node;
	}
}

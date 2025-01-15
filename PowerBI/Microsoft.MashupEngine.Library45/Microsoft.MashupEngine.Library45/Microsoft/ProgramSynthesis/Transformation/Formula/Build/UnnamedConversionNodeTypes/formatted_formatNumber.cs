using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001523 RID: 5411
	public struct formatted_formatNumber : IProgramNodeBuilder, IEquatable<formatted_formatNumber>
	{
		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x0600B055 RID: 45141 RVA: 0x0026EE96 File Offset: 0x0026D096
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B056 RID: 45142 RVA: 0x0026EE9E File Offset: 0x0026D09E
		private formatted_formatNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B057 RID: 45143 RVA: 0x0026EEA7 File Offset: 0x0026D0A7
		public static formatted_formatNumber CreateUnsafe(ProgramNode node)
		{
			return new formatted_formatNumber(node);
		}

		// Token: 0x0600B058 RID: 45144 RVA: 0x0026EEB0 File Offset: 0x0026D0B0
		public static formatted_formatNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.formatted_formatNumber)
			{
				return null;
			}
			return new formatted_formatNumber?(formatted_formatNumber.CreateUnsafe(node));
		}

		// Token: 0x0600B059 RID: 45145 RVA: 0x0026EEE5 File Offset: 0x0026D0E5
		public formatted_formatNumber(GrammarBuilders g, formatNumber value0)
		{
			this._node = g.UnnamedConversion.formatted_formatNumber.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B05A RID: 45146 RVA: 0x0026EF04 File Offset: 0x0026D104
		public static implicit operator formatted(formatted_formatNumber arg)
		{
			return formatted.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x0600B05B RID: 45147 RVA: 0x0026EF12 File Offset: 0x0026D112
		public formatNumber formatNumber
		{
			get
			{
				return formatNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B05C RID: 45148 RVA: 0x0026EF26 File Offset: 0x0026D126
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B05D RID: 45149 RVA: 0x0026EF3C File Offset: 0x0026D13C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B05E RID: 45150 RVA: 0x0026EF66 File Offset: 0x0026D166
		public bool Equals(formatted_formatNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D1 RID: 17873
		private ProgramNode _node;
	}
}

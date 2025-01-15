using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001529 RID: 5417
	public struct concatPrefix_formatted : IProgramNodeBuilder, IEquatable<concatPrefix_formatted>
	{
		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x0600B091 RID: 45201 RVA: 0x0026F3EE File Offset: 0x0026D5EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B092 RID: 45202 RVA: 0x0026F3F6 File Offset: 0x0026D5F6
		private concatPrefix_formatted(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B093 RID: 45203 RVA: 0x0026F3FF File Offset: 0x0026D5FF
		public static concatPrefix_formatted CreateUnsafe(ProgramNode node)
		{
			return new concatPrefix_formatted(node);
		}

		// Token: 0x0600B094 RID: 45204 RVA: 0x0026F408 File Offset: 0x0026D608
		public static concatPrefix_formatted? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatPrefix_formatted)
			{
				return null;
			}
			return new concatPrefix_formatted?(concatPrefix_formatted.CreateUnsafe(node));
		}

		// Token: 0x0600B095 RID: 45205 RVA: 0x0026F43D File Offset: 0x0026D63D
		public concatPrefix_formatted(GrammarBuilders g, formatted value0)
		{
			this._node = g.UnnamedConversion.concatPrefix_formatted.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B096 RID: 45206 RVA: 0x0026F45C File Offset: 0x0026D65C
		public static implicit operator concatPrefix(concatPrefix_formatted arg)
		{
			return concatPrefix.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x0600B097 RID: 45207 RVA: 0x0026F46A File Offset: 0x0026D66A
		public formatted formatted
		{
			get
			{
				return formatted.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B098 RID: 45208 RVA: 0x0026F47E File Offset: 0x0026D67E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B099 RID: 45209 RVA: 0x0026F494 File Offset: 0x0026D694
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B09A RID: 45210 RVA: 0x0026F4BE File Offset: 0x0026D6BE
		public bool Equals(concatPrefix_formatted other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D7 RID: 17879
		private ProgramNode _node;
	}
}

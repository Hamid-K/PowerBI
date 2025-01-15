using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001528 RID: 5416
	public struct concatPrefix_concatSegment : IProgramNodeBuilder, IEquatable<concatPrefix_concatSegment>
	{
		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x0600B087 RID: 45191 RVA: 0x0026F30A File Offset: 0x0026D50A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B088 RID: 45192 RVA: 0x0026F312 File Offset: 0x0026D512
		private concatPrefix_concatSegment(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B089 RID: 45193 RVA: 0x0026F31B File Offset: 0x0026D51B
		public static concatPrefix_concatSegment CreateUnsafe(ProgramNode node)
		{
			return new concatPrefix_concatSegment(node);
		}

		// Token: 0x0600B08A RID: 45194 RVA: 0x0026F324 File Offset: 0x0026D524
		public static concatPrefix_concatSegment? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatPrefix_concatSegment)
			{
				return null;
			}
			return new concatPrefix_concatSegment?(concatPrefix_concatSegment.CreateUnsafe(node));
		}

		// Token: 0x0600B08B RID: 45195 RVA: 0x0026F359 File Offset: 0x0026D559
		public concatPrefix_concatSegment(GrammarBuilders g, concatSegment value0)
		{
			this._node = g.UnnamedConversion.concatPrefix_concatSegment.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B08C RID: 45196 RVA: 0x0026F378 File Offset: 0x0026D578
		public static implicit operator concatPrefix(concatPrefix_concatSegment arg)
		{
			return concatPrefix.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x0600B08D RID: 45197 RVA: 0x0026F386 File Offset: 0x0026D586
		public concatSegment concatSegment
		{
			get
			{
				return concatSegment.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B08E RID: 45198 RVA: 0x0026F39A File Offset: 0x0026D59A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B08F RID: 45199 RVA: 0x0026F3B0 File Offset: 0x0026D5B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B090 RID: 45200 RVA: 0x0026F3DA File Offset: 0x0026D5DA
		public bool Equals(concatPrefix_concatSegment other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D6 RID: 17878
		private ProgramNode _node;
	}
}

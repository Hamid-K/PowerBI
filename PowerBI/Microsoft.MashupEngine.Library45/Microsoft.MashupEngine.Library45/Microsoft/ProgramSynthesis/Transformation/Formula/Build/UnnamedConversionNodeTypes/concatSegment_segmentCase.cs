using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200152C RID: 5420
	public struct concatSegment_segmentCase : IProgramNodeBuilder, IEquatable<concatSegment_segmentCase>
	{
		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x0600B0AF RID: 45231 RVA: 0x0026F69A File Offset: 0x0026D89A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0B0 RID: 45232 RVA: 0x0026F6A2 File Offset: 0x0026D8A2
		private concatSegment_segmentCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0B1 RID: 45233 RVA: 0x0026F6AB File Offset: 0x0026D8AB
		public static concatSegment_segmentCase CreateUnsafe(ProgramNode node)
		{
			return new concatSegment_segmentCase(node);
		}

		// Token: 0x0600B0B2 RID: 45234 RVA: 0x0026F6B4 File Offset: 0x0026D8B4
		public static concatSegment_segmentCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatSegment_segmentCase)
			{
				return null;
			}
			return new concatSegment_segmentCase?(concatSegment_segmentCase.CreateUnsafe(node));
		}

		// Token: 0x0600B0B3 RID: 45235 RVA: 0x0026F6E9 File Offset: 0x0026D8E9
		public concatSegment_segmentCase(GrammarBuilders g, segmentCase value0)
		{
			this._node = g.UnnamedConversion.concatSegment_segmentCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0B4 RID: 45236 RVA: 0x0026F708 File Offset: 0x0026D908
		public static implicit operator concatSegment(concatSegment_segmentCase arg)
		{
			return concatSegment.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x0600B0B5 RID: 45237 RVA: 0x0026F716 File Offset: 0x0026D916
		public segmentCase segmentCase
		{
			get
			{
				return segmentCase.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0B6 RID: 45238 RVA: 0x0026F72A File Offset: 0x0026D92A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0B7 RID: 45239 RVA: 0x0026F740 File Offset: 0x0026D940
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0B8 RID: 45240 RVA: 0x0026F76A File Offset: 0x0026D96A
		public bool Equals(concatSegment_segmentCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045DA RID: 17882
		private ProgramNode _node;
	}
}

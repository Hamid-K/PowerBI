using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001520 RID: 5408
	public struct segmentCase_segment : IProgramNodeBuilder, IEquatable<segmentCase_segment>
	{
		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x0600B037 RID: 45111 RVA: 0x0026EBEA File Offset: 0x0026CDEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B038 RID: 45112 RVA: 0x0026EBF2 File Offset: 0x0026CDF2
		private segmentCase_segment(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B039 RID: 45113 RVA: 0x0026EBFB File Offset: 0x0026CDFB
		public static segmentCase_segment CreateUnsafe(ProgramNode node)
		{
			return new segmentCase_segment(node);
		}

		// Token: 0x0600B03A RID: 45114 RVA: 0x0026EC04 File Offset: 0x0026CE04
		public static segmentCase_segment? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.segmentCase_segment)
			{
				return null;
			}
			return new segmentCase_segment?(segmentCase_segment.CreateUnsafe(node));
		}

		// Token: 0x0600B03B RID: 45115 RVA: 0x0026EC39 File Offset: 0x0026CE39
		public segmentCase_segment(GrammarBuilders g, segment value0)
		{
			this._node = g.UnnamedConversion.segmentCase_segment.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B03C RID: 45116 RVA: 0x0026EC58 File Offset: 0x0026CE58
		public static implicit operator segmentCase(segmentCase_segment arg)
		{
			return segmentCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x0600B03D RID: 45117 RVA: 0x0026EC66 File Offset: 0x0026CE66
		public segment segment
		{
			get
			{
				return segment.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B03E RID: 45118 RVA: 0x0026EC7A File Offset: 0x0026CE7A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B03F RID: 45119 RVA: 0x0026EC90 File Offset: 0x0026CE90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B040 RID: 45120 RVA: 0x0026ECBA File Offset: 0x0026CEBA
		public bool Equals(segmentCase_segment other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045CE RID: 17870
		private ProgramNode _node;
	}
}

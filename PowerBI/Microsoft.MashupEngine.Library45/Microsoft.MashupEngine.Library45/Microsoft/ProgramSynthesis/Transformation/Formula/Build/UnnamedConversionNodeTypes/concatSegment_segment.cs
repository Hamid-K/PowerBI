using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200152B RID: 5419
	public struct concatSegment_segment : IProgramNodeBuilder, IEquatable<concatSegment_segment>
	{
		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x0600B0A5 RID: 45221 RVA: 0x0026F5B6 File Offset: 0x0026D7B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B0A6 RID: 45222 RVA: 0x0026F5BE File Offset: 0x0026D7BE
		private concatSegment_segment(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B0A7 RID: 45223 RVA: 0x0026F5C7 File Offset: 0x0026D7C7
		public static concatSegment_segment CreateUnsafe(ProgramNode node)
		{
			return new concatSegment_segment(node);
		}

		// Token: 0x0600B0A8 RID: 45224 RVA: 0x0026F5D0 File Offset: 0x0026D7D0
		public static concatSegment_segment? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.concatSegment_segment)
			{
				return null;
			}
			return new concatSegment_segment?(concatSegment_segment.CreateUnsafe(node));
		}

		// Token: 0x0600B0A9 RID: 45225 RVA: 0x0026F605 File Offset: 0x0026D805
		public concatSegment_segment(GrammarBuilders g, segment value0)
		{
			this._node = g.UnnamedConversion.concatSegment_segment.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B0AA RID: 45226 RVA: 0x0026F624 File Offset: 0x0026D824
		public static implicit operator concatSegment(concatSegment_segment arg)
		{
			return concatSegment.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x0600B0AB RID: 45227 RVA: 0x0026F632 File Offset: 0x0026D832
		public segment segment
		{
			get
			{
				return segment.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B0AC RID: 45228 RVA: 0x0026F646 File Offset: 0x0026D846
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B0AD RID: 45229 RVA: 0x0026F65C File Offset: 0x0026D85C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B0AE RID: 45230 RVA: 0x0026F686 File Offset: 0x0026D886
		public bool Equals(concatSegment_segment other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045D9 RID: 17881
		private ProgramNode _node;
	}
}

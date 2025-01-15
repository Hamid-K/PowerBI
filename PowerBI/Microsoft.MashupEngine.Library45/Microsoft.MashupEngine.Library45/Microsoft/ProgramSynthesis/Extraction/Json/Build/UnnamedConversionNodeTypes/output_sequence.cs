using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000B59 RID: 2905
	public struct output_sequence : IProgramNodeBuilder, IEquatable<output_sequence>
	{
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x000E793A File Offset: 0x000E5B3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x000E7942 File Offset: 0x000E5B42
		private output_sequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x000E794B File Offset: 0x000E5B4B
		public static output_sequence CreateUnsafe(ProgramNode node)
		{
			return new output_sequence(node);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x000E7954 File Offset: 0x000E5B54
		public static output_sequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.output_sequence)
			{
				return null;
			}
			return new output_sequence?(output_sequence.CreateUnsafe(node));
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x000E7989 File Offset: 0x000E5B89
		public output_sequence(GrammarBuilders g, sequence value0)
		{
			this._node = g.UnnamedConversion.output_sequence.BuildASTNode(value0.Node);
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x000E79A8 File Offset: 0x000E5BA8
		public static implicit operator output(output_sequence arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x000E79B6 File Offset: 0x000E5BB6
		public sequence sequence
		{
			get
			{
				return sequence.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x000E79CA File Offset: 0x000E5BCA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x000E79E0 File Offset: 0x000E5BE0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x000E7A0A File Offset: 0x000E5C0A
		public bool Equals(output_sequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002154 RID: 8532
		private ProgramNode _node;
	}
}

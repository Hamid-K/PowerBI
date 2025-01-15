using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000B58 RID: 2904
	public struct output_struct : IProgramNodeBuilder, IEquatable<output_struct>
	{
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x000E7857 File Offset: 0x000E5A57
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x000E785F File Offset: 0x000E5A5F
		private output_struct(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x000E7868 File Offset: 0x000E5A68
		public static output_struct CreateUnsafe(ProgramNode node)
		{
			return new output_struct(node);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x000E7870 File Offset: 0x000E5A70
		public static output_struct? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.output_struct)
			{
				return null;
			}
			return new output_struct?(output_struct.CreateUnsafe(node));
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x000E78A5 File Offset: 0x000E5AA5
		public output_struct(GrammarBuilders g, @struct value0)
		{
			this._node = g.UnnamedConversion.output_struct.BuildASTNode(value0.Node);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x000E78C4 File Offset: 0x000E5AC4
		public static implicit operator output(output_struct arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x000E78D2 File Offset: 0x000E5AD2
		public @struct @struct
		{
			get
			{
				return @struct.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x000E78E6 File Offset: 0x000E5AE6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x000E78FC File Offset: 0x000E5AFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x000E7926 File Offset: 0x000E5B26
		public bool Equals(output_struct other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002153 RID: 8531
		private ProgramNode _node;
	}
}

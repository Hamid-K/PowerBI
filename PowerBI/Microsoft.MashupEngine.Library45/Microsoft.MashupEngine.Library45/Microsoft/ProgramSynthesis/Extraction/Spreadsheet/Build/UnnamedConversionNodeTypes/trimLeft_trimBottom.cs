using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E09 RID: 3593
	public struct trimLeft_trimBottom : IProgramNodeBuilder, IEquatable<trimLeft_trimBottom>
	{
		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06005F9A RID: 24474 RVA: 0x0013C7F6 File Offset: 0x0013A9F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x0013C7FE File Offset: 0x0013A9FE
		private trimLeft_trimBottom(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x0013C807 File Offset: 0x0013AA07
		public static trimLeft_trimBottom CreateUnsafe(ProgramNode node)
		{
			return new trimLeft_trimBottom(node);
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x0013C810 File Offset: 0x0013AA10
		public static trimLeft_trimBottom? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.trimLeft_trimBottom)
			{
				return null;
			}
			return new trimLeft_trimBottom?(trimLeft_trimBottom.CreateUnsafe(node));
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x0013C845 File Offset: 0x0013AA45
		public trimLeft_trimBottom(GrammarBuilders g, trimBottom value0)
		{
			this._node = g.UnnamedConversion.trimLeft_trimBottom.BuildASTNode(value0.Node);
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x0013C864 File Offset: 0x0013AA64
		public static implicit operator trimLeft(trimLeft_trimBottom arg)
		{
			return trimLeft.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06005FA0 RID: 24480 RVA: 0x0013C872 File Offset: 0x0013AA72
		public trimBottom trimBottom
		{
			get
			{
				return trimBottom.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x0013C886 File Offset: 0x0013AA86
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x0013C89C File Offset: 0x0013AA9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x0013C8C6 File Offset: 0x0013AAC6
		public bool Equals(trimLeft_trimBottom other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB3 RID: 11187
		private ProgramNode _node;
	}
}

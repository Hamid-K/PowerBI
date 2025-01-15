using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E0B RID: 3595
	public struct trimTop_sheetSection : IProgramNodeBuilder, IEquatable<trimTop_sheetSection>
	{
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06005FAE RID: 24494 RVA: 0x0013C9BE File Offset: 0x0013ABBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x0013C9C6 File Offset: 0x0013ABC6
		private trimTop_sheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x0013C9CF File Offset: 0x0013ABCF
		public static trimTop_sheetSection CreateUnsafe(ProgramNode node)
		{
			return new trimTop_sheetSection(node);
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x0013C9D8 File Offset: 0x0013ABD8
		public static trimTop_sheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.trimTop_sheetSection)
			{
				return null;
			}
			return new trimTop_sheetSection?(trimTop_sheetSection.CreateUnsafe(node));
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x0013CA0D File Offset: 0x0013AC0D
		public trimTop_sheetSection(GrammarBuilders g, sheetSection value0)
		{
			this._node = g.UnnamedConversion.trimTop_sheetSection.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x0013CA2C File Offset: 0x0013AC2C
		public static implicit operator trimTop(trimTop_sheetSection arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06005FB4 RID: 24500 RVA: 0x0013CA3A File Offset: 0x0013AC3A
		public sheetSection sheetSection
		{
			get
			{
				return sheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x0013CA4E File Offset: 0x0013AC4E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x0013CA64 File Offset: 0x0013AC64
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x0013CA8E File Offset: 0x0013AC8E
		public bool Equals(trimTop_sheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB5 RID: 11189
		private ProgramNode _node;
	}
}

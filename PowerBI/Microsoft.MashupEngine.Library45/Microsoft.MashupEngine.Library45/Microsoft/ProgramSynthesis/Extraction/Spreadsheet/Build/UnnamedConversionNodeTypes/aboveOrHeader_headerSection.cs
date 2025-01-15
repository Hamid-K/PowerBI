using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E17 RID: 3607
	public struct aboveOrHeader_headerSection : IProgramNodeBuilder, IEquatable<aboveOrHeader_headerSection>
	{
		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06006026 RID: 24614 RVA: 0x0013D46E File Offset: 0x0013B66E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x0013D476 File Offset: 0x0013B676
		private aboveOrHeader_headerSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x0013D47F File Offset: 0x0013B67F
		public static aboveOrHeader_headerSection CreateUnsafe(ProgramNode node)
		{
			return new aboveOrHeader_headerSection(node);
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x0013D488 File Offset: 0x0013B688
		public static aboveOrHeader_headerSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.aboveOrHeader_headerSection)
			{
				return null;
			}
			return new aboveOrHeader_headerSection?(aboveOrHeader_headerSection.CreateUnsafe(node));
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x0013D4BD File Offset: 0x0013B6BD
		public aboveOrHeader_headerSection(GrammarBuilders g, headerSection value0)
		{
			this._node = g.UnnamedConversion.aboveOrHeader_headerSection.BuildASTNode(value0.Node);
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x0013D4DC File Offset: 0x0013B6DC
		public static implicit operator aboveOrHeader(aboveOrHeader_headerSection arg)
		{
			return aboveOrHeader.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600602C RID: 24620 RVA: 0x0013D4EA File Offset: 0x0013B6EA
		public headerSection headerSection
		{
			get
			{
				return headerSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x0013D4FE File Offset: 0x0013B6FE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x0013D514 File Offset: 0x0013B714
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x0013D53E File Offset: 0x0013B73E
		public bool Equals(aboveOrHeader_headerSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC1 RID: 11201
		private ProgramNode _node;
	}
}

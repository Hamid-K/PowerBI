using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes
{
	// Token: 0x0200127C RID: 4732
	public struct CreateStringRegion : IProgramNodeBuilder, IEquatable<CreateStringRegion>
	{
		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06008EFB RID: 36603 RVA: 0x001E1D9E File Offset: 0x001DFF9E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008EFC RID: 36604 RVA: 0x001E1DA6 File Offset: 0x001DFFA6
		private CreateStringRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008EFD RID: 36605 RVA: 0x001E1DAF File Offset: 0x001DFFAF
		public static CreateStringRegion CreateUnsafe(ProgramNode node)
		{
			return new CreateStringRegion(node);
		}

		// Token: 0x06008EFE RID: 36606 RVA: 0x001E1DB8 File Offset: 0x001DFFB8
		public static CreateStringRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.CreateStringRegion)
			{
				return null;
			}
			return new CreateStringRegion?(CreateStringRegion.CreateUnsafe(node));
		}

		// Token: 0x06008EFF RID: 36607 RVA: 0x001E1DED File Offset: 0x001DFFED
		public CreateStringRegion(GrammarBuilders g, file value0)
		{
			this._node = g.Rule.CreateStringRegion.BuildASTNode(value0.Node);
		}

		// Token: 0x06008F00 RID: 36608 RVA: 0x001E1E0C File Offset: 0x001E000C
		public static implicit operator _LetB0(CreateStringRegion arg)
		{
			return _LetB0.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06008F01 RID: 36609 RVA: 0x001E1E1A File Offset: 0x001E001A
		public file file
		{
			get
			{
				return file.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06008F02 RID: 36610 RVA: 0x001E1E2E File Offset: 0x001E002E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F03 RID: 36611 RVA: 0x001E1E44 File Offset: 0x001E0044
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F04 RID: 36612 RVA: 0x001E1E6E File Offset: 0x001E006E
		public bool Equals(CreateStringRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A6D RID: 14957
		private ProgramNode _node;
	}
}

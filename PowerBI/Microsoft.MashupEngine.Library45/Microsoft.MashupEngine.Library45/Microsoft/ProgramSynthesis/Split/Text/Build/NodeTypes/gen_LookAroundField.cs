using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200136F RID: 4975
	public struct gen_LookAroundField : IProgramNodeBuilder, IEquatable<gen_LookAroundField>
	{
		// Token: 0x17001A78 RID: 6776
		// (get) Token: 0x06009A35 RID: 39477 RVA: 0x0020A8D6 File Offset: 0x00208AD6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A36 RID: 39478 RVA: 0x0020A8DE File Offset: 0x00208ADE
		private gen_LookAroundField(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x0020A8E7 File Offset: 0x00208AE7
		public static gen_LookAroundField CreateUnsafe(ProgramNode node)
		{
			return new gen_LookAroundField(node);
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x0020A8F0 File Offset: 0x00208AF0
		public static gen_LookAroundField? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.gen_LookAroundField)
			{
				return null;
			}
			return new gen_LookAroundField?(gen_LookAroundField.CreateUnsafe(node));
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x0020A92A File Offset: 0x00208B2A
		public static gen_LookAroundField CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new gen_LookAroundField(new Hole(g.Symbol.gen_LookAroundField, holeId));
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x0020A942 File Offset: 0x00208B42
		public GEN_FieldLookAroundEndPoints Cast_GEN_FieldLookAroundEndPoints()
		{
			return GEN_FieldLookAroundEndPoints.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_GEN_FieldLookAroundEndPoints(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x0020A94F File Offset: 0x00208B4F
		public bool Is_GEN_FieldLookAroundEndPoints(GrammarBuilders g, out GEN_FieldLookAroundEndPoints value)
		{
			value = GEN_FieldLookAroundEndPoints.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x0020A963 File Offset: 0x00208B63
		public GEN_FieldLookAroundEndPoints? As_GEN_FieldLookAroundEndPoints(GrammarBuilders g)
		{
			return new GEN_FieldLookAroundEndPoints?(GEN_FieldLookAroundEndPoints.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A3E RID: 39486 RVA: 0x0020A975 File Offset: 0x00208B75
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A3F RID: 39487 RVA: 0x0020A988 File Offset: 0x00208B88
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A40 RID: 39488 RVA: 0x0020A9B2 File Offset: 0x00208BB2
		public bool Equals(gen_LookAroundField other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE6 RID: 15846
		private ProgramNode _node;
	}
}

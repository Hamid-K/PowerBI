using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E08 RID: 3592
	public struct area_trimLeft : IProgramNodeBuilder, IEquatable<area_trimLeft>
	{
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06005F90 RID: 24464 RVA: 0x0013C711 File Offset: 0x0013A911
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x0013C719 File Offset: 0x0013A919
		private area_trimLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x0013C722 File Offset: 0x0013A922
		public static area_trimLeft CreateUnsafe(ProgramNode node)
		{
			return new area_trimLeft(node);
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x0013C72C File Offset: 0x0013A92C
		public static area_trimLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.area_trimLeft)
			{
				return null;
			}
			return new area_trimLeft?(area_trimLeft.CreateUnsafe(node));
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x0013C761 File Offset: 0x0013A961
		public area_trimLeft(GrammarBuilders g, trimLeft value0)
		{
			this._node = g.UnnamedConversion.area_trimLeft.BuildASTNode(value0.Node);
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x0013C780 File Offset: 0x0013A980
		public static implicit operator area(area_trimLeft arg)
		{
			return area.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06005F96 RID: 24470 RVA: 0x0013C78E File Offset: 0x0013A98E
		public trimLeft trimLeft
		{
			get
			{
				return trimLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x0013C7A2 File Offset: 0x0013A9A2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x0013C7B8 File Offset: 0x0013A9B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x0013C7E2 File Offset: 0x0013A9E2
		public bool Equals(area_trimLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB2 RID: 11186
		private ProgramNode _node;
	}
}

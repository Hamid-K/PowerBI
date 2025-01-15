using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200108E RID: 4238
	public struct cssSelector : IProgramNodeBuilder, IEquatable<cssSelector>
	{
		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x06007F93 RID: 32659 RVA: 0x001AC2FA File Offset: 0x001AA4FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x001AC302 File Offset: 0x001AA502
		private cssSelector(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007F95 RID: 32661 RVA: 0x001AC30B File Offset: 0x001AA50B
		public static cssSelector CreateUnsafe(ProgramNode node)
		{
			return new cssSelector(node);
		}

		// Token: 0x06007F96 RID: 32662 RVA: 0x001AC314 File Offset: 0x001AA514
		public static cssSelector? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.cssSelector)
			{
				return null;
			}
			return new cssSelector?(cssSelector.CreateUnsafe(node));
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x001AC34E File Offset: 0x001AA54E
		public static cssSelector CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new cssSelector(new Hole(g.Symbol.cssSelector, holeId));
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x001AC366 File Offset: 0x001AA566
		public cssSelector(GrammarBuilders g, string value)
		{
			this = new cssSelector(new LiteralNode(g.Symbol.cssSelector, value));
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x06007F99 RID: 32665 RVA: 0x001AC37F File Offset: 0x001AA57F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x001AC396 File Offset: 0x001AA596
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x001AC3AC File Offset: 0x001AA5AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x001AC3D6 File Offset: 0x001AA5D6
		public bool Equals(cssSelector other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A7 RID: 13223
		private ProgramNode _node;
	}
}

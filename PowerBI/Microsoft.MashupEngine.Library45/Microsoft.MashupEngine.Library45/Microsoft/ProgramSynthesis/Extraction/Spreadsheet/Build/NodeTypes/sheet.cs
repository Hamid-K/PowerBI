using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E5D RID: 3677
	public struct sheet : IProgramNodeBuilder, IEquatable<sheet>
	{
		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06006373 RID: 25459 RVA: 0x001437DA File Offset: 0x001419DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x001437E2 File Offset: 0x001419E2
		private sheet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x001437EB File Offset: 0x001419EB
		public static sheet CreateUnsafe(ProgramNode node)
		{
			return new sheet(node);
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x001437F4 File Offset: 0x001419F4
		public static sheet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sheet)
			{
				return null;
			}
			return new sheet?(sheet.CreateUnsafe(node));
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x0014382E File Offset: 0x00141A2E
		public static sheet CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sheet(new Hole(g.Symbol.sheet, holeId));
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x00143846 File Offset: 0x00141A46
		public WithFormatting Cast_WithFormatting()
		{
			return WithFormatting.CreateUnsafe(this.Node);
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_WithFormatting(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x00143853 File Offset: 0x00141A53
		public bool Is_WithFormatting(GrammarBuilders g, out WithFormatting value)
		{
			value = WithFormatting.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x00143867 File Offset: 0x00141A67
		public WithFormatting? As_WithFormatting(GrammarBuilders g)
		{
			return new WithFormatting?(WithFormatting.CreateUnsafe(this.Node));
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x00143879 File Offset: 0x00141A79
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x0014388C File Offset: 0x00141A8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x001438B6 File Offset: 0x00141AB6
		public bool Equals(sheet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C07 RID: 11271
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000975 RID: 2421
	public struct newLineSep : IProgramNodeBuilder, IEquatable<newLineSep>
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000B2756 File Offset: 0x000B0956
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000B275E File Offset: 0x000B095E
		private newLineSep(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000B2767 File Offset: 0x000B0967
		public static newLineSep CreateUnsafe(ProgramNode node)
		{
			return new newLineSep(node);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000B2770 File Offset: 0x000B0970
		public static newLineSep? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.newLineSep)
			{
				return null;
			}
			return new newLineSep?(newLineSep.CreateUnsafe(node));
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000B27AA File Offset: 0x000B09AA
		public static newLineSep CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new newLineSep(new Hole(g.Symbol.newLineSep, holeId));
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000B27C2 File Offset: 0x000B09C2
		public newLineSep(GrammarBuilders g, string value)
		{
			this = new newLineSep(new LiteralNode(g.Symbol.newLineSep, value));
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060039BE RID: 14782 RVA: 0x000B27DB File Offset: 0x000B09DB
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000B27F2 File Offset: 0x000B09F2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000B2808 File Offset: 0x000B0A08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000B2832 File Offset: 0x000B0A32
		public bool Equals(newLineSep other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A95 RID: 6805
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E72 RID: 3698
	public struct styleFilter : IProgramNodeBuilder, IEquatable<styleFilter>
	{
		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x060064D9 RID: 25817 RVA: 0x00147176 File Offset: 0x00145376
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x0014717E File Offset: 0x0014537E
		private styleFilter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x00147187 File Offset: 0x00145387
		public static styleFilter CreateUnsafe(ProgramNode node)
		{
			return new styleFilter(node);
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x00147190 File Offset: 0x00145390
		public static styleFilter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.styleFilter)
			{
				return null;
			}
			return new styleFilter?(styleFilter.CreateUnsafe(node));
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x001471CA File Offset: 0x001453CA
		public static styleFilter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new styleFilter(new Hole(g.Symbol.styleFilter, holeId));
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x001471E2 File Offset: 0x001453E2
		public styleFilter(GrammarBuilders g, StyleFilter value)
		{
			this = new styleFilter(new LiteralNode(g.Symbol.styleFilter, value));
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x060064DF RID: 25823 RVA: 0x001471FB File Offset: 0x001453FB
		public StyleFilter Value
		{
			get
			{
				return (StyleFilter)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x00147212 File Offset: 0x00145412
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x00147228 File Offset: 0x00145428
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x00147252 File Offset: 0x00145452
		public bool Equals(styleFilter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C1C RID: 11292
		private ProgramNode _node;
	}
}

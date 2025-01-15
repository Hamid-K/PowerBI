using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E73 RID: 3699
	public struct titleAboveMode : IProgramNodeBuilder, IEquatable<titleAboveMode>
	{
		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x060064E3 RID: 25827 RVA: 0x00147266 File Offset: 0x00145466
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x0014726E File Offset: 0x0014546E
		private titleAboveMode(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x00147277 File Offset: 0x00145477
		public static titleAboveMode CreateUnsafe(ProgramNode node)
		{
			return new titleAboveMode(node);
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x00147280 File Offset: 0x00145480
		public static titleAboveMode? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.titleAboveMode)
			{
				return null;
			}
			return new titleAboveMode?(titleAboveMode.CreateUnsafe(node));
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x001472BA File Offset: 0x001454BA
		public static titleAboveMode CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new titleAboveMode(new Hole(g.Symbol.titleAboveMode, holeId));
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x001472D2 File Offset: 0x001454D2
		public titleAboveMode(GrammarBuilders g, TitleAboveMode value)
		{
			this = new titleAboveMode(new LiteralNode(g.Symbol.titleAboveMode, value));
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x060064E9 RID: 25833 RVA: 0x001472F0 File Offset: 0x001454F0
		public TitleAboveMode Value
		{
			get
			{
				return (TitleAboveMode)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x00147307 File Offset: 0x00145507
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x0014731C File Offset: 0x0014551C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x00147346 File Offset: 0x00145546
		public bool Equals(titleAboveMode other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C1D RID: 11293
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B6B RID: 2923
	public struct selectSequence : IProgramNodeBuilder, IEquatable<selectSequence>
	{
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004A1F RID: 18975 RVA: 0x000E949E File Offset: 0x000E769E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A20 RID: 18976 RVA: 0x000E94A6 File Offset: 0x000E76A6
		private selectSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x000E94AF File Offset: 0x000E76AF
		public static selectSequence CreateUnsafe(ProgramNode node)
		{
			return new selectSequence(node);
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x000E94B8 File Offset: 0x000E76B8
		public static selectSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectSequence)
			{
				return null;
			}
			return new selectSequence?(selectSequence.CreateUnsafe(node));
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x000E94F2 File Offset: 0x000E76F2
		public static selectSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectSequence(new Hole(g.Symbol.selectSequence, holeId));
		}

		// Token: 0x06004A24 RID: 18980 RVA: 0x000E950A File Offset: 0x000E770A
		public SelectSequence Cast_SelectSequence()
		{
			return SelectSequence.CreateUnsafe(this.Node);
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SelectSequence(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x000E9517 File Offset: 0x000E7717
		public bool Is_SelectSequence(GrammarBuilders g, out SelectSequence value)
		{
			value = SelectSequence.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004A27 RID: 18983 RVA: 0x000E952B File Offset: 0x000E772B
		public SelectSequence? As_SelectSequence(GrammarBuilders g)
		{
			return new SelectSequence?(SelectSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06004A28 RID: 18984 RVA: 0x000E953D File Offset: 0x000E773D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A29 RID: 18985 RVA: 0x000E9550 File Offset: 0x000E7750
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x000E957A File Offset: 0x000E777A
		public bool Equals(selectSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002166 RID: 8550
		private ProgramNode _node;
	}
}

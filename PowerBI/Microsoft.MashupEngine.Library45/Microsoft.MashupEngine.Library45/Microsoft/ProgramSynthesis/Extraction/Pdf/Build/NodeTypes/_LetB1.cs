using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C05 RID: 3077
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06004F73 RID: 20339 RVA: 0x000FABEA File Offset: 0x000F8DEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x000FABF2 File Offset: 0x000F8DF2
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x000FABFB File Offset: 0x000F8DFB
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x000FAC04 File Offset: 0x000F8E04
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x000FAC3E File Offset: 0x000F8E3E
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x000FAC56 File Offset: 0x000F8E56
		public LetBetweenBefore Cast_LetBetweenBefore()
		{
			return LetBetweenBefore.CreateUnsafe(this.Node);
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetBetweenBefore(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x000FAC63 File Offset: 0x000F8E63
		public bool Is_LetBetweenBefore(GrammarBuilders g, out LetBetweenBefore value)
		{
			value = LetBetweenBefore.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x000FAC77 File Offset: 0x000F8E77
		public LetBetweenBefore? As_LetBetweenBefore(GrammarBuilders g)
		{
			return new LetBetweenBefore?(LetBetweenBefore.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x000FAC89 File Offset: 0x000F8E89
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x000FAC9C File Offset: 0x000F8E9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x000FACC6 File Offset: 0x000F8EC6
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400232D RID: 9005
		private ProgramNode _node;
	}
}

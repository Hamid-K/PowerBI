using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000970 RID: 2416
	public struct _LetB1 : IProgramNodeBuilder, IEquatable<_LetB1>
	{
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003984 RID: 14724 RVA: 0x000B22A2 File Offset: 0x000B04A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000B22AA File Offset: 0x000B04AA
		private _LetB1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000B22B3 File Offset: 0x000B04B3
		public static _LetB1 CreateUnsafe(ProgramNode node)
		{
			return new _LetB1(node);
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000B22BC File Offset: 0x000B04BC
		public static _LetB1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB1)
			{
				return null;
			}
			return new _LetB1?(_LetB1.CreateUnsafe(node));
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000B22F6 File Offset: 0x000B04F6
		public static _LetB1 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB1(new Hole(g.Symbol._LetB1, holeId));
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000B230E File Offset: 0x000B050E
		public MergeRecordLines Cast_MergeRecordLines()
		{
			return MergeRecordLines.CreateUnsafe(this.Node);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_MergeRecordLines(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000B231B File Offset: 0x000B051B
		public bool Is_MergeRecordLines(GrammarBuilders g, out MergeRecordLines value)
		{
			value = MergeRecordLines.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000B232F File Offset: 0x000B052F
		public MergeRecordLines? As_MergeRecordLines(GrammarBuilders g)
		{
			return new MergeRecordLines?(MergeRecordLines.CreateUnsafe(this.Node));
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000B2341 File Offset: 0x000B0541
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000B2354 File Offset: 0x000B0554
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x000B237E File Offset: 0x000B057E
		public bool Equals(_LetB1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A90 RID: 6800
		private ProgramNode _node;
	}
}

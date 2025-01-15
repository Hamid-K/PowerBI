using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E36 RID: 3638
	public struct MTrimTopSingleLeftCellRows : IProgramNodeBuilder, IEquatable<MTrimTopSingleLeftCellRows>
	{
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06006164 RID: 24932 RVA: 0x0013F0FA File Offset: 0x0013D2FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006165 RID: 24933 RVA: 0x0013F102 File Offset: 0x0013D302
		private MTrimTopSingleLeftCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006166 RID: 24934 RVA: 0x0013F10B File Offset: 0x0013D30B
		public static MTrimTopSingleLeftCellRows CreateUnsafe(ProgramNode node)
		{
			return new MTrimTopSingleLeftCellRows(node);
		}

		// Token: 0x06006167 RID: 24935 RVA: 0x0013F114 File Offset: 0x0013D314
		public static MTrimTopSingleLeftCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.MTrimTopSingleLeftCellRows)
			{
				return null;
			}
			return new MTrimTopSingleLeftCellRows?(MTrimTopSingleLeftCellRows.CreateUnsafe(node));
		}

		// Token: 0x06006168 RID: 24936 RVA: 0x0013F149 File Offset: 0x0013D349
		public MTrimTopSingleLeftCellRows(GrammarBuilders g, mTable value0)
		{
			this._node = g.Rule.MTrimTopSingleLeftCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x06006169 RID: 24937 RVA: 0x0013F168 File Offset: 0x0013D368
		public static implicit operator mTable(MTrimTopSingleLeftCellRows arg)
		{
			return mTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x0600616A RID: 24938 RVA: 0x0013F176 File Offset: 0x0013D376
		public mTable mTable
		{
			get
			{
				return mTable.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600616B RID: 24939 RVA: 0x0013F18A File Offset: 0x0013D38A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x0013F1A0 File Offset: 0x0013D3A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x0013F1CA File Offset: 0x0013D3CA
		public bool Equals(MTrimTopSingleLeftCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE0 RID: 11232
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200093B RID: 2363
	public struct allRecords_allLines : IProgramNodeBuilder, IEquatable<allRecords_allLines>
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060036C7 RID: 14023 RVA: 0x000ACE2A File Offset: 0x000AB02A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000ACE32 File Offset: 0x000AB032
		private allRecords_allLines(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000ACE3B File Offset: 0x000AB03B
		public static allRecords_allLines CreateUnsafe(ProgramNode node)
		{
			return new allRecords_allLines(node);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000ACE44 File Offset: 0x000AB044
		public static allRecords_allLines? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.allRecords_allLines)
			{
				return null;
			}
			return new allRecords_allLines?(allRecords_allLines.CreateUnsafe(node));
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000ACE79 File Offset: 0x000AB079
		public allRecords_allLines(GrammarBuilders g, allLines value0)
		{
			this._node = g.UnnamedConversion.allRecords_allLines.BuildASTNode(value0.Node);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000ACE98 File Offset: 0x000AB098
		public static implicit operator allRecords(allRecords_allLines arg)
		{
			return allRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060036CD RID: 14029 RVA: 0x000ACEA6 File Offset: 0x000AB0A6
		public allLines allLines
		{
			get
			{
				return allLines.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000ACEBA File Offset: 0x000AB0BA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000ACED0 File Offset: 0x000AB0D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000ACEFA File Offset: 0x000AB0FA
		public bool Equals(allRecords_allLines other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A5B RID: 6747
		private ProgramNode _node;
	}
}

using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes
{
	// Token: 0x0200093E RID: 2366
	public struct TableFromCells : IProgramNodeBuilder, IEquatable<TableFromCells>
	{
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x000AD106 File Offset: 0x000AB306
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000AD10E File Offset: 0x000AB30E
		private TableFromCells(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000AD117 File Offset: 0x000AB317
		public static TableFromCells CreateUnsafe(ProgramNode node)
		{
			return new TableFromCells(node);
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000AD120 File Offset: 0x000AB320
		public static TableFromCells? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TableFromCells)
			{
				return null;
			}
			return new TableFromCells?(TableFromCells.CreateUnsafe(node));
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000AD155 File Offset: 0x000AB355
		public TableFromCells(GrammarBuilders g, delimiterSplit value0, hasHeader value1)
		{
			this._node = g.Rule.TableFromCells.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000AD17B File Offset: 0x000AB37B
		public static implicit operator splitRecords(TableFromCells arg)
		{
			return splitRecords.CreateUnsafe(arg.Node);
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x000AD189 File Offset: 0x000AB389
		public delimiterSplit delimiterSplit
		{
			get
			{
				return delimiterSplit.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000AD19D File Offset: 0x000AB39D
		public hasHeader hasHeader
		{
			get
			{
				return hasHeader.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000AD1B1 File Offset: 0x000AB3B1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000AD1C4 File Offset: 0x000AB3C4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000AD1EE File Offset: 0x000AB3EE
		public bool Equals(TableFromCells other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A5E RID: 6750
		private ProgramNode _node;
	}
}

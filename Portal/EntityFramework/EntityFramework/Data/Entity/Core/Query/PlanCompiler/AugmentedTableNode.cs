using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000332 RID: 818
	internal sealed class AugmentedTableNode : AugmentedNode
	{
		// Token: 0x06002701 RID: 9985 RVA: 0x00071238 File Offset: 0x0006F438
		internal AugmentedTableNode(int id, Node node)
			: base(id, node)
		{
			ScanTableOp scanTableOp = (ScanTableOp)node.Op;
			this.m_table = scanTableOp.Table;
			this.LastVisibleId = id;
			this.m_replacementTable = this;
			this.m_newLocationId = id;
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x0007127A File Offset: 0x0006F47A
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x00071282 File Offset: 0x0006F482
		// (set) Token: 0x06002704 RID: 9988 RVA: 0x0007128A File Offset: 0x0006F48A
		internal int LastVisibleId { get; set; }

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x00071293 File Offset: 0x0006F493
		internal bool IsEliminated
		{
			get
			{
				return this.m_replacementTable != this;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x000712A1 File Offset: 0x0006F4A1
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x000712A9 File Offset: 0x0006F4A9
		internal AugmentedTableNode ReplacementTable
		{
			get
			{
				return this.m_replacementTable;
			}
			set
			{
				this.m_replacementTable = value;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x000712B2 File Offset: 0x0006F4B2
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x000712BA File Offset: 0x0006F4BA
		internal int NewLocationId
		{
			get
			{
				return this.m_newLocationId;
			}
			set
			{
				this.m_newLocationId = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x000712C3 File Offset: 0x0006F4C3
		internal bool IsMoved
		{
			get
			{
				return this.m_newLocationId != base.Id;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000712D6 File Offset: 0x0006F4D6
		// (set) Token: 0x0600270C RID: 9996 RVA: 0x000712DE File Offset: 0x0006F4DE
		internal VarVec NullableColumns { get; set; }

		// Token: 0x04000D9C RID: 3484
		private readonly Table m_table;

		// Token: 0x04000D9D RID: 3485
		private AugmentedTableNode m_replacementTable;

		// Token: 0x04000D9E RID: 3486
		private int m_newLocationId;
	}
}

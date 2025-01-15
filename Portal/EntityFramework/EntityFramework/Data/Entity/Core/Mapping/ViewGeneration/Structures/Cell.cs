using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000599 RID: 1433
	internal class Cell : InternalBase
	{
		// Token: 0x0600454C RID: 17740 RVA: 0x000F4A89 File Offset: 0x000F2C89
		private Cell(CellQuery cQuery, CellQuery sQuery, CellLabel label, int cellNumber)
		{
			this.m_cQuery = cQuery;
			this.m_sQuery = sQuery;
			this.m_label = label;
			this.m_cellNumber = cellNumber;
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x000F4AB0 File Offset: 0x000F2CB0
		internal Cell(Cell source)
		{
			this.m_cQuery = new CellQuery(source.m_cQuery);
			this.m_sQuery = new CellQuery(source.m_sQuery);
			this.m_label = new CellLabel(source.m_label);
			this.m_cellNumber = source.m_cellNumber;
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600454E RID: 17742 RVA: 0x000F4B02 File Offset: 0x000F2D02
		internal CellQuery CQuery
		{
			get
			{
				return this.m_cQuery;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x000F4B0A File Offset: 0x000F2D0A
		internal CellQuery SQuery
		{
			get
			{
				return this.m_sQuery;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x000F4B12 File Offset: 0x000F2D12
		internal CellLabel CellLabel
		{
			get
			{
				return this.m_label;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x000F4B1A File Offset: 0x000F2D1A
		internal int CellNumber
		{
			get
			{
				return this.m_cellNumber;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06004552 RID: 17746 RVA: 0x000F4B22 File Offset: 0x000F2D22
		internal string CellNumberAsString
		{
			get
			{
				return StringUtil.FormatInvariant("V{0}", new object[] { this.CellNumber });
			}
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000F4B42 File Offset: 0x000F2D42
		internal void GetIdentifiers(CqlIdentifiers identifiers)
		{
			this.m_cQuery.GetIdentifiers(identifiers);
			this.m_sQuery.GetIdentifiers(identifiers);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x000F4B5C File Offset: 0x000F2D5C
		internal Set<EdmProperty> GetCSlotsForTableColumns(IEnumerable<MemberPath> columns)
		{
			List<int> projectedPositions = this.SQuery.GetProjectedPositions(columns);
			if (projectedPositions == null)
			{
				return null;
			}
			Set<EdmProperty> set = new Set<EdmProperty>();
			foreach (int num in projectedPositions)
			{
				MemberProjectedSlot memberProjectedSlot = this.CQuery.ProjectedSlotAt(num) as MemberProjectedSlot;
				if (memberProjectedSlot == null)
				{
					return null;
				}
				set.Add((EdmProperty)memberProjectedSlot.MemberPath.LeafEdmMember);
			}
			return set;
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x000F4BF4 File Offset: 0x000F2DF4
		internal CellQuery GetLeftQuery(ViewTarget side)
		{
			if (side != ViewTarget.QueryView)
			{
				return this.m_sQuery;
			}
			return this.m_cQuery;
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x000F4C06 File Offset: 0x000F2E06
		internal CellQuery GetRightQuery(ViewTarget side)
		{
			if (side != ViewTarget.QueryView)
			{
				return this.m_cQuery;
			}
			return this.m_sQuery;
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x000F4C18 File Offset: 0x000F2E18
		internal ViewCellRelation CreateViewCellRelation(int cellNumber)
		{
			if (this.m_viewCellRelation != null)
			{
				return this.m_viewCellRelation;
			}
			this.GenerateCellRelations(cellNumber);
			return this.m_viewCellRelation;
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x000F4C38 File Offset: 0x000F2E38
		private void GenerateCellRelations(int cellNumber)
		{
			List<ViewCellSlot> list = new List<ViewCellSlot>();
			for (int i = 0; i < this.CQuery.NumProjectedSlots; i++)
			{
				ProjectedSlot projectedSlot = this.CQuery.ProjectedSlotAt(i);
				ProjectedSlot projectedSlot2 = this.SQuery.ProjectedSlotAt(i);
				MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)projectedSlot;
				MemberProjectedSlot memberProjectedSlot2 = (MemberProjectedSlot)projectedSlot2;
				ViewCellSlot viewCellSlot = new ViewCellSlot(i, memberProjectedSlot, memberProjectedSlot2);
				list.Add(viewCellSlot);
			}
			this.m_viewCellRelation = new ViewCellRelation(this, list, cellNumber);
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000F4CA9 File Offset: 0x000F2EA9
		internal override void ToCompactString(StringBuilder builder)
		{
			this.CQuery.ToCompactString(builder);
			builder.Append(" = ");
			this.SQuery.ToCompactString(builder);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x000F4CCF File Offset: 0x000F2ECF
		internal override void ToFullString(StringBuilder builder)
		{
			this.CQuery.ToFullString(builder);
			builder.Append(" = ");
			this.SQuery.ToFullString(builder);
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x000F4CF5 File Offset: 0x000F2EF5
		public override string ToString()
		{
			return this.ToFullString();
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x000F4D00 File Offset: 0x000F2F00
		internal static void CellsToBuilder(StringBuilder builder, IEnumerable<Cell> cells)
		{
			builder.AppendLine();
			builder.AppendLine("=========================================================================");
			foreach (Cell cell in cells)
			{
				builder.AppendLine();
				StringUtil.FormatStringBuilder(builder, "Mapping Cell V{0}:", new object[] { cell.CellNumber });
				builder.AppendLine();
				builder.Append("C: ");
				cell.CQuery.ToFullString(builder);
				builder.AppendLine();
				builder.AppendLine();
				builder.Append("S: ");
				cell.SQuery.ToFullString(builder);
				builder.AppendLine();
			}
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x000F4DCC File Offset: 0x000F2FCC
		internal static Cell CreateCS(CellQuery cQuery, CellQuery sQuery, CellLabel label, int cellNumber)
		{
			return new Cell(cQuery, sQuery, label, cellNumber);
		}

		// Token: 0x040018E0 RID: 6368
		private readonly CellQuery m_cQuery;

		// Token: 0x040018E1 RID: 6369
		private readonly CellQuery m_sQuery;

		// Token: 0x040018E2 RID: 6370
		private readonly int m_cellNumber;

		// Token: 0x040018E3 RID: 6371
		private readonly CellLabel m_label;

		// Token: 0x040018E4 RID: 6372
		private ViewCellRelation m_viewCellRelation;
	}
}

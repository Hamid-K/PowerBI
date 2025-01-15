using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000766 RID: 1894
	[Serializable]
	internal sealed class CustomReportItemInstance : ReportItemInstance, IPageItem
	{
		// Token: 0x06006918 RID: 26904 RVA: 0x001995B6 File Offset: 0x001977B6
		internal CustomReportItemInstance()
		{
		}

		// Token: 0x06006919 RID: 26905 RVA: 0x001995CC File Offset: 0x001977CC
		internal CustomReportItemInstance(ReportProcessing.ProcessingContext pc, CustomReportItem reportItemDef)
			: base(pc.CreateUniqueName(), reportItemDef)
		{
			this.m_instanceInfo = new CustomReportItemInstanceInfo(pc, reportItemDef, this);
			pc.Pagination.EnterIgnoreHeight(reportItemDef.StartHidden);
			if (reportItemDef.DataSetName != null)
			{
				this.m_columnInstances = new CustomReportItemHeadingInstanceList();
				this.m_rowInstances = new CustomReportItemHeadingInstanceList();
				this.m_cells = new CustomReportItemCellInstancesList();
			}
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x0600691A RID: 26906 RVA: 0x0019963C File Offset: 0x0019783C
		// (set) Token: 0x0600691B RID: 26907 RVA: 0x00199644 File Offset: 0x00197844
		internal ReportItemColInstance AltReportItemColInstance
		{
			get
			{
				return this.m_altReportItemColInstance;
			}
			set
			{
				this.m_altReportItemColInstance = value;
			}
		}

		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x0600691C RID: 26908 RVA: 0x0019964D File Offset: 0x0019784D
		// (set) Token: 0x0600691D RID: 26909 RVA: 0x00199655 File Offset: 0x00197855
		internal CustomReportItemHeadingInstanceList ColumnInstances
		{
			get
			{
				return this.m_columnInstances;
			}
			set
			{
				this.m_columnInstances = value;
			}
		}

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x0600691E RID: 26910 RVA: 0x0019965E File Offset: 0x0019785E
		// (set) Token: 0x0600691F RID: 26911 RVA: 0x00199666 File Offset: 0x00197866
		internal CustomReportItemHeadingInstanceList RowInstances
		{
			get
			{
				return this.m_rowInstances;
			}
			set
			{
				this.m_rowInstances = value;
			}
		}

		// Token: 0x1700251E RID: 9502
		// (get) Token: 0x06006920 RID: 26912 RVA: 0x0019966F File Offset: 0x0019786F
		// (set) Token: 0x06006921 RID: 26913 RVA: 0x00199677 File Offset: 0x00197877
		internal CustomReportItemCellInstancesList Cells
		{
			get
			{
				return this.m_cells;
			}
			set
			{
				this.m_cells = value;
			}
		}

		// Token: 0x1700251F RID: 9503
		// (get) Token: 0x06006922 RID: 26914 RVA: 0x00199680 File Offset: 0x00197880
		internal int CurrentCellOuterIndex
		{
			get
			{
				return this.m_currentCellOuterIndex;
			}
		}

		// Token: 0x17002520 RID: 9504
		// (get) Token: 0x06006923 RID: 26915 RVA: 0x00199688 File Offset: 0x00197888
		internal int CurrentCellInnerIndex
		{
			get
			{
				return this.m_currentCellInnerIndex;
			}
		}

		// Token: 0x17002521 RID: 9505
		// (set) Token: 0x06006924 RID: 26916 RVA: 0x00199690 File Offset: 0x00197890
		internal int CurrentOuterStaticIndex
		{
			set
			{
				this.m_currentOuterStaticIndex = value;
			}
		}

		// Token: 0x17002522 RID: 9506
		// (set) Token: 0x06006925 RID: 26917 RVA: 0x00199699 File Offset: 0x00197899
		internal int CurrentInnerStaticIndex
		{
			set
			{
				this.m_currentInnerStaticIndex = value;
			}
		}

		// Token: 0x17002523 RID: 9507
		// (get) Token: 0x06006926 RID: 26918 RVA: 0x001996A2 File Offset: 0x001978A2
		// (set) Token: 0x06006927 RID: 26919 RVA: 0x001996AA File Offset: 0x001978AA
		internal CustomReportItemHeadingInstanceList InnerHeadingInstanceList
		{
			get
			{
				return this.m_innerHeadingInstanceList;
			}
			set
			{
				this.m_innerHeadingInstanceList = value;
			}
		}

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x06006928 RID: 26920 RVA: 0x001996B3 File Offset: 0x001978B3
		// (set) Token: 0x06006929 RID: 26921 RVA: 0x001996BB File Offset: 0x001978BB
		int IPageItem.StartPage
		{
			get
			{
				return this.m_startPage;
			}
			set
			{
				this.m_startPage = value;
			}
		}

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x0600692A RID: 26922 RVA: 0x001996C4 File Offset: 0x001978C4
		// (set) Token: 0x0600692B RID: 26923 RVA: 0x001996CC File Offset: 0x001978CC
		int IPageItem.EndPage
		{
			get
			{
				return this.m_endPage;
			}
			set
			{
				this.m_endPage = value;
			}
		}

		// Token: 0x17002526 RID: 9510
		// (get) Token: 0x0600692C RID: 26924 RVA: 0x001996D5 File Offset: 0x001978D5
		internal int CellColumnCount
		{
			get
			{
				if (0 < this.m_cells.Count)
				{
					return this.m_cells[0].Count;
				}
				return 0;
			}
		}

		// Token: 0x17002527 RID: 9511
		// (get) Token: 0x0600692D RID: 26925 RVA: 0x001996F8 File Offset: 0x001978F8
		internal int CellRowCount
		{
			get
			{
				return this.m_cells.Count;
			}
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x00199708 File Offset: 0x00197908
		internal CustomReportItemCellInstance AddCell(ReportProcessing.ProcessingContext pc)
		{
			CustomReportItem customReportItem = (CustomReportItem)this.m_reportItemDef;
			bool flag = customReportItem.ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column;
			int num;
			int num2;
			if (flag)
			{
				num = this.m_currentOuterStaticIndex;
				num2 = this.m_currentInnerStaticIndex;
			}
			else
			{
				num2 = this.m_currentOuterStaticIndex;
				num = this.m_currentInnerStaticIndex;
			}
			CustomReportItemCellInstance customReportItemCellInstance = new CustomReportItemCellInstance(num, num2, customReportItem, pc);
			if (flag)
			{
				this.m_cells[this.m_currentCellOuterIndex].Add(customReportItemCellInstance);
			}
			else
			{
				if (this.m_currentCellOuterIndex == 0)
				{
					Global.Tracer.Assert(this.m_cells.Count == this.m_currentCellInnerIndex);
					CustomReportItemCellInstanceList customReportItemCellInstanceList = new CustomReportItemCellInstanceList();
					this.m_cells.Add(customReportItemCellInstanceList);
				}
				this.m_cells[this.m_currentCellInnerIndex].Add(customReportItemCellInstance);
			}
			this.m_currentCellInnerIndex++;
			return customReportItemCellInstance;
		}

		// Token: 0x0600692F RID: 26927 RVA: 0x001997D4 File Offset: 0x001979D4
		internal void NewOuterCells()
		{
			if (0 < this.m_currentCellInnerIndex || this.m_cells.Count == 0)
			{
				if (((CustomReportItem)this.m_reportItemDef).ProcessingInnerGrouping == Pivot.ProcessingInnerGroupings.Column)
				{
					CustomReportItemCellInstanceList customReportItemCellInstanceList = new CustomReportItemCellInstanceList();
					this.m_cells.Add(customReportItemCellInstanceList);
				}
				if (0 < this.m_currentCellInnerIndex)
				{
					this.m_currentCellOuterIndex++;
					this.m_currentCellInnerIndex = 0;
				}
			}
		}

		// Token: 0x06006930 RID: 26928 RVA: 0x0019983A File Offset: 0x00197A3A
		internal override ReportItemInstanceInfo ReadInstanceInfo(IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
			return reader.ReadCustomReportItemInstanceInfo((CustomReportItem)this.m_reportItemDef);
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x00199868 File Offset: 0x00197A68
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ReportItemInstance, new MemberInfoList
			{
				new MemberInfo(MemberName.AltReportItemColInstance, ObjectType.ReportItemColInstance),
				new MemberInfo(MemberName.ColumnInstances, ObjectType.CustomReportItemHeadingInstanceList),
				new MemberInfo(MemberName.RowInstances, ObjectType.CustomReportItemHeadingInstanceList),
				new MemberInfo(MemberName.Cells, ObjectType.CustomReportItemCellInstancesList)
			});
		}

		// Token: 0x040033C9 RID: 13257
		private ReportItemColInstance m_altReportItemColInstance;

		// Token: 0x040033CA RID: 13258
		private CustomReportItemHeadingInstanceList m_columnInstances;

		// Token: 0x040033CB RID: 13259
		private CustomReportItemHeadingInstanceList m_rowInstances;

		// Token: 0x040033CC RID: 13260
		private CustomReportItemCellInstancesList m_cells;

		// Token: 0x040033CD RID: 13261
		[NonSerialized]
		private int m_currentCellOuterIndex;

		// Token: 0x040033CE RID: 13262
		[NonSerialized]
		private int m_currentCellInnerIndex;

		// Token: 0x040033CF RID: 13263
		[NonSerialized]
		private int m_currentOuterStaticIndex;

		// Token: 0x040033D0 RID: 13264
		[NonSerialized]
		private int m_currentInnerStaticIndex;

		// Token: 0x040033D1 RID: 13265
		[NonSerialized]
		private CustomReportItemHeadingInstanceList m_innerHeadingInstanceList;

		// Token: 0x040033D2 RID: 13266
		[NonSerialized]
		private int m_startPage = -1;

		// Token: 0x040033D3 RID: 13267
		[NonSerialized]
		private int m_endPage = -1;
	}
}

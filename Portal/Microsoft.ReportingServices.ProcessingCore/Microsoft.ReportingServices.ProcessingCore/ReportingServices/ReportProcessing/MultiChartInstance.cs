using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200074D RID: 1869
	[Serializable]
	internal sealed class MultiChartInstance
	{
		// Token: 0x060067BB RID: 26555 RVA: 0x00194C0D File Offset: 0x00192E0D
		internal MultiChartInstance(Chart reportItemDef)
		{
			this.m_columnInstances = new ChartHeadingInstanceList();
			this.m_rowInstances = new ChartHeadingInstanceList();
			this.m_cellDataPoints = new ChartDataPointInstancesList();
		}

		// Token: 0x060067BC RID: 26556 RVA: 0x00194C36 File Offset: 0x00192E36
		internal MultiChartInstance()
		{
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x060067BD RID: 26557 RVA: 0x00194C3E File Offset: 0x00192E3E
		// (set) Token: 0x060067BE RID: 26558 RVA: 0x00194C46 File Offset: 0x00192E46
		internal ChartHeadingInstanceList ColumnInstances
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

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x060067BF RID: 26559 RVA: 0x00194C4F File Offset: 0x00192E4F
		// (set) Token: 0x060067C0 RID: 26560 RVA: 0x00194C57 File Offset: 0x00192E57
		internal ChartHeadingInstanceList RowInstances
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

		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x060067C1 RID: 26561 RVA: 0x00194C60 File Offset: 0x00192E60
		// (set) Token: 0x060067C2 RID: 26562 RVA: 0x00194C68 File Offset: 0x00192E68
		internal ChartDataPointInstancesList DataPoints
		{
			get
			{
				return this.m_cellDataPoints;
			}
			set
			{
				this.m_cellDataPoints = value;
			}
		}

		// Token: 0x170024AC RID: 9388
		// (get) Token: 0x060067C3 RID: 26563 RVA: 0x00194C71 File Offset: 0x00192E71
		// (set) Token: 0x060067C4 RID: 26564 RVA: 0x00194C79 File Offset: 0x00192E79
		internal ChartHeadingInstanceList InnerHeadingInstanceList
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

		// Token: 0x060067C5 RID: 26565 RVA: 0x00194C84 File Offset: 0x00192E84
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.ColumnInstances, ObjectType.ChartHeadingInstanceList),
				new MemberInfo(MemberName.RowInstances, ObjectType.ChartHeadingInstanceList),
				new MemberInfo(MemberName.CellDataPoints, ObjectType.ChartDataPointInstancesList)
			});
		}

		// Token: 0x04003360 RID: 13152
		private ChartHeadingInstanceList m_columnInstances;

		// Token: 0x04003361 RID: 13153
		private ChartHeadingInstanceList m_rowInstances;

		// Token: 0x04003362 RID: 13154
		private ChartDataPointInstancesList m_cellDataPoints;

		// Token: 0x04003363 RID: 13155
		[NonSerialized]
		private ChartHeadingInstanceList m_innerHeadingInstanceList;
	}
}

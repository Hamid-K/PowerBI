using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000424 RID: 1060
	public sealed class TableItem : DataRegionItem
	{
		// Token: 0x0600219F RID: 8607 RVA: 0x000811EE File Offset: 0x0007F3EE
		public TableItem()
		{
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00081217 File Offset: 0x0007F417
		public TableItem(string name)
		{
			base.Name = name;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00081247 File Offset: 0x0007F447
		public TableItem(string name, int columCount)
			: this(name)
		{
			while (columCount > 0)
			{
				this.m_columns.Add(new TableColumn());
				columCount--;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0008126B File Offset: 0x0007F46B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<TableColumn> TableColumns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x00081273 File Offset: 0x0007F473
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x0008127B File Offset: 0x0007F47B
		public TableHeader Header
		{
			get
			{
				return this.m_header;
			}
			set
			{
				this.m_header = value;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x00081284 File Offset: 0x0007F484
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x0008128C File Offset: 0x0007F48C
		public TableHeader Footer
		{
			get
			{
				return this.m_footer;
			}
			set
			{
				this.m_footer = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00081295 File Offset: 0x0007F495
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<TableGroup> TableGroups
		{
			get
			{
				return this.m_groups;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x0008129D File Offset: 0x0007F49D
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x000812A5 File Offset: 0x0007F4A5
		public TableDetails Details
		{
			get
			{
				return this.m_details;
			}
			set
			{
				this.m_details = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x000812AE File Offset: 0x0007F4AE
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x000812B6 File Offset: 0x0007F4B6
		[DefaultValue("")]
		public string DetailDataElementName
		{
			get
			{
				return this.m_detailDataElementName;
			}
			set
			{
				this.m_detailDataElementName = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x000812BF File Offset: 0x0007F4BF
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x000812C7 File Offset: 0x0007F4C7
		[DefaultValue("")]
		public string DetailDataCollectionName
		{
			get
			{
				return this.m_detailDataCollectionName;
			}
			set
			{
				this.m_detailDataCollectionName = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000812D0 File Offset: 0x0007F4D0
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x000812D8 File Offset: 0x0007F4D8
		[DefaultValue(GroupingDataElementOutputs.Output)]
		public GroupingDataElementOutputs DetailDataElementOutput
		{
			get
			{
				return this.m_detailDataElementOutput;
			}
			set
			{
				this.m_detailDataElementOutput = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000812E1 File Offset: 0x0007F4E1
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x000812E9 File Offset: 0x0007F4E9
		[DefaultValue(false)]
		public bool FillPage
		{
			get
			{
				return this.m_fillPage;
			}
			set
			{
				this.m_fillPage = value;
			}
		}

		// Token: 0x04000EC1 RID: 3777
		private List<TableColumn> m_columns = new List<TableColumn>();

		// Token: 0x04000EC2 RID: 3778
		private List<TableGroup> m_groups = new List<TableGroup>();

		// Token: 0x04000EC3 RID: 3779
		private TableHeader m_header;

		// Token: 0x04000EC4 RID: 3780
		private TableHeader m_footer;

		// Token: 0x04000EC5 RID: 3781
		private TableDetails m_details = new TableDetails();

		// Token: 0x04000EC6 RID: 3782
		private bool m_fillPage;

		// Token: 0x04000EC7 RID: 3783
		private string m_detailDataElementName;

		// Token: 0x04000EC8 RID: 3784
		private string m_detailDataCollectionName;

		// Token: 0x04000EC9 RID: 3785
		private GroupingDataElementOutputs m_detailDataElementOutput;
	}
}

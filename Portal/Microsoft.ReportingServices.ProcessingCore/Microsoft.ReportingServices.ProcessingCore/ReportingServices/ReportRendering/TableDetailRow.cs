using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000049 RID: 73
	internal sealed class TableDetailRow : TableRow
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x000135DC File Offset: 0x000117DC
		internal TableDetailRow(Table owner, TableRow rowDef, TableRowInstance rowInstance, TableDetailRowCollection detail)
			: base(owner, rowDef, rowInstance)
		{
			this.m_detail = detail;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x000135EF File Offset: 0x000117EF
		public override bool Hidden
		{
			get
			{
				return this.m_detail.Hidden || base.Hidden;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00013608 File Offset: 0x00011808
		public override TextBox ToggleParent
		{
			get
			{
				TextBox textBox = null;
				if (this.m_detail.DetailInstance != null)
				{
					textBox = this.m_owner.RenderingContext.GetToggleParent(this.m_detail.DetailInstance.UniqueName);
				}
				if (textBox == null)
				{
					return base.ToggleParent;
				}
				return textBox;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x00013650 File Offset: 0x00011850
		public override bool HasToggle
		{
			get
			{
				return Visibility.HasToggle(((Table)this.m_owner.ReportItemDef).TableDetail.Visibility) || base.HasToggle;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001367C File Offset: 0x0001187C
		public override string ToggleItem
		{
			get
			{
				TableDetail tableDetail = ((Table)this.m_owner.ReportItemDef).TableDetail;
				string text = null;
				if (tableDetail.Visibility != null)
				{
					text = tableDetail.Visibility.Toggle;
				}
				if (text == null)
				{
					text = base.ToggleItem;
				}
				return text;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x000136C0 File Offset: 0x000118C0
		public override SharedHiddenState SharedHidden
		{
			get
			{
				SharedHiddenState sharedHidden = Visibility.GetSharedHidden(((Table)this.m_owner.ReportItemDef).TableDetail.Visibility);
				if (sharedHidden == SharedHiddenState.Always)
				{
					return SharedHiddenState.Always;
				}
				SharedHiddenState sharedHidden2 = base.SharedHidden;
				if (SharedHiddenState.Never == sharedHidden)
				{
					return sharedHidden2;
				}
				if (sharedHidden2 == SharedHiddenState.Always)
				{
					return SharedHiddenState.Always;
				}
				return SharedHiddenState.Sometimes;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00013708 File Offset: 0x00011908
		public override bool IsToggleChild
		{
			get
			{
				bool flag = false;
				if (this.m_detail.DetailInstance != null)
				{
					flag = this.m_owner.RenderingContext.IsToggleChild(this.m_detail.DetailInstance.UniqueName);
				}
				return flag || base.IsToggleChild;
			}
		}

		// Token: 0x0400015D RID: 349
		private TableDetailRowCollection m_detail;
	}
}

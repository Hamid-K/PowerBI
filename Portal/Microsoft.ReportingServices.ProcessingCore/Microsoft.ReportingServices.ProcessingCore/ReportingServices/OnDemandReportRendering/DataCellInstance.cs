using System;
using System.Globalization;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000288 RID: 648
	public sealed class DataCellInstance : BaseInstance, IReportScopeInstance
	{
		// Token: 0x06001921 RID: 6433 RVA: 0x00066B87 File Offset: 0x00064D87
		internal DataCellInstance(DataCell dataCellDef)
			: base(dataCellDef)
		{
			this.m_dataCellDef = dataCellDef;
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x00066BA0 File Offset: 0x00064DA0
		string IReportScopeInstance.UniqueName
		{
			get
			{
				if (this.m_dataCellDef.CriDef.IsOldSnapshot)
				{
					return string.Concat(new string[]
					{
						this.m_dataCellDef.CriDef.ID,
						"i",
						this.m_dataCellDef.RenderItem.RowIndex.ToString(CultureInfo.InvariantCulture),
						"x",
						this.m_dataCellDef.RenderItem.ColumnIndex.ToString(CultureInfo.InvariantCulture)
					});
				}
				return this.m_dataCellDef.DataCellDef.UniqueName;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06001923 RID: 6435 RVA: 0x00066C3E File Offset: 0x00064E3E
		// (set) Token: 0x06001924 RID: 6436 RVA: 0x00066C46 File Offset: 0x00064E46
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06001925 RID: 6437 RVA: 0x00066C4F File Offset: 0x00064E4F
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00066C57 File Offset: 0x00064E57
		internal override void SetNewContext()
		{
			if (this.m_isNewContext)
			{
				return;
			}
			this.m_isNewContext = true;
			base.SetNewContext();
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00066C6F File Offset: 0x00064E6F
		protected override void ResetInstanceCache()
		{
		}

		// Token: 0x04000CA1 RID: 3233
		private DataCell m_dataCellDef;

		// Token: 0x04000CA2 RID: 3234
		private bool m_isNewContext = true;
	}
}

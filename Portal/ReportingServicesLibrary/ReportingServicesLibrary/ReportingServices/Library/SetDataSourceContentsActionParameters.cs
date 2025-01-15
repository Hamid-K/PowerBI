using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap2005;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000EB RID: 235
	internal sealed class SetDataSourceContentsActionParameters : UpdateItemActionParameters
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00026601 File Offset: 0x00024801
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x00026609 File Offset: 0x00024809
		public DataSourceDefinition DataSourceDefinition
		{
			get
			{
				return this.m_dataSourceDefinition;
			}
			set
			{
				this.m_dataSourceDefinition = value;
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00026612 File Offset: 0x00024812
		internal override void Validate()
		{
			if (base.ItemPath == null)
			{
				throw new MissingParameterException("DataSource");
			}
			if (this.DataSourceDefinition == null)
			{
				throw new MissingParameterException("Definition");
			}
		}

		// Token: 0x04000476 RID: 1142
		private DataSourceDefinition m_dataSourceDefinition;
	}
}

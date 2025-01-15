using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200079B RID: 1947
	internal sealed class DataSourceImpl : DataSource
	{
		// Token: 0x06006C60 RID: 27744 RVA: 0x001B784C File Offset: 0x001B5A4C
		internal DataSourceImpl(DataSource dataSourceDef)
		{
			this.m_dataSource = dataSourceDef;
		}

		// Token: 0x170025BA RID: 9658
		// (get) Token: 0x06006C61 RID: 27745 RVA: 0x001B785B File Offset: 0x001B5A5B
		public override string DataSourceReference
		{
			get
			{
				if (this.m_dataSource.SharedDataSourceReferencePath == null)
				{
					return this.m_dataSource.DataSourceReference;
				}
				return this.m_dataSource.SharedDataSourceReferencePath;
			}
		}

		// Token: 0x170025BB RID: 9659
		// (get) Token: 0x06006C62 RID: 27746 RVA: 0x001B7881 File Offset: 0x001B5A81
		public override string Type
		{
			get
			{
				return this.m_dataSource.Type;
			}
		}

		// Token: 0x0400367B RID: 13947
		private DataSource m_dataSource;
	}
}

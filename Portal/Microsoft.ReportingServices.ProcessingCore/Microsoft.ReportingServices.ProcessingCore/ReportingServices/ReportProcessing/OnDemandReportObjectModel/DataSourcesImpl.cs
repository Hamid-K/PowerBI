using System;
using System.Collections;
using System.Threading;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007BE RID: 1982
	internal sealed class DataSourcesImpl : DataSources
	{
		// Token: 0x0600705C RID: 28764 RVA: 0x001D4469 File Offset: 0x001D2669
		internal DataSourcesImpl(int size)
		{
			this.m_lockAdd = size > 1;
			this.m_collection = new Hashtable(size);
		}

		// Token: 0x0600705D RID: 28765 RVA: 0x001D4488 File Offset: 0x001D2688
		internal void Add(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSourceDef)
		{
			try
			{
				if (this.m_lockAdd)
				{
					Monitor.Enter(this.m_collection);
				}
				if (!this.m_collection.ContainsKey(dataSourceDef.Name))
				{
					this.m_collection.Add(dataSourceDef.Name, new DataSourceImpl(dataSourceDef));
				}
			}
			finally
			{
				if (this.m_lockAdd)
				{
					Monitor.Exit(this.m_collection);
				}
			}
		}

		// Token: 0x1700264A RID: 9802
		public override Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSource this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingDataSourceReference(key);
				}
				Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSource dataSource2;
				try
				{
					Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSource dataSource = this.m_collection[key] as Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSource;
					if (dataSource == null)
					{
						throw new ReportProcessingException_NonExistingDataSourceReference(key);
					}
					dataSource2 = dataSource;
				}
				catch
				{
					throw new ReportProcessingException_NonExistingDataSourceReference(key);
				}
				return dataSource2;
			}
		}

		// Token: 0x04003A09 RID: 14857
		private bool m_lockAdd;

		// Token: 0x04003A0A RID: 14858
		private Hashtable m_collection;
	}
}

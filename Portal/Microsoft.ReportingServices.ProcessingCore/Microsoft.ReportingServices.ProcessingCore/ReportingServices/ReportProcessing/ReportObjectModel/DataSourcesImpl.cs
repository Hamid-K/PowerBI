using System;
using System.Collections;
using System.Threading;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200079A RID: 1946
	internal sealed class DataSourcesImpl : DataSources
	{
		// Token: 0x06006C5D RID: 27741 RVA: 0x001B7762 File Offset: 0x001B5962
		internal DataSourcesImpl(int size)
		{
			this.m_lockAdd = size > 1;
			this.m_collection = new Hashtable(size);
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x001B7780 File Offset: 0x001B5980
		internal void Add(DataSource dataSourceDef)
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

		// Token: 0x170025B9 RID: 9657
		public override DataSource this[string key]
		{
			get
			{
				if (key == null || this.m_collection == null)
				{
					throw new ReportProcessingException_NonExistingDataSourceReference(key);
				}
				DataSource dataSource2;
				try
				{
					DataSource dataSource = this.m_collection[key] as DataSource;
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

		// Token: 0x04003677 RID: 13943
		private bool m_lockAdd;

		// Token: 0x04003678 RID: 13944
		private Hashtable m_collection;

		// Token: 0x04003679 RID: 13945
		internal const string Name = "DataSources";

		// Token: 0x0400367A RID: 13946
		internal const string FullName = "Microsoft.ReportingServices.ReportProcessing.ReportObjectModel.DataSources";
	}
}

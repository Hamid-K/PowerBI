using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B1 RID: 1457
	[Serializable]
	public sealed class DataSourcePromptCollection
	{
		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x06005268 RID: 21096 RVA: 0x0015B8B2 File Offset: 0x00159AB2
		public bool NeedPrompt
		{
			get
			{
				return this.m_needPrompt;
			}
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x06005269 RID: 21097 RVA: 0x0015B8BA File Offset: 0x00159ABA
		public int Count
		{
			get
			{
				return this.m_collection.Count;
			}
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0015B8C7 File Offset: 0x00159AC7
		public IEnumerator GetEnumerator()
		{
			return this.m_collection.Values.GetEnumerator();
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x0015B8DC File Offset: 0x00159ADC
		internal void Add(DataSourceInfo dataSource, ServerDataSourceSettings serverDatasourceSettings)
		{
			string originalName = dataSource.OriginalName;
			Global.Tracer.Assert(this.m_collection[originalName] == null, "Collection already contains this data source.");
			dataSource.ThrowIfNotUsable(serverDatasourceSettings);
			this.m_collection.Add(originalName, dataSource);
			if (dataSource.NeedPrompt)
			{
				this.m_needPrompt = true;
			}
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x0015B934 File Offset: 0x00159B34
		public void AddSingleIfPrompt(DataSourceInfo dataSource, ServerDataSourceSettings serverDatasourceSettings)
		{
			Global.Tracer.Assert(dataSource.OriginalName == null, "Data source has non-null name when adding single");
			if (this.m_collection.Count != 0)
			{
				throw new InternalCatalogException("Prompt collection is not empty when adding single data source");
			}
			if (dataSource.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt)
			{
				dataSource.ThrowIfNotUsable(serverDatasourceSettings);
				this.m_collection.Add("", dataSource);
				if (dataSource.NeedPrompt)
				{
					this.m_needPrompt = true;
				}
			}
		}

		// Token: 0x04002995 RID: 10645
		private Hashtable m_collection = new Hashtable();

		// Token: 0x04002996 RID: 10646
		private bool m_needPrompt;
	}
}

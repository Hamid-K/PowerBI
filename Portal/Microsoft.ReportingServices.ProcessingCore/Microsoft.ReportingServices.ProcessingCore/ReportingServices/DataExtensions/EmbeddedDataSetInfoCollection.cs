using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B9 RID: 1465
	[Serializable]
	public sealed class EmbeddedDataSetInfoCollection
	{
		// Token: 0x06005305 RID: 21253 RVA: 0x0015D5BD File Offset: 0x0015B7BD
		public EmbeddedDataSetInfoCollection()
		{
			this.m_dataSetsByID = new Dictionary<Guid, EmbeddedDataSetInfo>();
			this.m_dataSetsByName = new Dictionary<string, EmbeddedDataSetInfo>(StringComparer.Ordinal);
		}

		// Token: 0x06005306 RID: 21254 RVA: 0x0015D5E0 File Offset: 0x0015B7E0
		public IEnumerator<EmbeddedDataSetInfo> GetEnumerator()
		{
			return this.m_dataSetsByID.Values.GetEnumerator();
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06005307 RID: 21255 RVA: 0x0015D5F7 File Offset: 0x0015B7F7
		public int Count
		{
			get
			{
				return this.m_dataSetsByID.Count;
			}
		}

		// Token: 0x06005308 RID: 21256 RVA: 0x0015D604 File Offset: 0x0015B804
		public void Add(EmbeddedDataSetInfo dataSet)
		{
			this.m_dataSetsByID.Add(dataSet.ID, dataSet);
			if (!this.m_dataSetsByName.ContainsKey(dataSet.DataSetName))
			{
				this.m_dataSetsByName.Add(dataSet.DataSetName, dataSet);
			}
		}

		// Token: 0x06005309 RID: 21257 RVA: 0x0015D640 File Offset: 0x0015B840
		public EmbeddedDataSetInfo GetByName(string name)
		{
			EmbeddedDataSetInfo embeddedDataSetInfo = null;
			if (this.m_dataSetsByName != null)
			{
				this.m_dataSetsByName.TryGetValue(name, out embeddedDataSetInfo);
			}
			return embeddedDataSetInfo;
		}

		// Token: 0x040029D7 RID: 10711
		private Dictionary<string, EmbeddedDataSetInfo> m_dataSetsByName;

		// Token: 0x040029D8 RID: 10712
		private Dictionary<Guid, EmbeddedDataSetInfo> m_dataSetsByID;
	}
}

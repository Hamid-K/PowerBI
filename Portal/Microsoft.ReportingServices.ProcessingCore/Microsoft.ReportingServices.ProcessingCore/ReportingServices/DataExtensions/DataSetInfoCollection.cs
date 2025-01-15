using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005A9 RID: 1449
	[Serializable]
	public sealed class DataSetInfoCollection
	{
		// Token: 0x06005218 RID: 21016 RVA: 0x0015A5B2 File Offset: 0x001587B2
		public DataSetInfoCollection()
		{
			this.m_dataSetsByID = new Dictionary<Guid, DataSetInfo>();
			this.m_dataSetsByName = new Dictionary<string, DataSetInfo>(StringComparer.Ordinal);
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0015A5D5 File Offset: 0x001587D5
		public IEnumerator<DataSetInfo> GetEnumerator()
		{
			return this.m_dataSetsByID.Values.GetEnumerator();
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x0600521A RID: 21018 RVA: 0x0015A5EC File Offset: 0x001587EC
		public int Count
		{
			get
			{
				return this.m_dataSetsByID.Count;
			}
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x0015A5F9 File Offset: 0x001587F9
		public void Add(DataSetInfo dataSet)
		{
			this.m_dataSetsByID.Add(dataSet.ID, dataSet);
			if (!this.m_dataSetsByName.ContainsKey(dataSet.DataSetName))
			{
				this.m_dataSetsByName.Add(dataSet.DataSetName, dataSet);
			}
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0015A634 File Offset: 0x00158834
		public DataSetInfo GetByName(string name)
		{
			DataSetInfo dataSetInfo = null;
			if (this.m_dataSetsByName != null)
			{
				this.m_dataSetsByName.TryGetValue(name, out dataSetInfo);
			}
			return dataSetInfo;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x0015A65C File Offset: 0x0015885C
		public void CombineOnSetDataSets(DataSetInfoCollection newDataSets)
		{
			if (newDataSets == null)
			{
				return;
			}
			foreach (DataSetInfo dataSetInfo in newDataSets)
			{
				DataSetInfo byName = this.GetByName(dataSetInfo.DataSetName);
				if (byName == null)
				{
					throw new DataSetNotFoundException(dataSetInfo.DataSetName);
				}
				byName.AbsolutePath = dataSetInfo.AbsolutePath;
				byName.LinkedSharedDataSetID = Guid.Empty;
			}
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x0015A6D4 File Offset: 0x001588D4
		public DataSetInfoCollection CombineOnSetDefinition(DataSetInfoCollection newDataSets)
		{
			DataSetInfoCollection dataSetInfoCollection = new DataSetInfoCollection();
			if (newDataSets == null)
			{
				return dataSetInfoCollection;
			}
			foreach (DataSetInfo dataSetInfo in newDataSets)
			{
				DataSetInfo byName = this.GetByName(dataSetInfo.DataSetName);
				if (byName == null)
				{
					dataSetInfoCollection.Add(dataSetInfo);
				}
				else
				{
					byName.ID = dataSetInfo.ID;
					dataSetInfoCollection.Add(byName);
				}
			}
			return dataSetInfoCollection;
		}

		// Token: 0x0400297D RID: 10621
		private Dictionary<string, DataSetInfo> m_dataSetsByName;

		// Token: 0x0400297E RID: 10622
		private Dictionary<Guid, DataSetInfo> m_dataSetsByID;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005AB RID: 1451
	[Serializable]
	public sealed class RuntimeDataSetInfoCollection
	{
		// Token: 0x06005233 RID: 21043 RVA: 0x0015A9C4 File Offset: 0x00158BC4
		public byte[] Serialize()
		{
			MemoryStream memoryStream = null;
			byte[] array;
			try
			{
				memoryStream = new MemoryStream();
				new BinaryFormatter().Serialize(memoryStream, this);
				array = memoryStream.ToArray();
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return array;
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0015AA0C File Offset: 0x00158C0C
		public static RuntimeDataSetInfoCollection Deserialize(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			MemoryStream memoryStream = null;
			RuntimeDataSetInfoCollection runtimeDataSetInfoCollection;
			try
			{
				memoryStream = new MemoryStream(data, false);
				runtimeDataSetInfoCollection = (RuntimeDataSetInfoCollection)new BinaryFormatter().Deserialize(memoryStream);
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return runtimeDataSetInfoCollection;
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0015AA58 File Offset: 0x00158C58
		internal DataSetInfo GetByID(Guid ID)
		{
			DataSetInfo dataSetInfo = null;
			if (this.m_collectionByID != null)
			{
				this.m_collectionByID.TryGetValue(ID, out dataSetInfo);
			}
			return dataSetInfo;
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0015AA80 File Offset: 0x00158C80
		internal DataSetInfo GetByName(string name, ICatalogItemContext item)
		{
			DataSetInfo dataSetInfo = null;
			if (this.m_collectionByReport != null)
			{
				DataSetInfoCollection dataSetInfoCollection = null;
				if (this.m_collectionByReport.TryGetValue(item.StableItemPath, out dataSetInfoCollection))
				{
					dataSetInfo = dataSetInfoCollection.GetByName(name);
				}
			}
			return dataSetInfo;
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0015AAB7 File Offset: 0x00158CB7
		public void Add(DataSetInfo dataSet, ICatalogItemContext report)
		{
			if (Guid.Empty == dataSet.ID)
			{
				this.AddToCollectionByReport(dataSet, report);
				return;
			}
			this.AddToCollectionByID(dataSet);
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0015AADC File Offset: 0x00158CDC
		private void AddToCollectionByReport(DataSetInfo dataSet, ICatalogItemContext report)
		{
			DataSetInfoCollection dataSetInfoCollection = null;
			if (this.m_collectionByReport == null)
			{
				this.m_collectionByReport = new Dictionary<string, DataSetInfoCollection>(StringComparer.Ordinal);
			}
			else
			{
				this.m_collectionByReport.TryGetValue(report.StableItemPath, out dataSetInfoCollection);
			}
			if (dataSetInfoCollection == null)
			{
				dataSetInfoCollection = new DataSetInfoCollection();
				this.m_collectionByReport.Add(report.StableItemPath, dataSetInfoCollection);
			}
			dataSetInfoCollection.Add(dataSet);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x0015AB3B File Offset: 0x00158D3B
		private void AddToCollectionByID(DataSetInfo dataSet)
		{
			if (this.m_collectionByID == null)
			{
				this.m_collectionByID = new Dictionary<Guid, DataSetInfo>();
			}
			else if (this.m_collectionByID.ContainsKey(dataSet.ID))
			{
				return;
			}
			this.m_collectionByID.Add(dataSet.ID, dataSet);
		}

		// Token: 0x04002989 RID: 10633
		private Dictionary<Guid, DataSetInfo> m_collectionByID;

		// Token: 0x0400298A RID: 10634
		private Dictionary<string, DataSetInfoCollection> m_collectionByReport;
	}
}

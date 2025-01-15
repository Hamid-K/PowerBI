using System;
using System.Collections;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B5 RID: 1461
	[Serializable]
	public sealed class DataSourceInfoCollection
	{
		// Token: 0x060052E9 RID: 21225 RVA: 0x0015CFD4 File Offset: 0x0015B1D4
		public DataSourceInfoCollection()
		{
		}

		// Token: 0x060052EA RID: 21226 RVA: 0x0015CFE8 File Offset: 0x0015B1E8
		public DataSourceInfoCollection(DataSourceInfoCollection other)
		{
			RSTrace processingTracer = RSTrace.ProcessingTracer;
			processingTracer.Assert(other != null);
			processingTracer.Assert(other.m_collection != null);
			this.m_collection = (Hashtable)other.m_collection.Clone();
		}

		// Token: 0x060052EB RID: 21227 RVA: 0x0015D039 File Offset: 0x0015B239
		public DataSourceInfoCollection(string dataSourcesXml, IDataProtection dataProtection)
		{
			this.ConstructFromXml(dataSourcesXml, false, dataProtection);
		}

		// Token: 0x060052EC RID: 21228 RVA: 0x0015D055 File Offset: 0x0015B255
		public DataSourceInfoCollection(string dataSourcesXml, bool clientLoad, IDataProtection dataProtection)
		{
			this.ConstructFromXml(dataSourcesXml, clientLoad, dataProtection);
		}

		// Token: 0x060052ED RID: 21229 RVA: 0x0015D074 File Offset: 0x0015B274
		private void ConstructFromXml(string dataSourcesXml, bool clientLoad, IDataProtection dataProtection)
		{
			XmlDocument xmlDocument = XmlUtil.CreateXmlDocumentWithNullResolver();
			try
			{
				XmlUtil.SafeOpenXmlDocumentString(xmlDocument, dataSourcesXml);
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			try
			{
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/DataSources");
				if (xmlNode == null)
				{
					throw new InvalidXmlException();
				}
				foreach (object obj in xmlNode.ChildNodes)
				{
					DataSourceInfo dataSourceInfo = DataSourceInfo.ParseDataSourceNode((XmlNode)obj, clientLoad, dataProtection);
					this.Add(dataSourceInfo);
				}
			}
			catch (XmlException)
			{
				throw new InvalidXmlException();
			}
		}

		// Token: 0x060052EE RID: 21230 RVA: 0x0015D120 File Offset: 0x0015B320
		public DataSourceInfo GetTheOnlyDataSource()
		{
			if (this.Count != 1)
			{
				throw new InternalCatalogException(string.Format(CultureInfo.CurrentCulture, "Data source collection for a standalone datasource contains {0} items, must be 1.", this.Count));
			}
			using (IEnumerator enumerator = this.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return (DataSourceInfo)enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x0015D19C File Offset: 0x0015B39C
		public DataSourceInfoCollection CombineOnSetDefinition(DataSourceInfoCollection newDataSources)
		{
			return this.CombineOnSetDefinition(newDataSources, false, true);
		}

		// Token: 0x060052F0 RID: 21232 RVA: 0x0015D1A7 File Offset: 0x0015B3A7
		public DataSourceInfoCollection CombineOnSetDefinitionWithoutSideEffects(DataSourceInfoCollection newDataSources)
		{
			return this.CombineOnSetDefinition(newDataSources, false, false);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x0015D1B2 File Offset: 0x0015B3B2
		public DataSourceInfoCollection CombineOnSetDefinitionKeepOriginalDataSourceId(DataSourceInfoCollection newDataSources)
		{
			return this.CombineOnSetDefinition(newDataSources, true, true);
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x0015D1C0 File Offset: 0x0015B3C0
		private DataSourceInfoCollection CombineOnSetDefinition(DataSourceInfoCollection newDataSources, bool keepOriginalDataSourceId, bool overrideOriginalConnectString)
		{
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			foreach (object obj in newDataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				DataSourceInfo byOriginalName = this.GetByOriginalName(dataSourceInfo.OriginalName);
				if (byOriginalName == null)
				{
					dataSourceInfoCollection.Add(dataSourceInfo);
				}
				else
				{
					if (!keepOriginalDataSourceId)
					{
						byOriginalName.ID = dataSourceInfo.ID;
					}
					if (overrideOriginalConnectString)
					{
						byOriginalName.SetOriginalConnectionString(dataSourceInfo.OriginalConnectionStringEncrypted);
						byOriginalName.SetOriginalConnectStringExpressionBased(dataSourceInfo.OriginalConnectStringExpressionBased);
						byOriginalName.Extension = dataSourceInfo.Extension;
					}
					dataSourceInfoCollection.Add(byOriginalName);
				}
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x060052F3 RID: 21235 RVA: 0x0015D270 File Offset: 0x0015B470
		public DataSourceInfoCollection CombineOnSetDataSources(DataSourceInfoCollection newDataSources)
		{
			DataSourceInfoCollection dataSourceInfoCollection = new DataSourceInfoCollection();
			foreach (object obj in newDataSources)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				DataSourceInfo byOriginalName = this.GetByOriginalName(dataSourceInfo.OriginalName);
				if (byOriginalName == null)
				{
					throw new DataSourceNotFoundException(dataSourceInfo.OriginalName);
				}
				dataSourceInfo.ID = byOriginalName.ID;
				dataSourceInfo.SetOriginalConnectionString(byOriginalName.OriginalConnectionStringEncrypted);
				dataSourceInfo.SetOriginalConnectStringExpressionBased(byOriginalName.OriginalConnectStringExpressionBased);
				dataSourceInfoCollection.Add(dataSourceInfo);
			}
			foreach (object obj2 in this)
			{
				DataSourceInfo dataSourceInfo2 = (DataSourceInfo)obj2;
				if (newDataSources.GetByOriginalName(dataSourceInfo2.OriginalName) == null)
				{
					dataSourceInfoCollection.Add(dataSourceInfo2);
				}
			}
			return dataSourceInfoCollection;
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x0015D368 File Offset: 0x0015B568
		public bool TryGetCachedDataSourceId(string dataSourceName, out Guid dataSourceId)
		{
			dataSourceId = Guid.Empty;
			DataSourceInfo byOriginalName = this.GetByOriginalName(dataSourceName);
			if (byOriginalName != null)
			{
				dataSourceId = byOriginalName.ID;
				return true;
			}
			return false;
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x0015D39C File Offset: 0x0015B59C
		public void Add(DataSourceInfo dataSource)
		{
			if (dataSource.OriginalName == null)
			{
				RSTrace.ProcessingTracer.Assert(this.m_collection.Count == 0, "Adding more than one data source with null original name");
				this.m_collection.Add("", dataSource);
				return;
			}
			if (!this.m_collection.ContainsKey(dataSource.OriginalName))
			{
				this.m_collection.Add(dataSource.OriginalName, dataSource);
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x0015D405 File Offset: 0x0015B605
		public DataSourceInfo GetByOriginalName(string name)
		{
			return (DataSourceInfo)this.m_collection[name];
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0015D418 File Offset: 0x0015B618
		public IEnumerator GetEnumerator()
		{
			return this.m_collection.Values.GetEnumerator();
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x060052F8 RID: 21240 RVA: 0x0015D42A File Offset: 0x0015B62A
		public int Count
		{
			get
			{
				return this.m_collection.Count;
			}
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x0015D438 File Offset: 0x0015B638
		public bool GoodForDataCaching()
		{
			foreach (object obj in this.m_collection.Values)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (dataSourceInfo.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt)
				{
					return false;
				}
				if (dataSourceInfo.HasConnectionStringUseridReference)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x0015D4AC File Offset: 0x0015B6AC
		public bool HasConnectionStringUseridReference()
		{
			using (IEnumerator enumerator = this.m_collection.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataSourceInfo)enumerator.Current).HasConnectionStringUseridReference)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0015D510 File Offset: 0x0015B710
		public void AddOrUpdate(string key, DataSourceInfo dsInfo)
		{
			RSTrace.ProcessingTracer.Assert(key == ((dsInfo.OriginalName != null) ? dsInfo.OriginalName : string.Empty), "DataSourceInfo.AddOrUpdate: (dsInfo.OriginalName != null ? dsInfo.OriginalName : string.Empty)");
			if (this.m_collection.ContainsKey(key))
			{
				this.m_collection.Remove(key);
			}
			this.Add(dsInfo);
		}

		// Token: 0x060052FC RID: 21244 RVA: 0x0015D568 File Offset: 0x0015B768
		public DataSourceInfo GetDataSourceFromKey(string key)
		{
			return this.GetByOriginalName(key);
		}

		// Token: 0x040029D2 RID: 10706
		private Hashtable m_collection = new Hashtable();
	}
}

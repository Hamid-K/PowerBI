using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005AE RID: 1454
	[Serializable]
	public sealed class RuntimeDataSourceInfoCollection
	{
		// Token: 0x06005244 RID: 21060 RVA: 0x0015AD5F File Offset: 0x00158F5F
		public RuntimeDataSourceInfoCollection()
		{
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x0015AD68 File Offset: 0x00158F68
		public RuntimeDataSourceInfoCollection(RuntimeDataSourceInfoCollection other)
		{
			Global.Tracer.Assert(other != null, "(null != other)");
			if (other.m_collectionByID != null)
			{
				this.m_collectionByID = (Hashtable)other.m_collectionByID.Clone();
			}
			if (other.m_collectionByReport != null)
			{
				this.m_collectionByReport = (Hashtable)other.m_collectionByReport.Clone();
			}
			if (other.m_collectionByPrompt != null)
			{
				this.m_collectionByPrompt = other.m_collectionByPrompt.Clone();
			}
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0015ADE4 File Offset: 0x00158FE4
		public RuntimeDataSourceInfoCollection(SerializationInfo info, StreamingContext context)
		{
			this.m_collectionByID = (Hashtable)info.GetValue("dscollectionbyid", typeof(Hashtable));
			this.m_collectionByReport = (Hashtable)info.GetValue("dscollectionbyreport", typeof(Hashtable));
			this.m_collectionByPrompt = (CollectionByPrompt)info.GetValue("dscollectionbyprompt", typeof(CollectionByPrompt));
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x0015AE58 File Offset: 0x00159058
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

		// Token: 0x06005248 RID: 21064 RVA: 0x0015AEA0 File Offset: 0x001590A0
		public static RuntimeDataSourceInfoCollection Deserialize(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			MemoryStream memoryStream = null;
			RuntimeDataSourceInfoCollection runtimeDataSourceInfoCollection;
			try
			{
				memoryStream = new MemoryStream(data, false);
				runtimeDataSourceInfoCollection = (RuntimeDataSourceInfoCollection)new BinaryFormatter().Deserialize(memoryStream);
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return runtimeDataSourceInfoCollection;
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0015AEEC File Offset: 0x001590EC
		public void SetCredentials(DatasourceCredentialsCollection allCredentials, IDataProtection dataProtection)
		{
			if (allCredentials != null)
			{
				foreach (object obj in allCredentials)
				{
					DatasourceCredentials datasourceCredentials = (DatasourceCredentials)obj;
					this.SetCredentials(datasourceCredentials, dataProtection);
				}
			}
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x0015AF44 File Offset: 0x00159144
		private void SetCredentials(DatasourceCredentials credentials, IDataProtection dataProtection)
		{
			string promptID = credentials.PromptID;
			if (this.m_collectionByPrompt == null)
			{
				if (this.GetByOriginalName(promptID) != null)
				{
					throw new DataSourceNoPromptException(promptID);
				}
				throw new DataSourceNotFoundException(promptID);
			}
			else
			{
				PromptBucket bucketByOriginalName = this.m_collectionByPrompt.GetBucketByOriginalName(promptID);
				if (bucketByOriginalName != null)
				{
					bucketByOriginalName.SetCredentials(credentials, dataProtection);
					return;
				}
				if (this.GetByOriginalName(promptID) != null)
				{
					throw new DataSourceNoPromptException(promptID);
				}
				throw new DataSourceNotFoundException(promptID);
			}
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x0015AFA4 File Offset: 0x001591A4
		public DataSourceInfo GetByOriginalName(string originalName)
		{
			if (this.m_collectionByID == null)
			{
				return null;
			}
			foreach (object obj in this.m_collectionByID.Values)
			{
				DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
				if (dataSourceInfo.OriginalName == originalName)
				{
					return dataSourceInfo;
				}
			}
			return null;
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0015B01C File Offset: 0x0015921C
		public bool CredentialsAreSame(DatasourceCredentialsCollection creds, bool noCredentialsMeansSame, IDataProtection dataProtection)
		{
			if (noCredentialsMeansSame && (creds == null || creds.Count == 0))
			{
				return true;
			}
			if ((this.m_collectionByPrompt == null || this.m_collectionByPrompt.Count == 0) != (creds == null || creds.Count == 0))
			{
				return false;
			}
			if (creds == null || creds.Count == 0)
			{
				return true;
			}
			if (creds.Count != this.m_collectionByPrompt.Count)
			{
				return false;
			}
			foreach (object obj in creds)
			{
				DatasourceCredentials datasourceCredentials = (DatasourceCredentials)obj;
				DataSourceInfo representative = this.m_collectionByPrompt.GetBucketByOriginalName(datasourceCredentials.PromptID).GetRepresentative();
				if (representative == null)
				{
					return false;
				}
				if (representative.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Prompt)
				{
					return false;
				}
				if (representative.GetPasswordDecrypted(dataProtection) != datasourceCredentials.Password)
				{
					return false;
				}
				if (representative.GetUserName(dataProtection) != datasourceCredentials.UserName)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x0015B128 File Offset: 0x00159328
		public bool TrueForAll(Predicate<DataSourceInfo> predicate)
		{
			if (this.m_collectionByID != null)
			{
				foreach (object obj in this.m_collectionByID.Values)
				{
					DataSourceInfo dataSourceInfo = (DataSourceInfo)obj;
					if (!predicate(dataSourceInfo))
					{
						return false;
					}
				}
			}
			if (this.m_collectionByReport != null)
			{
				foreach (object obj2 in this.m_collectionByReport.Values)
				{
					foreach (object obj3 in ((DataSourceInfoCollection)obj2))
					{
						DataSourceInfo dataSourceInfo2 = (DataSourceInfo)obj3;
						if (!predicate(dataSourceInfo2))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x0015B238 File Offset: 0x00159438
		public bool GoodForDataCaching()
		{
			if (this.m_collectionByID != null)
			{
				foreach (object obj in this.m_collectionByID.Values)
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
			}
			if (this.m_collectionByReport != null)
			{
				using (IEnumerator enumerator = this.m_collectionByReport.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((DataSourceInfoCollection)enumerator.Current).GoodForDataCaching())
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0015B30C File Offset: 0x0015950C
		public bool HasConnectionStringUseridReference()
		{
			if (this.m_collectionByID != null)
			{
				using (IEnumerator enumerator = this.m_collectionByID.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((DataSourceInfo)enumerator.Current).HasConnectionStringUseridReference)
						{
							return true;
						}
					}
				}
			}
			if (this.m_collectionByReport != null)
			{
				using (IEnumerator enumerator = this.m_collectionByReport.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((DataSourceInfoCollection)enumerator.Current).HasConnectionStringUseridReference())
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x0015B3D0 File Offset: 0x001595D0
		public void Add(DataSourceInfo dataSource, ICatalogItemContext report)
		{
			if (Guid.Empty == dataSource.ID)
			{
				this.AddToCollectionByReport(dataSource, report);
			}
			else
			{
				this.AddToCollectionByID(dataSource);
			}
			this.CheckedAddByPrompt(dataSource);
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x0015B3FC File Offset: 0x001595FC
		internal DataSourceInfo GetForSharedDataSetExecution()
		{
			if (this.m_collectionByID != null)
			{
				Global.Tracer.Assert(1 == this.m_collectionByID.Count, "Shared dataset: RuntimeDataSourceInfoCollection must contain 1 data source");
				using (IEnumerator enumerator = this.m_collectionByID.Values.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return (DataSourceInfo)enumerator.Current;
					}
				}
			}
			return null;
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0015B480 File Offset: 0x00159680
		internal DataSourceInfo GetByID(Guid ID)
		{
			if (this.m_collectionByID != null)
			{
				return (DataSourceInfo)this.m_collectionByID[ID];
			}
			return null;
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x0015B4A4 File Offset: 0x001596A4
		internal DataSourceInfo GetByName(string name, ICatalogItemContext report)
		{
			if (this.m_collectionByReport != null)
			{
				DataSourceInfoCollection dataSourceInfoCollection = (DataSourceInfoCollection)this.m_collectionByReport[report.StableItemPath];
				if (dataSourceInfoCollection != null)
				{
					return dataSourceInfoCollection.GetByOriginalName(name);
				}
			}
			return null;
		}

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06005254 RID: 21076 RVA: 0x0015B4DC File Offset: 0x001596DC
		public bool NeedPrompt
		{
			get
			{
				return this.m_collectionByPrompt != null && this.m_collectionByPrompt.NeedPrompt;
			}
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0015B4F3 File Offset: 0x001596F3
		public DataSourcePromptCollection GetPromptRepresentatives(ServerDataSourceSettings serverDatasourceSettings)
		{
			if (this.m_collectionByPrompt == null)
			{
				return new DataSourcePromptCollection();
			}
			return this.m_collectionByPrompt.GetPromptRepresentatives(serverDatasourceSettings);
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x0015B510 File Offset: 0x00159710
		private void AddToCollectionByReport(DataSourceInfo dataSource, ICatalogItemContext report)
		{
			DataSourceInfoCollection dataSourceInfoCollection = null;
			if (this.m_collectionByReport == null)
			{
				this.m_collectionByReport = new Hashtable();
			}
			else
			{
				dataSourceInfoCollection = (DataSourceInfoCollection)this.m_collectionByReport[report.StableItemPath];
			}
			if (dataSourceInfoCollection == null)
			{
				dataSourceInfoCollection = new DataSourceInfoCollection();
				this.m_collectionByReport.Add(report.StableItemPath, dataSourceInfoCollection);
			}
			dataSourceInfoCollection.Add(dataSource);
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x0015B570 File Offset: 0x00159770
		private void AddToCollectionByID(DataSourceInfo dataSource)
		{
			if (this.m_collectionByID == null)
			{
				this.m_collectionByID = new Hashtable();
			}
			else if (this.m_collectionByID.ContainsKey(dataSource.ID))
			{
				return;
			}
			this.m_collectionByID.Add(dataSource.ID, dataSource);
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0015B5C2 File Offset: 0x001597C2
		private void CheckedAddByPrompt(DataSourceInfo dataSource)
		{
			if (dataSource.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Prompt)
			{
				return;
			}
			if (this.m_collectionByPrompt == null)
			{
				this.m_collectionByPrompt = new CollectionByPrompt();
			}
			this.m_collectionByPrompt.CheckedAdd(dataSource);
		}

		// Token: 0x04002992 RID: 10642
		private Hashtable m_collectionByID;

		// Token: 0x04002993 RID: 10643
		private Hashtable m_collectionByReport;

		// Token: 0x04002994 RID: 10644
		private CollectionByPrompt m_collectionByPrompt;
	}
}

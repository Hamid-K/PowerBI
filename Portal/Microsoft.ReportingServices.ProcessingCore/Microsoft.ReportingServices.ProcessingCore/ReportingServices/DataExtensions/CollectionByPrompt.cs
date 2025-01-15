using System;
using System.Collections;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005AF RID: 1455
	[Serializable]
	internal sealed class CollectionByPrompt : ArrayList
	{
		// Token: 0x06005259 RID: 21081 RVA: 0x0015B5ED File Offset: 0x001597ED
		internal CollectionByPrompt()
		{
		}

		// Token: 0x17001EA3 RID: 7843
		internal PromptBucket this[int index]
		{
			get
			{
				return (PromptBucket)base[index];
			}
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0015B604 File Offset: 0x00159804
		internal new CollectionByPrompt Clone()
		{
			CollectionByPrompt collectionByPrompt = new CollectionByPrompt();
			for (int i = 0; i < this.Count; i++)
			{
				collectionByPrompt.Add(this[i].Clone());
			}
			return collectionByPrompt;
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x0015B63C File Offset: 0x0015983C
		internal void CheckedAdd(DataSourceInfo dataSource)
		{
			if (dataSource.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Prompt)
			{
				return;
			}
			PromptBucket bucketByLinkID = this.GetBucketByLinkID(dataSource.LinkID);
			PromptBucket bucketByOriginalName = this.GetBucketByOriginalName(dataSource.PromptIdentifier);
			if (bucketByLinkID == null)
			{
				if (bucketByOriginalName == null)
				{
					this.Add(new PromptBucket { dataSource });
					return;
				}
				bucketByOriginalName.Add(dataSource);
				return;
			}
			else
			{
				if (bucketByOriginalName == null)
				{
					bucketByLinkID.Add(dataSource);
					return;
				}
				if (bucketByLinkID == bucketByOriginalName)
				{
					bucketByLinkID.Add(dataSource);
					return;
				}
				bucketByLinkID.AddRange(bucketByOriginalName);
				this.Remove(bucketByOriginalName);
				bucketByLinkID.Add(dataSource);
				return;
			}
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x0015B6C4 File Offset: 0x001598C4
		private PromptBucket GetBucketByLinkID(Guid linkID)
		{
			if (linkID == Guid.Empty)
			{
				return null;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].HasItemWithLinkID(linkID))
				{
					return this[i];
				}
			}
			return null;
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0015B70C File Offset: 0x0015990C
		internal PromptBucket GetBucketByOriginalName(string originalName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].HasItemWithOriginalName(originalName))
				{
					return this[i];
				}
			}
			return null;
		}

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x0600525F RID: 21087 RVA: 0x0015B744 File Offset: 0x00159944
		internal bool NeedPrompt
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i].NeedPrompt)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x0015B774 File Offset: 0x00159974
		internal DataSourcePromptCollection GetPromptRepresentatives(ServerDataSourceSettings serverDatasourceSettings)
		{
			DataSourcePromptCollection dataSourcePromptCollection = new DataSourcePromptCollection();
			for (int i = 0; i < this.Count; i++)
			{
				dataSourcePromptCollection.Add(this[i].GetRepresentative(), serverDatasourceSettings);
			}
			return dataSourcePromptCollection;
		}
	}
}

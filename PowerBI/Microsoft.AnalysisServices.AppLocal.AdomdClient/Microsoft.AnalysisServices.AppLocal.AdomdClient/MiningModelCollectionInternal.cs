using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BD RID: 189
	internal sealed class MiningModelCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002BAF8 File Offset: 0x00029CF8
		internal MiningModelCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningModel, MiningModelCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002BB28 File Offset: 0x00029D28
		internal MiningModelCollectionInternal(MiningStructure structure)
			: base(structure.ParentConnection)
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningModelCollectionInternal.structNameRest, structure.Name);
			this.parentObject = structure;
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(structure.ParentConnection, InternalObjectType.InternalTypeMiningModel, MiningModelCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x170003B2 RID: 946
		public MiningModel this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return this.GetMiningModelByRow(dataRow);
			}
		}

		// Token: 0x170003B3 RID: 947
		public MiningModel this[string index]
		{
			get
			{
				MiningModel miningModel = this.Find(index);
				if (null == miningModel)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningModel;
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002BBE8 File Offset: 0x00029DE8
		public MiningModel Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningModel.miningModelNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return this.GetMiningModelByRow(dataRow);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002BC20 File Offset: 0x00029E20
		private MiningModel GetMiningModelByRow(DataRow row)
		{
			MiningModel miningModel;
			if (row[0] is DBNull)
			{
				miningModel = new MiningModel(row, base.Connection, this.populatedTime, base.Catalog, base.SessionId, this.parentObject);
				row[0] = miningModel;
			}
			else
			{
				miningModel = (MiningModel)row[0];
			}
			return miningModel;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0002BC78 File Offset: 0x00029E78
		public override IEnumerator GetEnumerator()
		{
			return new MiningModelCollection.Enumerator(this);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0002BC85 File Offset: 0x00029E85
		internal override void CheckCache()
		{
		}

		// Token: 0x04000712 RID: 1810
		internal static string schemaName = "DMSCHEMA_MINING_MODELS";

		// Token: 0x04000713 RID: 1811
		internal static string miningModelNameRest = "MODEL_NAME";

		// Token: 0x04000714 RID: 1812
		internal static string structNameRest = "MINING_STRUCTURE";

		// Token: 0x04000715 RID: 1813
		private MiningStructure parentObject;
	}
}

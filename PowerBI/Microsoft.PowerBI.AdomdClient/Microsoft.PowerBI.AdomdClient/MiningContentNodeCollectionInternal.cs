using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B6 RID: 182
	internal sealed class MiningContentNodeCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x0002ABD8 File Offset: 0x00028DD8
		internal MiningContentNodeCollectionInternal(AdomdConnection connection, MiningModel parentMiningModel)
			: base(connection)
		{
			this.parentMiningModel = parentMiningModel;
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningContentNodeCollectionInternal.modelNameRest, parentMiningModel.Name);
			listDictionary.Add(MiningContentNodeCollectionInternal.nodeTypeRest, 1);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningContentNode, MiningContentNodeCollectionInternal.schemaName, listDictionary, true);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002AC3C File Offset: 0x00028E3C
		internal MiningContentNodeCollectionInternal(AdomdConnection connection, MiningModel parentMiningModel, string nodeUniqueName)
			: base(connection)
		{
			this.parentMiningModel = parentMiningModel;
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningContentNodeCollectionInternal.modelNameRest, parentMiningModel.Name);
			listDictionary.Add(MiningContentNodeCollectionInternal.nodeUniqueNameRest, nodeUniqueName);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningContentNode, MiningContentNodeCollectionInternal.schemaName, listDictionary, true);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002AC98 File Offset: 0x00028E98
		internal MiningContentNodeCollectionInternal(AdomdConnection connection, MiningContentNode parentNode, MiningNodeTreeOpType operation)
			: base(connection)
		{
			this.parentNode = parentNode;
			this.parentMiningModel = parentNode.ParentMiningModel;
			this.operation = operation;
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningContentNodeCollectionInternal.modelNameRest, this.parentMiningModel.Name);
			listDictionary.Add(MiningContentNodeCollectionInternal.nodeUniqueNameRest, parentNode.UniqueName);
			listDictionary.Add(MiningContentNodeCollectionInternal.treeOperationRest, (int)operation);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningContentNode, MiningContentNodeCollectionInternal.schemaName, listDictionary, true);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x1700036D RID: 877
		public MiningContentNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				MiningContentNode miningContentNodeByRow = MiningContentNodeCollectionInternal.GetMiningContentNodeByRow(this.nestedDataset, base.Connection, dataRow, this.parentMiningModel, base.Catalog, base.SessionId);
				if (miningContentNodeByRow != null && this.operation == MiningNodeTreeOpType.TreeopChildren)
				{
					miningContentNodeByRow.SetParentNode(this.parentNode);
				}
				return miningContentNodeByRow;
			}
		}

		// Token: 0x1700036E RID: 878
		public MiningContentNode this[string index]
		{
			get
			{
				MiningContentNode miningContentNode = this.Find(index);
				if (null == miningContentNode)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningContentNode;
			}
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002ADCC File Offset: 0x00028FCC
		public MiningContentNode Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningContentNode.miningContentNodeNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			MiningContentNode miningContentNodeByRow = MiningContentNodeCollectionInternal.GetMiningContentNodeByRow(this.nestedDataset, base.Connection, dataRow, this.parentMiningModel, base.Catalog, base.SessionId);
			if (miningContentNodeByRow != null && this.operation == MiningNodeTreeOpType.TreeopChildren)
			{
				miningContentNodeByRow.SetParentNode(this.parentNode);
			}
			return miningContentNodeByRow;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002AE3E File Offset: 0x0002903E
		public override IEnumerator GetEnumerator()
		{
			return new MiningContentNodeCollection.Enumerator(this);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002AE4C File Offset: 0x0002904C
		internal static MiningContentNode GetMiningContentNodeByRow(DataSet dataSet, AdomdConnection connection, DataRow row, MiningModel parentMiningModel, string catalog, string sessionId)
		{
			MiningContentNode miningContentNode;
			if (row[0] is DBNull)
			{
				DataRowCollection rows = dataSet.Tables[0].Rows;
				int num = (int)row[1];
				DataRow dataRow = rows[num];
				miningContentNode = new MiningContentNode(connection, dataRow, parentMiningModel, catalog, sessionId);
				row[0] = miningContentNode;
			}
			else
			{
				miningContentNode = (MiningContentNode)row[0];
			}
			return miningContentNode;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002AEB1 File Offset: 0x000290B1
		internal override void CheckCache()
		{
		}

		// Token: 0x040006CB RID: 1739
		internal static string schemaName = "DMSCHEMA_MINING_MODEL_CONTENT";

		// Token: 0x040006CC RID: 1740
		internal static string modelNameRest = "MODEL_NAME";

		// Token: 0x040006CD RID: 1741
		internal static string treeOperationRest = "TREE_OPERATION";

		// Token: 0x040006CE RID: 1742
		internal static string nodeTypeRest = "NODE_TYPE";

		// Token: 0x040006CF RID: 1743
		internal static string nodeUniqueNameRest = "NODE_UNIQUE_NAME";

		// Token: 0x040006D0 RID: 1744
		private MiningModel parentMiningModel;

		// Token: 0x040006D1 RID: 1745
		private MiningContentNode parentNode;

		// Token: 0x040006D2 RID: 1746
		private MiningNodeTreeOpType operation = MiningNodeTreeOpType.TreeopSelf;
	}
}

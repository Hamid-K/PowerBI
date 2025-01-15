using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B6 RID: 182
	internal sealed class MiningContentNodeCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x0002AF08 File Offset: 0x00029108
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

		// Token: 0x06000A46 RID: 2630 RVA: 0x0002AF6C File Offset: 0x0002916C
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

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002AFC8 File Offset: 0x000291C8
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

		// Token: 0x17000373 RID: 883
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

		// Token: 0x17000374 RID: 884
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

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002B0FC File Offset: 0x000292FC
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

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002B16E File Offset: 0x0002936E
		public override IEnumerator GetEnumerator()
		{
			return new MiningContentNodeCollection.Enumerator(this);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002B17C File Offset: 0x0002937C
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

		// Token: 0x06000A4D RID: 2637 RVA: 0x0002B1E1 File Offset: 0x000293E1
		internal override void CheckCache()
		{
		}

		// Token: 0x040006D8 RID: 1752
		internal static string schemaName = "DMSCHEMA_MINING_MODEL_CONTENT";

		// Token: 0x040006D9 RID: 1753
		internal static string modelNameRest = "MODEL_NAME";

		// Token: 0x040006DA RID: 1754
		internal static string treeOperationRest = "TREE_OPERATION";

		// Token: 0x040006DB RID: 1755
		internal static string nodeTypeRest = "NODE_TYPE";

		// Token: 0x040006DC RID: 1756
		internal static string nodeUniqueNameRest = "NODE_UNIQUE_NAME";

		// Token: 0x040006DD RID: 1757
		private MiningModel parentMiningModel;

		// Token: 0x040006DE RID: 1758
		private MiningContentNode parentNode;

		// Token: 0x040006DF RID: 1759
		private MiningNodeTreeOpType operation = MiningNodeTreeOpType.TreeopSelf;
	}
}

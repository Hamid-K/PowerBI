using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BD RID: 189
	internal sealed class MiningModelCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002B7C8 File Offset: 0x000299C8
		internal MiningModelCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningModel, MiningModelCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002B7F8 File Offset: 0x000299F8
		internal MiningModelCollectionInternal(MiningStructure structure)
			: base(structure.ParentConnection)
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningModelCollectionInternal.structNameRest, structure.Name);
			this.parentObject = structure;
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(structure.ParentConnection, InternalObjectType.InternalTypeMiningModel, MiningModelCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x170003AC RID: 940
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

		// Token: 0x170003AD RID: 941
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

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002B8B8 File Offset: 0x00029AB8
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

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0002B8F0 File Offset: 0x00029AF0
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

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002B948 File Offset: 0x00029B48
		public override IEnumerator GetEnumerator()
		{
			return new MiningModelCollection.Enumerator(this);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002B955 File Offset: 0x00029B55
		internal override void CheckCache()
		{
		}

		// Token: 0x04000705 RID: 1797
		internal static string schemaName = "DMSCHEMA_MINING_MODELS";

		// Token: 0x04000706 RID: 1798
		internal static string miningModelNameRest = "MODEL_NAME";

		// Token: 0x04000707 RID: 1799
		internal static string structNameRest = "MINING_STRUCTURE";

		// Token: 0x04000708 RID: 1800
		private MiningStructure parentObject;
	}
}

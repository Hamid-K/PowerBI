using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D3 RID: 211
	internal sealed class MiningStructureCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0002D5BC File Offset: 0x0002B7BC
		internal MiningStructureCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningStructure, MiningStructureCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x1700045A RID: 1114
		public MiningStructure this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return this.GetMiningStructureByRow(dataRow);
			}
		}

		// Token: 0x1700045B RID: 1115
		public MiningStructure this[string index]
		{
			get
			{
				MiningStructure miningStructure = this.Find(index);
				if (null == miningStructure)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningStructure;
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002D658 File Offset: 0x0002B858
		public MiningStructure Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningStructure.miningStructureNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return this.GetMiningStructureByRow(dataRow);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002D690 File Offset: 0x0002B890
		private MiningStructure GetMiningStructureByRow(DataRow row)
		{
			MiningStructure miningStructure;
			if (row[0] is DBNull)
			{
				miningStructure = new MiningStructure(row, base.Connection, this.populatedTime, base.Catalog, base.SessionId);
				row[0] = miningStructure;
			}
			else
			{
				miningStructure = (MiningStructure)row[0];
			}
			return miningStructure;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002D6E2 File Offset: 0x0002B8E2
		public override IEnumerator GetEnumerator()
		{
			return new MiningStructureCollection.Enumerator(this);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002D6EF File Offset: 0x0002B8EF
		internal override void CheckCache()
		{
		}

		// Token: 0x040007A4 RID: 1956
		internal static string schemaName = "DMSCHEMA_MINING_STRUCTURES";

		// Token: 0x040007A5 RID: 1957
		internal static string miningStructureNameRest = "STRUCTURE_NAME";
	}
}

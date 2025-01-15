using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D3 RID: 211
	internal sealed class MiningStructureCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		internal MiningStructureCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningStructure, MiningStructureCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x17000460 RID: 1120
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

		// Token: 0x17000461 RID: 1121
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

		// Token: 0x06000BCA RID: 3018 RVA: 0x0002D988 File Offset: 0x0002BB88
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

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002D9C0 File Offset: 0x0002BBC0
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

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002DA12 File Offset: 0x0002BC12
		public override IEnumerator GetEnumerator()
		{
			return new MiningStructureCollection.Enumerator(this);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0002DA1F File Offset: 0x0002BC1F
		internal override void CheckCache()
		{
		}

		// Token: 0x040007B1 RID: 1969
		internal static string schemaName = "DMSCHEMA_MINING_STRUCTURES";

		// Token: 0x040007B2 RID: 1970
		internal static string miningStructureNameRest = "STRUCTURE_NAME";
	}
}

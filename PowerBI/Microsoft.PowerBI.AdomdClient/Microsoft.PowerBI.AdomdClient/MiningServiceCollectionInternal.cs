using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C8 RID: 200
	internal sealed class MiningServiceCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x0002CB58 File Offset: 0x0002AD58
		internal MiningServiceCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningService, MiningServiceCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x17000415 RID: 1045
		public MiningService this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return this.GetMiningServiceByRow(dataRow);
			}
		}

		// Token: 0x17000416 RID: 1046
		public MiningService this[string index]
		{
			get
			{
				MiningService miningService = this.Find(index);
				if (null == miningService)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningService;
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		public MiningService Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningService.miningServiceNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return this.GetMiningServiceByRow(dataRow);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002CC2C File Offset: 0x0002AE2C
		private MiningService GetMiningServiceByRow(DataRow row)
		{
			MiningService miningService;
			if (row[0] is DBNull)
			{
				miningService = new MiningService(row, base.Connection, this.populatedTime, base.Catalog, base.SessionId);
				row[0] = miningService;
			}
			else
			{
				miningService = (MiningService)row[0];
			}
			return miningService;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002CC7E File Offset: 0x0002AE7E
		public override IEnumerator GetEnumerator()
		{
			return new MiningServiceCollection.Enumerator(this);
		}

		// Token: 0x04000769 RID: 1897
		internal static string schemaName = "DMSCHEMA_MINING_SERVICES";

		// Token: 0x0400076A RID: 1898
		internal static string miningServiceNameRest = "SERVICE_NAME";
	}
}

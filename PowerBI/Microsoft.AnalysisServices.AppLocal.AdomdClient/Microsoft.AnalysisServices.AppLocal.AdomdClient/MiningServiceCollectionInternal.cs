using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C8 RID: 200
	internal sealed class MiningServiceCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x0002CE88 File Offset: 0x0002B088
		internal MiningServiceCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningService, MiningServiceCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x1700041B RID: 1051
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

		// Token: 0x1700041C RID: 1052
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

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002CF24 File Offset: 0x0002B124
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

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002CF5C File Offset: 0x0002B15C
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

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002CFAE File Offset: 0x0002B1AE
		public override IEnumerator GetEnumerator()
		{
			return new MiningServiceCollection.Enumerator(this);
		}

		// Token: 0x04000776 RID: 1910
		internal static string schemaName = "DMSCHEMA_MINING_SERVICES";

		// Token: 0x04000777 RID: 1911
		internal static string miningServiceNameRest = "SERVICE_NAME";
	}
}

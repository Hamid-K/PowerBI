using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000CD RID: 205
	internal sealed class MiningServiceParameterCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0002D308 File Offset: 0x0002B508
		internal MiningServiceParameterCollectionInternal(AdomdConnection connection, MiningService parentService)
			: base(connection)
		{
			string name = parentService.Name;
			this.parentService = parentService;
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningServiceParameterCollectionInternal.serviceNameRest, name);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningServiceParameter, MiningServiceParameterCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x17000439 RID: 1081
		public MiningServiceParameter this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return MiningServiceParameterCollectionInternal.GetMiningServiceParameterByRow(base.Connection, dataRow, this.parentService, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x1700043A RID: 1082
		public MiningServiceParameter this[string index]
		{
			get
			{
				MiningServiceParameter miningServiceParameter = this.Find(index);
				if (null == miningServiceParameter)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningServiceParameter;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		public MiningServiceParameter Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningServiceParameter.serviceParameterColumn);
			if (dataRow == null)
			{
				return null;
			}
			return MiningServiceParameterCollectionInternal.GetMiningServiceParameterByRow(base.Connection, dataRow, this.parentService, base.Catalog, base.SessionId);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002D420 File Offset: 0x0002B620
		public override IEnumerator GetEnumerator()
		{
			return new MiningServiceParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002D430 File Offset: 0x0002B630
		internal static MiningServiceParameter GetMiningServiceParameterByRow(AdomdConnection connection, DataRow row, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			MiningServiceParameter miningServiceParameter;
			if (row[0] is DBNull)
			{
				miningServiceParameter = new MiningServiceParameter(connection, row, parentObject, catalog, sessionId);
				row[0] = miningServiceParameter;
			}
			else
			{
				miningServiceParameter = (MiningServiceParameter)row[0];
			}
			return miningServiceParameter;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D46F File Offset: 0x0002B66F
		internal override void CheckCache()
		{
		}

		// Token: 0x0400078D RID: 1933
		internal static string schemaName = "DMSCHEMA_MINING_SERVICE_PARAMETERS";

		// Token: 0x0400078E RID: 1934
		internal static string columnNameRest = "PARAMETER_NAME";

		// Token: 0x0400078F RID: 1935
		internal static string serviceNameRest = "SERVICE_NAME";

		// Token: 0x04000790 RID: 1936
		private MiningService parentService;
	}
}

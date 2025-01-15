using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000CD RID: 205
	internal sealed class MiningServiceParameterCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
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

		// Token: 0x17000433 RID: 1075
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

		// Token: 0x17000434 RID: 1076
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

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D0A4 File Offset: 0x0002B2A4
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

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
		public override IEnumerator GetEnumerator()
		{
			return new MiningServiceParameterCollection.Enumerator(this);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D100 File Offset: 0x0002B300
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

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002D13F File Offset: 0x0002B33F
		internal override void CheckCache()
		{
		}

		// Token: 0x04000780 RID: 1920
		internal static string schemaName = "DMSCHEMA_MINING_SERVICE_PARAMETERS";

		// Token: 0x04000781 RID: 1921
		internal static string columnNameRest = "PARAMETER_NAME";

		// Token: 0x04000782 RID: 1922
		internal static string serviceNameRest = "SERVICE_NAME";

		// Token: 0x04000783 RID: 1923
		private MiningService parentService;
	}
}

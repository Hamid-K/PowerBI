using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009B RID: 155
	internal sealed class KpiCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00027BC8 File Offset: 0x00025DC8
		internal KpiCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeKpi, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x170002BF RID: 703
		public Kpi this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return KpiCollectionInternal.GetKpiByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x170002C0 RID: 704
		public Kpi this[string index]
		{
			get
			{
				Kpi kpi = this.Find(index);
				if (null == kpi)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return kpi;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00027C60 File Offset: 0x00025E60
		public Kpi Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, Kpi.kpiNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return KpiCollectionInternal.GetKpiByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00027CAC File Offset: 0x00025EAC
		public override IEnumerator GetEnumerator()
		{
			return new KpiCollection.Enumerator(this);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00027CBC File Offset: 0x00025EBC
		internal static Kpi GetKpiByRow(AdomdConnection connection, DataRow row, CubeDef parentCube, string catalog, string sessionId)
		{
			Kpi kpi;
			if (row[0] is DBNull)
			{
				kpi = new Kpi(connection, row, parentCube, catalog, sessionId);
				row[0] = kpi;
			}
			else
			{
				kpi = (Kpi)row[0];
			}
			return kpi;
		}

		// Token: 0x040005DF RID: 1503
		internal static string schemaName = "MDSCHEMA_KPIS";

		// Token: 0x040005E0 RID: 1504
		private CubeDef parentCube;
	}
}

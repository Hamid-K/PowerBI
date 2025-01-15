using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009B RID: 155
	internal sealed class KpiCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00027EF8 File Offset: 0x000260F8
		internal KpiCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeKpi, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x170002C5 RID: 709
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

		// Token: 0x170002C6 RID: 710
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

		// Token: 0x060008FC RID: 2300 RVA: 0x00027F90 File Offset: 0x00026190
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

		// Token: 0x060008FD RID: 2301 RVA: 0x00027FDC File Offset: 0x000261DC
		public override IEnumerator GetEnumerator()
		{
			return new KpiCollection.Enumerator(this);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00027FEC File Offset: 0x000261EC
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

		// Token: 0x040005EC RID: 1516
		internal static string schemaName = "MDSCHEMA_KPIS";

		// Token: 0x040005ED RID: 1517
		private CubeDef parentCube;
	}
}

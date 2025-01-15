using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A5 RID: 165
	internal sealed class MeasureCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x0600097F RID: 2431 RVA: 0x00028C30 File Offset: 0x00026E30
		internal MeasureCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeMeasure, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x1700030E RID: 782
		public Measure this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return MeasureCollectionInternal.GetMeasureByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x1700030F RID: 783
		public Measure this[string index]
		{
			get
			{
				Measure measure = this.Find(index);
				if (null == measure)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return measure;
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00028CC8 File Offset: 0x00026EC8
		public Measure Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, Measure.measureNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return MeasureCollectionInternal.GetMeasureByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00028D14 File Offset: 0x00026F14
		public override IEnumerator GetEnumerator()
		{
			return new MeasureCollection.Enumerator(this);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00028D24 File Offset: 0x00026F24
		internal static Measure GetMeasureByRow(AdomdConnection connection, DataRow row, CubeDef parentCube, string catalog, string sessionId)
		{
			Measure measure;
			if (row[0] is DBNull)
			{
				measure = new Measure(connection, row, parentCube, catalog, sessionId);
				row[0] = measure;
			}
			else
			{
				measure = (Measure)row[0];
			}
			return measure;
		}

		// Token: 0x04000649 RID: 1609
		internal static string schemaName = "MDSCHEMA_MEASURES";

		// Token: 0x0400064A RID: 1610
		private CubeDef parentCube;
	}
}

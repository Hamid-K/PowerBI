using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A5 RID: 165
	internal sealed class MeasureCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x00028900 File Offset: 0x00026B00
		internal MeasureCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeMeasure, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x17000308 RID: 776
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

		// Token: 0x17000309 RID: 777
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

		// Token: 0x06000975 RID: 2421 RVA: 0x00028998 File Offset: 0x00026B98
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

		// Token: 0x06000976 RID: 2422 RVA: 0x000289E4 File Offset: 0x00026BE4
		public override IEnumerator GetEnumerator()
		{
			return new MeasureCollection.Enumerator(this);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000289F4 File Offset: 0x00026BF4
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

		// Token: 0x0400063C RID: 1596
		internal static string schemaName = "MDSCHEMA_MEASURES";

		// Token: 0x0400063D RID: 1597
		private CubeDef parentCube;
	}
}

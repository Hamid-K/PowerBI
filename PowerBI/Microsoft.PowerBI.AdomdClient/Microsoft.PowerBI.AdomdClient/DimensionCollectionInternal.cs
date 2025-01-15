using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000084 RID: 132
	internal sealed class DimensionCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x0600083D RID: 2109 RVA: 0x00026D84 File Offset: 0x00024F84
		internal DimensionCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeDimension, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x1700025C RID: 604
		public Dimension this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return DimensionCollectionInternal.GetDimensionByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x1700025D RID: 605
		public Dimension this[string index]
		{
			get
			{
				Dimension dimension = this.Find(index);
				if (null == dimension)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index));
				}
				return dimension;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00026E18 File Offset: 0x00025018
		public Dimension Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, Dimension.dimensionNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return DimensionCollectionInternal.GetDimensionByRow(base.Connection, dataRow, this.parentCube, base.Catalog, base.SessionId);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00026E64 File Offset: 0x00025064
		public override IEnumerator GetEnumerator()
		{
			return new DimensionCollection.Enumerator(this);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00026E74 File Offset: 0x00025074
		internal static Dimension GetDimensionByRow(AdomdConnection connection, DataRow row, CubeDef parentCube, string catalog, string sessionId)
		{
			Dimension dimension;
			if (row[0] is DBNull)
			{
				dimension = new Dimension(connection, row, parentCube, catalog, sessionId);
				row[0] = dimension;
			}
			else
			{
				dimension = (Dimension)row[0];
			}
			return dimension;
		}

		// Token: 0x04000580 RID: 1408
		internal static string schemaName = "MDSCHEMA_DIMENSIONS";

		// Token: 0x04000581 RID: 1409
		internal static string dimUNameRest = "DIMENSION_UNIQUE_NAME";

		// Token: 0x04000582 RID: 1410
		private CubeDef parentCube;
	}
}

using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000084 RID: 132
	internal sealed class DimensionCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x000270B4 File Offset: 0x000252B4
		internal DimensionCollectionInternal(AdomdConnection connection, CubeDef parentCube)
			: base(connection, InternalObjectType.InternalTypeDimension, parentCube.metadataCache)
		{
			this.parentCube = parentCube;
		}

		// Token: 0x17000262 RID: 610
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

		// Token: 0x17000263 RID: 611
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

		// Token: 0x0600084D RID: 2125 RVA: 0x00027148 File Offset: 0x00025348
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

		// Token: 0x0600084E RID: 2126 RVA: 0x00027194 File Offset: 0x00025394
		public override IEnumerator GetEnumerator()
		{
			return new DimensionCollection.Enumerator(this);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000271A4 File Offset: 0x000253A4
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

		// Token: 0x0400058D RID: 1421
		internal static string schemaName = "MDSCHEMA_DIMENSIONS";

		// Token: 0x0400058E RID: 1422
		internal static string dimUNameRest = "DIMENSION_UNIQUE_NAME";

		// Token: 0x0400058F RID: 1423
		private CubeDef parentCube;
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007D RID: 125
	internal sealed class CubeCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x060007BB RID: 1979 RVA: 0x0002567C File Offset: 0x0002387C
		internal CubeCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			AdomdUtils.AddCubeSourceRestrictionIfApplicable(connection, listDictionary);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeCube, CubeCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x1700021F RID: 543
		public CubeDef this[int index]
		{
			get
			{
				if (index < 0 || index >= base.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return this.GetCubeByRow(dataRow);
			}
		}

		// Token: 0x17000220 RID: 544
		public CubeDef this[string index]
		{
			get
			{
				CubeDef cubeDef = this.Find(index);
				if (null == cubeDef)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return cubeDef;
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00025720 File Offset: 0x00023920
		public CubeDef Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, CubeDef.cubeNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return this.GetCubeByRow(dataRow);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00025758 File Offset: 0x00023958
		private CubeDef GetCubeByRow(DataRow row)
		{
			CubeDef cubeDef;
			if (row[0] is DBNull)
			{
				cubeDef = new CubeDef(row, base.Connection, this.populatedTime, base.Catalog, base.SessionId);
				row[0] = cubeDef;
			}
			else
			{
				cubeDef = (CubeDef)row[0];
			}
			return cubeDef;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000257AC File Offset: 0x000239AC
		internal override void MarkCacheAsNeedCheckForValidness()
		{
			base.MarkCacheAsNeedCheckForValidness();
			if (this.isPopulated)
			{
				foreach (object obj in this.internalCollection)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow[0] is CubeDef)
					{
						((CubeDef)dataRow[0]).metadataCache.MarkNeedCheckForValidness();
					}
				}
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00025830 File Offset: 0x00023A30
		internal override void AbandonCache()
		{
			base.AbandonCache();
			if (this.isPopulated)
			{
				foreach (object obj in this.internalCollection)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow[0] is CubeDef)
					{
						((CubeDef)dataRow[0]).metadataCache.MarkAbandoned();
					}
				}
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000258B4 File Offset: 0x00023AB4
		public override IEnumerator GetEnumerator()
		{
			return new CubeCollection.Enumerator(this);
		}

		// Token: 0x0400054B RID: 1355
		internal static string schemaName = "MDSCHEMA_CUBES";

		// Token: 0x0400054C RID: 1356
		internal static string cubeNameRest = "CUBE_NAME";
	}
}

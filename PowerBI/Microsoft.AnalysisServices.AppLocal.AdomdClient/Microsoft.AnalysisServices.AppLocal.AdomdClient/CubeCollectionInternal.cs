using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007D RID: 125
	internal sealed class CubeCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x000259AC File Offset: 0x00023BAC
		internal CubeCollectionInternal(AdomdConnection connection)
			: base(connection)
		{
			ListDictionary listDictionary = new ListDictionary();
			AdomdUtils.AddCubeSourceRestrictionIfApplicable(connection, listDictionary);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeCube, CubeCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x17000225 RID: 549
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

		// Token: 0x17000226 RID: 550
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

		// Token: 0x060007CB RID: 1995 RVA: 0x00025A50 File Offset: 0x00023C50
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

		// Token: 0x060007CC RID: 1996 RVA: 0x00025A88 File Offset: 0x00023C88
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

		// Token: 0x060007CD RID: 1997 RVA: 0x00025ADC File Offset: 0x00023CDC
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

		// Token: 0x060007CE RID: 1998 RVA: 0x00025B60 File Offset: 0x00023D60
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

		// Token: 0x060007CF RID: 1999 RVA: 0x00025BE4 File Offset: 0x00023DE4
		public override IEnumerator GetEnumerator()
		{
			return new CubeCollection.Enumerator(this);
		}

		// Token: 0x04000558 RID: 1368
		internal static string schemaName = "MDSCHEMA_CUBES";

		// Token: 0x04000559 RID: 1369
		internal static string cubeNameRest = "CUBE_NAME";
	}
}

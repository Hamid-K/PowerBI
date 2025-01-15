using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009E RID: 158
	internal sealed class LevelCollectionInternal : CacheBasedFilteredCollection
	{
		// Token: 0x06000934 RID: 2356 RVA: 0x00028478 File Offset: 0x00026678
		internal LevelCollectionInternal(AdomdConnection connection, Hierarchy parentHierarchy)
			: base(connection, InternalObjectType.InternalTypeLevel, parentHierarchy.ParentDimension.ParentCube.metadataCache)
		{
			this.parentHierarchy = parentHierarchy;
			base.Initialize((DataRow)((IAdomdBaseObject)parentHierarchy).MetadataData, null);
		}

		// Token: 0x170002E6 RID: 742
		public Level this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return LevelCollectionInternal.GetLevelByRow(base.Connection, dataRow, this.parentHierarchy, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x170002E7 RID: 743
		public Level this[string index]
		{
			get
			{
				Level level = this.Find(index);
				if (null == level)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return level;
			}
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00028528 File Offset: 0x00026728
		public Level Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, (DataRow)((IAdomdBaseObject)this.parentHierarchy).MetadataData, Level.levelNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return LevelCollectionInternal.GetLevelByRow(base.Connection, dataRow, this.parentHierarchy, base.Catalog, base.SessionId);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00028583 File Offset: 0x00026783
		public override IEnumerator GetEnumerator()
		{
			return new LevelCollection.Enumerator(this);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00028590 File Offset: 0x00026790
		internal static Level GetLevelByRow(AdomdConnection connection, DataRow row, Hierarchy parentHierarchy, string catalog, string sessionId)
		{
			Level level;
			if (row[0] is DBNull)
			{
				level = new Level(connection, row, parentHierarchy, catalog, sessionId);
				row[0] = level;
			}
			else
			{
				level = (Level)row[0];
			}
			return level;
		}

		// Token: 0x040005FC RID: 1532
		internal static string schemaName = "MDSCHEMA_LEVELS";

		// Token: 0x040005FD RID: 1533
		internal static string levelUNameRest = "LEVEL_UNIQUE_NAME";

		// Token: 0x040005FE RID: 1534
		private Hierarchy parentHierarchy;
	}
}

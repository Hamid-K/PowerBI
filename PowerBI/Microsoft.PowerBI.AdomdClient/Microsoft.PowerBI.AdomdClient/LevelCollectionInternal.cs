using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009E RID: 158
	internal sealed class LevelCollectionInternal : CacheBasedFilteredCollection
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00028148 File Offset: 0x00026348
		internal LevelCollectionInternal(AdomdConnection connection, Hierarchy parentHierarchy)
			: base(connection, InternalObjectType.InternalTypeLevel, parentHierarchy.ParentDimension.ParentCube.metadataCache)
		{
			this.parentHierarchy = parentHierarchy;
			base.Initialize((DataRow)((IAdomdBaseObject)parentHierarchy).MetadataData, null);
		}

		// Token: 0x170002E0 RID: 736
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

		// Token: 0x170002E1 RID: 737
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

		// Token: 0x0600092A RID: 2346 RVA: 0x000281F8 File Offset: 0x000263F8
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

		// Token: 0x0600092B RID: 2347 RVA: 0x00028253 File Offset: 0x00026453
		public override IEnumerator GetEnumerator()
		{
			return new LevelCollection.Enumerator(this);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00028260 File Offset: 0x00026460
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

		// Token: 0x040005EF RID: 1519
		internal static string schemaName = "MDSCHEMA_LEVELS";

		// Token: 0x040005F0 RID: 1520
		internal static string levelUNameRest = "LEVEL_UNIQUE_NAME";

		// Token: 0x040005F1 RID: 1521
		private Hierarchy parentHierarchy;
	}
}

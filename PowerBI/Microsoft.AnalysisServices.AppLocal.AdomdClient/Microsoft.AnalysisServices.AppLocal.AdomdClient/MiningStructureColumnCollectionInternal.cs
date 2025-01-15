using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D6 RID: 214
	internal sealed class MiningStructureColumnCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0002DEE4 File Offset: 0x0002C0E4
		internal MiningStructureColumnCollectionInternal(AdomdConnection connection, MiningStructure parentStructure)
			: base(connection)
		{
			string name = parentStructure.Name;
			this.parentObject = parentStructure;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002DF10 File Offset: 0x0002C110
		internal MiningStructureColumnCollectionInternal(AdomdConnection connection, MiningStructureColumn parentColumn)
			: base(connection)
		{
			string name = parentColumn.ParentMiningStructure.Name;
			this.parentObject = parentColumn;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002DF40 File Offset: 0x0002C140
		private void InternalConstructor(AdomdConnection connection, string parentStructureName)
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningStructureColumnCollectionInternal.structureNameRest, parentStructureName);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningStructureColumn, MiningStructureColumnCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x17000488 RID: 1160
		public MiningStructureColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return MiningStructureColumnCollectionInternal.GetMiningStructureColumnByRow(base.Connection, dataRow, this.parentObject, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x17000489 RID: 1161
		public MiningStructureColumn this[string index]
		{
			get
			{
				MiningStructureColumn miningStructureColumn = this.Find(index);
				if (null == miningStructureColumn)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningStructureColumn;
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002DFF8 File Offset: 0x0002C1F8
		public MiningStructureColumn Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningStructureColumn.miningStructureColumnNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return MiningStructureColumnCollectionInternal.GetMiningStructureColumnByRow(base.Connection, dataRow, this.parentObject, base.Catalog, base.SessionId);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0002E044 File Offset: 0x0002C244
		public override IEnumerator GetEnumerator()
		{
			return new MiningStructureColumnCollection.Enumerator(this);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002E054 File Offset: 0x0002C254
		internal static MiningStructureColumn GetMiningStructureColumnByRow(AdomdConnection connection, DataRow row, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			MiningStructureColumn miningStructureColumn;
			if (row[0] is DBNull)
			{
				miningStructureColumn = new MiningStructureColumn(connection, row, parentObject, catalog, sessionId);
				row[0] = miningStructureColumn;
			}
			else
			{
				miningStructureColumn = (MiningStructureColumn)row[0];
			}
			return miningStructureColumn;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002E094 File Offset: 0x0002C294
		protected override void PopulateCollection()
		{
			if (!this.isPopulated)
			{
				base.PopulateCollection();
				string text = "";
				if (this.parentObject is MiningStructure)
				{
					text = "";
				}
				else if (this.parentObject is MiningStructureColumn)
				{
					text = ((MiningStructureColumn)this.parentObject).Name;
				}
				int i = 0;
				while (i < this.Count)
				{
					if (this[i].ContainingColumn != text)
					{
						this.internalCollection.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002E11D File Offset: 0x0002C31D
		internal override void CheckCache()
		{
		}

		// Token: 0x040007C3 RID: 1987
		internal static string schemaName = "DMSCHEMA_MINING_STRUCTURE_COLUMNS";

		// Token: 0x040007C4 RID: 1988
		internal static string columnNameRest = "COLUMN_NAME";

		// Token: 0x040007C5 RID: 1989
		internal static string structureNameRest = "STRUCTURE_NAME";

		// Token: 0x040007C6 RID: 1990
		private IAdomdBaseObject parentObject;
	}
}

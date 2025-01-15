using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D6 RID: 214
	internal sealed class MiningStructureColumnCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
		internal MiningStructureColumnCollectionInternal(AdomdConnection connection, MiningStructure parentStructure)
			: base(connection)
		{
			string name = parentStructure.Name;
			this.parentObject = parentStructure;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
		internal MiningStructureColumnCollectionInternal(AdomdConnection connection, MiningStructureColumn parentColumn)
			: base(connection)
		{
			string name = parentColumn.ParentMiningStructure.Name;
			this.parentObject = parentColumn;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002DC10 File Offset: 0x0002BE10
		private void InternalConstructor(AdomdConnection connection, string parentStructureName)
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningStructureColumnCollectionInternal.structureNameRest, parentStructureName);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningStructureColumn, MiningStructureColumnCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x17000482 RID: 1154
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

		// Token: 0x17000483 RID: 1155
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

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002DCC8 File Offset: 0x0002BEC8
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

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002DD14 File Offset: 0x0002BF14
		public override IEnumerator GetEnumerator()
		{
			return new MiningStructureColumnCollection.Enumerator(this);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002DD24 File Offset: 0x0002BF24
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

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002DD64 File Offset: 0x0002BF64
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

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002DDED File Offset: 0x0002BFED
		internal override void CheckCache()
		{
		}

		// Token: 0x040007B6 RID: 1974
		internal static string schemaName = "DMSCHEMA_MINING_STRUCTURE_COLUMNS";

		// Token: 0x040007B7 RID: 1975
		internal static string columnNameRest = "COLUMN_NAME";

		// Token: 0x040007B8 RID: 1976
		internal static string structureNameRest = "STRUCTURE_NAME";

		// Token: 0x040007B9 RID: 1977
		private IAdomdBaseObject parentObject;
	}
}

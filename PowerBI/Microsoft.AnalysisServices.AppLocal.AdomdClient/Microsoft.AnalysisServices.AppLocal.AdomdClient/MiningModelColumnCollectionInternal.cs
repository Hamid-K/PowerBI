using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C0 RID: 192
	internal sealed class MiningModelColumnCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002C2FC File Offset: 0x0002A4FC
		internal MiningModelColumnCollectionInternal(AdomdConnection connection, MiningModel parentModel)
			: base(connection)
		{
			string name = parentModel.Name;
			this.parentObject = parentModel;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0002C328 File Offset: 0x0002A528
		internal MiningModelColumnCollectionInternal(AdomdConnection connection, MiningModelColumn parentColumn)
			: base(connection)
		{
			string name = parentColumn.ParentMiningModel.Name;
			this.parentObject = parentColumn;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002C358 File Offset: 0x0002A558
		private void InternalConstructor(AdomdConnection connection, string parentModelName)
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningModelColumnCollectionInternal.modelNameRest, parentModelName);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningModelColumn, MiningModelColumnCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x170003E1 RID: 993
		public MiningModelColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				return MiningModelColumnCollectionInternal.GetMiningModelColumnByRow(base.Connection, dataRow, this.parentObject, base.Catalog, base.SessionId);
			}
		}

		// Token: 0x170003E2 RID: 994
		public MiningModelColumn this[string index]
		{
			get
			{
				MiningModelColumn miningModelColumn = this.Find(index);
				if (null == miningModelColumn)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return miningModelColumn;
			}
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0002C410 File Offset: 0x0002A610
		public MiningModelColumn Find(string index)
		{
			if (index == null)
			{
				throw new ArgumentNullException("index");
			}
			DataRow dataRow = base.FindObjectByName(index, null, MiningModelColumn.miningModelColumnNameColumn);
			if (dataRow == null)
			{
				return null;
			}
			return MiningModelColumnCollectionInternal.GetMiningModelColumnByRow(base.Connection, dataRow, this.parentObject, base.Catalog, base.SessionId);
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002C45C File Offset: 0x0002A65C
		public override IEnumerator GetEnumerator()
		{
			return new MiningModelColumnCollection.Enumerator(this);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0002C46C File Offset: 0x0002A66C
		internal static MiningModelColumn GetMiningModelColumnByRow(AdomdConnection connection, DataRow row, IAdomdBaseObject parentObject, string catalog, string sessionId)
		{
			MiningModelColumn miningModelColumn;
			if (row[0] is DBNull)
			{
				miningModelColumn = new MiningModelColumn(connection, row, parentObject, catalog, sessionId);
				row[0] = miningModelColumn;
			}
			else
			{
				miningModelColumn = (MiningModelColumn)row[0];
			}
			return miningModelColumn;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		protected override void PopulateCollection()
		{
			if (!this.isPopulated)
			{
				base.PopulateCollection();
				string text = "";
				if (this.parentObject is MiningModel)
				{
					text = "";
				}
				else if (this.parentObject is MiningModelColumn)
				{
					text = ((MiningModelColumn)this.parentObject).Name;
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

		// Token: 0x06000B02 RID: 2818 RVA: 0x0002C535 File Offset: 0x0002A735
		internal override void CheckCache()
		{
		}

		// Token: 0x0400072C RID: 1836
		internal static string schemaName = "DMSCHEMA_MINING_COLUMNS";

		// Token: 0x0400072D RID: 1837
		internal static string columnNameRest = "COLUMN_NAME";

		// Token: 0x0400072E RID: 1838
		internal static string modelNameRest = "MODEL_NAME";

		// Token: 0x0400072F RID: 1839
		private IAdomdBaseObject parentObject;
	}
}

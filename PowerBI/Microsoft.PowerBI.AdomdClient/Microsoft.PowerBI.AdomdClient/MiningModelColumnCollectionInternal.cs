using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000C0 RID: 192
	internal sealed class MiningModelColumnCollectionInternal : CacheBasedNotFilteredCollection
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x0002BFCC File Offset: 0x0002A1CC
		internal MiningModelColumnCollectionInternal(AdomdConnection connection, MiningModel parentModel)
			: base(connection)
		{
			string name = parentModel.Name;
			this.parentObject = parentModel;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		internal MiningModelColumnCollectionInternal(AdomdConnection connection, MiningModelColumn parentColumn)
			: base(connection)
		{
			string name = parentColumn.ParentMiningModel.Name;
			this.parentObject = parentColumn;
			this.InternalConstructor(connection, name);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002C028 File Offset: 0x0002A228
		private void InternalConstructor(AdomdConnection connection, string parentModelName)
		{
			ListDictionary listDictionary = new ListDictionary();
			listDictionary.Add(MiningModelColumnCollectionInternal.modelNameRest, parentModelName);
			ObjectMetadataCache objectMetadataCache = new ObjectMetadataCache(connection, InternalObjectType.InternalTypeMiningModelColumn, MiningModelColumnCollectionInternal.schemaName, listDictionary);
			base.Initialize(objectMetadataCache);
		}

		// Token: 0x170003DB RID: 987
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

		// Token: 0x170003DC RID: 988
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

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
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

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002C12C File Offset: 0x0002A32C
		public override IEnumerator GetEnumerator()
		{
			return new MiningModelColumnCollection.Enumerator(this);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002C13C File Offset: 0x0002A33C
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

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002C17C File Offset: 0x0002A37C
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

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002C205 File Offset: 0x0002A405
		internal override void CheckCache()
		{
		}

		// Token: 0x0400071F RID: 1823
		internal static string schemaName = "DMSCHEMA_MINING_COLUMNS";

		// Token: 0x04000720 RID: 1824
		internal static string columnNameRest = "COLUMN_NAME";

		// Token: 0x04000721 RID: 1825
		internal static string modelNameRest = "MODEL_NAME";

		// Token: 0x04000722 RID: 1826
		private IAdomdBaseObject parentObject;
	}
}

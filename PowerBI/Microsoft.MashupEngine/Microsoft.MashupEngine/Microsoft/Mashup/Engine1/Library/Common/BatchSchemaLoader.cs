using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200102B RID: 4139
	internal abstract class BatchSchemaLoader
	{
		// Token: 0x06006BF2 RID: 27634 RVA: 0x00173984 File Offset: 0x00171B84
		protected BatchSchemaLoader(DbEnvironment environment, bool useInClause, bool usePrefetch)
		{
			this.batchSize = (usePrefetch ? 100 : 0);
			this.smallDatabase = (BatchSchemaLoader.TreatAsSmallDatabase ? 0 : this.batchSize);
			this.environment = environment;
			this.useInClause = useInClause;
			this.columns = new Dictionary<SchemaItem, DataTable>();
			this.foreignKeysParent = new Dictionary<SchemaItem, DataTable>();
			this.foreignKeysChild = new Dictionary<SchemaItem, DataTable>();
			this.indexes = new Dictionary<SchemaItem, DataTable>();
			IClearableTransientCache transientCache = this.environment.Host.GetTransientCache();
			if (transientCache != null)
			{
				transientCache.Add(new Action(this.Clear));
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x06006BF3 RID: 27635 RVA: 0x00173A1B File Offset: 0x00171C1B
		// (set) Token: 0x06006BF4 RID: 27636 RVA: 0x00173A23 File Offset: 0x00171C23
		private protected SchemaItem[] Catalog { protected get; private set; }

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x06006BF5 RID: 27637 RVA: 0x00173A2C File Offset: 0x00171C2C
		// (set) Token: 0x06006BF6 RID: 27638 RVA: 0x00173A34 File Offset: 0x00171C34
		private protected bool LoadAllValues { protected get; private set; }

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x06006BF7 RID: 27639 RVA: 0x00173A3D File Offset: 0x00171C3D
		protected DbEnvironment Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x06006BF8 RID: 27640 RVA: 0x00173A45 File Offset: 0x00171C45
		protected virtual int MaxItemsInList
		{
			get
			{
				return 1000000;
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x06006BF9 RID: 27641 RVA: 0x00173A4C File Offset: 0x00171C4C
		protected virtual int SmallDatabase
		{
			get
			{
				return this.smallDatabase;
			}
		}

		// Token: 0x06006BFA RID: 27642
		protected abstract void InitializeQueues();

		// Token: 0x06006BFB RID: 27643
		protected abstract BatchSchemaLoader.SchemaItemQueue GetQueueForSchema(QueueKind queueKind, string schemaName);

		// Token: 0x06006BFC RID: 27644
		protected abstract void ClearQueues();

		// Token: 0x06006BFD RID: 27645
		protected abstract string GetColumnsQuery(SchemaItem[] items);

		// Token: 0x06006BFE RID: 27646
		protected abstract string GetForeignKeyQuery(SchemaItem[] items);

		// Token: 0x06006BFF RID: 27647
		protected abstract string GetIndexesQuery(SchemaItem[] items);

		// Token: 0x06006C00 RID: 27648 RVA: 0x00173A54 File Offset: 0x00171C54
		public void SetTables(DataTable tables)
		{
			SchemaItem[] array = new SchemaItem[tables.Rows.Count];
			DataColumn dataColumn = tables.Columns["TABLE_SCHEMA"];
			DataColumn dataColumn2 = tables.Columns["TABLE_NAME"];
			for (int i = 0; i < tables.Rows.Count; i++)
			{
				array[i] = new SchemaItem((string)tables.Rows[i][dataColumn], (string)tables.Rows[i][dataColumn2], string.Empty);
			}
			this.Catalog = array;
			if (this.Catalog.Length <= this.SmallDatabase)
			{
				this.LoadAllValues = true;
			}
			this.InitializeQueues();
		}

		// Token: 0x06006C01 RID: 27649 RVA: 0x00173B10 File Offset: 0x00171D10
		public DataTable LoadColumns(DbConnection connection, SchemaItem schemaItem)
		{
			DataTable dataTable;
			if (!this.columns.TryGetValue(schemaItem, out dataTable))
			{
				SchemaItem[] itemsToLoad = this.GetItemsToLoad(schemaItem, this.GetQueueForSchema(QueueKind.Columns, schemaItem.Schema), this.columns);
				this.Load("Columns", connection, this.GetColumnsQuery(itemsToLoad), itemsToLoad ?? this.Catalog, this.columns);
				this.columns.TryGetValue(schemaItem, out dataTable);
			}
			return dataTable;
		}

		// Token: 0x06006C02 RID: 27650 RVA: 0x00173B80 File Offset: 0x00171D80
		public DataTable LoadForeignKeysParent(DbConnection connection, SchemaItem schemaItem)
		{
			DataTable dataTable;
			if (!this.foreignKeysParent.TryGetValue(schemaItem, out dataTable))
			{
				this.LoadForeignKeys(connection, this.GetItemsToLoad(schemaItem, this.GetQueueForSchema(QueueKind.ForeignKeys, schemaItem.Schema), this.foreignKeysParent));
				this.foreignKeysParent.TryGetValue(schemaItem, out dataTable);
			}
			return dataTable;
		}

		// Token: 0x06006C03 RID: 27651 RVA: 0x00173BD0 File Offset: 0x00171DD0
		public DataTable LoadForeignKeysChild(DbConnection connection, SchemaItem schemaItem)
		{
			DataTable dataTable;
			if (!this.foreignKeysChild.TryGetValue(schemaItem, out dataTable))
			{
				this.LoadForeignKeys(connection, this.GetItemsToLoad(schemaItem, this.GetQueueForSchema(QueueKind.ForeignKeys, schemaItem.Schema), this.foreignKeysChild));
				this.foreignKeysChild.TryGetValue(schemaItem, out dataTable);
			}
			return dataTable;
		}

		// Token: 0x06006C04 RID: 27652 RVA: 0x00173C20 File Offset: 0x00171E20
		public DataTable LoadIndexes(DbConnection connection, SchemaItem schemaItem)
		{
			DataTable dataTable;
			if (!this.indexes.TryGetValue(schemaItem, out dataTable))
			{
				SchemaItem[] itemsToLoad = this.GetItemsToLoad(schemaItem, this.GetQueueForSchema(QueueKind.Indexes, schemaItem.Schema), this.indexes);
				this.Load("Indexes", connection, this.GetIndexesQuery(itemsToLoad), itemsToLoad ?? this.Catalog, this.indexes);
				if (!this.indexes.TryGetValue(schemaItem, out dataTable))
				{
					dataTable = new DataTable
					{
						Locale = CultureInfo.InvariantCulture
					};
					dataTable.Columns.Add("INDEX_NAME", typeof(string));
					dataTable.Columns.Add("COLUMN_NAME", typeof(string));
					dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
					dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
				}
			}
			return dataTable;
		}

		// Token: 0x06006C05 RID: 27653 RVA: 0x00173D10 File Offset: 0x00171F10
		protected void Clear()
		{
			this.columns.Clear();
			this.foreignKeysParent.Clear();
			this.foreignKeysChild.Clear();
			this.indexes.Clear();
			this.ClearQueues();
			this.Catalog = null;
			this.LoadAllValues = false;
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x00173D60 File Offset: 0x00171F60
		private SchemaItem[] GetItemsToLoad(SchemaItem item, BatchSchemaLoader.SchemaItemQueue queue, Dictionary<SchemaItem, DataTable> cache)
		{
			if (this.LoadAllValues)
			{
				return null;
			}
			HashSet<SchemaItem> hashSet = new HashSet<SchemaItem>();
			hashSet.Add(item);
			while (hashSet.Count < this.batchSize && queue.ContainsItems)
			{
				SchemaItem schemaItem = queue.Dequeue();
				if (!cache.ContainsKey(schemaItem))
				{
					hashSet.Add(schemaItem);
				}
			}
			return hashSet.ToArray<SchemaItem>();
		}

		// Token: 0x06006C07 RID: 27655 RVA: 0x00173DBC File Offset: 0x00171FBC
		private void Load(string queryName, DbConnection connection, string query, SchemaItem[] items, Dictionary<SchemaItem, DataTable> cache)
		{
			connection.Open();
			DataTable dataTable = this.environment.LoadData(queryName, connection, query);
			foreach (SchemaItem schemaItem in items)
			{
				DataTable dataTable2 = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataTable2.Columns.Add(dataColumn.ColumnName, dataColumn.DataType);
				}
				cache[schemaItem] = dataTable2;
			}
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj2;
				SchemaItem schemaItem2 = new SchemaItem((string)dataRow[0], (string)dataRow[1], string.Empty);
				DataTable dataTable3;
				if (cache.TryGetValue(schemaItem2, out dataTable3))
				{
					dataTable3.Rows.Add(dataRow.ItemArray);
				}
			}
		}

		// Token: 0x06006C08 RID: 27656 RVA: 0x00173F14 File Offset: 0x00172114
		private void LoadForeignKeys(DbConnection connection, SchemaItem[] items)
		{
			connection.Open();
			DataTable foreignKeysTable = this.GetForeignKeysTable(connection, items);
			items = items ?? this.Catalog;
			Dictionary<SchemaItem, DataTable> dictionary = new Dictionary<SchemaItem, DataTable>(items.Length);
			Dictionary<SchemaItem, DataTable> dictionary2 = new Dictionary<SchemaItem, DataTable>(items.Length);
			foreach (SchemaItem schemaItem in items)
			{
				DataTable dataTable = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				dataTable.Columns.Add("FK_NAME", typeof(string));
				dataTable.Columns.Add("ORDINAL", typeof(long));
				dataTable.Columns.Add("FK_TABLE_SCHEMA", typeof(string));
				dataTable.Columns.Add("FK_TABLE_NAME", typeof(string));
				dataTable.Columns.Add("FK_COLUMN_NAME", typeof(string));
				dataTable.Columns.Add("PK_COLUMN_NAME", typeof(string));
				this.foreignKeysParent[schemaItem] = dataTable;
				dictionary[schemaItem] = dataTable;
				DataTable dataTable2 = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				dataTable2.Columns.Add("FK_NAME", typeof(string));
				dataTable2.Columns.Add("ORDINAL", typeof(long));
				dataTable2.Columns.Add("PK_TABLE_SCHEMA", typeof(string));
				dataTable2.Columns.Add("PK_TABLE_NAME", typeof(string));
				dataTable2.Columns.Add("PK_COLUMN_NAME", typeof(string));
				dataTable2.Columns.Add("FK_COLUMN_NAME", typeof(string));
				this.foreignKeysChild[schemaItem] = dataTable2;
				dictionary2[schemaItem] = dataTable2;
			}
			foreach (object obj in foreignKeysTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				SchemaItem schemaItem2 = new SchemaItem((string)dataRow[5], (string)dataRow[6], string.Empty);
				DataTable dataTable3;
				if (dictionary.TryGetValue(schemaItem2, out dataTable3))
				{
					dataTable3.Rows.Add(new object[]
					{
						dataRow[0],
						dataRow[1],
						dataRow[2],
						dataRow[3],
						dataRow[4],
						dataRow[7]
					});
				}
				else
				{
					this.GetQueueForSchema(QueueKind.Columns, schemaItem2.Schema).Enqueue(schemaItem2);
					this.GetQueueForSchema(QueueKind.ForeignKeys, schemaItem2.Schema).Enqueue(schemaItem2);
					this.GetQueueForSchema(QueueKind.Indexes, schemaItem2.Schema).Enqueue(schemaItem2);
				}
				SchemaItem schemaItem3 = new SchemaItem((string)dataRow[2], (string)dataRow[3], string.Empty);
				if (dictionary2.TryGetValue(schemaItem3, out dataTable3))
				{
					dataTable3.Rows.Add(new object[]
					{
						dataRow[0],
						dataRow[1],
						dataRow[5],
						dataRow[6],
						dataRow[7],
						dataRow[4]
					});
				}
				else
				{
					this.GetQueueForSchema(QueueKind.Columns, schemaItem3.Schema).Enqueue(schemaItem3);
					this.GetQueueForSchema(QueueKind.ForeignKeys, schemaItem3.Schema).Enqueue(schemaItem3);
					this.GetQueueForSchema(QueueKind.Indexes, schemaItem3.Schema).Enqueue(schemaItem3);
				}
			}
		}

		// Token: 0x06006C09 RID: 27657 RVA: 0x00174308 File Offset: 0x00172508
		protected virtual DataTable GetForeignKeysTable(DbConnection connection, SchemaItem[] items)
		{
			return this.environment.LoadData("ForeignKeys", connection, this.GetForeignKeyQuery(items));
		}

		// Token: 0x06006C0A RID: 27658 RVA: 0x00174322 File Offset: 0x00172522
		protected string GenerateClause(string schemaField, string itemField, SchemaItem[] items)
		{
			if (items == null)
			{
				return "1=1";
			}
			if (items.Length == 0)
			{
				return "1=0";
			}
			if (!this.useInClause)
			{
				return this.GenerateClauseWithoutIn(schemaField, itemField, items);
			}
			return this.GenerateClauseWithIn(schemaField, itemField, items);
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x00174354 File Offset: 0x00172554
		protected static Dictionary<string, List<SchemaItem>> GroupSchemaItems(SchemaItem[] catalog)
		{
			Dictionary<string, List<SchemaItem>> dictionary = new Dictionary<string, List<SchemaItem>>();
			foreach (SchemaItem schemaItem in catalog)
			{
				List<SchemaItem> list;
				if (!dictionary.TryGetValue(schemaItem.Schema, out list))
				{
					list = new List<SchemaItem>();
					dictionary.Add(schemaItem.Schema, list);
				}
				list.Add(schemaItem);
			}
			return dictionary;
		}

		// Token: 0x06006C0C RID: 27660 RVA: 0x001743B0 File Offset: 0x001725B0
		private string GenerateClauseWithIn(string schemaField, string itemField, SchemaItem[] items)
		{
			Dictionary<string, List<SchemaItem>> dictionary = BatchSchemaLoader.GroupSchemaItems(items);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			bool flag = true;
			foreach (KeyValuePair<string, List<SchemaItem>> keyValuePair in dictionary)
			{
				if (!flag)
				{
					stringBuilder.Append(global::System.Environment.NewLine + "  or " + global::System.Environment.NewLine);
				}
				if (schemaField != null)
				{
					stringBuilder.Append('(');
					stringBuilder.Append(schemaField);
					stringBuilder.Append(" = ");
					stringBuilder.Append(this.environment.SqlSettings.QuoteNationalStringLiteral(keyValuePair.Key));
					stringBuilder.Append(") and ");
				}
				stringBuilder.Append('(');
				int i = 0;
				while (i < keyValuePair.Value.Count)
				{
					if (i != 0)
					{
						stringBuilder.Append("  or " + global::System.Environment.NewLine);
					}
					stringBuilder.Append(itemField + " in (");
					int num = 0;
					while (num < this.MaxItemsInList && i < keyValuePair.Value.Count)
					{
						if (num != 0)
						{
							stringBuilder.Append(", ");
						}
						stringBuilder.Append(this.environment.SqlSettings.QuoteNationalStringLiteral(keyValuePair.Value[i].Item));
						num++;
						i++;
					}
					stringBuilder.Append(')');
				}
				stringBuilder.Append(')');
				flag = false;
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06006C0D RID: 27661 RVA: 0x00174570 File Offset: 0x00172770
		private string GenerateClauseWithoutIn(string schemaField, string itemField, SchemaItem[] items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			for (int i = 0; i < items.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" or ");
				}
				stringBuilder.Append('(');
				if (schemaField != null)
				{
					stringBuilder.Append(schemaField);
					stringBuilder.Append(" = ");
					stringBuilder.Append(this.environment.SqlSettings.QuoteNationalStringLiteral(items[i].Schema));
					stringBuilder.Append(" and ");
				}
				stringBuilder.Append(itemField);
				stringBuilder.Append(" = ");
				stringBuilder.Append(this.environment.SqlSettings.QuoteNationalStringLiteral(items[i].Item));
				stringBuilder.Append(')');
				stringBuilder.Append(global::System.Environment.NewLine);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x04003C27 RID: 15399
		private const int DefaultBatchSize = 100;

		// Token: 0x04003C28 RID: 15400
		internal static bool TreatAsSmallDatabase;

		// Token: 0x04003C29 RID: 15401
		private readonly int batchSize;

		// Token: 0x04003C2A RID: 15402
		private readonly int smallDatabase;

		// Token: 0x04003C2B RID: 15403
		private readonly bool useInClause;

		// Token: 0x04003C2C RID: 15404
		private readonly DbEnvironment environment;

		// Token: 0x04003C2D RID: 15405
		private readonly Dictionary<SchemaItem, DataTable> columns;

		// Token: 0x04003C2E RID: 15406
		private readonly Dictionary<SchemaItem, DataTable> foreignKeysParent;

		// Token: 0x04003C2F RID: 15407
		private readonly Dictionary<SchemaItem, DataTable> foreignKeysChild;

		// Token: 0x04003C30 RID: 15408
		private readonly Dictionary<SchemaItem, DataTable> indexes;

		// Token: 0x0200102C RID: 4140
		protected class SchemaItemQueue
		{
			// Token: 0x06006C0E RID: 27662 RVA: 0x00174664 File Offset: 0x00172864
			public SchemaItemQueue()
			{
				this.queue = new Queue<SchemaItem>();
				this.allItems = EmptyArray<SchemaItem>.Instance;
				this.mark = 0;
			}

			// Token: 0x17001ECE RID: 7886
			// (get) Token: 0x06006C0F RID: 27663 RVA: 0x00174689 File Offset: 0x00172889
			public bool ContainsItems
			{
				get
				{
					return this.queue.Count > 0 || this.mark < this.allItems.Count;
				}
			}

			// Token: 0x06006C10 RID: 27664 RVA: 0x001746AE File Offset: 0x001728AE
			public void SetAllItems(IList<SchemaItem> allItems)
			{
				this.allItems = allItems.ToArray<SchemaItem>();
			}

			// Token: 0x06006C11 RID: 27665 RVA: 0x001746BC File Offset: 0x001728BC
			public void Enqueue(SchemaItem item)
			{
				this.queue.Enqueue(item);
			}

			// Token: 0x06006C12 RID: 27666 RVA: 0x001746CC File Offset: 0x001728CC
			public SchemaItem Dequeue()
			{
				if (this.queue.Count > 0)
				{
					return this.queue.Dequeue();
				}
				IList<SchemaItem> list = this.allItems;
				int num = this.mark;
				this.mark = num + 1;
				return list[num];
			}

			// Token: 0x06006C13 RID: 27667 RVA: 0x0017470F File Offset: 0x0017290F
			public void Clear()
			{
				this.queue.Clear();
				this.allItems = EmptyArray<SchemaItem>.Instance;
				this.mark = 0;
			}

			// Token: 0x04003C33 RID: 15411
			private readonly Queue<SchemaItem> queue;

			// Token: 0x04003C34 RID: 15412
			private IList<SchemaItem> allItems;

			// Token: 0x04003C35 RID: 15413
			private int mark;
		}
	}
}

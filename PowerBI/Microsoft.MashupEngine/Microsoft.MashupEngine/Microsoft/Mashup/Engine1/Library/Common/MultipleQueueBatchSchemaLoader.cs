using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010DD RID: 4317
	internal abstract class MultipleQueueBatchSchemaLoader : BatchSchemaLoader
	{
		// Token: 0x060070F3 RID: 28915 RVA: 0x00183A12 File Offset: 0x00181C12
		protected MultipleQueueBatchSchemaLoader(DbEnvironment environment, bool useInClause, bool usePrefetch)
			: base(environment, useInClause, usePrefetch)
		{
			this.queueCollections = new Dictionary<QueueKind, Dictionary<string, BatchSchemaLoader.SchemaItemQueue>>
			{
				{
					QueueKind.Columns,
					new Dictionary<string, BatchSchemaLoader.SchemaItemQueue>()
				},
				{
					QueueKind.ForeignKeys,
					new Dictionary<string, BatchSchemaLoader.SchemaItemQueue>()
				},
				{
					QueueKind.Indexes,
					new Dictionary<string, BatchSchemaLoader.SchemaItemQueue>()
				}
			};
		}

		// Token: 0x060070F4 RID: 28916 RVA: 0x00183A4C File Offset: 0x00181C4C
		protected override void InitializeQueues()
		{
			Dictionary<string, List<SchemaItem>> dictionary = BatchSchemaLoader.GroupSchemaItems(base.Catalog);
			foreach (string text in dictionary.Keys)
			{
				foreach (Dictionary<string, BatchSchemaLoader.SchemaItemQueue> dictionary2 in this.queueCollections.Values)
				{
					dictionary2.Add(text, new BatchSchemaLoader.SchemaItemQueue());
				}
			}
			if (!base.LoadAllValues)
			{
				foreach (KeyValuePair<string, List<SchemaItem>> keyValuePair in dictionary)
				{
					foreach (Dictionary<string, BatchSchemaLoader.SchemaItemQueue> dictionary3 in this.queueCollections.Values)
					{
						dictionary3[keyValuePair.Key].SetAllItems(keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x060070F5 RID: 28917 RVA: 0x00183B88 File Offset: 0x00181D88
		protected override BatchSchemaLoader.SchemaItemQueue GetQueueForSchema(QueueKind queueKind, string schemaName)
		{
			Dictionary<string, BatchSchemaLoader.SchemaItemQueue> dictionary = this.queueCollections[queueKind];
			BatchSchemaLoader.SchemaItemQueue schemaItemQueue;
			if (dictionary.TryGetValue(schemaName, out schemaItemQueue))
			{
				return schemaItemQueue;
			}
			schemaItemQueue = new BatchSchemaLoader.SchemaItemQueue();
			dictionary.Add(schemaName, schemaItemQueue);
			return schemaItemQueue;
		}

		// Token: 0x060070F6 RID: 28918 RVA: 0x00183BC0 File Offset: 0x00181DC0
		protected override void ClearQueues()
		{
			foreach (Dictionary<string, BatchSchemaLoader.SchemaItemQueue> dictionary in this.queueCollections.Values)
			{
				dictionary.Clear();
			}
		}

		// Token: 0x04003E33 RID: 15923
		private readonly Dictionary<QueueKind, Dictionary<string, BatchSchemaLoader.SchemaItemQueue>> queueCollections;
	}
}

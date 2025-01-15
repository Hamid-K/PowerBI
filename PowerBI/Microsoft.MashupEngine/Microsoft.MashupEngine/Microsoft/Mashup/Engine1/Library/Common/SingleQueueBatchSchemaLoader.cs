using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001127 RID: 4391
	internal abstract class SingleQueueBatchSchemaLoader : BatchSchemaLoader
	{
		// Token: 0x060072C7 RID: 29383 RVA: 0x0018A612 File Offset: 0x00188812
		protected SingleQueueBatchSchemaLoader(DbEnvironment environment, bool useInClause, bool usePrefetch)
			: base(environment, useInClause, usePrefetch)
		{
			this.queues = new Dictionary<QueueKind, BatchSchemaLoader.SchemaItemQueue>
			{
				{
					QueueKind.Columns,
					new BatchSchemaLoader.SchemaItemQueue()
				},
				{
					QueueKind.ForeignKeys,
					new BatchSchemaLoader.SchemaItemQueue()
				},
				{
					QueueKind.Indexes,
					new BatchSchemaLoader.SchemaItemQueue()
				}
			};
		}

		// Token: 0x060072C8 RID: 29384 RVA: 0x0018A64C File Offset: 0x0018884C
		protected override void InitializeQueues()
		{
			if (!base.LoadAllValues)
			{
				foreach (BatchSchemaLoader.SchemaItemQueue schemaItemQueue in this.queues.Values)
				{
					schemaItemQueue.SetAllItems(base.Catalog);
				}
			}
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x0018A6B0 File Offset: 0x001888B0
		protected override BatchSchemaLoader.SchemaItemQueue GetQueueForSchema(QueueKind queueKind, string schemaName)
		{
			return this.queues[queueKind];
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x0018A6C0 File Offset: 0x001888C0
		protected override void ClearQueues()
		{
			foreach (BatchSchemaLoader.SchemaItemQueue schemaItemQueue in this.queues.Values)
			{
				schemaItemQueue.Clear();
			}
		}

		// Token: 0x04003F45 RID: 16197
		private readonly Dictionary<QueueKind, BatchSchemaLoader.SchemaItemQueue> queues;
	}
}

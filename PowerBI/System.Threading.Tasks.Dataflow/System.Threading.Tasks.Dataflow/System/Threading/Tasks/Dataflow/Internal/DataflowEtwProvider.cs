using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Linq;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x0200003C RID: 60
	[EventSource(Name = "System.Threading.Tasks.Dataflow.DataflowEventSource", Guid = "16F53577-E41D-43D4-B47E-C17025BF4025", LocalizationResources = "FxResources.System.Threading.Tasks.Dataflow.SR")]
	internal sealed class DataflowEtwProvider : EventSource
	{
		// Token: 0x06000219 RID: 537 RVA: 0x00008EB5 File Offset: 0x000070B5
		private DataflowEtwProvider()
		{
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008EBD File Offset: 0x000070BD
		[NonEvent]
		internal void DataflowBlockCreated(IDataflowBlock block, DataflowBlockOptions dataflowBlockOptions)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.DataflowBlockCreated(Common.GetNameForDebugger(block, dataflowBlockOptions), Common.GetBlockId(block));
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008EDD File Offset: 0x000070DD
		[Event(1, Level = EventLevel.Informational)]
		private void DataflowBlockCreated(string blockName, int blockId)
		{
			base.WriteEvent(1, blockName, blockId);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008EE8 File Offset: 0x000070E8
		[NonEvent]
		internal void TaskLaunchedForMessageHandling(IDataflowBlock block, Task task, DataflowEtwProvider.TaskLaunchedReason reason, int availableMessages)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.TaskLaunchedForMessageHandling(Common.GetBlockId(block), reason, availableMessages, task.Id);
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008F0A File Offset: 0x0000710A
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "WriteEvent Parameters are trimmer safe")]
		[Event(2, Level = EventLevel.Informational)]
		private void TaskLaunchedForMessageHandling(int blockId, DataflowEtwProvider.TaskLaunchedReason reason, int availableMessages, int taskId)
		{
			base.WriteEvent(2, new object[] { blockId, reason, availableMessages, taskId });
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008F40 File Offset: 0x00007140
		[NonEvent]
		internal void DataflowBlockCompleted(IDataflowBlock block)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				Task potentiallyNotSupportedCompletionTask = Common.GetPotentiallyNotSupportedCompletionTask(block);
				bool flag = potentiallyNotSupportedCompletionTask != null && potentiallyNotSupportedCompletionTask.IsCompleted;
				if (flag)
				{
					DataflowEtwProvider.BlockCompletionReason status = (DataflowEtwProvider.BlockCompletionReason)potentiallyNotSupportedCompletionTask.Status;
					string text = string.Empty;
					if (potentiallyNotSupportedCompletionTask.IsFaulted)
					{
						try
						{
							text = string.Join(Environment.NewLine, potentiallyNotSupportedCompletionTask.Exception.InnerExceptions.Select((Exception e) => e.ToString()));
						}
						catch
						{
						}
					}
					this.DataflowBlockCompleted(Common.GetBlockId(block), status, text);
				}
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008FE4 File Offset: 0x000071E4
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "WriteEvent Parameters are trimmer safe")]
		[Event(3, Level = EventLevel.Informational)]
		private void DataflowBlockCompleted(int blockId, DataflowEtwProvider.BlockCompletionReason reason, string exceptionData)
		{
			base.WriteEvent(3, new object[] { blockId, reason, exceptionData });
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009009 File Offset: 0x00007209
		[NonEvent]
		internal void DataflowBlockLinking<T>(ISourceBlock<T> source, ITargetBlock<T> target)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.DataflowBlockLinking(Common.GetBlockId(source), Common.GetBlockId(target));
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00009028 File Offset: 0x00007228
		[Event(4, Level = EventLevel.Informational)]
		private void DataflowBlockLinking(int sourceId, int targetId)
		{
			base.WriteEvent(4, sourceId, targetId);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00009033 File Offset: 0x00007233
		[NonEvent]
		internal void DataflowBlockUnlinking<T>(ISourceBlock<T> source, ITargetBlock<T> target)
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				this.DataflowBlockUnlinking(Common.GetBlockId(source), Common.GetBlockId(target));
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009052 File Offset: 0x00007252
		[Event(5, Level = EventLevel.Informational)]
		private void DataflowBlockUnlinking(int sourceId, int targetId)
		{
			base.WriteEvent(5, sourceId, targetId);
		}

		// Token: 0x04000091 RID: 145
		internal static readonly DataflowEtwProvider Log = new DataflowEtwProvider();

		// Token: 0x04000092 RID: 146
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04000093 RID: 147
		private const int DATAFLOWBLOCKCREATED_EVENTID = 1;

		// Token: 0x04000094 RID: 148
		private const int TASKLAUNCHED_EVENTID = 2;

		// Token: 0x04000095 RID: 149
		private const int BLOCKCOMPLETED_EVENTID = 3;

		// Token: 0x04000096 RID: 150
		private const int BLOCKLINKED_EVENTID = 4;

		// Token: 0x04000097 RID: 151
		private const int BLOCKUNLINKED_EVENTID = 5;

		// Token: 0x02000083 RID: 131
		internal enum TaskLaunchedReason
		{
			// Token: 0x040001AE RID: 430
			ProcessingInputMessages = 1,
			// Token: 0x040001AF RID: 431
			OfferingOutputMessages
		}

		// Token: 0x02000084 RID: 132
		internal enum BlockCompletionReason
		{
			// Token: 0x040001B1 RID: 433
			RanToCompletion = 5,
			// Token: 0x040001B2 RID: 434
			Faulted = 7,
			// Token: 0x040001B3 RID: 435
			Canceled = 6
		}
	}
}

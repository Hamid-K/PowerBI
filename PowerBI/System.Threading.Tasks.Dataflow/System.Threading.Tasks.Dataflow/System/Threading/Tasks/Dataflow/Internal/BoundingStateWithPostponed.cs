using System;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000039 RID: 57
	[DebuggerDisplay("BoundedCapacity={BoundedCapacity}, PostponedMessages={PostponedMessagesCountForDebugger}")]
	internal class BoundingStateWithPostponed<TInput> : BoundingState
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00008E8B File Offset: 0x0000708B
		internal BoundingStateWithPostponed(int boundedCapacity)
			: base(boundedCapacity)
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008E9F File Offset: 0x0000709F
		private int PostponedMessagesCountForDebugger
		{
			get
			{
				return this.PostponedMessages.Count;
			}
		}

		// Token: 0x0400008E RID: 142
		internal readonly QueuedMap<ISourceBlock<TInput>, DataflowMessageHeader> PostponedMessages = new QueuedMap<ISourceBlock<TInput>, DataflowMessageHeader>();

		// Token: 0x0400008F RID: 143
		internal int OutstandingTransfers;
	}
}

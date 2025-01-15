using System;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x0200003A RID: 58
	internal sealed class BoundingStateWithPostponedAndTask<TInput> : BoundingStateWithPostponed<TInput>
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00008EAC File Offset: 0x000070AC
		internal BoundingStateWithPostponedAndTask(int boundedCapacity)
			: base(boundedCapacity)
		{
		}

		// Token: 0x04000090 RID: 144
		internal Task TaskForInputProcessing;
	}
}

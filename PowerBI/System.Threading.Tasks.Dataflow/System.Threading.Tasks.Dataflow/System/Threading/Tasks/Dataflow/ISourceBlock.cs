using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	public interface ISourceBlock<[Nullable(2)] out TOutput> : IDataflowBlock
	{
		// Token: 0x060000B1 RID: 177
		IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions);

		// Token: 0x060000B2 RID: 178
		[return: Nullable(2)]
		TOutput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed);

		// Token: 0x060000B3 RID: 179
		bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target);

		// Token: 0x060000B4 RID: 180
		void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target);
	}
}

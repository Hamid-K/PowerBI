using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000025 RID: 37
	public interface ITargetBlock<[Nullable(2)] in TInput> : IDataflowBlock
	{
		// Token: 0x060000B5 RID: 181
		[NullableContext(1)]
		DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, [Nullable(new byte[] { 2, 1 })] ISourceBlock<TInput> source, bool consumeToAccept);
	}
}

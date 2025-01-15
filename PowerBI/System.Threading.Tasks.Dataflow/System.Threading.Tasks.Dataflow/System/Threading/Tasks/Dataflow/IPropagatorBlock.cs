using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000022 RID: 34
	[NullableContext(2)]
	public interface IPropagatorBlock<in TInput, out TOutput> : ITargetBlock<TInput>, IDataflowBlock, ISourceBlock<TOutput>
	{
	}
}

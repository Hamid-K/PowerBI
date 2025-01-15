using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000023 RID: 35
	public interface IReceivableSourceBlock<[Nullable(2)] TOutput> : ISourceBlock<TOutput>, IDataflowBlock
	{
		// Token: 0x060000AF RID: 175
		[NullableContext(1)]
		bool TryReceive([Nullable(new byte[] { 2, 1 })] Predicate<TOutput> filter, [MaybeNullWhen(false)] out TOutput item);

		// Token: 0x060000B0 RID: 176
		bool TryReceiveAll([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IList<TOutput> items);
	}
}

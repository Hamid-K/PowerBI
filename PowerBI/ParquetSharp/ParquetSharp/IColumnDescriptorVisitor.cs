using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200003F RID: 63
	[NullableContext(2)]
	public interface IColumnDescriptorVisitor<out TReturn>
	{
		// Token: 0x060001EB RID: 491
		[return: Nullable(1)]
		TReturn OnColumnDescriptor<[IsUnmanaged, Nullable(0)] TPhysical, TLogical, TElement>() where TPhysical : struct;
	}
}

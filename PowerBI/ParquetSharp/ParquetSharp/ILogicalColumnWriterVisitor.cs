using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000043 RID: 67
	[NullableContext(1)]
	public interface ILogicalColumnWriterVisitor<[Nullable(2)] out TReturn>
	{
		// Token: 0x060001EF RID: 495
		TReturn OnLogicalColumnWriter<[Nullable(2)] TValue>(LogicalColumnWriter<TValue> columnWriter);
	}
}

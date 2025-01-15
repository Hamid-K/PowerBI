using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200005A RID: 90
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LogicalColumnWriter<[Nullable(2)] TElement> : LogicalColumnWriter
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008728 File Offset: 0x00006928
		protected LogicalColumnWriter(ColumnWriter columnWriter, int bufferLength)
			: base(columnWriter, typeof(TElement), bufferLength)
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000873C File Offset: 0x0000693C
		public override TReturn Apply<[Nullable(2)] TReturn>(ILogicalColumnWriterVisitor<TReturn> visitor)
		{
			return visitor.OnLogicalColumnWriter<TElement>(this);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008748 File Offset: 0x00006948
		public void WriteBatch(TElement[] values)
		{
			this.WriteBatch(values.AsSpan<TElement>());
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000875C File Offset: 0x0000695C
		public void WriteBatch(TElement[] values, int start, int length)
		{
			this.WriteBatch(values.AsSpan(start, length));
		}

		// Token: 0x06000251 RID: 593
		public abstract void WriteBatch([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<TElement> values);
	}
}

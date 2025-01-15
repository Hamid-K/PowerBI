using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class LogicalColumnReader<[Nullable(2)] TElement> : LogicalColumnReader, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00008288 File Offset: 0x00006488
		protected LogicalColumnReader(ColumnReader columnReader, int bufferLength)
			: base(columnReader, typeof(TElement), bufferLength)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000829C File Offset: 0x0000649C
		public override TReturn Apply<[Nullable(2)] TReturn>(ILogicalColumnReaderVisitor<TReturn> visitor)
		{
			return visitor.OnLogicalColumnReader<TElement>(this);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000082A8 File Offset: 0x000064A8
		public IEnumerator<TElement> GetEnumerator()
		{
			TElement[] buffer = new TElement[base.BufferLength];
			while (this.HasNext)
			{
				int read = this.ReadBatch(buffer);
				int num;
				for (int i = 0; i != read; i = num)
				{
					yield return buffer[i];
					num = i + 1;
				}
			}
			yield break;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000082B8 File Offset: 0x000064B8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000082C0 File Offset: 0x000064C0
		public TElement[] ReadAll(int rows)
		{
			TElement[] array = new TElement[rows];
			int num = this.ReadBatch(array);
			if (num != rows)
			{
				throw new ArgumentException(string.Format("read {0} rows, expected {1} rows", num, rows));
			}
			return array;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000830C File Offset: 0x0000650C
		public int ReadBatch(TElement[] destination, int start, int length)
		{
			return this.ReadBatch(destination.AsSpan(start, length));
		}

		// Token: 0x0600023D RID: 573
		public abstract int ReadBatch([Nullable(new byte[] { 0, 1 })] Span<TElement> destination);
	}
}

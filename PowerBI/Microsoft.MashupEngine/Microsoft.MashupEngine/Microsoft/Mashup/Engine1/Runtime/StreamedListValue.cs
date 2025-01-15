using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200161B RID: 5659
	public abstract class StreamedListValue : ListValue
	{
		// Token: 0x1700254D RID: 9549
		// (get) Token: 0x06008E21 RID: 36385 RVA: 0x00002105 File Offset: 0x00000305
		public sealed override bool IsBuffered
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700254E RID: 9550
		// (get) Token: 0x06008E22 RID: 36386 RVA: 0x001DB1A0 File Offset: 0x001D93A0
		public override long LargeCount
		{
			get
			{
				long num2;
				using (IEnumerator<IValueReference> enumerator = this.GetEnumerator())
				{
					long num = 0L;
					while (enumerator.MoveNext())
					{
						num += 1L;
						if (num > ListValue.MaxCount)
						{
							throw ValueException.ListCountTooLarge(num);
						}
					}
					num2 = num;
				}
				return num2;
			}
		}

		// Token: 0x1700254F RID: 9551
		// (get) Token: 0x06008E23 RID: 36387 RVA: 0x001DB1F4 File Offset: 0x001D93F4
		public sealed override int Count
		{
			get
			{
				long largeCount = this.LargeCount;
				if (largeCount > 2147483647L)
				{
					throw ValueException.ListCountTooLarge(largeCount);
				}
				return (int)largeCount;
			}
		}

		// Token: 0x17002550 RID: 9552
		public override Value this[int index]
		{
			get
			{
				return this.GetReference(index).Value;
			}
		}

		// Token: 0x06008E25 RID: 36389 RVA: 0x001DB228 File Offset: 0x001D9428
		public override IValueReference GetReference(int index)
		{
			if (index < 0)
			{
				throw ValueException.StructureIndexCannotBeNegative(index, this);
			}
			foreach (IValueReference valueReference in this)
			{
				if (index == 0)
				{
					return valueReference;
				}
				index--;
			}
			throw ValueException.InsufficientElements(this);
		}
	}
}

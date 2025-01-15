using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001044 RID: 4164
	internal class CountTableValue : TableValue
	{
		// Token: 0x06006C9B RID: 27803 RVA: 0x00176183 File Offset: 0x00174383
		public CountTableValue(long count)
		{
			this.count = count;
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x06006C9C RID: 27804 RVA: 0x0000EE09 File Offset: 0x0000D009
		public override TypeValue Type
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x06006C9D RID: 27805 RVA: 0x00176192 File Offset: 0x00174392
		public override long LargeCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x0017619A File Offset: 0x0017439A
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new CountTableValue.CountOnlyEnumerator(this.count);
		}

		// Token: 0x04003C68 RID: 15464
		private readonly long count;

		// Token: 0x02001045 RID: 4165
		private sealed class CountOnlyEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06006C9F RID: 27807 RVA: 0x001761A7 File Offset: 0x001743A7
			public CountOnlyEnumerator(long count)
			{
				this.countRemaining = count;
			}

			// Token: 0x06006CA0 RID: 27808 RVA: 0x001761B6 File Offset: 0x001743B6
			public bool MoveNext()
			{
				if (this.countRemaining > 0L)
				{
					this.countRemaining -= 1L;
					return true;
				}
				return false;
			}

			// Token: 0x17001EE8 RID: 7912
			// (get) Token: 0x06006CA1 RID: 27809 RVA: 0x0000EE09 File Offset: 0x0000D009
			public IValueReference Current
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06006CA2 RID: 27810 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x06006CA3 RID: 27811 RVA: 0x001761D4 File Offset: 0x001743D4
			public void Dispose()
			{
				this.countRemaining = 0L;
			}

			// Token: 0x17001EE9 RID: 7913
			// (get) Token: 0x06006CA4 RID: 27812 RVA: 0x001761DE File Offset: 0x001743DE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x04003C69 RID: 15465
			private long countRemaining;
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200024F RID: 591
	internal struct RecordFieldEnumerator : IEnumerator<NamedValue>, IDisposable, IEnumerator
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x00031EF9 File Offset: 0x000300F9
		public RecordFieldEnumerator(RecordValue record)
		{
			this.record = record;
			this.keys = record.Keys;
			this.count = this.keys.Length;
			this.index = -1;
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x00031F26 File Offset: 0x00030126
		public NamedValue Current
		{
			get
			{
				return new NamedValue(this.keys[this.index], this.record[this.index]);
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00031F4F File Offset: 0x0003014F
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00031F5C File Offset: 0x0003015C
		public bool MoveNext()
		{
			int num = this.index + 1;
			if (num >= this.count)
			{
				return false;
			}
			this.index = num;
			return true;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00031F85 File Offset: 0x00030185
		public void Reset()
		{
			this.index = -1;
		}

		// Token: 0x040006CE RID: 1742
		private readonly RecordValue record;

		// Token: 0x040006CF RID: 1743
		private readonly Keys keys;

		// Token: 0x040006D0 RID: 1744
		private readonly int count;

		// Token: 0x040006D1 RID: 1745
		private int index;
	}
}

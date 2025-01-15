using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200024E RID: 590
	internal struct RecordFieldEnumerable : IEnumerable<NamedValue>, IEnumerable
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x00031ED6 File Offset: 0x000300D6
		public RecordFieldEnumerable(RecordValue record)
		{
			this.record = record;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00031EDF File Offset: 0x000300DF
		public RecordFieldEnumerator GetEnumerator()
		{
			return new RecordFieldEnumerator(this.record);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00031EEC File Offset: 0x000300EC
		IEnumerator<NamedValue> IEnumerable<NamedValue>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x00031EEC File Offset: 0x000300EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040006CD RID: 1741
		private readonly RecordValue record;
	}
}

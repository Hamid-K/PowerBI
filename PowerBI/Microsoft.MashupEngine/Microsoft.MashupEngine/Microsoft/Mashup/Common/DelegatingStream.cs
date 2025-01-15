using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE6 RID: 7142
	public abstract class DelegatingStream : VirtualStream
	{
		// Token: 0x0600B275 RID: 45685 RVA: 0x00245762 File Offset: 0x00243962
		protected DelegatingStream(Stream stream)
		{
			this.stream = stream;
		}

		// Token: 0x17002CD1 RID: 11473
		// (get) Token: 0x0600B276 RID: 45686 RVA: 0x00245771 File Offset: 0x00243971
		protected sealed override Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x04005B36 RID: 23350
		private readonly Stream stream;
	}
}

using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE2 RID: 7138
	public sealed class DelayedStream : VirtualStream
	{
		// Token: 0x0600B222 RID: 45602 RVA: 0x002452BE File Offset: 0x002434BE
		public DelayedStream(Func<Stream> getStream)
		{
			this.getStream = getStream;
		}

		// Token: 0x17002CBD RID: 11453
		// (get) Token: 0x0600B223 RID: 45603 RVA: 0x002452CD File Offset: 0x002434CD
		protected override Stream Stream
		{
			get
			{
				if (this.stream == null)
				{
					this.stream = this.getStream();
				}
				return this.stream;
			}
		}

		// Token: 0x04005B31 RID: 23345
		private readonly Func<Stream> getStream;

		// Token: 0x04005B32 RID: 23346
		private Stream stream;
	}
}

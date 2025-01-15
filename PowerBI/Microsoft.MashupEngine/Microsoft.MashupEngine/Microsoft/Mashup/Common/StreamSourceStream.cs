using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C29 RID: 7209
	public sealed class StreamSourceStream : DelegatingStream
	{
		// Token: 0x0600B406 RID: 46086 RVA: 0x00248AD8 File Offset: 0x00246CD8
		public StreamSourceStream(IStreamSource streamSource)
			: base(streamSource.Stream)
		{
			this.streamSource = streamSource;
		}

		// Token: 0x0600B407 RID: 46087 RVA: 0x00248AF0 File Offset: 0x00246CF0
		public override void Close()
		{
			try
			{
				base.Close();
			}
			finally
			{
				this.streamSource.Dispose();
			}
		}

		// Token: 0x0600B408 RID: 46088 RVA: 0x00248B24 File Offset: 0x00246D24
		protected override void Dispose(bool disposing)
		{
			try
			{
				base.Dispose(disposing);
			}
			finally
			{
				if (disposing)
				{
					this.streamSource.Dispose();
				}
			}
		}

		// Token: 0x04005BA7 RID: 23463
		private readonly IStreamSource streamSource;
	}
}

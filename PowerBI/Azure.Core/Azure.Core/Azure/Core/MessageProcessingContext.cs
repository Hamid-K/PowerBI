using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200004C RID: 76
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MessageProcessingContext
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00006DCA File Offset: 0x00004FCA
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00006DD7 File Offset: 0x00004FD7
		public DateTimeOffset StartTime
		{
			get
			{
				return this._message.ProcessingStartTime;
			}
			internal set
			{
				this._message.ProcessingStartTime = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00006DE5 File Offset: 0x00004FE5
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00006DF2 File Offset: 0x00004FF2
		public int RetryNumber
		{
			get
			{
				return this._message.RetryNumber;
			}
			set
			{
				this._message.RetryNumber = value;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006E00 File Offset: 0x00005000
		internal MessageProcessingContext(HttpMessage message)
		{
			this._message = message;
		}

		// Token: 0x040000FA RID: 250
		private readonly HttpMessage _message;
	}
}

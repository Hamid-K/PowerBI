using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000015 RID: 21
	public class BaseNotification<TOptions> : BaseContext<TOptions>
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000026CB File Offset: 0x000008CB
		protected BaseNotification(IOwinContext context, TOptions options)
			: base(context, options)
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000026D5 File Offset: 0x000008D5
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000026DD File Offset: 0x000008DD
		public NotificationResultState State { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000026E6 File Offset: 0x000008E6
		public bool HandledResponse
		{
			get
			{
				return this.State == NotificationResultState.HandledResponse;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000026F1 File Offset: 0x000008F1
		public bool Skipped
		{
			get
			{
				return this.State == NotificationResultState.Skipped;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000026FC File Offset: 0x000008FC
		public void HandleResponse()
		{
			this.State = NotificationResultState.HandledResponse;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002705 File Offset: 0x00000905
		public void SkipToNextMiddleware()
		{
			this.State = NotificationResultState.Skipped;
		}
	}
}

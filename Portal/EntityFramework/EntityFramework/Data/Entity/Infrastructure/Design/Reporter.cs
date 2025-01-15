using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x020002A2 RID: 674
	internal class Reporter
	{
		// Token: 0x06002172 RID: 8562 RVA: 0x0005DD30 File Offset: 0x0005BF30
		public Reporter(IReportHandler handler)
		{
			this._handler = handler;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0005DD3F File Offset: 0x0005BF3F
		public void WriteError(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnError(message);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0005DD52 File Offset: 0x0005BF52
		public void WriteWarning(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnWarning(message);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0005DD65 File Offset: 0x0005BF65
		public void WriteInformation(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnInformation(message);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0005DD78 File Offset: 0x0005BF78
		public void WriteVerbose(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnVerbose(message);
		}

		// Token: 0x04000B9F RID: 2975
		private readonly IReportHandler _handler;
	}
}

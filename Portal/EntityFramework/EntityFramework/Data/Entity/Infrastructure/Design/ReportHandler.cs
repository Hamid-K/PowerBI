using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x020002A3 RID: 675
	public class ReportHandler : HandlerBase, IReportHandler
	{
		// Token: 0x06002177 RID: 8567 RVA: 0x0005DD8B File Offset: 0x0005BF8B
		public ReportHandler(Action<string> errorHandler, Action<string> warningHandler, Action<string> informationHandler, Action<string> verboseHandler)
		{
			this._errorHandler = errorHandler;
			this._warningHandler = warningHandler;
			this._informationHandler = informationHandler;
			this._verboseHandler = verboseHandler;
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0005DDB0 File Offset: 0x0005BFB0
		public virtual void OnError(string message)
		{
			Action<string> errorHandler = this._errorHandler;
			if (errorHandler == null)
			{
				return;
			}
			errorHandler(message);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0005DDC3 File Offset: 0x0005BFC3
		public virtual void OnWarning(string message)
		{
			Action<string> warningHandler = this._warningHandler;
			if (warningHandler == null)
			{
				return;
			}
			warningHandler(message);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0005DDD6 File Offset: 0x0005BFD6
		public virtual void OnInformation(string message)
		{
			Action<string> informationHandler = this._informationHandler;
			if (informationHandler == null)
			{
				return;
			}
			informationHandler(message);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0005DDE9 File Offset: 0x0005BFE9
		public virtual void OnVerbose(string message)
		{
			Action<string> verboseHandler = this._verboseHandler;
			if (verboseHandler == null)
			{
				return;
			}
			verboseHandler(message);
		}

		// Token: 0x04000BA0 RID: 2976
		private readonly Action<string> _errorHandler;

		// Token: 0x04000BA1 RID: 2977
		private readonly Action<string> _warningHandler;

		// Token: 0x04000BA2 RID: 2978
		private readonly Action<string> _informationHandler;

		// Token: 0x04000BA3 RID: 2979
		private readonly Action<string> _verboseHandler;
	}
}

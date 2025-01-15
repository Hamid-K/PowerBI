using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x020002A5 RID: 677
	internal class WrappedReportHandler : IReportHandler
	{
		// Token: 0x06002184 RID: 8580 RVA: 0x0005DE54 File Offset: 0x0005C054
		public WrappedReportHandler(object handler)
		{
			if (handler != null)
			{
				HandlerBase handlerBase = (handler as HandlerBase) ?? new ForwardingProxy<HandlerBase>(handler).GetTransparentProxy();
				this._handler = (handler as IReportHandler) ?? (handlerBase.ImplementsContract(typeof(IReportHandler).FullName) ? new ForwardingProxy<IReportHandler>(handler).GetTransparentProxy() : null);
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0005DEB5 File Offset: 0x0005C0B5
		public void OnError(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnError(message);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0005DEC8 File Offset: 0x0005C0C8
		public void OnInformation(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnInformation(message);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0005DEDB File Offset: 0x0005C0DB
		public void OnVerbose(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnVerbose(message);
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0005DEEE File Offset: 0x0005C0EE
		public void OnWarning(string message)
		{
			IReportHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnWarning(message);
		}

		// Token: 0x04000BA9 RID: 2985
		private readonly IReportHandler _handler;
	}
}

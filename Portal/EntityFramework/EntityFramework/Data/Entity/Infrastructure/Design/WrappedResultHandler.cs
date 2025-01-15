using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x020002A6 RID: 678
	internal class WrappedResultHandler : IResultHandler
	{
		// Token: 0x06002189 RID: 8585 RVA: 0x0005DF04 File Offset: 0x0005C104
		public WrappedResultHandler(object handler)
		{
			HandlerBase handlerBase = (handler as HandlerBase) ?? new ForwardingProxy<HandlerBase>(handler).GetTransparentProxy();
			this._handler = (handler as IResultHandler) ?? (handlerBase.ImplementsContract(typeof(IResultHandler).FullName) ? new ForwardingProxy<IResultHandler>(handler).GetTransparentProxy() : null);
			this._handler2 = (handler as IResultHandler2) ?? (handlerBase.ImplementsContract(typeof(IResultHandler2).FullName) ? new ForwardingProxy<IResultHandler2>(handler).GetTransparentProxy() : null);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0005DF97 File Offset: 0x0005C197
		public void SetResult(object value)
		{
			if (this._handler != null)
			{
				this._handler.SetResult(value);
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0005DFAD File Offset: 0x0005C1AD
		public bool SetError(string type, string message, string stackTrace)
		{
			if (this._handler2 == null)
			{
				return false;
			}
			this._handler2.SetError(type, message, stackTrace);
			return true;
		}

		// Token: 0x04000BAA RID: 2986
		private readonly IResultHandler _handler;

		// Token: 0x04000BAB RID: 2987
		private readonly IResultHandler2 _handler2;
	}
}

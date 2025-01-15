using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000019 RID: 25
	internal sealed class SessionHandle : IDisposable
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005C31 File Offset: 0x00003E31
		public SessionHandle(string session, IEvaluationEventHandler handlers)
		{
			this.session = session;
			this.handlers = handlers;
			SessionHandle.sessionHandles.RegisterHandle(session, this);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005C54 File Offset: 0x00003E54
		~SessionHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005C84 File Offset: 0x00003E84
		public static IEnumerable<IEvaluationEventHandler> GetEvaluationHandlers(ConnectionContext context)
		{
			List<SessionHandle> list;
			if (context.Session == null || !SessionHandle.sessionHandles.TryGetValues(context.Session, out list))
			{
				return EmptyArray<IEvaluationEventHandler>.Instance;
			}
			return list.Select((SessionHandle handle) => handle.handlers);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005CD8 File Offset: 0x00003ED8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005CE1 File Offset: 0x00003EE1
		private void Dispose(bool disposing)
		{
			SessionHandle.sessionHandles.UnregisterHandle(this.session, this);
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000098 RID: 152
		private static readonly HandleManager<SessionHandle> sessionHandles = new HandleManager<SessionHandle>();

		// Token: 0x04000099 RID: 153
		private readonly string session;

		// Token: 0x0400009A RID: 154
		private readonly IEvaluationEventHandler handlers;
	}
}

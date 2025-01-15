using System;
using System.Collections.Concurrent;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x02000012 RID: 18
	public interface ISapBwProvider
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C9 RID: 201
		ConcurrentDictionary<string, IRfcStructure> Structures { get; }

		// Token: 0x060000CA RID: 202
		IFileTracer GetFileTracerOrNull(string driverTracePath);

		// Token: 0x060000CB RID: 203
		IDestination GetDestination(string destinationName, RfcConfigParameters parameters, Func<string, IDisposable> impersonationWrapper);

		// Token: 0x060000CC RID: 204
		IRfcFunction CreateFunction(string functionName, IDestination destination);

		// Token: 0x060000CD RID: 205
		void RemoveDestination(string destinationName);
	}
}

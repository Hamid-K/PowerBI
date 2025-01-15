using System;
using System.IO;
using System.Net.Sockets;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200080F RID: 2063
	public interface IConnection
	{
		// Token: 0x0600412E RID: 16686
		void Close();

		// Token: 0x0600412F RID: 16687
		void Close(bool disconnect);

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06004130 RID: 16688
		Stream InputStream { get; }

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06004131 RID: 16689
		Stream OutputStream { get; }

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06004132 RID: 16690
		Socket Socket { get; }
	}
}

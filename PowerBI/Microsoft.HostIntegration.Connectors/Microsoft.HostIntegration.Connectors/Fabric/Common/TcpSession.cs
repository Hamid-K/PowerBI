using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000452 RID: 1106
	internal class TcpSession : TcpOutputSession, ISession, ITransportConnection, ITransportObject
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x00076B7D File Offset: 0x00074D7D
		public TcpSession(Uri remoteAddress, TcpTransportFactory factory)
			: base(remoteAddress, factory)
		{
		}
	}
}

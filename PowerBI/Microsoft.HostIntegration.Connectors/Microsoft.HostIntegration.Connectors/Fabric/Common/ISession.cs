using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200044C RID: 1100
	internal interface ISession : ITransportConnection, ITransportObject
	{
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002682 RID: 9858
		string Id { get; }

		// Token: 0x06002683 RID: 9859
		void ForceClosure();
	}
}

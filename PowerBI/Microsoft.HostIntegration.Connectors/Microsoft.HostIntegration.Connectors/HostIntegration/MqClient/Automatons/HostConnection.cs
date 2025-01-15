using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AEF RID: 2799
	internal class HostConnection
	{
		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x060058A3 RID: 22691 RVA: 0x0016CB97 File Offset: 0x0016AD97
		// (set) Token: 0x060058A4 RID: 22692 RVA: 0x0016CB9F File Offset: 0x0016AD9F
		public string Host { get; private set; }

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060058A5 RID: 22693 RVA: 0x0016CBA8 File Offset: 0x0016ADA8
		// (set) Token: 0x060058A6 RID: 22694 RVA: 0x0016CBB0 File Offset: 0x0016ADB0
		public Dictionary<int, PortConnection> PortToPortConnections { get; private set; }

		// Token: 0x060058A7 RID: 22695 RVA: 0x0016CBB9 File Offset: 0x0016ADB9
		public HostConnection(string host)
		{
			this.Host = host;
			this.PortToPortConnections = new Dictionary<int, PortConnection>();
		}
	}
}

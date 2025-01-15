using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002D3 RID: 723
	internal class RecvMessageState
	{
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00051777 File Offset: 0x0004F977
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x0005177F File Offset: 0x0004F97F
		public IOState IOState { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x00051788 File Offset: 0x0004F988
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x00051790 File Offset: 0x0004F990
		public IList<IVelocityPacket> Packets { get; private set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x00051799 File Offset: 0x0004F999
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x000517A1 File Offset: 0x0004F9A1
		public IEnumerator<ArraySegment<byte>> BufferEnumerator { get; private set; }

		// Token: 0x06001ADD RID: 6877 RVA: 0x000517AA File Offset: 0x0004F9AA
		public RecvMessageState(IEnumerator<ArraySegment<byte>> enumerator, IList<IVelocityPacket> packets)
		{
			this.Packets = packets;
			this.BufferEnumerator = enumerator;
		}
	}
}

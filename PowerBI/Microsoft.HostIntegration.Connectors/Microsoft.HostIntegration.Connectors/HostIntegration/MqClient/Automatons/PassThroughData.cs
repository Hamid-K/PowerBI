using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.Automaton;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000AF2 RID: 2802
	internal class PassThroughData : DeterminantMessage
	{
		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x060058B4 RID: 22708 RVA: 0x0016CC49 File Offset: 0x0016AE49
		// (set) Token: 0x060058B5 RID: 22709 RVA: 0x0016CC51 File Offset: 0x0016AE51
		public List<DynamicDataBuffer> Buffers { get; set; }

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0016CC5A File Offset: 0x0016AE5A
		// (set) Token: 0x060058B7 RID: 22711 RVA: 0x0016CC62 File Offset: 0x0016AE62
		public SegmentHeaderInformation SegmentHeaderInformation { get; set; }

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x060058B8 RID: 22712 RVA: 0x0016CC6B File Offset: 0x0016AE6B
		// (set) Token: 0x060058B9 RID: 22713 RVA: 0x0016CC73 File Offset: 0x0016AE73
		public ApiHeaderInformation ApiHeaderInformation { get; set; }

		// Token: 0x060058BA RID: 22714 RVA: 0x0016CC7C File Offset: 0x0016AE7C
		public void ReturnBuffers()
		{
			if (this.Buffers != null)
			{
				foreach (DynamicDataBuffer dynamicDataBuffer in this.Buffers)
				{
					dynamicDataBuffer.ReturnToOwner();
				}
				this.Buffers.Clear();
			}
		}
	}
}

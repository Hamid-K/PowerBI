using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000490 RID: 1168
	public class CommunicationFrameworkTrace : TraceSourceBase<CommunicationFrameworkTrace>
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x0008145B File Offset: 0x0007F65B
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Communication");
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}

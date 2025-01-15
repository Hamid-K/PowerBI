using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x0200046B RID: 1131
	public class PlatformHttpListenerTrace : TraceSourceBase<PlatformHttpListenerTrace>
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0007F0B9 File Offset: 0x0007D2B9
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.HttpListener");
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}

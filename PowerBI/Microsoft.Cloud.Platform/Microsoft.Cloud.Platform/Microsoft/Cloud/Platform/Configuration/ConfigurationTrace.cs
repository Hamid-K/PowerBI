using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000430 RID: 1072
	public class ConfigurationTrace : TraceSourceBase<ConfigurationTrace>
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x0007C5C9 File Offset: 0x0007A7C9
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Configuration");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}

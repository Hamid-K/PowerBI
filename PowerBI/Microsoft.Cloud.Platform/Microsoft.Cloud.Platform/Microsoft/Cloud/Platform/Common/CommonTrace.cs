using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000510 RID: 1296
	public class CommonTrace : TraceSourceBase<CommonTrace>
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x000919D0 File Offset: 0x0008FBD0
		public override TraceSourceIdentifier ID
		{
			get
			{
				return new TraceSourceIdentifier("P.Common");
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x000034D8 File Offset: 0x000016D8
		public override TraceVerbosity DefaultVerbosity
		{
			get
			{
				return TraceVerbosity.Info;
			}
		}
	}
}

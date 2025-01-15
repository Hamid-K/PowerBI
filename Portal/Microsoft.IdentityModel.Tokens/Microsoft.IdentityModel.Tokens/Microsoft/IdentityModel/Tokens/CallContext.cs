using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000123 RID: 291
	public class CallContext : LoggerContext
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x00039974 File Offset: 0x00037B74
		public CallContext()
		{
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003997C File Offset: 0x00037B7C
		public CallContext(Guid activityId)
			: base(activityId)
		{
		}
	}
}

using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F6 RID: 1270
	public class VoidRetryPolicy : IRetryPolicy
	{
		// Token: 0x0600268F RID: 9871 RVA: 0x00005EB7 File Offset: 0x000040B7
		public object CreateInitialState()
		{
			return null;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0000E568 File Offset: 0x0000C768
		public virtual bool ShouldRetryToTheSameEndpoint(EndpointFault exceptionInformation, object state)
		{
			return false;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x0000E568 File Offset: 0x0000C768
		public virtual bool ShouldRetryToDifferentEndpoint(EndpointFault exceptionInformation, object state)
		{
			return false;
		}
	}
}

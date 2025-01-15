using System;

namespace Microsoft.BIServer.HostingEnvironment.Exceptions
{
	// Token: 0x02000032 RID: 50
	public sealed class TrustedProcessTokenExpiredException : HostingEnvironmentException
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005156 File Offset: 0x00003356
		public TrustedProcessTokenExpiredException(string message, params object[] parameters)
			: base(string.Format(message, parameters), Array.Empty<object>())
		{
		}
	}
}

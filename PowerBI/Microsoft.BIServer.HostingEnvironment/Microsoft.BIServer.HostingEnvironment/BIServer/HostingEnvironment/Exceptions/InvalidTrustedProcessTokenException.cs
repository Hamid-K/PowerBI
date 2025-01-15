using System;

namespace Microsoft.BIServer.HostingEnvironment.Exceptions
{
	// Token: 0x02000031 RID: 49
	public sealed class InvalidTrustedProcessTokenException : HostingEnvironmentException
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005119 File Offset: 0x00003319
		public InvalidTrustedProcessTokenException(string message, params object[] parameters)
			: base(string.Format("Invalid Trusted Process Token!  " + message, parameters), Array.Empty<object>())
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005137 File Offset: 0x00003337
		public InvalidTrustedProcessTokenException(Exception ex, string message, params object[] parameters)
			: base(ex, string.Format("Invalid Trusted Process Token!  " + message, parameters), Array.Empty<object>())
		{
		}
	}
}

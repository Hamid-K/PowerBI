using System;

namespace Microsoft.BIServer.Owin.Common.Exceptions
{
	// Token: 0x02000024 RID: 36
	public sealed class AuthenticationExtensionException : Exception
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0000412E File Offset: 0x0000232E
		public AuthenticationExtensionException(string method, Exception innerException)
			: base(method, innerException)
		{
		}
	}
}

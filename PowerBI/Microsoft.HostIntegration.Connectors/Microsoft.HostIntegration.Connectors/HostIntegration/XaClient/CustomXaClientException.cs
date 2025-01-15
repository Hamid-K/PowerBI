using System;

namespace Microsoft.HostIntegration.XaClient
{
	// Token: 0x020006FE RID: 1790
	public class CustomXaClientException : ApplicationException
	{
		// Token: 0x060038DF RID: 14559 RVA: 0x000BE99A File Offset: 0x000BCB9A
		public CustomXaClientException(string message)
			: base(message)
		{
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000BE9A3 File Offset: 0x000BCBA3
		public CustomXaClientException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}

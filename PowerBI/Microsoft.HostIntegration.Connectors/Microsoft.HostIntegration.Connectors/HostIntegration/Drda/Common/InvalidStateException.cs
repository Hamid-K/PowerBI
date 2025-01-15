using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200081F RID: 2079
	public class InvalidStateException : Exception
	{
		// Token: 0x060041C2 RID: 16834 RVA: 0x0001E12D File Offset: 0x0001C32D
		public InvalidStateException()
		{
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x0001E135 File Offset: 0x0001C335
		public InvalidStateException(string msg)
			: base(msg)
		{
		}
	}
}

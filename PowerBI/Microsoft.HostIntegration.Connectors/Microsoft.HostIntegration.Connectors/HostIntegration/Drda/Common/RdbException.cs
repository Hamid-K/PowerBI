using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000830 RID: 2096
	public class RdbException : Exception
	{
		// Token: 0x060042D1 RID: 17105 RVA: 0x0001E12D File Offset: 0x0001C32D
		public RdbException()
		{
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x0001E135 File Offset: 0x0001C335
		public RdbException(string message)
			: base(message)
		{
		}
	}
}

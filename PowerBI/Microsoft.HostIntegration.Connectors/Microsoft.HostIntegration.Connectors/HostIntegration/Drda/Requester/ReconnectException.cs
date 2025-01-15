using System;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200095E RID: 2398
	internal class ReconnectException : Exception
	{
		// Token: 0x06004AD6 RID: 19158 RVA: 0x0011E09E File Offset: 0x0011C29E
		public ReconnectException(EncryptionAlgorithm algorithm, SecurityMechanism secmec)
		{
			this.Algorithm = algorithm;
			this.Secmec = secmec;
		}

		// Token: 0x04003994 RID: 14740
		public readonly EncryptionAlgorithm Algorithm;

		// Token: 0x04003995 RID: 14741
		public readonly SecurityMechanism Secmec;
	}
}

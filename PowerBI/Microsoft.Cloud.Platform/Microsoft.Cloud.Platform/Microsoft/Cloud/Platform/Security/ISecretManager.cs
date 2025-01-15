using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x0200005F RID: 95
	public interface ISecretManager
	{
		// Token: 0x060002A6 RID: 678
		void Subscribe(IEnumerable<string> key, SecretManagerEventHandler registeredEventHandler);

		// Token: 0x060002A7 RID: 679
		void Unsubscribe(SecretManagerEventHandler registeredEventHandler);

		// Token: 0x060002A8 RID: 680
		IEnumerable<string> GetAvailableCertificateKeys();
	}
}

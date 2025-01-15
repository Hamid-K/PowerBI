using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200011D RID: 285
	public interface IObfuscation : IDisposable
	{
		// Token: 0x060007A0 RID: 1952
		int GetKeyIdentifier();

		// Token: 0x060007A1 RID: 1953
		string ComputePiiReplacement(byte[] piiData);

		// Token: 0x060007A2 RID: 1954
		string DecryptObfuscatedData(string obfuscatedData);
	}
}

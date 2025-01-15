using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200011F RID: 287
	public abstract class OneWayObfuscation : IObfuscation, IDisposable
	{
		// Token: 0x060007AE RID: 1966
		public abstract int GetKeyIdentifier();

		// Token: 0x060007AF RID: 1967 RVA: 0x0001AC20 File Offset: 0x00018E20
		public virtual string ComputePiiReplacement(byte[] piiData)
		{
			byte[] array = this.ComputeHash(piiData);
			return this.SerializeByteArray(array);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public string DecryptObfuscatedData(string obfuscatedData)
		{
			throw new NotSupportedException("One-way obfuscation does not support the decryption operation.");
		}

		// Token: 0x060007B1 RID: 1969
		public abstract void Dispose();

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001AC48 File Offset: 0x00018E48
		protected virtual string SerializeByteArray(byte[] bytes)
		{
			return ObfuscationUtility.SerializeByteArray(bytes);
		}

		// Token: 0x060007B3 RID: 1971
		protected abstract byte[] ComputeHash(byte[] inputData);
	}
}

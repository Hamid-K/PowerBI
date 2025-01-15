using System;
using System.Security.Cryptography;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000E1 RID: 225
	internal abstract class SqlColumnEncryptionEnclaveProvider
	{
		// Token: 0x06000FA9 RID: 4009
		internal abstract void GetEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, bool generateCustomData, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out long counter, out byte[] customData, out int customDataLength);

		// Token: 0x06000FAA RID: 4010
		internal abstract SqlEnclaveAttestationParameters GetAttestationParameters(string attestationUrl, byte[] customData, int customDataLength);

		// Token: 0x06000FAB RID: 4011
		internal abstract void CreateEnclaveSession(byte[] enclaveAttestationInfo, ECDiffieHellman clientDiffieHellmanKey, EnclaveSessionParameters enclaveSessionParameters, byte[] customData, int customDataLength, out SqlEnclaveSession sqlEnclaveSession, out long counter);

		// Token: 0x06000FAC RID: 4012
		internal abstract void InvalidateEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSession);
	}
}

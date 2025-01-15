using System;
using System.Security.Cryptography;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200002D RID: 45
	internal class NoneAttestationEnclaveProvider : EnclaveProviderBase
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0000E34C File Offset: 0x0000C54C
		internal override void GetEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, bool generateCustomData, bool isRetry, out SqlEnclaveSession sqlEnclaveSession, out long counter, out byte[] customData, out int customDataLength)
		{
			base.GetEnclaveSessionHelper(enclaveSessionParameters, false, isRetry, out sqlEnclaveSession, out counter, out customData, out customDataLength);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000E360 File Offset: 0x0000C560
		internal override SqlEnclaveAttestationParameters GetAttestationParameters(string attestationUrl, byte[] customData, int customDataLength)
		{
			ECDiffieHellman ecdiffieHellman = KeyConverter.CreateECDiffieHellman(384);
			return new SqlEnclaveAttestationParameters(2, Array.Empty<byte>(), ecdiffieHellman);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000E384 File Offset: 0x0000C584
		internal override void CreateEnclaveSession(byte[] attestationInfo, ECDiffieHellman clientDHKey, EnclaveSessionParameters enclaveSessionParameters, byte[] customData, int customDataLength, out SqlEnclaveSession sqlEnclaveSession, out long counter)
		{
			sqlEnclaveSession = null;
			counter = 0L;
			try
			{
				EnclaveProviderBase.ThreadRetryCache.Remove(Thread.CurrentThread.ManagedThreadId.ToString(), null);
				sqlEnclaveSession = base.GetEnclaveSessionFromCache(enclaveSessionParameters, out counter);
				if (sqlEnclaveSession == null)
				{
					int num = 0;
					uint num2 = BitConverter.ToUInt32(attestationInfo, num);
					num += 4;
					int num3 = checked((int)num2);
					uint num4 = BitConverter.ToUInt32(attestationInfo, num);
					num += 4;
					byte[] array = new byte[NoneAttestationEnclaveProvider.EnclaveSessionHandleSize];
					Buffer.BlockCopy(attestationInfo, num, array, 0, NoneAttestationEnclaveProvider.EnclaveSessionHandleSize);
					num += NoneAttestationEnclaveProvider.EnclaveSessionHandleSize;
					uint num5 = BitConverter.ToUInt32(attestationInfo, num);
					num += 4;
					uint num6 = BitConverter.ToUInt32(attestationInfo, num);
					num += 4;
					int num7 = checked((int)num5);
					byte[] array2 = new byte[num5];
					Buffer.BlockCopy(attestationInfo, num, array2, 0, num7);
					num += num7;
					byte[] array3 = new byte[num6];
					Buffer.BlockCopy(attestationInfo, num, array3, 0, checked((int)num6));
					using (ECDiffieHellman ecdiffieHellman = KeyConverter.CreateECDiffieHellmanFromPublicKeyBlob(array2))
					{
						byte[] array4 = KeyConverter.DeriveKey(clientDHKey, ecdiffieHellman.PublicKey);
						long num8 = BitConverter.ToInt64(array, 0);
						sqlEnclaveSession = base.AddEnclaveSessionToCache(enclaveSessionParameters, array4, num8, out counter);
						if (sqlEnclaveSession == null)
						{
							throw SQL.AttestationFailed(Strings.FailToCreateEnclaveSession, null);
						}
					}
				}
			}
			finally
			{
				base.UpdateEnclaveSessionLockStatus(sqlEnclaveSession);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000CE20 File Offset: 0x0000B020
		internal override void InvalidateEnclaveSession(EnclaveSessionParameters enclaveSessionParameters, SqlEnclaveSession enclaveSessionToInvalidate)
		{
			base.InvalidateEnclaveSessionHelper(enclaveSessionParameters, enclaveSessionToInvalidate);
		}

		// Token: 0x040000A7 RID: 167
		private static readonly int EnclaveSessionHandleSize = 8;

		// Token: 0x040000A8 RID: 168
		private const int DiffieHellmanKeySize = 384;

		// Token: 0x040000A9 RID: 169
		private const int NoneAttestationProtocolId = 2;
	}
}

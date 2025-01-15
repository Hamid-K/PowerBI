using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000BE RID: 190
	internal class HostGuardianServiceEnclaveProvider : VirtualizationBasedSecurityEnclaveProviderBase
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0002C976 File Offset: 0x0002AB76
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0002C97E File Offset: 0x0002AB7E
		public uint MaxNumRetries { get; set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0002C987 File Offset: 0x0002AB87
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0002C98F File Offset: 0x0002AB8F
		public int EnclaveRetrySleepInSeconds
		{
			get
			{
				return this.enclaveRetrySleepInSeconds;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentException(Strings.EnclaveRetrySleepInSecondsValueException);
				}
				this.enclaveRetrySleepInSeconds = value;
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002C9A7 File Offset: 0x0002ABA7
		protected override string GetAttestationUrl(string attestationUrl)
		{
			return attestationUrl.TrimEnd(new char[] { '/' }) + "/v2.0/signingCertificates";
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002C9C4 File Offset: 0x0002ABC4
		protected override byte[] MakeRequest(string url)
		{
			Exception ex = null;
			int num = 0;
			while ((long)num < (long)((ulong)(this.MaxNumRetries + 1U)))
			{
				try
				{
					if (num != 0)
					{
						Thread.Sleep(this.EnclaveRetrySleepInSeconds * 1000);
					}
					using (Stream result = HostGuardianServiceEnclaveProvider.s_client.GetStreamAsync(url).ConfigureAwait(false).GetAwaiter()
						.GetResult())
					{
						DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(byte[]));
						return (byte[])dataContractJsonSerializer.ReadObject(result);
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				num++;
			}
			throw SQL.AttestationFailed(string.Format(Strings.GetAttestationSigningCertificateRequestFailedFormat, url, ex.Message), ex);
		}

		// Token: 0x040005F0 RID: 1520
		private static readonly HttpClient s_client = new HttpClient();

		// Token: 0x040005F1 RID: 1521
		private const string AttestationUrlSuffix = "/v2.0/signingCertificates";

		// Token: 0x040005F3 RID: 1523
		private int enclaveRetrySleepInSeconds = 3;
	}
}

using System;
using Microsoft.BIServer.HostingEnvironment.Cryptography;
using Microsoft.ReportingServices.Portal.Interfaces;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x0200006C RID: 108
	public sealed class LegacyCrypto : Crypto
	{
		// Token: 0x0600032F RID: 815 RVA: 0x000151CA File Offset: 0x000133CA
		public LegacyCrypto(IEncryptionService encryptionService)
		{
			if (encryptionService == null)
			{
				throw new ArgumentNullException("service");
			}
			this._service = encryptionService;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000151E7 File Offset: 0x000133E7
		public override string GetName()
		{
			return "MachineKeyCrypto";
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000151EE File Offset: 0x000133EE
		protected override byte[] EncryptInternal(byte[] data)
		{
			return this._service.Encrypt(data);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000151FC File Offset: 0x000133FC
		protected override byte[] DecryptInternal(byte[] data)
		{
			return this._service.Decrypt(data);
		}

		// Token: 0x040000E0 RID: 224
		private readonly IEncryptionService _service;
	}
}

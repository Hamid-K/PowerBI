using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Storage.Memory
{
	// Token: 0x020020A1 RID: 8353
	public class MemorySecureTokenServicesStorage : SecureTokenServicesStorage
	{
		// Token: 0x0600CC9C RID: 52380 RVA: 0x0028AE08 File Offset: 0x00289008
		public MemorySecureTokenServicesStorage()
		{
			this.secureTokenServices = new SecureTokenServicesList();
		}

		// Token: 0x0600CC9D RID: 52381 RVA: 0x0028AE1B File Offset: 0x0028901B
		public MemorySecureTokenServicesStorage(IEnumerable<SecureTokenServiceXml> secureTokenServices)
		{
			this.secureTokenServices = new SecureTokenServicesList(secureTokenServices.ToList<SecureTokenServiceXml>());
		}

		// Token: 0x0600CC9E RID: 52382 RVA: 0x0028AE34 File Offset: 0x00289034
		public override List<ISecureTokenService> GetSecureTokenServices()
		{
			return this.secureTokenServices.List.Select((SecureTokenServiceXml sts) => new SecureTokenService(sts.AuthorityId, sts.AuthorizeUri, sts.TokenUri, sts.LogoutUri)).Cast<ISecureTokenService>().ToList<ISecureTokenService>();
		}

		// Token: 0x0600CC9F RID: 52383 RVA: 0x0028AE6F File Offset: 0x0028906F
		public override void AddSecureTokenService(ISecureTokenService tokenService)
		{
			this.secureTokenServices.List.Add(new SecureTokenServiceXml(tokenService));
		}

		// Token: 0x0600CCA0 RID: 52384 RVA: 0x0028AE87 File Offset: 0x00289087
		public override void DeleteSecureTokenService(ISecureTokenService tokenService)
		{
			this.secureTokenServices.List.Remove(new SecureTokenServiceXml(tokenService));
		}

		// Token: 0x04006797 RID: 26519
		private readonly SecureTokenServicesList secureTokenServices;
	}
}

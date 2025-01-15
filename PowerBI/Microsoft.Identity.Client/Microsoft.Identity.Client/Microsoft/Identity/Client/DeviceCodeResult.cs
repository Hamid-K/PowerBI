using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200014A RID: 330
	public class DeviceCodeResult
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x0003B6E0 File Offset: 0x000398E0
		internal DeviceCodeResult(string userCode, string deviceCode, string verificationUrl, DateTimeOffset expiresOn, long interval, string message, string clientId, ISet<string> scopes)
		{
			this.UserCode = userCode;
			this.DeviceCode = deviceCode;
			this.VerificationUrl = verificationUrl;
			this.ExpiresOn = expiresOn;
			this.Interval = interval;
			this.Message = message;
			this.ClientId = clientId;
			this.Scopes = new ReadOnlyCollection<string>(scopes.AsEnumerable<string>().ToList<string>());
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0003B73F File Offset: 0x0003993F
		public string UserCode { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0003B747 File Offset: 0x00039947
		public string DeviceCode { get; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0003B74F File Offset: 0x0003994F
		public string VerificationUrl { get; }

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0003B757 File Offset: 0x00039957
		public DateTimeOffset ExpiresOn { get; }

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0003B75F File Offset: 0x0003995F
		public long Interval { get; }

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0003B767 File Offset: 0x00039967
		public string Message { get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x0003B76F File Offset: 0x0003996F
		public string ClientId { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x0003B777 File Offset: 0x00039977
		public IReadOnlyCollection<string> Scopes { get; }
	}
}

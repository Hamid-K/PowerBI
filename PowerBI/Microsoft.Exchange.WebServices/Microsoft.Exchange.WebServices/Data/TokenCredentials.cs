using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001D2 RID: 466
	public sealed class TokenCredentials : WSSecurityBasedCredentials
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x0003BBAC File Offset: 0x0003ABAC
		public TokenCredentials(string securityToken)
			: base(securityToken)
		{
			EwsUtilities.ValidateParam(securityToken, "securityToken");
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0003BBC0 File Offset: 0x0003ABC0
		internal override void PrepareWebRequest(IEwsHttpWebRequest request)
		{
			base.EwsUrl = request.RequestUri;
		}
	}
}

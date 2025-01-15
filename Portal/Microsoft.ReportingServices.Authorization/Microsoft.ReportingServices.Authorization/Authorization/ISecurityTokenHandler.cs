using System;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000014 RID: 20
	public interface ISecurityTokenHandler : ISecurityTokenValidator
	{
		// Token: 0x06000043 RID: 67
		string WriteToken(SecurityToken token);
	}
}

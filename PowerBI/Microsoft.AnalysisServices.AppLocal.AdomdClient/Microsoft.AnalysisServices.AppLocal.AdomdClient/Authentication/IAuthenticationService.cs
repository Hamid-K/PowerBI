using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000102 RID: 258
	internal interface IAuthenticationService
	{
		// Token: 0x06000EF8 RID: 3832
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password);

		// Token: 0x06000EF9 RID: 3833
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId);

		// Token: 0x06000EFA RID: 3834
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo);

		// Token: 0x06000EFB RID: 3835
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate);

		// Token: 0x06000EFC RID: 3836
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret);
	}
}

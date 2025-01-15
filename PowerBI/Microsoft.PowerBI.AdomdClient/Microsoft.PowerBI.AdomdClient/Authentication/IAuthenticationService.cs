using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000102 RID: 258
	internal interface IAuthenticationService
	{
		// Token: 0x06000EEB RID: 3819
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password);

		// Token: 0x06000EEC RID: 3820
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId);

		// Token: 0x06000EED RID: 3821
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo);

		// Token: 0x06000EEE RID: 3822
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate);

		// Token: 0x06000EEF RID: 3823
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret);
	}
}

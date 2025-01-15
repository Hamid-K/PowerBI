using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F7 RID: 247
	internal interface IAuthenticationService
	{
		// Token: 0x06000F87 RID: 3975
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password);

		// Token: 0x06000F88 RID: 3976
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId);

		// Token: 0x06000F89 RID: 3977
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo);

		// Token: 0x06000F8A RID: 3978
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate);

		// Token: 0x06000F8B RID: 3979
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret);
	}
}

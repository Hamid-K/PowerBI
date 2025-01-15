using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x0200001B RID: 27
	internal interface IAuthenticationService
	{
		// Token: 0x060000BB RID: 187
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId, string password);

		// Token: 0x060000BC RID: 188
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo, string userId);

		// Token: 0x060000BD RID: 189
		AuthenticationHandle AuthenticateUser(AuthenticationOptions options, AuthenticationInformation authInfo);

		// Token: 0x060000BE RID: 190
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, X509Certificate2 certificate);

		// Token: 0x060000BF RID: 191
		AuthenticationHandle AuthenticateServicePrincipal(AuthenticationOptions options, AuthenticationInformation authInfo, string applicationId, string secret);
	}
}

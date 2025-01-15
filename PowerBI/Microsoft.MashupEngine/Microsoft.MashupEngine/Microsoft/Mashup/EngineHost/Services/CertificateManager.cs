using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001996 RID: 6550
	public static class CertificateManager
	{
		// Token: 0x0600A629 RID: 42537 RVA: 0x00225E05 File Offset: 0x00224005
		public static void DisableEvaluationContainerCertificateValidation(bool evaluationContainerRejectOnUnknownRevocation)
		{
			if (CertificateManager.evaluationContainerCertificateValidationEnabled)
			{
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Remove(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(CertificateManager.ValidateBasic));
				CertificateManager.evaluationContainerCertificateValidationEnabled = false;
				CertificateManager.evaluationContainerRejectOnUnknownRevocation = evaluationContainerRejectOnUnknownRevocation;
			}
		}

		// Token: 0x0600A62A RID: 42538 RVA: 0x00225E3A File Offset: 0x0022403A
		public static void EnableEvaluationContainerCertificateValidation(bool evaluationContainerRejectOnUnknownRevocation)
		{
			if (!CertificateManager.evaluationContainerCertificateValidationEnabled)
			{
				CertificateManager.evaluationContainerCertificateValidationEnabled = true;
				ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(CertificateManager.ValidateBasic));
				CertificateManager.evaluationContainerRejectOnUnknownRevocation = evaluationContainerRejectOnUnknownRevocation;
			}
		}

		// Token: 0x0600A62B RID: 42539 RVA: 0x00225E6F File Offset: 0x0022406F
		public static bool IsCertificateChainValid(X509Chain chain, X509Certificate2 certificate)
		{
			if (!CertificateManager.evaluationContainerCertificateValidationEnabled)
			{
				return true;
			}
			chain.EnableRevocationChecks(CertificateManager.evaluationContainerRejectOnUnknownRevocation);
			return chain.Build(certificate);
		}

		// Token: 0x0600A62C RID: 42540 RVA: 0x00225E8C File Offset: 0x0022408C
		private static bool ValidateBasic(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return CertificateManager.IsCertificateChainValid(chain, new X509Certificate2(certificate)) && sslPolicyErrors == SslPolicyErrors.None;
		}

		// Token: 0x04005678 RID: 22136
		private static bool evaluationContainerCertificateValidationEnabled;

		// Token: 0x04005679 RID: 22137
		private static bool evaluationContainerRejectOnUnknownRevocation;
	}
}

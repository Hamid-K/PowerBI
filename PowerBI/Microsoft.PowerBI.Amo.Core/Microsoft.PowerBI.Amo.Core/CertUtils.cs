using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000017 RID: 23
	public static class CertUtils
	{
		// Token: 0x0600008B RID: 139 RVA: 0x000054F4 File Offset: 0x000036F4
		public static X509Certificate2 LoadCertificateByThumbprint(string thumbprint, bool requirePrivateKey)
		{
			if (string.IsNullOrEmpty(thumbprint))
			{
				throw new ArgumentNullException("thumbprint");
			}
			X509Certificate2 x509Certificate;
			if (!CertUtils.TryLoadCertificateByThumbprint(thumbprint, out x509Certificate))
			{
				return null;
			}
			if (requirePrivateKey && !x509Certificate.HasPrivateKey)
			{
				return null;
			}
			return x509Certificate;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005530 File Offset: 0x00003730
		internal static bool TryLoadCertificateByThumbprint(string thumbprint, out X509Certificate2 certificate)
		{
			foreach (StoreName storeName in CertUtils.CertificateStoreNames)
			{
				foreach (StoreLocation storeLocation in CertUtils.CertificateStoreLocations)
				{
					if (CertUtils.TryLoadCertificate(thumbprint, storeName, storeLocation, out certificate))
					{
						return true;
					}
				}
			}
			certificate = null;
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005588 File Offset: 0x00003788
		private static bool TryLoadCertificate(string thumbprint, StoreName storeName, StoreLocation storeLocation, out X509Certificate2 certificate)
		{
			X509Store x509Store = new X509Store(storeName, storeLocation);
			x509Store.Open(OpenFlags.MaxAllowed);
			bool flag;
			try
			{
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
				{
					certificate = null;
					flag = false;
				}
				else
				{
					certificate = x509Certificate2Collection[0];
					flag = true;
				}
			}
			finally
			{
				x509Store.Close();
			}
			return flag;
		}

		// Token: 0x04000074 RID: 116
		private static readonly StoreName[] CertificateStoreNames = new StoreName[]
		{
			StoreName.My,
			StoreName.Root
		};

		// Token: 0x04000075 RID: 117
		private static readonly StoreLocation[] CertificateStoreLocations = new StoreLocation[]
		{
			StoreLocation.CurrentUser,
			StoreLocation.LocalMachine
		};
	}
}

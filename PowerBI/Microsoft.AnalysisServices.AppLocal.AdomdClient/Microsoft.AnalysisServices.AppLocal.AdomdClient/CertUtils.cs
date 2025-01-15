using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000003 RID: 3
	internal static class CertUtils
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static X509Certificate2 LoadCertificateByThumbprint(string thumbprint, bool requirePrivateKey)
		{
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

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
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

		// Token: 0x06000003 RID: 3 RVA: 0x000020D0 File Offset: 0x000002D0
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

		// Token: 0x04000028 RID: 40
		private static readonly StoreName[] CertificateStoreNames = new StoreName[]
		{
			StoreName.My,
			StoreName.Root
		};

		// Token: 0x04000029 RID: 41
		private static readonly StoreLocation[] CertificateStoreLocations = new StoreLocation[]
		{
			StoreLocation.CurrentUser,
			StoreLocation.LocalMachine
		};
	}
}

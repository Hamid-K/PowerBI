using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B9 RID: 441
	public static class CertificateUtilities
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00027954 File Offset: 0x00025B54
		public static IEnumerable<X509Certificate2> GetChain(X509Certificate2 certificate)
		{
			X509Chain x509Chain = new X509Chain(true);
			x509Chain.Build(certificate);
			return (from e in x509Chain.ChainElements.OfType<X509ChainElement>()
				select e.Certificate into c
				where !c.Thumbprint.Equals(certificate.Thumbprint, StringComparison.OrdinalIgnoreCase)
				select c).Materialize<X509Certificate2>();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000279C5 File Offset: 0x00025BC5
		public static X509Certificate2 TryLoadPersonalCertificate(X509FindType findType, [NotNull] string property)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(property, "certificate property");
			return CertificateUtilities.TryLoadCertificate(new StoreLocation[]
			{
				StoreLocation.LocalMachine,
				StoreLocation.CurrentUser
			}, new StoreName[] { StoreName.My }, findType, property);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000279F1 File Offset: 0x00025BF1
		public static X509Certificate2Collection TryLoadPersonalCertificates(X509FindType findType, [NotNull] string property)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(property, "certificate property");
			return CertificateUtilities.TryLoadCertificates(new StoreLocation[]
			{
				StoreLocation.LocalMachine,
				StoreLocation.CurrentUser
			}, new StoreName[] { StoreName.My }, findType, property);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00027A20 File Offset: 0x00025C20
		public static X509Certificate2 TryLoadCertificate([NotNull] IEnumerable<StoreLocation> storeLocations, [NotNull] IEnumerable<StoreName> storeNames, X509FindType findType, [NotNull] string certificateProperty)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<StoreLocation>>(storeLocations, "storeLocations");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<StoreName>>(storeNames, "storeNames");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(certificateProperty, "certificateProperty");
			return (from certificateStoreLocation in storeLocations
				from certificateStore in storeNames
				select CertificateUtilities.TryLoadCertificate(certificateStoreLocation, certificateStore, findType, certificateProperty)).FirstOrDefault((X509Certificate2 requiredCertificate) => requiredCertificate != null);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00027AB8 File Offset: 0x00025CB8
		public static X509Certificate2Collection TryLoadCertificates([NotNull] IEnumerable<StoreLocation> storeLocations, [NotNull] IEnumerable<StoreName> storeNames, X509FindType findType, [NotNull] string certificateProperty)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<StoreLocation>>(storeLocations, "storeLocations");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<StoreName>>(storeNames, "storeNames");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(certificateProperty, "certificateProperty");
			return (from certificateStoreLocation in storeLocations
				from certificateStore in storeNames
				select CertificateUtilities.TryLoadCertificates(certificateStoreLocation, certificateStore, findType, certificateProperty)).FirstOrDefault((X509Certificate2Collection requiredCertificates) => requiredCertificates != null) ?? new X509Certificate2Collection();
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00027B58 File Offset: 0x00025D58
		public static X509Certificate2 TryLoadCertificate(StoreLocation certificateStoreLocation, StoreName certificateStoreName, X509FindType findType, [NotNull] string certificateProperty)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(certificateProperty, "certificateProperty");
			X509Store x509Store = null;
			X509Certificate2 x509Certificate;
			try
			{
				x509Store = new X509Store(certificateStoreName, certificateStoreLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(findType, certificateProperty, false);
				x509Certificate = (x509Certificate2Collection.Cast<X509Certificate2>().Any<X509Certificate2>() ? x509Certificate2Collection.Cast<X509Certificate2>().FirstOrDefault<X509Certificate2>() : null);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return x509Certificate;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00027BCC File Offset: 0x00025DCC
		public static X509Certificate2Collection TryLoadCertificates(StoreLocation certificateStoreLocation, StoreName certificateStoreName, X509FindType findType, [NotNull] string certificateProperty)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(certificateProperty, "certificateProperty");
			X509Store x509Store = null;
			X509Certificate2Collection x509Certificate2Collection2;
			try
			{
				x509Store = new X509Store(certificateStoreName, certificateStoreLocation);
				x509Store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(findType, certificateProperty, false);
				x509Certificate2Collection2 = (x509Certificate2Collection.Cast<X509Certificate2>().Any<X509Certificate2>() ? x509Certificate2Collection : null);
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return x509Certificate2Collection2;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00027C34 File Offset: 0x00025E34
		public static void ImportCertificate(X509Certificate2 certificate, StoreName storeName, StoreLocation storeLocation, bool replaceExisting)
		{
			X509Store x509Store = new X509Store(storeName, storeLocation);
			try
			{
				x509Store.Open(OpenFlags.ReadWrite);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySerialNumber, certificate.SerialNumber, true);
				if (x509Certificate2Collection.Cast<X509Certificate2>().Any<X509Certificate2>())
				{
					if (!replaceExisting)
					{
						return;
					}
					x509Store.RemoveRange(x509Certificate2Collection);
				}
				x509Store.Add(certificate);
			}
			finally
			{
				x509Store.Close();
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00027CA0 File Offset: 0x00025EA0
		public static void RemoveCertificate(X509Certificate2 certificate, StoreName storeName, StoreLocation storeLocation)
		{
			X509Store x509Store = new X509Store(storeName, storeLocation);
			try
			{
				x509Store.Open(OpenFlags.ReadWrite);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindBySerialNumber, certificate.SerialNumber, false);
				if (x509Certificate2Collection.Cast<X509Certificate2>().Any<X509Certificate2>())
				{
					x509Store.RemoveRange(x509Certificate2Collection);
				}
			}
			finally
			{
				x509Store.Close();
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00027D00 File Offset: 0x00025F00
		public static string GetTrimmedSubjectName(string subjectName)
		{
			int num = subjectName.IndexOf("CN=", StringComparison.Ordinal) + 3;
			return subjectName.Substring(num).Split(new char[] { ',' })[0];
		}
	}
}

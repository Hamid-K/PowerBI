using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x0200005E RID: 94
	public sealed class CertificatesContainer : ICertificatesContainer
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00009940 File Offset: 0x00007B40
		public CertificatesContainer([NotNull] IDictionary<string, IEnumerable<X509Certificate2>> certificates, [NotNull] ISecretManagerEventsKit secretManagerEventsKit)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IDictionary<string, IEnumerable<X509Certificate2>>>(certificates, "certificates");
			ExtendedDiagnostics.EnsureArgumentNotNull<ISecretManagerEventsKit>(secretManagerEventsKit, "secretManagerEventsKit");
			this.m_secretManagetEventsKit = secretManagerEventsKit;
			this.m_certificates = certificates.ToDictionary((KeyValuePair<string, IEnumerable<X509Certificate2>> k) => k.Key, (KeyValuePair<string, IEnumerable<X509Certificate2>> v) => v.Value.OrderByDescending((X509Certificate2 cert) => cert.NotBefore).Materialize<X509Certificate2>());
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000099BC File Offset: 0x00007BBC
		public IEnumerable<X509Certificate2> GetCertificates([NotNull] string certificateKey)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(certificateKey, "certificateKey");
			IEnumerable<X509Certificate2> enumerable;
			if (!this.m_certificates.TryGetValue(certificateKey, out enumerable))
			{
				CertificateKeyNotSupportedException ex = new CertificateKeyNotSupportedException(certificateKey);
				this.m_secretManagetEventsKit.NotifyCertificateKeyNotSupported(certificateKey, ex);
				throw ex;
			}
			return enumerable.OrderByDescending((X509Certificate2 certificate2) => certificate2.NotBefore).Materialize<X509Certificate2>();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009A24 File Offset: 0x00007C24
		public X509Certificate2 GetPrimaryCertificate(string certificateKey)
		{
			return this.GetCertificates(certificateKey).FirstOrDefault<X509Certificate2>();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009A34 File Offset: 0x00007C34
		[CanBeNull]
		public X509Certificate2 GetSecondaryCertificate(string certificateKey)
		{
			IEnumerable<X509Certificate2> certificates = this.GetCertificates(certificateKey);
			if (certificates.Count<X509Certificate2>() < 2)
			{
				TraceSourceBase<SecretManagerTracer>.Tracer.Trace(TraceVerbosity.Info, "A secondary certificate for {0} not found ".FormatWithInvariantCulture(new object[] { certificateKey }));
				return null;
			}
			return certificates.LastOrDefault<X509Certificate2>();
		}

		// Token: 0x040000F6 RID: 246
		private readonly Dictionary<string, IEnumerable<X509Certificate2>> m_certificates;

		// Token: 0x040000F7 RID: 247
		private readonly ISecretManagerEventsKit m_secretManagetEventsKit;
	}
}

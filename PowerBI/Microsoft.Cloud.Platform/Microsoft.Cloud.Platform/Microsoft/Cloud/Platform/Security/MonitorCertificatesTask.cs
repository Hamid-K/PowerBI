using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.SecretManager;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000065 RID: 101
	internal sealed class MonitorCertificatesTask : IScheduledTask, IIdentifiable
	{
		// Token: 0x060002BA RID: 698 RVA: 0x00009A7C File Offset: 0x00007C7C
		public MonitorCertificatesTask([NotNull] Dictionary<string, IEnumerable<X509Certificate2>> currentCertificateSnapshot, [NotNull] ISecretManagerEventsKit eventsKit, [NotNull] UpdateCertificateHandler updateHandler, [NotNull] IConfigurationManager configManager)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Dictionary<string, IEnumerable<X509Certificate2>>>(currentCertificateSnapshot, "currentCertificateSnapshot");
			ExtendedDiagnostics.EnsureArgumentNotNull<ISecretManagerEventsKit>(eventsKit, "eventsKit");
			ExtendedDiagnostics.EnsureArgumentNotNull<UpdateCertificateHandler>(updateHandler, "updateHandler");
			ExtendedDiagnostics.EnsureArgumentNotNull<IConfigurationManager>(configManager, "configManager");
			this.m_currentCertificateSnapshot = currentCertificateSnapshot;
			this.m_eventsKit = eventsKit;
			this.m_updateHandler = updateHandler;
			this.m_locker = new object();
			configManager.Subscribe(new Type[] { typeof(SecretManagerConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00009B02 File Offset: 0x00007D02
		public string Name
		{
			get
			{
				return typeof(MonitorCertificatesTask).Name;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00009B13 File Offset: 0x00007D13
		public IAsyncResult BeginExecute(ScheduledTaskInformation info, AsyncCallback callback, object state)
		{
			TraceSourceBase<SecretManagerTracer>.Tracer.Trace(TraceVerbosity.Verbose, "Executing certificate monitoring");
			this.MonitorCertificates();
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00009B32 File Offset: 0x00007D32
		public ScheduledTaskResult EndExecute(IAsyncResult asyncResult)
		{
			CompletedAsyncResult.End(asyncResult);
			return ScheduledTaskResult.Succeeded;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void Abort()
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009B40 File Offset: 0x00007D40
		private void MonitorCertificates()
		{
			Dictionary<string, IEnumerable<X509Certificate2>> dictionary = new Dictionary<string, IEnumerable<X509Certificate2>>();
			object locker = this.m_locker;
			lock (locker)
			{
				using (IEnumerator<CertificateConfiguration> enumerator = this.m_secretManagerConfiguration.Certificates.Where((CertificateConfiguration cert) => this.m_currentCertificateSnapshot.Keys.Contains(cert.CertificateKey)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CertificateConfiguration configuredCertificate = enumerator.Current;
						List<X509Certificate2> list = (from X509Certificate2 certificate in CertificateUtilities.TryLoadPersonalCertificates(X509FindType.FindBySubjectName, configuredCertificate.CertificateProperty)
							where certificate.SubjectName.Name.Split(new char[] { ',' })[0].Equals("CN={0}".FormatWithInvariantCulture(new object[] { configuredCertificate.CertificateProperty }), StringComparison.OrdinalIgnoreCase)
							select certificate).ToList<X509Certificate2>();
						if (list.Any<X509Certificate2>())
						{
							List<X509Certificate2> list2 = this.m_currentCertificateSnapshot[configuredCertificate.CertificateKey].ToList<X509Certificate2>();
							IEnumerable<X509Certificate2> enumerable = this.HandleCertificateGroup(list, configuredCertificate);
							if (enumerable.Any<X509Certificate2>())
							{
								this.m_eventsKit.NotifyCertificateChanged(configuredCertificate.CertificateKey, list2.Select((X509Certificate2 c) => c.Thumbprint).StringJoin(","), enumerable.Select((X509Certificate2 c) => c.Thumbprint).StringJoin(","));
								dictionary.Add(configuredCertificate.CertificateKey, enumerable);
							}
						}
						else
						{
							this.m_eventsKit.NotifyCertificateDoesNotExist(configuredCertificate.CertificateProperty);
						}
					}
				}
			}
			if (dictionary.Any<KeyValuePair<string, IEnumerable<X509Certificate2>>>())
			{
				this.m_updateHandler(dictionary);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00009D24 File Offset: 0x00007F24
		private IEnumerable<X509Certificate2> HandleCertificateGroup(List<X509Certificate2> certificatesFromStore, CertificateConfiguration configuredCertificate)
		{
			List<X509Certificate2> list = new List<X509Certificate2>();
			bool flag = false;
			this.CheckCertificateExpirationAndAlert(certificatesFromStore, TimeSpan.FromDays((double)configuredCertificate.PreExpirationAlertPeriod));
			List<X509Certificate2> list2 = this.m_currentCertificateSnapshot[configuredCertificate.CertificateKey].Intersect(certificatesFromStore).ToList<X509Certificate2>();
			foreach (X509Certificate2 x509Certificate in certificatesFromStore)
			{
				if (this.IsNewCertificate(x509Certificate, list2))
				{
					if (this.CheckNewCertificateValidity(x509Certificate, list2, TimeSpan.FromDays((double)configuredCertificate.PreExpirationAlertPeriod)))
					{
						list.Add(x509Certificate);
						flag = true;
					}
				}
				else if (this.IsExpiredCertificate(x509Certificate))
				{
					list2.Remove(x509Certificate);
					flag = true;
				}
				else
				{
					list.Add(x509Certificate);
				}
			}
			if (!flag)
			{
				return new List<X509Certificate2>();
			}
			return list;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00009DFC File Offset: 0x00007FFC
		private bool CheckNewCertificateValidity(X509Certificate2 certificate, IEnumerable<X509Certificate2> currentSnapshot, TimeSpan aboutToExpireInterval)
		{
			if (this.IsExpiredCertificate(certificate))
			{
				return false;
			}
			if (currentSnapshot.Any<X509Certificate2>())
			{
				DateTime dateTime = currentSnapshot.Max((X509Certificate2 cert) => cert.NotBefore).ToUniversalTime();
				if (certificate.NotBefore.ToUniversalTime() < dateTime)
				{
					this.m_eventsKit.NotifyUpdatedCertificateStartDateIsLessThanCurrent(certificate.Subject, certificate.Thumbprint, dateTime, certificate.NotBefore.ToUniversalTime());
					return false;
				}
			}
			if (certificate.NotBefore.ToUniversalTime() > ExtendedDateTime.UtcNow)
			{
				this.m_eventsKit.NotifyUpdatedCertificateCannotBeUsedBeforeFutureTime(certificate.Subject, certificate.Thumbprint, certificate.NotAfter.ToUniversalTime(), certificate.NotBefore.ToUniversalTime());
				return false;
			}
			if (this.IsAboutToExpireCertificate(certificate, aboutToExpireInterval))
			{
				this.m_eventsKit.NotifyCertificateAboutToExpire(certificate.Subject, certificate.Thumbprint, certificate.NotAfter.ToUniversalTime());
				return false;
			}
			TraceSourceBase<SecretManagerTracer>.Tracer.TraceInformation("Certificate {0} was updated with new expiration date {1}", new object[]
			{
				certificate.Subject,
				certificate.NotAfter.ToUniversalTime()
			});
			return true;
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00009F40 File Offset: 0x00008140
		private bool IsExpiredCertificate(X509Certificate2 certificate)
		{
			return ExtendedDateTime.UtcNow > certificate.NotAfter.ToUniversalTime();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00009F65 File Offset: 0x00008165
		private bool IsNewCertificate(X509Certificate2 certificate, IEnumerable<X509Certificate2> currentSnapshot)
		{
			return !currentSnapshot.Contains(certificate);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00009F74 File Offset: 0x00008174
		private void CheckCertificateExpirationAndAlert(List<X509Certificate2> certificates, TimeSpan aboutToExpireInterval)
		{
			X509Certificate2 x509Certificate = certificates.OrderByDescending((X509Certificate2 cert) => cert.NotBefore).FirstOrDefault<X509Certificate2>();
			if (this.IsAboutToExpireCertificate(x509Certificate, aboutToExpireInterval))
			{
				this.m_eventsKit.NotifyCertificateAboutToExpire(x509Certificate.Subject, x509Certificate.Thumbprint, x509Certificate.NotAfter.ToUniversalTime());
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00009FDC File Offset: 0x000081DC
		private bool IsAboutToExpireCertificate(X509Certificate2 certificate, TimeSpan aboutToExpireInterval)
		{
			return certificate.NotAfter.ToUniversalTime() <= ExtendedDateTime.UtcNow + aboutToExpireInterval;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000A008 File Offset: 0x00008208
		private void OnConfigurationChange(IConfigurationContainer configurationContainer)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_secretManagerConfiguration = configurationContainer.GetConfiguration<SecretManagerConfiguration>();
			}
		}

		// Token: 0x040000FB RID: 251
		private readonly Dictionary<string, IEnumerable<X509Certificate2>> m_currentCertificateSnapshot;

		// Token: 0x040000FC RID: 252
		private ISecretManagerEventsKit m_eventsKit;

		// Token: 0x040000FD RID: 253
		private UpdateCertificateHandler m_updateHandler;

		// Token: 0x040000FE RID: 254
		private SecretManagerConfiguration m_secretManagerConfiguration;

		// Token: 0x040000FF RID: 255
		private readonly object m_locker;
	}
}

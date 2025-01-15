using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.SecretManager;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000066 RID: 102
	[BlockServiceProvider(typeof(ISecretManager))]
	public class SecretManager : Block, ISecretManager
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000A068 File Offset: 0x00008268
		public SecretManager()
			: this(typeof(SecretManager).Name)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000A07F File Offset: 0x0000827F
		public SecretManager(string name)
			: base(name)
		{
			this.m_subscribers = new Dictionary<SecretManagerEventHandler, IEnumerable<string>>();
			this.m_certificates = new Dictionary<string, IEnumerable<X509Certificate2>>(StringComparer.OrdinalIgnoreCase);
			this.m_locker = new object();
			this.m_callgate = new CallGate();
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000A0BC File Offset: 0x000082BC
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.m_eventsKit = this.m_eventsKitFactory.CreateEventsKit<ISecretManagerEventsKit>();
			this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
			this.m_configurationManager.Subscribe(new Type[] { typeof(SecretManagerConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			this.LoadCertificates();
			this.m_monitorCertificatesTask = new MonitorCertificatesTask(this.m_certificates, this.m_eventsKit, new UpdateCertificateHandler(this.OnCertificateChange), this.m_configurationManagerFactory.GetConfigurationManager());
			this.m_monitorCertificatesTaskHandle = this.m_monitoredTaskScheduler.RegisterScheduledTask("MonitorCertificates", this.m_monitorCertificatesTask);
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000A171 File Offset: 0x00008371
		protected override void OnStart()
		{
			base.OnStart();
			this.m_monitorCertificatesTaskHandle.Start();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000A184 File Offset: 0x00008384
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			this.m_monitorCertificatesTaskHandle.Dispose();
			base.OnStop();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public void Subscribe([NotNull] IEnumerable<string> certificateKeys, [NotNull] SecretManagerEventHandler registeredEventHandler)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<string>>(certificateKeys, "certificateKeys");
			ExtendedDiagnostics.EnsureArgumentNotNull<SecretManagerEventHandler>(registeredEventHandler, "registeredEventHandler");
			object locker = this.m_locker;
			lock (locker)
			{
				SecretManager.Trace.Trace(TraceVerbosity.Verbose, "Subscribing to {0} certificates : {1}", new object[]
				{
					certificateKeys.Count<string>(),
					certificateKeys.StringJoin(", ")
				});
				if (this.m_subscribers.ContainsKey(registeredEventHandler))
				{
					throw new SecretManagerEventAlreadySubscribedException();
				}
				IEnumerable<string> enumerable = certificateKeys.Except(this.m_certificates.Keys);
				if (enumerable.Any<string>())
				{
					string text = string.Join("; ", enumerable);
					CertificateKeyNotSupportedException ex = new CertificateKeyNotSupportedException(text, null);
					this.m_eventsKit.NotifyCertificateKeyNotSupported(text, ex);
					throw ex;
				}
				this.m_subscribers.Add(registeredEventHandler, certificateKeys);
				CertificatesContainer certificatesContainer = this.CreateCertificatesContainer(certificateKeys);
				this.m_callgate.CallSync(new Action<object>(SecretManager.UpdateSubscribersWithNewCertificate), new SecretManager.UpdateContext(registeredEventHandler, new List<CertificatesContainer> { certificatesContainer }));
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000A2C8 File Offset: 0x000084C8
		public void Unsubscribe([NotNull] SecretManagerEventHandler registeredEventHandler)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<SecretManagerEventHandler>(registeredEventHandler, "registeredEventHandler");
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_subscribers.ContainsKey(registeredEventHandler))
				{
					throw new SecretManagerEventNotSubscribedException("The event handler given was not subscribed");
				}
				this.m_subscribers.Remove(registeredEventHandler);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000A334 File Offset: 0x00008534
		public IEnumerable<string> GetAvailableCertificateKeys()
		{
			return this.m_certificates.Keys;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A344 File Offset: 0x00008544
		private CertificatesContainer CreateCertificatesContainer([NotNull] IEnumerable<string> certificateKeys)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<string>>(certificateKeys, "certificateKeys");
			Dictionary<string, IEnumerable<X509Certificate2>> dictionary = new Dictionary<string, IEnumerable<X509Certificate2>>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in certificateKeys)
			{
				IEnumerable<X509Certificate2> enumerable;
				if (!this.m_certificates.TryGetValue(text, out enumerable))
				{
					CertificateKeyNotSupportedException ex = new CertificateKeyNotSupportedException(text);
					this.m_eventsKit.NotifyCertificateKeyNotSupported(text, ex);
					throw ex;
				}
				IEnumerable<X509Certificate2> enumerable2 = enumerable.Select((X509Certificate2 certificate) => new X509Certificate2(certificate));
				dictionary.Add(text, enumerable2);
			}
			return new CertificatesContainer(dictionary, this.m_eventsKit);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A400 File Offset: 0x00008600
		private static void UpdateSubscribersWithNewCertificate(object context)
		{
			SecretManager.UpdateContext updateContext = context as SecretManager.UpdateContext;
			SecretManager.Trace.Trace(TraceVerbosity.Info, "About to notify {0} subscribers of certificate changes", new object[] { updateContext.CertificatesContainers.Count<CertificatesContainer>() });
			foreach (CertificatesContainer certificatesContainer in updateContext.CertificatesContainers)
			{
				updateContext.EventHandler(certificatesContainer);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A484 File Offset: 0x00008684
		private void LoadCertificates()
		{
			DateTime utcNow = ExtendedDateTime.UtcNow;
			object locker = this.m_locker;
			bool flag = false;
			try
			{
				Monitor.Enter(locker, ref flag);
				using (IEnumerator<CertificateConfiguration> enumerator = this.m_secretManagerConfiguration.Certificates.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CertificateConfiguration configuredCertificate = enumerator.Current;
						X509Certificate2Collection x509Certificate2Collection = CertificateUtilities.TryLoadPersonalCertificates(X509FindType.FindBySubjectName, configuredCertificate.CertificateProperty);
						if (x509Certificate2Collection == null)
						{
							SecretManager.Trace.Trace(TraceVerbosity.Info, "Could not find certificate with CN {0}".FormatWithInvariantCulture(new object[] { configuredCertificate.CertificateProperty }));
						}
						else
						{
							Func<string, bool> <>9__7;
							List<X509Certificate2> list = x509Certificate2Collection.Cast<X509Certificate2>().Where(delegate(X509Certificate2 certificate)
							{
								IEnumerable<string> enumerable = certificate.SubjectName.Name.Split(new char[] { ',' });
								Func<string, bool> func;
								if ((func = <>9__7) == null)
								{
									func = (<>9__7 = (string attribute) => attribute.Trim().Equals("CN={0}".FormatWithInvariantCulture(new object[] { configuredCertificate.CertificateProperty }), StringComparison.OrdinalIgnoreCase));
								}
								return enumerable.Any(func);
							}).ToList<X509Certificate2>();
							if (list.None<X509Certificate2>())
							{
								SecretManager.Trace.Trace(TraceVerbosity.Info, "Could not find a valid certificate with CN {0}".FormatWithInvariantCulture(new object[] { configuredCertificate.CertificateProperty }));
							}
							else
							{
								this.m_certificates.Add(configuredCertificate.CertificateKey, list.OrderByDescending((X509Certificate2 certificate2) => certificate2.NotBefore));
								SecretManager.Trace.Trace(TraceVerbosity.Info, "Certificates with CN {0} are added to SecretManager local cache".FormatWithInvariantCulture(new object[] { configuredCertificate.CertificateProperty }));
							}
						}
					}
				}
				HashSet<X509Certificate2> nonLeafCertificates = new HashSet<X509Certificate2>(new CertificateThumprintComparer());
				Parallel.ForEach<X509Certificate2>(this.m_certificates.Values.SelectMany((IEnumerable<X509Certificate2> c) => c), delegate(X509Certificate2 cert)
				{
					IEnumerable<X509Certificate2> chain = CertificateUtilities.GetChain(cert);
					HashSet<X509Certificate2> nonLeafCertificates2 = nonLeafCertificates;
					lock (nonLeafCertificates2)
					{
						nonLeafCertificates.AddRange(chain);
					}
				});
				TraceSourceBase<SecretManagerTracer> trace = SecretManager.Trace;
				string text = "non-leaf certificates:" + Environment.NewLine + "{0}";
				object[] array = new object[1];
				array[0] = nonLeafCertificates.StringJoin(Environment.NewLine, (X509Certificate2 c) => "{0} ({1})".FormatWithInvariantCulture(new object[]
				{
					c.SerialNumber,
					c.SubjectName.Format(false)
				}));
				trace.TraceInformation(text, array);
				bool accountIsElevated = ExtendedEnvironment.IsElevated();
				Parallel.ForEach<X509Certificate2>(nonLeafCertificates, delegate(X509Certificate2 c)
				{
					StoreName storeName = (CertificateUtilities.GetChain(c).Any<X509Certificate2>() ? StoreName.CertificateAuthority : StoreName.Root);
					if (CertificateUtilities.TryLoadCertificate(StoreLocation.LocalMachine, storeName, X509FindType.FindByThumbprint, c.Thumbprint) == null)
					{
						if (accountIsElevated)
						{
							SecretManager.Trace.TraceInformation("{0}: Importing  {0} ({1})", new object[]
							{
								storeName,
								c.SerialNumber,
								c.SubjectName.Format(false)
							});
							try
							{
								CertificateUtilities.ImportCertificate(c, storeName, StoreLocation.LocalMachine, false);
								return;
							}
							catch (CryptographicException ex)
							{
								SecretManager.Trace.TraceWarning("Failed to import missing certificate {0} ({1}) due to {2}", new object[]
								{
									c.SerialNumber,
									c.SubjectName.Format(false),
									ex
								});
								return;
							}
						}
						SecretManager.Trace.TraceWarning("Cannot import missing certificate {0} ({1}) due to running non-elevated", new object[]
						{
							c.SerialNumber,
							c.SubjectName.Format(false)
						});
						return;
					}
					SecretManager.Trace.TraceInformation("{0}: found {1} ({2})", new object[]
					{
						storeName,
						c.SerialNumber,
						c.SubjectName.Format(false)
					});
				});
				TraceSourceBase<SecretManagerTracer> trace2 = SecretManager.Trace;
				string text2 = "Loading {0} certificates from store took {1} milliseconds";
				object[] array2 = new object[2];
				array2[0] = this.m_certificates.Values.SelectMany((IEnumerable<X509Certificate2> c) => c).Count<X509Certificate2>();
				array2[1] = (ExtendedDateTime.UtcNow - utcNow).TotalMilliseconds;
				trace2.TraceInformation(text2.FormatWithInvariantCulture(array2));
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(locker);
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A774 File Offset: 0x00008974
		private void OnCertificateChange(Dictionary<string, IEnumerable<X509Certificate2>> updatedCertificates)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				foreach (KeyValuePair<string, IEnumerable<X509Certificate2>> keyValuePair in updatedCertificates)
				{
					this.m_certificates[keyValuePair.Key] = keyValuePair.Value.OrderByDescending((X509Certificate2 cert) => cert.NotBefore);
				}
				foreach (KeyValuePair<SecretManagerEventHandler, IEnumerable<string>> keyValuePair2 in this.m_subscribers)
				{
					if (keyValuePair2.Value.Intersect(updatedCertificates.Keys).ToList<string>().Any<string>())
					{
						CertificatesContainer certificatesContainer = this.CreateCertificatesContainer(keyValuePair2.Value);
						this.m_callgate.CallAsync(new Action<object>(SecretManager.UpdateSubscribersWithNewCertificate), new SecretManager.UpdateContext(keyValuePair2.Key, new List<CertificatesContainer> { certificatesContainer }), CallGateAsyncOptions.AllowSyncCall);
					}
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000A8BC File Offset: 0x00008ABC
		private void OnConfigurationChange(IConfigurationContainer configurationContainer)
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_secretManagerConfiguration = configurationContainer.GetConfiguration<SecretManagerConfiguration>();
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000A904 File Offset: 0x00008B04
		private static SecretManagerTracer Trace
		{
			get
			{
				return TraceSourceBase<SecretManagerTracer>.Tracer;
			}
		}

		// Token: 0x04000100 RID: 256
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000101 RID: 257
		[BlockServiceDependency]
		private readonly ITaskScheduler m_monitoredTaskScheduler;

		// Token: 0x04000102 RID: 258
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x04000103 RID: 259
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000104 RID: 260
		private IScheduledTaskHandle m_monitorCertificatesTaskHandle;

		// Token: 0x04000105 RID: 261
		private ISecretManagerEventsKit m_eventsKit;

		// Token: 0x04000106 RID: 262
		private SecretManagerConfiguration m_secretManagerConfiguration;

		// Token: 0x04000107 RID: 263
		private MonitorCertificatesTask m_monitorCertificatesTask;

		// Token: 0x04000108 RID: 264
		private readonly Dictionary<string, IEnumerable<X509Certificate2>> m_certificates;

		// Token: 0x04000109 RID: 265
		private readonly Dictionary<SecretManagerEventHandler, IEnumerable<string>> m_subscribers;

		// Token: 0x0400010A RID: 266
		private readonly object m_locker;

		// Token: 0x0400010B RID: 267
		private readonly CallGate m_callgate;

		// Token: 0x0200059C RID: 1436
		private sealed class UpdateContext
		{
			// Token: 0x06002AF2 RID: 10994 RVA: 0x000999E6 File Offset: 0x00097BE6
			public UpdateContext(SecretManagerEventHandler eventHandler, IEnumerable<CertificatesContainer> certificatesContainers)
			{
				this.EventHandler = eventHandler;
				this.CertificatesContainers = certificatesContainers;
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000999FC File Offset: 0x00097BFC
			// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x00099A04 File Offset: 0x00097C04
			public SecretManagerEventHandler EventHandler { get; private set; }

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x00099A0D File Offset: 0x00097C0D
			// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x00099A15 File Offset: 0x00097C15
			public IEnumerable<CertificatesContainer> CertificatesContainers { get; private set; }
		}
	}
}

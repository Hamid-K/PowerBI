using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B1 RID: 1201
	public class ProviderServiceConfiguration : ServiceConfigurationBase, IProviderServiceConfiguration, IServiceConfiguration
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060024C3 RID: 9411 RVA: 0x000838CC File Offset: 0x00081ACC
		// (set) Token: 0x060024C4 RID: 9412 RVA: 0x000838D4 File Offset: 0x00081AD4
		public IEnumerable<EndpointInfo> EndpointsInformation { get; private set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000838DD File Offset: 0x00081ADD
		// (set) Token: 0x060024C6 RID: 9414 RVA: 0x000838E5 File Offset: 0x00081AE5
		public int MaxConcurrentCalls { get; private set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000838EE File Offset: 0x00081AEE
		// (set) Token: 0x060024C8 RID: 9416 RVA: 0x000838F6 File Offset: 0x00081AF6
		public int MaxConcurrentSessions { get; private set; }

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x000838FF File Offset: 0x00081AFF
		// (set) Token: 0x060024CA RID: 9418 RVA: 0x00083907 File Offset: 0x00081B07
		public string ServiceCertificateName { get; set; }

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x00083910 File Offset: 0x00081B10
		// (set) Token: 0x060024CC RID: 9420 RVA: 0x00083918 File Offset: 0x00081B18
		public NonContractualExceptionBehavior CrashServerOnNonContractualException { get; set; }

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x00083921 File Offset: 0x00081B21
		// (set) Token: 0x060024CE RID: 9422 RVA: 0x00083929 File Offset: 0x00081B29
		public bool DisableDefaultErrorHandler { get; set; }

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x00083932 File Offset: 0x00081B32
		// (set) Token: 0x060024D0 RID: 9424 RVA: 0x0008393A File Offset: 0x00081B3A
		public TimeSpan RequestInitializationTimeout { get; set; }

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x00083943 File Offset: 0x00081B43
		// (set) Token: 0x060024D2 RID: 9426 RVA: 0x0008394B File Offset: 0x00081B4B
		public int MaxPendingAccepts { get; set; }

		// Token: 0x060024D3 RID: 9427 RVA: 0x00083954 File Offset: 0x00081B54
		public ProviderServiceConfiguration(string serviceName, IEnumerable<EndpointInfo> endpointsInformation, NonContractualExceptionBehavior crashServerOnNonContractualException)
			: this(serviceName, null, null, endpointsInformation, 512, -1, crashServerOnNonContractualException, false, 0L, 0)
		{
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00083978 File Offset: 0x00081B78
		public ProviderServiceConfiguration(string serviceName, IEnumerable<EndpointInfo> endpointsInformation)
			: this(serviceName, null, null, endpointsInformation, 512, -1, NonContractualExceptionBehavior.CrashOnNonMonitoredExceptions, false, 0L, 0)
		{
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0008399C File Offset: 0x00081B9C
		public ProviderServiceConfiguration(string serviceName, IEnumerable<Type> knownTypes, IEnumerable<Type> knownExceptions, IEnumerable<EndpointInfo> providerConfiguration, int maxConcurrentCalls, int maxConcurrentSessions, NonContractualExceptionBehavior crashServerOnNonContractualException, bool disableDefaultErrorHandler, long requestInitializationTimeoutInSeconds, int maxPendingAccepts)
			: base(serviceName, knownTypes, knownExceptions)
		{
			this.EndpointsInformation = providerConfiguration;
			this.MaxConcurrentCalls = maxConcurrentCalls;
			this.MaxConcurrentSessions = maxConcurrentSessions;
			this.CrashServerOnNonContractualException = crashServerOnNonContractualException;
			this.DisableDefaultErrorHandler = disableDefaultErrorHandler;
			this.RequestInitializationTimeout = TimeSpan.FromSeconds((double)requestInitializationTimeoutInSeconds);
			this.MaxPendingAccepts = maxPendingAccepts;
		}
	}
}

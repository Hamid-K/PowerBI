using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000496 RID: 1174
	public class ConsumerServiceConfiguration : ServiceConfigurationBase, IConsumerServiceConfiguration, IServiceConfiguration
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x00081A34 File Offset: 0x0007FC34
		// (set) Token: 0x06002427 RID: 9255 RVA: 0x00081A3C File Offset: 0x0007FC3C
		public EndpointInfo EndpointInfo { get; private set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x00081A45 File Offset: 0x0007FC45
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x00081A4D File Offset: 0x0007FC4D
		public bool AllowImpersonation { get; private set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x00081A56 File Offset: 0x0007FC56
		// (set) Token: 0x0600242B RID: 9259 RVA: 0x00081A5E File Offset: 0x0007FC5E
		public bool UseDoubleWrap { get; private set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x00081A67 File Offset: 0x0007FC67
		// (set) Token: 0x0600242D RID: 9261 RVA: 0x00081A6F File Offset: 0x0007FC6F
		public bool TraceHttpResponseHeadersOnFaultException { get; private set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x00081A78 File Offset: 0x0007FC78
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x00081A80 File Offset: 0x0007FC80
		public ClientCertificateData ClientCertificateData { get; private set; }

		// Token: 0x06002430 RID: 9264 RVA: 0x00081A8C File Offset: 0x0007FC8C
		public ConsumerServiceConfiguration(string serviceName, EndpointInfo endpointInfo, ClientCertificateData clientCertificateData)
			: this(serviceName, null, null, endpointInfo, false, clientCertificateData, true, false)
		{
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00081AA7 File Offset: 0x0007FCA7
		public ConsumerServiceConfiguration(string serviceName, IEnumerable<Type> knownTypes, IEnumerable<Type> knownExceptions, EndpointInfo endpointInfo, bool allowImpersonation, ClientCertificateData clientCertificateData, bool useDoubleWrap, bool traceHttpResponseHeadersOnFaultException)
			: base(serviceName, knownTypes, knownExceptions)
		{
			this.EndpointInfo = endpointInfo;
			this.AllowImpersonation = allowImpersonation;
			this.ClientCertificateData = clientCertificateData;
			this.UseDoubleWrap = useDoubleWrap;
			this.TraceHttpResponseHeadersOnFaultException = traceHttpResponseHeadersOnFaultException;
		}
	}
}

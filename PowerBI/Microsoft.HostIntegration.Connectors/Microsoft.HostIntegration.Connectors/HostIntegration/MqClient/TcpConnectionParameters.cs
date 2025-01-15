using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BCC RID: 3020
	public class TcpConnectionParameters
	{
		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x0017F28F File Offset: 0x0017D48F
		// (set) Token: 0x06005DAF RID: 23983 RVA: 0x0017F297 File Offset: 0x0017D497
		public string Server { get; set; }

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x0017F2A0 File Offset: 0x0017D4A0
		// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x0017F2A8 File Offset: 0x0017D4A8
		public int Port { get; set; }

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x0017F2B1 File Offset: 0x0017D4B1
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x0017F2B9 File Offset: 0x0017D4B9
		public bool UseSsl { get; set; }

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x0017F2C2 File Offset: 0x0017D4C2
		// (set) Token: 0x06005DB5 RID: 23989 RVA: 0x0017F2CA File Offset: 0x0017D4CA
		public X509CertificateCollection CertificateCollection { get; set; }

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x0017F2D3 File Offset: 0x0017D4D3
		// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x0017F2DB File Offset: 0x0017D4DB
		public List<string> ServerCertificateThumbprints { get; set; }
	}
}

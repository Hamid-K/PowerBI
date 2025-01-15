using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000090 RID: 144
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpPipelineTransportOptions
	{
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000E08D File Offset: 0x0000C28D
		public HttpPipelineTransportOptions()
		{
			this.ClientCertificates = new List<X509Certificate2>();
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000E0A8 File Offset: 0x0000C2A8
		[Nullable(new byte[] { 2, 1 })]
		public Func<ServerCertificateCustomValidationArgs, bool> ServerCertificateCustomValidationCallback
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1 })]
			set;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000E0B1 File Offset: 0x0000C2B1
		public IList<X509Certificate2> ClientCertificates { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000E0B9 File Offset: 0x0000C2B9
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		public bool IsClientRedirectEnabled { get; set; }
	}
}

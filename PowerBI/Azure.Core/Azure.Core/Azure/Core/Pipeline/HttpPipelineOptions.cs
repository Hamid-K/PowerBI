using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200008C RID: 140
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpPipelineOptions
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x0000DE62 File Offset: 0x0000C062
		public HttpPipelineOptions(ClientOptions options)
		{
			Argument.AssertNotNull<ClientOptions>(options, "options");
			this.ClientOptions = options;
			this.PerCallPolicies = new List<HttpPipelinePolicy>();
			this.PerRetryPolicies = new List<HttpPipelinePolicy>();
			this.RequestFailedDetailsParser = new DefaultRequestFailedDetailsParser();
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000DE9D File Offset: 0x0000C09D
		public ClientOptions ClientOptions { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000DEA5 File Offset: 0x0000C0A5
		public IList<HttpPipelinePolicy> PerCallPolicies { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000DEAD File Offset: 0x0000C0AD
		public IList<HttpPipelinePolicy> PerRetryPolicies { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000DEB5 File Offset: 0x0000C0B5
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000DEBD File Offset: 0x0000C0BD
		[Nullable(2)]
		public ResponseClassifier ResponseClassifier
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000DEC6 File Offset: 0x0000C0C6
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000DECE File Offset: 0x0000C0CE
		public RequestFailedDetailsParser RequestFailedDetailsParser { get; set; }
	}
}

using System;
using System.Runtime.CompilerServices;
using Azure.Core;

namespace Azure
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpAuthorization
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002637 File Offset: 0x00000837
		public string Scheme { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000263F File Offset: 0x0000083F
		public string Parameter { get; }

		// Token: 0x0600005D RID: 93 RVA: 0x00002647 File Offset: 0x00000847
		public HttpAuthorization(string scheme, string parameter)
		{
			Argument.AssertNotNullOrWhiteSpace(scheme, "scheme");
			Argument.AssertNotNullOrWhiteSpace(parameter, "parameter");
			this.Scheme = scheme;
			this.Parameter = parameter;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002673 File Offset: 0x00000873
		public override string ToString()
		{
			return this.Scheme + " " + this.Parameter;
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000004 RID: 4
	[DataContract]
	public class AadOAuthError : OAuthError
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020AB File Offset: 0x000002AB
		public AadOAuthError()
		{
			this.ErrorCodes = new int[0];
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020BF File Offset: 0x000002BF
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020C7 File Offset: 0x000002C7
		[DataMember(Name = "error_codes", IsRequired = false)]
		public int[] ErrorCodes { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		[DataMember(Name = "timestamp", IsRequired = false)]
		public string Timestamp { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E1 File Offset: 0x000002E1
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020E9 File Offset: 0x000002E9
		[DataMember(Name = "trace_id", IsRequired = false)]
		public string TraceId { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F2 File Offset: 0x000002F2
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020FA File Offset: 0x000002FA
		[DataMember(Name = "correlation_id", IsRequired = false)]
		public string CorrelationId { get; set; }

		// Token: 0x04000003 RID: 3
		public const int ErrorCodeInactiveToken = 700082;
	}
}

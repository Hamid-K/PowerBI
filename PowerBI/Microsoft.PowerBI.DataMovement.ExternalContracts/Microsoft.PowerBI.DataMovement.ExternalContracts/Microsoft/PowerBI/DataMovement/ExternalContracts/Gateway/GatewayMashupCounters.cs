using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x0200001A RID: 26
	[DataContract]
	public sealed class GatewayMashupCounters
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002BEE File Offset: 0x00000DEE
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002BF6 File Offset: 0x00000DF6
		[DataMember(Name = "running")]
		public bool Running { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002BFF File Offset: 0x00000DFF
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002C07 File Offset: 0x00000E07
		[DataMember(Name = "workingSet")]
		public long? WorkingSet { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002C10 File Offset: 0x00000E10
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002C18 File Offset: 0x00000E18
		[DataMember(Name = "commit")]
		public long? Commit { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002C21 File Offset: 0x00000E21
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002C29 File Offset: 0x00000E29
		[DataMember(Name = "percentProcessorTime")]
		public float? PercentProcessorTime { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002C32 File Offset: 0x00000E32
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002C3A File Offset: 0x00000E3A
		[DataMember(Name = "ioDataBytesPerSecond")]
		public float? IODataBytesPerSecond { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002C43 File Offset: 0x00000E43
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002C4B File Offset: 0x00000E4B
		[DataMember(Name = "totalProcessorTime")]
		public TimeSpan? TotalProcessorTime { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E02 RID: 3586
	[DataContract]
	internal class CdpaFunnelStepDefinition
	{
		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x0014A068 File Offset: 0x00148268
		// (set) Token: 0x060060A4 RID: 24740 RVA: 0x0014A070 File Offset: 0x00148270
		public ScopePath ScopePath { get; set; }

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x0014A079 File Offset: 0x00148279
		// (set) Token: 0x060060A6 RID: 24742 RVA: 0x0014A081 File Offset: 0x00148281
		[DataMember(Name = "stepName", IsRequired = true)]
		public string StepName { get; set; }

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x060060A7 RID: 24743 RVA: 0x0014A08A File Offset: 0x0014828A
		// (set) Token: 0x060060A8 RID: 24744 RVA: 0x0014A092 File Offset: 0x00148292
		[DataMember(Name = "maxStepCompletionTime", IsRequired = false)]
		public string MaxStepCompletionTime { get; set; }

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x060060A9 RID: 24745 RVA: 0x0014A09B File Offset: 0x0014829B
		// (set) Token: 0x060060AA RID: 24746 RVA: 0x0014A0A3 File Offset: 0x001482A3
		[DataMember(Name = "configuration", IsRequired = true)]
		public CdpaTableOrMetricConfiguration Configuration { get; set; }

		// Token: 0x060060AB RID: 24747 RVA: 0x0014A0AC File Offset: 0x001482AC
		public CdpaFunnelStepDefinition ShallowCopy()
		{
			return new CdpaFunnelStepDefinition
			{
				StepName = this.StepName,
				MaxStepCompletionTime = this.MaxStepCompletionTime,
				Configuration = this.Configuration
			};
		}
	}
}

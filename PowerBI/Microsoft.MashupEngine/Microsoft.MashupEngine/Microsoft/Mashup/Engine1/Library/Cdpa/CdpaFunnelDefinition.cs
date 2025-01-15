using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E03 RID: 3587
	[DataContract]
	internal class CdpaFunnelDefinition
	{
		// Token: 0x060060AD RID: 24749 RVA: 0x0014A0D7 File Offset: 0x001482D7
		public CdpaFunnelDefinition()
		{
			this.Steps = EmptyArray<CdpaFunnelStepDefinition>.Instance;
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x0014A0EA File Offset: 0x001482EA
		// (set) Token: 0x060060AF RID: 24751 RVA: 0x0014A0F2 File Offset: 0x001482F2
		[DataMember(Name = "funnelName", IsRequired = true)]
		public string FunnelName { get; set; }

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x060060B0 RID: 24752 RVA: 0x0014A0FB File Offset: 0x001482FB
		// (set) Token: 0x060060B1 RID: 24753 RVA: 0x0014A103 File Offset: 0x00148303
		[DataMember(Name = "funnelKey", IsRequired = true)]
		public string FunnelKey { get; set; }

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x060060B2 RID: 24754 RVA: 0x0014A10C File Offset: 0x0014830C
		// (set) Token: 0x060060B3 RID: 24755 RVA: 0x0014A114 File Offset: 0x00148314
		[DataMember(Name = "steps", IsRequired = true)]
		public IList<CdpaFunnelStepDefinition> Steps { get; set; }

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x060060B4 RID: 24756 RVA: 0x0014A11D File Offset: 0x0014831D
		// (set) Token: 0x060060B5 RID: 24757 RVA: 0x0014A125 File Offset: 0x00148325
		[DataMember(Name = "maxCompletionTime", IsRequired = true)]
		public string MaxCompletionTime { get; set; }

		// Token: 0x060060B6 RID: 24758 RVA: 0x0014A12E File Offset: 0x0014832E
		public CdpaFunnelDefinition ShallowCopy()
		{
			return new CdpaFunnelDefinition
			{
				FunnelName = this.FunnelName,
				FunnelKey = this.FunnelKey,
				Steps = this.Steps,
				MaxCompletionTime = this.MaxCompletionTime
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE2 RID: 3554
	[DataContract]
	internal abstract class CdpaMetricMeasure : IUnionable<CdpaMetricMeasure>
	{
		// Token: 0x06006013 RID: 24595 RVA: 0x001494CD File Offset: 0x001476CD
		public CdpaMetricMeasure()
		{
			this.Operations = EmptyArray<CdpaOperation>.Instance;
		}

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x06006014 RID: 24596 RVA: 0x001494E0 File Offset: 0x001476E0
		// (set) Token: 0x06006015 RID: 24597 RVA: 0x001494E8 File Offset: 0x001476E8
		[DataMember(Name = "operations", IsRequired = true)]
		public IList<CdpaOperation> Operations { get; set; }

		// Token: 0x06006016 RID: 24598
		public abstract CdpaMetricMeasure Union(CdpaMetricMeasure other);
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A0 RID: 1440
	[DataContract]
	internal sealed class TopLimitOperator : LimitOperator
	{
		// Token: 0x06005200 RID: 20992 RVA: 0x0015A480 File Offset: 0x00158680
		internal TopLimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x0015A48F File Offset: 0x0015868F
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0015A497 File Offset: 0x00158697
		public override DataShapeLimitOperator TranslateToRIF()
		{
			return new TopLimitOperator(this.Count);
		}

		// Token: 0x0400296C RID: 10604
		[DataMember(Name = "Count", Order = 1)]
		private readonly int m_count;
	}
}

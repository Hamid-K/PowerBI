using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A2 RID: 1442
	[DataContract]
	internal sealed class BottomLimitOperator : LimitOperator
	{
		// Token: 0x06005206 RID: 20998 RVA: 0x0015A4C8 File Offset: 0x001586C8
		internal BottomLimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x06005207 RID: 20999 RVA: 0x0015A4D7 File Offset: 0x001586D7
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0015A4DF File Offset: 0x001586DF
		public override DataShapeLimitOperator TranslateToRIF()
		{
			return new BottomLimitOperator(this.Count);
		}

		// Token: 0x0400296E RID: 10606
		[DataMember(Name = "Count", Order = 1)]
		private readonly int m_count;
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A1 RID: 1441
	[DataContract]
	internal sealed class SampleLimitOperator : LimitOperator
	{
		// Token: 0x06005203 RID: 20995 RVA: 0x0015A4A4 File Offset: 0x001586A4
		internal SampleLimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06005204 RID: 20996 RVA: 0x0015A4B3 File Offset: 0x001586B3
		internal int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0015A4BB File Offset: 0x001586BB
		public override DataShapeLimitOperator TranslateToRIF()
		{
			return new SampleLimitOperator(this.Count);
		}

		// Token: 0x0400296D RID: 10605
		[DataMember(Name = "Count", Order = 1)]
		private readonly int m_count;
	}
}

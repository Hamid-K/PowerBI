using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF0 RID: 3568
	[DataContract]
	internal class SumCdpaMetricPropertyOperation : StatisticalOperationCdpaMetricPropertyOperation
	{
		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x00149661 File Offset: 0x00147861
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "sum";
			}
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF1 RID: 3569
	[DataContract]
	internal class AverageCdpaMetricPropertyOperation : StatisticalOperationCdpaMetricPropertyOperation
	{
		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x06006040 RID: 24640 RVA: 0x00149668 File Offset: 0x00147868
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "average";
			}
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DEE RID: 3566
	[DataContract]
	internal class MinCdpaMetricPropertyOperation : StatisticalOperationCdpaMetricPropertyOperation
	{
		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x0014964B File Offset: 0x0014784B
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "min";
			}
		}
	}
}

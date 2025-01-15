using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF3 RID: 3571
	[DataContract]
	internal class VarianceCdpaMetricPropertyOperation : StatisticalOperationCdpaMetricPropertyOperation
	{
		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x00149676 File Offset: 0x00147876
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "variance";
			}
		}
	}
}

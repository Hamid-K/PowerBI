using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF2 RID: 3570
	[DataContract]
	internal class StandardDeviationCdpaMetricPropertyOperation : StatisticalOperationCdpaMetricPropertyOperation
	{
		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x06006042 RID: 24642 RVA: 0x0014966F File Offset: 0x0014786F
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "standardDeviation";
			}
		}
	}
}

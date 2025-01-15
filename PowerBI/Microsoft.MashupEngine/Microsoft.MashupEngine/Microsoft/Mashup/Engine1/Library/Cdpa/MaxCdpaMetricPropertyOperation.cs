using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DEF RID: 3567
	[DataContract]
	internal class MaxCdpaMetricPropertyOperation : StatisticalOperationCdpaMetricPropertyOperation
	{
		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x0600603C RID: 24636 RVA: 0x0014965A File Offset: 0x0014785A
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "max";
			}
		}
	}
}

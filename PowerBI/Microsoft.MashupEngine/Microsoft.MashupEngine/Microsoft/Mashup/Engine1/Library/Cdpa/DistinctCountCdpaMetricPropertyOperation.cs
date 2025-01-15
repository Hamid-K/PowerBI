using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DEA RID: 3562
	[DataContract]
	internal class DistinctCountCdpaMetricPropertyOperation : CdpaMetricPropertyOperation
	{
		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x001495A8 File Offset: 0x001477A8
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "distinctCount";
			}
		}
	}
}

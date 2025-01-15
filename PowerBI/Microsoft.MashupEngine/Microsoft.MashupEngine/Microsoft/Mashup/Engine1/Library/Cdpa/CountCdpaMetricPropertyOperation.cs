using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE9 RID: 3561
	[DataContract]
	internal class CountCdpaMetricPropertyOperation : CdpaMetricPropertyOperation
	{
		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x06006027 RID: 24615 RVA: 0x00149599 File Offset: 0x00147799
		[DataMember(Name = "name", IsRequired = true)]
		public override string Name
		{
			get
			{
				return "count";
			}
		}
	}
}

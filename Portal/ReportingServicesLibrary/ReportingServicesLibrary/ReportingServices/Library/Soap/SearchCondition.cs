using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000331 RID: 817
	public class SearchCondition : Property
	{
		// Token: 0x04000B04 RID: 2820
		public ConditionEnum Condition;

		// Token: 0x04000B05 RID: 2821
		[XmlIgnore]
		public bool ConditionSpecified;
	}
}

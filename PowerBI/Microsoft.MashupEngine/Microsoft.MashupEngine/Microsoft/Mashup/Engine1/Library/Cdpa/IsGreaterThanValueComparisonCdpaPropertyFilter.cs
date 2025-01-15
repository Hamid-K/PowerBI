using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DD4 RID: 3540
	[DataContract]
	internal class IsGreaterThanValueComparisonCdpaPropertyFilter : ValueComparisonCdpaPropertyFilter
	{
		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x06005FCD RID: 24525 RVA: 0x00148C33 File Offset: 0x00146E33
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isGreaterThan";
			}
		}
	}
}

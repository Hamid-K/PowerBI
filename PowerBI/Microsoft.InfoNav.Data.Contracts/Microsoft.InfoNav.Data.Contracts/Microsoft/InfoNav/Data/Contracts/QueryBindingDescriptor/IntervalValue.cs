using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor
{
	// Token: 0x020000E1 RID: 225
	[DataContract]
	public class IntervalValue
	{
		// Token: 0x0400028F RID: 655
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public string MinValue;

		// Token: 0x04000290 RID: 656
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public string MaxValue;
	}
}

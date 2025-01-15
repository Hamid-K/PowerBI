using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DDB RID: 3547
	internal class IsNotOneOfMembershipCdpaPropertyFilter : MembershipCdpaPropertyFilter
	{
		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x06005FE0 RID: 24544 RVA: 0x00148D56 File Offset: 0x00146F56
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isNotOneOf";
			}
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x00148D5D File Offset: 0x00146F5D
		public override CdpaPropertyFilter Not()
		{
			return new IsOneOfMembershipCdpaPropertyFilter
			{
				PropertyName = base.PropertyName,
				Values = base.Values,
				ComparisonOptions = base.ComparisonOptions
			};
		}
	}
}

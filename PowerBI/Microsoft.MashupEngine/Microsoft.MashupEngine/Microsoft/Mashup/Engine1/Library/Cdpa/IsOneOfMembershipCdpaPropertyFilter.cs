using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DDA RID: 3546
	internal class IsOneOfMembershipCdpaPropertyFilter : MembershipCdpaPropertyFilter
	{
		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x06005FDD RID: 24541 RVA: 0x00148D1C File Offset: 0x00146F1C
		[DataMember(Name = "operator", IsRequired = true)]
		public override string Operator
		{
			get
			{
				return "isOneOf";
			}
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x00148D23 File Offset: 0x00146F23
		public override CdpaPropertyFilter Not()
		{
			return new IsNotOneOfMembershipCdpaPropertyFilter
			{
				PropertyName = base.PropertyName,
				Values = base.Values,
				ComparisonOptions = base.ComparisonOptions
			};
		}
	}
}

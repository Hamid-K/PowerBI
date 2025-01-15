using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000105 RID: 261
	internal interface IDataMemberContainer<TResult>
	{
		// Token: 0x0600070B RID: 1803
		DataMemberBuilder<TResult> WithPrimaryMember(Identifier identifier, bool? subtotalStartPosition = null);

		// Token: 0x0600070C RID: 1804
		DataMemberBuilder<TResult> WithSecondaryMember(Identifier identifier);

		// Token: 0x0600070D RID: 1805
		DataMemberBuilder<TResult> WithStaticMember(string identifier, bool isPrimary, bool? subtotalStartPosition = null);
	}
}

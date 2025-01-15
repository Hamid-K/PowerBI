using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E0 RID: 224
	internal sealed class DataMemberBuilder : DataMemberBuilder<DataMember>
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x0000D340 File Offset: 0x0000B540
		internal DataMemberBuilder(DataMember parent)
			: base(parent, parent)
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000D34A File Offset: 0x0000B54A
		internal static DataMemberBuilder With(string id)
		{
			return new DataMemberBuilder(new DataMember
			{
				Id = id
			});
		}
	}
}

using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000A0 RID: 160
	internal interface IIdentifiable
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003BD RID: 957
		Identifier Id { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003BE RID: 958
		ObjectType ObjectType { get; }
	}
}

using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200010D RID: 269
	internal interface IPropertyValuesItem
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001324 RID: 4900
		// (set) Token: 0x06001325 RID: 4901
		object Value { get; set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001326 RID: 4902
		string Name { get; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001327 RID: 4903
		bool IsComplex { get; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001328 RID: 4904
		Type Type { get; }
	}
}

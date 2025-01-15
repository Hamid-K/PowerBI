using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200001F RID: 31
	internal enum PersistMethod
	{
		// Token: 0x04000116 RID: 278
		PrimitiveGenericList,
		// Token: 0x04000117 RID: 279
		PrimitiveList,
		// Token: 0x04000118 RID: 280
		PrimitiveArray,
		// Token: 0x04000119 RID: 281
		PrimitiveTypedArray,
		// Token: 0x0400011A RID: 282
		GenericListOfReferences,
		// Token: 0x0400011B RID: 283
		ListOfReferences,
		// Token: 0x0400011C RID: 284
		Reference,
		// Token: 0x0400011D RID: 285
		GenericListOfGlobalReferences,
		// Token: 0x0400011E RID: 286
		ListOfGlobalReferences,
		// Token: 0x0400011F RID: 287
		SerializableArray
	}
}

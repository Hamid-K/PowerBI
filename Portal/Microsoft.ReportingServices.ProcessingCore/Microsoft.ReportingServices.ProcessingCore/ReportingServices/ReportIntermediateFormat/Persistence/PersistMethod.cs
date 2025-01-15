using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x0200053C RID: 1340
	internal enum PersistMethod
	{
		// Token: 0x04002096 RID: 8342
		PrimitiveGenericList,
		// Token: 0x04002097 RID: 8343
		PrimitiveList,
		// Token: 0x04002098 RID: 8344
		PrimitiveArray,
		// Token: 0x04002099 RID: 8345
		PrimitiveTypedArray,
		// Token: 0x0400209A RID: 8346
		GenericListOfReferences,
		// Token: 0x0400209B RID: 8347
		ListOfReferences,
		// Token: 0x0400209C RID: 8348
		Reference,
		// Token: 0x0400209D RID: 8349
		GenericListOfGlobalReferences,
		// Token: 0x0400209E RID: 8350
		ListOfGlobalReferences,
		// Token: 0x0400209F RID: 8351
		SerializableArray
	}
}

using System;

namespace ParquetSharp
{
	// Token: 0x0200008D RID: 141
	public enum StatusCode : byte
	{
		// Token: 0x04000120 RID: 288
		OK,
		// Token: 0x04000121 RID: 289
		OutOfMemory,
		// Token: 0x04000122 RID: 290
		KeyError,
		// Token: 0x04000123 RID: 291
		TypeError,
		// Token: 0x04000124 RID: 292
		Invalid,
		// Token: 0x04000125 RID: 293
		IOError,
		// Token: 0x04000126 RID: 294
		CapacityError,
		// Token: 0x04000127 RID: 295
		UnknownError = 9,
		// Token: 0x04000128 RID: 296
		NotImplemented,
		// Token: 0x04000129 RID: 297
		SerializationError,
		// Token: 0x0400012A RID: 298
		PythonError,
		// Token: 0x0400012B RID: 299
		RError,
		// Token: 0x0400012C RID: 300
		PlasmaObjectExists = 20,
		// Token: 0x0400012D RID: 301
		PlasmaObjectNonexistent,
		// Token: 0x0400012E RID: 302
		PlasmaStoreFull,
		// Token: 0x0400012F RID: 303
		PlasmaObjectAlreadySealed,
		// Token: 0x04000130 RID: 304
		StillExecuting,
		// Token: 0x04000131 RID: 305
		CodeGenError = 40,
		// Token: 0x04000132 RID: 306
		ExpressionValidationError,
		// Token: 0x04000133 RID: 307
		ExecutionError
	}
}

using System;

namespace Microsoft.Lucia
{
	// Token: 0x0200000E RID: 14
	public struct ConditionalResult<T>
	{
		// Token: 0x06000032 RID: 50 RVA: 0x0000275C File Offset: 0x0000095C
		public ConditionalResult(bool succeeded, T result)
		{
			this.Succeeded = succeeded;
			this.Result = result;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000277B File Offset: 0x0000097B
		public readonly bool Succeeded { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002783 File Offset: 0x00000983
		public readonly T Result { get; }

		// Token: 0x06000035 RID: 53 RVA: 0x0000278C File Offset: 0x0000098C
		public static implicit operator ConditionalResult<T>(FailedConditionalResult failed)
		{
			return new ConditionalResult<T>(false, default(T));
		}
	}
}

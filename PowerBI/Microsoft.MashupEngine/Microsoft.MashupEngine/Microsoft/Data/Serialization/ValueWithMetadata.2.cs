using System;

namespace Microsoft.Data.Serialization
{
	// Token: 0x02000160 RID: 352
	public sealed class ValueWithMetadata<T> : ValueWithMetadata
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000AF32 File Offset: 0x00009132
		public T TypedValue
		{
			get
			{
				return (T)((object)base.Value);
			}
		}
	}
}

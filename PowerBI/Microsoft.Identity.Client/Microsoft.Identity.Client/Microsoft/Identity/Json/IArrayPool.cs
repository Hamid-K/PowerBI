using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000015 RID: 21
	internal interface IArrayPool<T>
	{
		// Token: 0x06000015 RID: 21
		T[] Rent(int minimumLength);

		// Token: 0x06000016 RID: 22
		void Return([Nullable(new byte[] { 2, 0 })] T[] array);
	}
}

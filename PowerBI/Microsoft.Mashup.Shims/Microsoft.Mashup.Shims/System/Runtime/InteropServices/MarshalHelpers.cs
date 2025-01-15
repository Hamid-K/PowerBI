using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200000B RID: 11
	public static class MarshalHelpers
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000021EE File Offset: 0x000003EE
		public static T PtrToStructure<T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000168 RID: 360
	internal static class CryptographicOperations
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x0004022E File Offset: 0x0003E42E
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static void ZeroMemory(byte[] buffer)
		{
			Array.Clear(buffer, 0, buffer.Length);
		}
	}
}

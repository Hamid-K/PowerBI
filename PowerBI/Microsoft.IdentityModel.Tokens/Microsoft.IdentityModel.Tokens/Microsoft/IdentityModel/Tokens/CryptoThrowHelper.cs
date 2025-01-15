using System;
using System.Security.Cryptography;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000169 RID: 361
	internal static class CryptoThrowHelper
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x0004023C File Offset: 0x0003E43C
		public static CryptographicException ToCryptographicException(this int hr)
		{
			string message = Interop.Kernel32.GetMessage(hr);
			if (((long)hr & (long)((ulong)(-2147483648))) != (long)((ulong)(-2147483648)))
			{
				hr = (hr & 65535) | -2147024896;
			}
			return new CryptoThrowHelper.WindowsCryptographicException(hr, message);
		}

		// Token: 0x0200027B RID: 635
		private sealed class WindowsCryptographicException : CryptographicException
		{
			// Token: 0x060014DE RID: 5342 RVA: 0x00055366 File Offset: 0x00053566
			public WindowsCryptographicException(int hr, string message)
				: base(message)
			{
				base.HResult = hr;
			}

			// Token: 0x060014DF RID: 5343 RVA: 0x00055376 File Offset: 0x00053576
			public WindowsCryptographicException(string message)
				: base(message)
			{
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x0005537F File Offset: 0x0005357F
			public WindowsCryptographicException(string message, Exception innerException)
				: base(message, innerException)
			{
			}

			// Token: 0x060014E1 RID: 5345 RVA: 0x00055389 File Offset: 0x00053589
			public WindowsCryptographicException()
			{
			}
		}
	}
}

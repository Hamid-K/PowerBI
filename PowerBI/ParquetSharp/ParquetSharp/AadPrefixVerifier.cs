using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class AadPrefixVerifier
	{
		// Token: 0x06000009 RID: 9
		public abstract void Verify(string aadPrefix);

		// Token: 0x0600000A RID: 10 RVA: 0x000020A8 File Offset: 0x000002A8
		internal IntPtr CreateGcHandle()
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Normal));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020B8 File Offset: 0x000002B8
		internal static AadPrefixVerifier GetGcHandleTarget(IntPtr handle)
		{
			return (AadPrefixVerifier)GCHandle.FromIntPtr(handle).Target;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020DC File Offset: 0x000002DC
		private static void FreeGcHandle(IntPtr handle)
		{
			GCHandle.FromIntPtr(handle).Free();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020FC File Offset: 0x000002FC
		[NullableContext(2)]
		private static void Verify(IntPtr handle, IntPtr aadPrefix, out string exception)
		{
			exception = null;
			try
			{
				AadPrefixVerifier aadPrefixVerifier = (AadPrefixVerifier)GCHandle.FromIntPtr(handle).Target;
				string text = StringUtil.PtrToStringUtf8(aadPrefix);
				aadPrefixVerifier.Verify(text);
			}
			catch (Exception ex)
			{
				exception = ex.ToString();
			}
		}

		// Token: 0x04000007 RID: 7
		internal static readonly AadPrefixVerifier.FreeGcHandleFunc FreeGcHandleCallback = new AadPrefixVerifier.FreeGcHandleFunc(AadPrefixVerifier.FreeGcHandle);

		// Token: 0x04000008 RID: 8
		internal static readonly AadPrefixVerifier.VerifyFunc VerifyFuncCallback = new AadPrefixVerifier.VerifyFunc(AadPrefixVerifier.Verify);

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x06000900 RID: 2304
		[NullableContext(0)]
		internal delegate void FreeGcHandleFunc(IntPtr handle);

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x06000904 RID: 2308
		[NullableContext(0)]
		internal delegate void VerifyFunc(IntPtr handle, IntPtr aadPrefix, [MarshalAs(UnmanagedType.LPStr)] out string exception);
	}
}

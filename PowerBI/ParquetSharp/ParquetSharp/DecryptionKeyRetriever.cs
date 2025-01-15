using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DecryptionKeyRetriever
	{
		// Token: 0x0600014B RID: 331
		public abstract byte[] GetKey(string keyMetadata);

		// Token: 0x0600014C RID: 332 RVA: 0x00005708 File Offset: 0x00003908
		internal IntPtr CreateGcHandle()
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(this, GCHandleType.Normal));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005718 File Offset: 0x00003918
		internal static DecryptionKeyRetriever GetGcHandleTarget(IntPtr handle)
		{
			return (DecryptionKeyRetriever)GCHandle.FromIntPtr(handle).Target;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000573C File Offset: 0x0000393C
		private static void FreeGcHandle(IntPtr handle)
		{
			GCHandle.FromIntPtr(handle).Free();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000575C File Offset: 0x0000395C
		[NullableContext(2)]
		private static void GetKey(IntPtr handle, IntPtr keyMetadata, out AesKey key, out string exception)
		{
			exception = null;
			try
			{
				DecryptionKeyRetriever decryptionKeyRetriever = (DecryptionKeyRetriever)GCHandle.FromIntPtr(handle).Target;
				string text = StringUtil.PtrToStringUtf8(keyMetadata);
				key = new AesKey(decryptionKeyRetriever.GetKey(text));
			}
			catch (Exception ex)
			{
				key = default(AesKey);
				exception = ex.ToString();
			}
		}

		// Token: 0x04000051 RID: 81
		internal static readonly DecryptionKeyRetriever.FreeGcHandleFunc FreeGcHandleCallback = new DecryptionKeyRetriever.FreeGcHandleFunc(DecryptionKeyRetriever.FreeGcHandle);

		// Token: 0x04000052 RID: 82
		internal static readonly DecryptionKeyRetriever.GetKeyFunc GetKeyFuncCallback = new DecryptionKeyRetriever.GetKeyFunc(DecryptionKeyRetriever.GetKey);

		// Token: 0x02000101 RID: 257
		// (Invoke) Token: 0x06000922 RID: 2338
		[NullableContext(0)]
		internal delegate void FreeGcHandleFunc(IntPtr handle);

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x06000926 RID: 2342
		[NullableContext(0)]
		internal delegate void GetKeyFunc(IntPtr handle, IntPtr keyMetadata, out AesKey key, [MarshalAs(UnmanagedType.LPStr)] out string exception);
	}
}
